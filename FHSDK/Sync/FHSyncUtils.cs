using System;
using Newtonsoft.Json;
using FHSDK.Services;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using FHSDK.Services.Data;
using FHSDK.Services.Hash;

namespace FHSDK.Sync
{
    /// <summary>
    /// Utiliies class for synchronization.
    /// </summary>
	public class FHSyncUtils
	{
        private readonly IHashService _hasher;

        /// <summary>
        /// Default constructor.
        /// </summary>
		public FHSyncUtils (IHashService hashService)
        {
            _hasher = hashService;
        }

        /// <summary>
        /// Gnerate SHA1 hash.
        /// </summary>
        /// <param name="pObject"></param>
        /// <returns></returns>
		public string GenerateSHA1Hash (object pObject)
		{
			var sorted = SortObj (pObject);
			var strVal = sorted.ToString (Formatting.None);
			return _hasher.GenerateSha1Hash (strVal);
		}

        /// <summary>
        /// Clone an object.
        /// </summary>
        /// <param name="pData"></param>
        /// <returns></returns>
		public static object Clone (object pData)
		{
			string strVal = SerializeObject (pData);
			object cloned = FHSyncUtils.DeserializeObject (strVal, pData.GetType());
			return cloned;
		}

        /// <summary>
        /// Serialize an object into a string.
        /// </summary>
        /// <param name="pData"></param>
        /// <returns></returns>
		public static string SerializeObject(object pData)
		{
			return JsonConvert.SerializeObject (pData);
		}

        /// <summary>
        /// Deserialized a string into an object.
        /// </summary>
        /// <param name="pVal"></param>
        /// <param name="t"></param>
        /// <returns></returns>
		public static object DeserializeObject(string pVal, Type t)
		{
			return JsonConvert.DeserializeObject(pVal, t);
		}

        /// <summary>
        /// Get default storage path.
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
		
        // TODO move this to another location    
        //public static string GetDefaultDataDir(string dataId)
		//{
		//	IIOService ioService = ServiceFinder.Resolve<IIOService> ();
		//	string dirName = "com_feedhenry_sync";
		//	string dataDirPath = Path.Combine (ioService.GetDataPersistDir (), dataId, dirName);
  //          return dataDirPath;
		//}

        /// <summary>
        /// Get storage path.
        /// </summary>
        /// <param name="dataId"></param>
        /// <param name="dataFileName"></param>
        /// <returns></returns>
        /// TODO Move to a better location
		//public static string GetDataFilePath(string dataId, string dataFileName)
		//{
		//	string defaultDataDir = GetDefaultDataDir (dataId);
		//	return Path.Combine (defaultDataDir, dataFileName);
		//}

		private static JArray SortObj(object pObject)
		{
			JToken jsonObj = JToken.FromObject (pObject);
			JArray sorted = new JArray ();
			if (jsonObj.Type == JTokenType.Array) {
				JArray casted = jsonObj as JArray;
				for (int i = 0; i < casted.Count; i++) {
					JObject obj = new JObject ();
					obj ["key"] = i + "";
					JToken value = casted [i];
					if (value.Type == JTokenType.Object || value.Type == JTokenType.Array) {
						obj ["value"] = SortObj(value);
					} else {
						obj ["value"] = value;
					}
					sorted.Add (obj);
				}
			} else if (jsonObj.Type == JTokenType.Object) {
				JObject casted = jsonObj as JObject;
				List<String> keys = casted.Properties ().Select (c => c.Name).ToList ();
				keys.Sort(Comparison);
				for (int i = 0; i < keys.Count; i++) {
					string key = keys [i];
					if ( pObject is IFHSyncModel && key.ToUpper().Equals ("UID")) {
						continue;
					}
					JObject obj = new JObject ();
					JToken value = casted [key];
					obj ["key"] = key;
					if (value.Type == JTokenType.Object || value.Type == JTokenType.Array) {
						obj ["value"] = SortObj (value);
					} else {
						obj ["value"] = value;
					}
					sorted.Add (obj);
				}
			} else {
				sorted.Add (jsonObj);
			}
			return sorted;
		}

        private static int Comparison(string x, string y)
        {
            var array1 = x.ToCharArray();
            var array2 = y.ToCharArray();
            var result = 0;
            for (var i = 0; i < array1.Length; i++)
            {
                result = array1[i] - (i >= array2.Length ? 0 : array2[i]);
                if (result != 0)
                {
                    break;
                }
            }

            return result;
        }
	}
}

