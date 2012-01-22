using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.DomainFree
{
    public static class AppDomainMixin
    {
        public static void UsingNewAppDomain(this AppDomain source, Action action)
        {
            if (action == null)
                throw new ArgumentNullException("action");
            if (!action.Method.IsStatic)
                throw new ArgumentException("The parameter must be the reference of a " +
                                            "static method.", "action");

            var domain = default(AppDomain);
            try
            {
                domain = AppDomain.CreateDomain("Domain " + action.Method.ToString(),
                                               source.Evidence, source.SetupInformation);
                var type = typeof(MarshalByRefRunner);
                var runner = (MarshalByRefRunner)domain.CreateInstanceAndUnwrap(
                                                  type.Assembly.FullName, type.FullName);
                runner.Action = action;
                runner.Run();
            }
            finally
            {
                try
                {
                    if (domain != null)
                        AppDomain.Unload(domain);
                }
                catch { }
            }
        }
    }
}
