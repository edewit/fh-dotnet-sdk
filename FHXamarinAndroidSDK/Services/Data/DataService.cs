using System;
using Android.App;
using FHSDK.Services;
using FHSDK.Services.Data;
using FHSDK.Services.Log;

namespace FHSDK.Services
{
    /// <summary>
    /// Data service provider for Android
    /// </summary>
	public class DataService: DataServiceBase
	{
		private const string PrefId = "fhprefs";
		public DataService (ILogService logService) : base(logService)
		{
		}

		protected override string DoRead(string dataId)
		{
			var prefs = Application.Context.GetSharedPreferences (PrefId, Android.Content.FileCreationMode.Private);
			var value = prefs.GetString (dataId, null);
			return value;
		}

		protected override void DoSave(string dataId, string dataValue)
		{
			var prefs = Application.Context.GetSharedPreferences (PrefId, Android.Content.FileCreationMode.Private);
			var prefsEditor = prefs.Edit ();
			prefsEditor.PutString(dataId, dataValue);
			prefsEditor.Commit();
		}

		public override void DeleteData(string dataId)
        {
            var prefs = Application.Context.GetSharedPreferences (PrefId, Android.Content.FileCreationMode.Private);
            var prefsEditor = prefs.Edit ();
            prefsEditor.Remove(dataId);
            prefsEditor.Commit();
        }
	}
}

