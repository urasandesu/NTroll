using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using mscoree;

namespace Urasandesu.NTroll.DomainFree
{
    public class CrossDomainDictionary<TKey, TValue> : MarshalByRefObject
    {
        const string DictionaryName = "Cross Domain Dictionary";
        static readonly object ms_lockObj = new object();
        static CrossDomainDictionary<TKey, TValue> ms_instance;
        static bool ms_ready = false;

        Dictionary<TKey, TValue> m_entries = new Dictionary<TKey, TValue>();
        readonly object m_lockObj = new object();

        protected CrossDomainDictionary() { }

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
                        return null;
                    
                    var domain = (AppDomain)pAppDomain;
                    if (domain.FriendlyName == friendlyName)
                        return domain;
                }
            }
            finally
            {
                host.CloseEnum(hEnum);
                Marshal.ReleaseComObject(host);
                host = null;
            }
        }

        static AppDomain CreateDictionaryDomainIfNecessary()
        {
            var domain = GetAppDomain(DictionaryName);
            if (domain == null)
            {
                var info = AppDomain.CurrentDomain.SetupInformation;
                domain = AppDomain.CreateDomain(DictionaryName, null, info);
            }
            return domain;
        }

        static CrossDomainDictionary<TKey, TValue> CreateDictionaryIfNecessary()
        {
            var domain = CreateDictionaryDomainIfNecessary();
            var type = typeof(CrossDomainDictionary<TKey, TValue>);
            var instance = (CrossDomainDictionary<TKey, TValue>)domain.GetData(type.AssemblyQualifiedName);
            if (instance == null)
            {
                instance = (CrossDomainDictionary<TKey, TValue>)domain.CreateInstanceAndUnwrap(
                                                     type.Assembly.FullName, type.FullName, false,
                                                     BindingFlags.NonPublic | BindingFlags.Instance,
                                                     null, null, null, null, null);
                domain.SetData(type.AssemblyQualifiedName, instance);
            }
            return instance;
        }

        public static CrossDomainDictionary<TKey, TValue> Instance
        {
            get
            {
                if (!ms_ready)
                {
                    lock (ms_lockObj)
                    {
                        if (!ms_ready)
                        {
                            ms_instance = CreateDictionaryIfNecessary();
                            Thread.MemoryBarrier();
                            ms_ready = true;
                        }
                    }
                }

                return ms_instance;
            }
        }

        public bool ContainsKey(TKey key)
        {
            lock (m_lockObj)
                return m_entries.ContainsKey(key);
        }

        public void Add(TKey key, TValue value)
        {
            lock (m_lockObj)
                m_entries.Add(key, value);
        }

        public void AddIfNotExist(TKey key, TValue value)
        {
            lock (m_lockObj)
            {
                if (!m_entries.ContainsKey(key))
                    m_entries.Add(key, value);
            }
        }

        public TValue Get(TKey key)
        {
            lock (m_lockObj)
                return m_entries[key];
        }

        public TValue GetIfExist(TKey key)
        {
            lock (m_lockObj)
                return m_entries.ContainsKey(key) ? m_entries[key] : default(TValue);
        }

        public void Clear()
        {
            lock (m_lockObj)
                m_entries.Clear();
        }

        static void UnloadDictionaryDomainIfAvailable()
        {
            var domain = GetAppDomain(DictionaryName);
            try
            {
                if (domain != null)
                    AppDomain.Unload(domain);
            }
            catch { }
        }

        public static void Unload()
        {
            if (ms_ready)
            {
                lock (ms_lockObj)
                {
                    if (ms_ready)
                    {
                        UnloadDictionaryDomainIfAvailable();
                        ms_instance = null;
                        Thread.MemoryBarrier();
                        ms_ready = false;
                    }
                }
            }
        }
    }
}
