using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using mscoree;

namespace Urasandesu.NTroll.DomainFree
{
    public class CrossDomainDictionary<TKey, TValue> : MarshalByRefObject
    {
        static readonly string DictionaryName = "Cross Domain Dictionary";
        static CrossDomainDictionary<TKey, TValue> m_instance;
        Dictionary<TKey, TValue> m_contents = new Dictionary<TKey, TValue>();

        static AppDomain GetAppDomain(string friendlyName)
        {
            var hEnum = IntPtr.Zero;
            var host = new CorRuntimeHost();
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


        public static CrossDomainDictionary<TKey, TValue> Instance
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
                    var type = typeof(CrossDomainDictionary<TKey, TValue>);
                    var instance = (CrossDomainDictionary<TKey, TValue>)domain.GetData(type.FullName);
                    if (instance == null)
                    {
                        instance = (CrossDomainDictionary<TKey, TValue>)domain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName);
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
