﻿using Windows.Storage;
using FHSDK.Services.Log;

namespace FHSDK.Services.Data
{
    /// <summary>
    ///     On device data service provider for windows phone
    /// </summary>
    internal class DataService : DataServiceBase
    {
        public DataService(ILogService logService) : base(logService)
        {
        }

        protected override string DoRead(string dataId)
        {
            var settings = ApplicationData.Current.LocalSettings;
            return (string) settings.Values[dataId];
        }

        protected override void DoSave(string dataId, string dataValue)
        {
            var settings = ApplicationData.Current.LocalSettings;
            settings.Values[dataId] = dataValue;
        }

        public override void DeleteData(string dataId)
        {
            var settings = ApplicationData.Current.LocalSettings;
            settings.Values.Remove(dataId);
        }
    }
}