﻿#if __MOBILE__
using Xunit;
using TestMethod = Xunit.FactAttribute;
#else
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#endif

using System.Threading.Tasks;
using FHSDK;
using FHSDK.Sync;
using tests.Mocks;

namespace tests
{
    [TestClass]
    public class SyncTest
    {
        [TestMethod]
        public async Task ShouldCreatePendingTaskForCreate()
        {
            //given
            await FHClient.Init();
            const string taskName = "task1";
            //TODO this should be easier now
            //var dataset1 = FHSyncDataset<TaskModel>.Build<TaskModel>("set", new FHSyncConfig(), null, null);
            //var task = new TaskModel
            //{
            //    TaksName = taskName
            //};
            //var dataset = dataset1;

            ////when
            //var savedTask = dataset.Create(task);

            ////then
            //Assert.IsNotNull(savedTask.UID);
            //Assert.AreEqual(1, dataset.GetPendingRecords().List().Count);

            //var taskRead = dataset.Read(savedTask.UID);
            //Assert.IsNotNull(taskRead);
            //Assert.AreEqual(taskName, taskRead.TaksName);
        }

        [TestMethod]
        public async Task ShouldUploadPendingEdits()
        {
            //given
            await FHClient.Init();

            //var dataset = new MockResponseDataset<TaskModel>("dataset");
            //dataset.MockResponse = dataset.AppliedCreateResponse;
            //var task = new TaskModel
            //{
            //    TaksName = "test"
            //};

            //dataset.Create(task);

            ////when
            //var shouldSync = dataset.ShouldSync();
            //await dataset.StartSyncLoop();

            ////then
            //Assert.IsTrue(shouldSync);
            //Assert.AreEqual(0, dataset.GetPendingRecords().List().Count);
        }

        [TestMethod]
        public async Task ShouldCreateUpdate()
        {
            //given
            await FHClient.Init();
            //var dataset = new MockResponseDataset<TaskModel>("dataset");
            //var task = new TaskModel
            //{
            //    TaksName = "test"
            //};
            
            ////when
            //task = dataset.Create(task);
            //const string name = "super";
            //task.TaksName = name;
            //dataset.Update(task);

            ////then
            //var readTask = dataset.Read(task.UID);
            //Assert.IsNotNull(readTask);
            //Assert.AreEqual(name, readTask.TaksName);

            ////when
            //dataset.MockResponse = dataset.AwkRespone;
            //await dataset.StartSyncLoop();

            ////then
            //var list = dataset.List();
            //Assert.AreEqual(1, list.Count);
            //Assert.AreEqual(name, list[0].TaksName);
        }

        [TestMethod]
        public async Task ShouldAddRemoteCreatedRecord()
        {
            //given
            await FHClient.Init();
            //var dataset = new MockResponseDataset<TaskModel>("dataset");
            //var task = new TaskModel
            //{
            //    TaksName = "test"
            //};

            ////when
            //task = dataset.Create(task);
            //dataset.MockResponse = dataset.RemoteCreatedResponse;
            //await dataset.StartSyncLoop();

            ////then
            //var list = dataset.List();
            //Assert.AreEqual(2, list.Count);
            //Assert.IsTrue(list.Contains(task));
            //Assert.IsTrue(list.Contains(new TaskModel() { UID = "561b7cf1810880dc18000029" }));
        }

        [TestMethod]
        public async Task ShouldDeleteRecord()
        {
            //given
            await FHClient.Init();
            //var dataset = new MockResponseDataset<TaskModel>("dataset");
            //dataset.MockResponse = dataset.AppliedCreateResponse;
            //var task = new TaskModel
            //{
            //    TaksName = "test"
            //};

            ////when
            //task = dataset.Create(task);
            //await dataset.StartSyncLoop();
            //dataset.Delete(task.UID);

            ////then
            //var list = dataset.List();
            //Assert.AreEqual(0, list.Count);
            //Assert.AreEqual(1, dataset.GetPendingRecords().List().Count);
        }

        [TestMethod]
        public async Task ShouldOmitFieldWithNull()
        {
            //given
            await FHClient.Init();
            //var dataset = new MockResponseDataset<TaskModel>("dataset")
            //{
            //    KeepSyncParamType = typeof(FHSyncDataset<TaskModel>.FHSyncLoopParams)
            //};
            //dataset.MockResponse = dataset.AppliedCreateResponse;
            //var task = new TaskModel();

            ////when
            //dataset.Create(task);
            //await dataset.StartSyncLoop();
            
            ////then
            //Assert.IsTrue(dataset.SyncParams.GetType() == typeof(FHSyncDataset<TaskModel>.FHSyncLoopParams));
            //var param = (FHSyncDataset<TaskModel>.FHSyncLoopParams)dataset.SyncParams;
            //Assert.AreEqual(1, param.Pendings.Count);
            //var post = param.Pendings[0]["post"];
            //Assert.IsTrue(post["taskName"] == null); 
        }
    }
}