using FHSDK.Services.Data;
using FHSDK.Services.Log;
using Foundation;

namespace FHSDK.Services
{
    /// <summary>
    ///     Data Service provider for ios
    /// </summary>
    public class DataService : DataServiceBase
    {
        public DataService(ILogService logService) : base(logService)
        {
        }

        protected override string DoRead(string dataId)
        {
            var prefs = NSUserDefaults.StandardUserDefaults;
            var value = prefs.ValueForKey(new NSString(dataId));
            return ((NSString) value)?.ToString();
        }

        protected override void DoSave(string dataId, string dataValue)
        {
            var prefs = NSUserDefaults.StandardUserDefaults;
            prefs.SetValueForKey(new NSString(dataValue), new NSString(dataId));
            prefs.Synchronize();
        }

        public override void DeleteData(string dataId)
        {
            var prefs = NSUserDefaults.StandardUserDefaults;
            prefs.RemoveObject(new NSString(dataId));
            prefs.Synchronize();
        }
    }
}