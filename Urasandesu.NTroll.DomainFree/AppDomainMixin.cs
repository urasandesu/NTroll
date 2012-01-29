using System;
using System.Security.Policy;

namespace Urasandesu.NTroll.DomainFree
{
    public static class AppDomainMixin
    {
        public static void RunAtIsolatedDomain(this AppDomain source, Action action)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            RunAtIsolatedDomain(source.Evidence, source.SetupInformation, action);
        }

        public static void RunAtIsolatedDomain(this AppDomain source, Evidence securityInfo, Action action)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            RunAtIsolatedDomain(securityInfo, source.SetupInformation, action);
        }

        public static void RunAtIsolatedDomain(this AppDomain source, AppDomainSetup info, Action action)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            RunAtIsolatedDomain(source.Evidence, info, action);
        }

        public static void RunAtIsolatedDomain(Evidence securityInfo, AppDomainSetup info, Action action)
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
                                               securityInfo, info);
                var type = typeof(MarshalByRefAction);
                var runner = (MarshalByRefAction)domain.CreateInstanceAndUnwrap(
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
