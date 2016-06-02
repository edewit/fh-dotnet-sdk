﻿
using System;
using System.Collections.Generic;
#if __MOBILE__
using Xunit;
using TestMethod = Xunit.FactAttribute;
#else
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#endif

using System.Threading.Tasks;
using FHSDK;
using FHSDK.Services.Network;

namespace tests
{
    [TestClass]
    public class PushBaseTest
    {
        [TestMethod]
        public async Task TestLoadingPushConfig()
        {
            //given
            await FHClient.Init();

            //when
            //TODO what was I thinking at the time I made this method private again
            //var config = PushBase.ReadConfig();

            ////then
            //Assert.AreEqual(new Uri("http://192.168.28.34:8001/api/v2/ag-push"), config.UnifiedPushUri);
            //Assert.IsNotNull (config.Categories);
            //Assert.AreEqual (2, config.Categories.Count);
            //Assert.IsTrue (config.Categories.IndexOf ("one") != -1);
            //Assert.IsTrue (config.Categories.IndexOf ("two") != -1);
        }
    }
}
