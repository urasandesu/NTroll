//#define ADD_NEW_FEATURE
//#define AFTER_FIX
#define BEFORE_FIX


#if _
#elif ADD_NEW_FEATURE
using System;
using System.IO;
using NUnit.Framework;
using ThirdPartyLibrary;
using Urasandesu.NTroll.DomainFree;

namespace Test.Urasandesu.NTroll.DomainFree
{
    [TestFixture]
    public class ConfigurationManagerTest
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
        }

        [Test]
        public void GetPropertyTestSuccess01ExistKeyExistValue()
        {
            var holiday = ConfigurationManager.GetProperty("Holiday", DayOfWeek.Sunday);
            Assert.AreEqual(DayOfWeek.Monday, holiday);
        }

        [Test]
        public void GetPropertyTestError01AppConfigNotFound()
        {
            var info = new AppDomainSetup();
            info.ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            info.ShadowCopyFiles = AppDomain.CurrentDomain.SetupInformation.ShadowCopyFiles;
            info.ConfigurationFile = Path.GetTempFileName();
            File.Delete(info.ConfigurationFile);
            AppDomain.CurrentDomain.RunAtIsolatedDomain(info, () =>
            {
                var holiday = ConfigurationManager.GetProperty("Holiday", DayOfWeek.Sunday);
                Assert.AreEqual(holiday, DayOfWeek.Sunday);
            });
        }

        [Test]
        [ExpectedException(typeof(System.Configuration.ConfigurationErrorsException))]
        public void GetPropertyTestError02InvalidAppConfig()
        {
            var info = new AppDomainSetup();
            info.ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            info.ShadowCopyFiles = AppDomain.CurrentDomain.SetupInformation.ShadowCopyFiles;
            info.ConfigurationFile = Path.GetTempFileName();
            using (var sw = new StreamWriter(info.ConfigurationFile))
            {
                sw.Write("Hoge");
            }
            AppDomain.CurrentDomain.RunAtIsolatedDomain(info, () =>
            {
                var holiday = ConfigurationManager.GetProperty("Holiday", DayOfWeek.Sunday);
            });
        }

        [Test]
        public void GetPropertyTestError03HolidayPropertyNotFound()
        {
            var info = new AppDomainSetup();
            info.ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            info.ShadowCopyFiles = AppDomain.CurrentDomain.SetupInformation.ShadowCopyFiles;
            info.ConfigurationFile = Path.GetTempFileName();
            using (var sw = new StreamWriter(info.ConfigurationFile))
            {
                sw.Write(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<configuration>
  <appSettings>
  </appSettings>
  <startup>
    <supportedRuntime version=""v2.0.50727"" sku=""Client""/>
  </startup>
</configuration>");
            }
            AppDomain.CurrentDomain.RunAtIsolatedDomain(info, () =>
            {
                var holiday = ConfigurationManager.GetProperty("Holiday", DayOfWeek.Sunday);
                Assert.AreEqual(holiday, DayOfWeek.Sunday);
            });
        }

        [Test]
        public void GetPropertyTestError04HolidayPropertyIsEmpty()
        {
            var info = new AppDomainSetup();
            info.ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            info.ShadowCopyFiles = AppDomain.CurrentDomain.SetupInformation.ShadowCopyFiles;
            info.ConfigurationFile = Path.GetTempFileName();
            using (var sw = new StreamWriter(info.ConfigurationFile))
            {
                sw.Write(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<configuration>
  <appSettings>
    <add key=""Holiday"" value="""" />
  </appSettings>
  <startup>
    <supportedRuntime version=""v2.0.50727"" sku=""Client""/>
  </startup>
</configuration>");
            }
            AppDomain.CurrentDomain.RunAtIsolatedDomain(info, () =>
            {
                var holiday = ConfigurationManager.GetProperty("Holiday", DayOfWeek.Sunday);
                Assert.AreEqual(holiday, DayOfWeek.Sunday);
            });
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void GetPropertyTestError05HolidayPropertyIsInvalidIfIgnoredCase()
        {
            var info = new AppDomainSetup();
            info.ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            info.ShadowCopyFiles = AppDomain.CurrentDomain.SetupInformation.ShadowCopyFiles;
            info.ConfigurationFile = Path.GetTempFileName();
            using (var sw = new StreamWriter(info.ConfigurationFile))
            {
                sw.Write(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<configuration>
  <appSettings>
    <add key=""Holiday"" value=""aaaaaaaaaaaaa"" />
  </appSettings>
  <startup>
    <supportedRuntime version=""v2.0.50727"" sku=""Client""/>
  </startup>
</configuration>");
            }
            AppDomain.CurrentDomain.RunAtIsolatedDomain(info, () =>
            {
                var holiday = ConfigurationManager.GetProperty("Holiday", DayOfWeek.Sunday);
            });
        }

        [Test]
        public void GetPropertyTestError06HolidayPropertyIsInvalidIfCaseSensitive()
        {
            var info = new AppDomainSetup();
            info.ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            info.ShadowCopyFiles = AppDomain.CurrentDomain.SetupInformation.ShadowCopyFiles;
            info.ConfigurationFile = Path.GetTempFileName();
            using (var sw = new StreamWriter(info.ConfigurationFile))
            {
                sw.Write(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<configuration>
  <appSettings>
    <add key=""Holiday"" value=""monday"" />
  </appSettings>
  <startup>
    <supportedRuntime version=""v2.0.50727"" sku=""Client""/>
  </startup>
</configuration>");
            }
            AppDomain.CurrentDomain.RunAtIsolatedDomain(info, () =>
            {
                var holiday = ConfigurationManager.GetProperty("Holiday", DayOfWeek.Sunday);
            });
        }
    }
}
#elif AFTER_FIX
using System;
using System.IO;
using NUnit.Framework;
using ThirdPartyLibrary;
using Urasandesu.NTroll.DomainFree;

namespace Test.Urasandesu.NTroll.DomainFree
{
    [TestFixture]
    public class ConfigurationManagerTest
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
        }

        [Test]
        public void GetPropertyTestSuccess01ExistKeyExistValue()
        {
            var holiday = ConfigurationManager.GetProperty("Holiday", DayOfWeek.Sunday);
            Assert.AreEqual(DayOfWeek.Monday, holiday);
        }

        [Test]
        public void GetPropertyTestError01AppConfigNotFound()
        {
            var info = new AppDomainSetup();
            info.ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            info.ShadowCopyFiles = AppDomain.CurrentDomain.SetupInformation.ShadowCopyFiles;
            info.ConfigurationFile = Path.GetTempFileName();
            File.Delete(info.ConfigurationFile);
            AppDomain.CurrentDomain.RunAtIsolatedDomain(info, () =>
            {
                var holiday = ConfigurationManager.GetProperty("Holiday", DayOfWeek.Sunday);
                Assert.AreEqual(holiday, DayOfWeek.Sunday);
            });
        }

        [Test]
        [ExpectedException(typeof(System.Configuration.ConfigurationErrorsException))]
        public void GetPropertyTestError02InvalidAppConfig()
        {
            var info = new AppDomainSetup();
            info.ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            info.ShadowCopyFiles = AppDomain.CurrentDomain.SetupInformation.ShadowCopyFiles;
            info.ConfigurationFile = Path.GetTempFileName();
            using (var sw = new StreamWriter(info.ConfigurationFile))
            {
                sw.Write("Hoge");
            }
            AppDomain.CurrentDomain.RunAtIsolatedDomain(info, () =>
            {
                var holiday = ConfigurationManager.GetProperty("Holiday", DayOfWeek.Sunday);
            });
        }

        [Test]
        public void GetPropertyTestError03HolidayPropertyNotFound()
        {
            var info = new AppDomainSetup();
            info.ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            info.ShadowCopyFiles = AppDomain.CurrentDomain.SetupInformation.ShadowCopyFiles;
            info.ConfigurationFile = Path.GetTempFileName();
            using (var sw = new StreamWriter(info.ConfigurationFile))
            {
                sw.Write(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<configuration>
  <appSettings>
  </appSettings>
  <startup>
    <supportedRuntime version=""v2.0.50727"" sku=""Client""/>
  </startup>
</configuration>");
            }
            AppDomain.CurrentDomain.RunAtIsolatedDomain(info, () =>
            {
                var holiday = ConfigurationManager.GetProperty("Holiday", DayOfWeek.Sunday);
                Assert.AreEqual(holiday, DayOfWeek.Sunday);
            });
        }

        [Test]
        public void GetPropertyTestError04HolidayPropertyIsEmpty()
        {
            var info = new AppDomainSetup();
            info.ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            info.ShadowCopyFiles = AppDomain.CurrentDomain.SetupInformation.ShadowCopyFiles;
            info.ConfigurationFile = Path.GetTempFileName();
            using (var sw = new StreamWriter(info.ConfigurationFile))
            {
                sw.Write(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<configuration>
  <appSettings>
    <add key=""Holiday"" value="""" />
  </appSettings>
  <startup>
    <supportedRuntime version=""v2.0.50727"" sku=""Client""/>
  </startup>
</configuration>");
            }
            AppDomain.CurrentDomain.RunAtIsolatedDomain(info, () =>
            {
                var holiday = ConfigurationManager.GetProperty("Holiday", DayOfWeek.Sunday);
                Assert.AreEqual(holiday, DayOfWeek.Sunday);
            });
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void GetPropertyTestError05HolidayPropertyIsInvalidIfIgnoredCase()
        {
            var info = new AppDomainSetup();
            info.ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            info.ShadowCopyFiles = AppDomain.CurrentDomain.SetupInformation.ShadowCopyFiles;
            info.ConfigurationFile = Path.GetTempFileName();
            using (var sw = new StreamWriter(info.ConfigurationFile))
            {
                sw.Write(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<configuration>
  <appSettings>
    <add key=""Holiday"" value=""aaaaaaaaaaaaa"" />
  </appSettings>
  <startup>
    <supportedRuntime version=""v2.0.50727"" sku=""Client""/>
  </startup>
</configuration>");
            }
            AppDomain.CurrentDomain.RunAtIsolatedDomain(info, () =>
            {
                var holiday = ConfigurationManager.GetProperty("Holiday", DayOfWeek.Sunday);
            });
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void GetPropertyTestError06HolidayPropertyIsInvalidIfCaseSensitive()
        {
            var info = new AppDomainSetup();
            info.ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            info.ShadowCopyFiles = AppDomain.CurrentDomain.SetupInformation.ShadowCopyFiles;
            info.ConfigurationFile = Path.GetTempFileName();
            using (var sw = new StreamWriter(info.ConfigurationFile))
            {
                sw.Write(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<configuration>
  <appSettings>
    <add key=""Holiday"" value=""monday"" />
  </appSettings>
  <startup>
    <supportedRuntime version=""v2.0.50727"" sku=""Client""/>
  </startup>
</configuration>");
            }
            AppDomain.CurrentDomain.RunAtIsolatedDomain(info, () =>
            {
                var holiday = ConfigurationManager.GetProperty("Holiday", DayOfWeek.Sunday);
            });
        }
    }
}
#elif BEFORE_FIX
using System;
using System.IO;
using NUnit.Framework;
using ThirdPartyLibrary;
using Urasandesu.NTroll.DomainFree;

namespace Test.Urasandesu.NTroll.DomainFree
{
    [TestFixture]
    public class ConfigurationManagerTest
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
        }

        [Test]
        public void GetPropertyTestSuccess01ExistKeyExistValue()
        {
            var holiday = ConfigurationManager.GetProperty("Holiday", DayOfWeek.Sunday);
            Assert.AreEqual(DayOfWeek.Monday, holiday);
        }

        [Test]
        [Ignore("This test cannot be passed through. " +
                "Though it needs modifying App.config, I could not do it " +
                "because the configurations have already been cached " +
                "when passing the success path.")]
        public void GetPropertyTestError01AppConfigNotFound()
        {
            //
        }

        [Test]
        [ExpectedException(typeof(System.Configuration.ConfigurationErrorsException))]
        [Ignore("This test cannot be passed through. " +
                "Though it needs modifying App.config, I could not do it " +
                "because the configurations have already been cached " +
                "when passing the success path.")]
        public void GetPropertyTestError02InvalidAppConfig()
        {
            //
        }

        [Test]
        [Ignore("This test cannot be passed through. " +
                "Though it needs modifying App.config, I could not do it " +
                "because the configurations have already been cached " +
                "when passing the success path.")]
        public void GetPropertyTestError03HolidayPropertyNotFound()
        {
            // 
        }

        [Test]
        [Ignore("This test cannot be passed through. " +
                "Though it needs modifying App.config, I could not do it " +
                "because the configurations have already been cached " +
                "when passing the success path.")]
        public void GetPropertyTestError04HolidayPropertyIsEmpty()
        {
            // 
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        [Ignore("This test cannot be passed through. " +
                "Though it needs modifying App.config, I could not do it " +
                "because the configurations have already been cached " +
                "when passing the success path.")]
        public void GetPropertyTestError05HolidayPropertyIsInvalidIfIgnoredCase()
        {
            // 
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        [Ignore("This test cannot be passed through. " +
                "Though it needs modifying App.config, I could not do it " +
                "because the configurations have already been cached " +
                "when passing the success path.")]
        public void GetPropertyTestError06HolidayPropertyIsInvalidIfCaseSensitive()
        {
            // 
        }
    }
}
#endif