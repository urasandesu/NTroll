using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using mscoree;

namespace Urasandesu.NTroll.DomainFree
{
    public class DomainFreeDictionary<TKey, TValue> : MarshalByRefObject
    {
        static readonly string DictionaryName = "Domain Free Dictionary";
        static DomainFreeDictionary<TKey, TValue> m_instance;
        Dictionary<TKey, TValue> m_contents = new Dictionary<TKey, TValue>();

        static AppDomain GetAppDomain(string friendlyName)
        {
            var hEnum = IntPtr.Zero;
            var host = new CorRuntimeHostClass();
            try
            {
                host.EnumDomains(out hEnum);

                var pAppDomain = default(object);
                while (true)
                {
                    host.NextDomain(hEnum, out pAppDomain);
                    if (pAppDomain == null)
                    {
                        break;
                    }
                    var domain = (AppDomain)pAppDomain;
                    if (domain.FriendlyName == friendlyName)
                    {
                        return domain;
                    }
                }
            }
            finally
            {
                host.CloseEnum(hEnum);
                Marshal.ReleaseComObject(host);
                host = null;
            }
            return null;
        }


        public static DomainFreeDictionary<TKey, TValue> Instance
        {
            get
            {
                if (m_instance == null)
                {
                    var domain = GetAppDomain(DictionaryName);
                    if (domain == null)
                    {
                        domain = AppDomain.CreateDomain(DictionaryName);
                    }
                    var type = typeof(DomainFreeDictionary<TKey, TValue>);
                    var instance = (DomainFreeDictionary<TKey, TValue>)domain.GetData(type.FullName);
                    if (instance == null)
                    {
                        instance = (DomainFreeDictionary<TKey, TValue>)domain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName);
                        domain.SetData(type.FullName, instance);
                    }
                    m_instance = instance;
                }

                return m_instance;
            }
        }

        public void AddIfNotExist(TKey key, TValue value)
        {
            if (!m_contents.ContainsKey(key))
            {
                m_contents.Add(key, value);
            }
        }

        public TValue GetIfExist(TKey key)
        {
            return m_contents.ContainsKey(key) ? m_contents[key] : default(TValue);
        }

        public void Clear()
        {
            m_contents.Clear();
        }
    }
}
