using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FHSDK.Config;
using FHSDK.Services.Data;

namespace FHSDK.API
{
    /// <summary>
    /// FHAuthSession is resposible to manage OAuth tokens.
    /// </summary>
    public sealed class FHAuthSession
    {
        private const string SessionTokenKey = "sessionToken";
        private const string VerifyPath = "box/srv/1.1/admin/authpolicy/verifysession";
        private const string RevokePath = "box/srv/1.1/admin/authpolicy/revokesession";
        private readonly TimeSpan _timeout = TimeSpan.FromMilliseconds(5*1000);
        private readonly IDataService _dataService;
        private FHHttpClient.FHHttpClient _httpClient;

        public FHAuthSession(IDataService dataService, FHHttpClient.FHHttpClient client)
        {
            _dataService = dataService;
            _httpClient = client;

        }

        /// <summary>
        ///     Save the session token
        /// </summary>
        /// <param name="sessionToken"></param>
        internal void SaveSession(string sessionToken)
        {
            _dataService.SaveData(SessionTokenKey, sessionToken);
        }

        /// <summary>
        ///     Check if a session token exists
        /// </summary>
        /// <returns></returns>
        public bool Exists()
        {
            var saved = _dataService.GetData(SessionTokenKey);
            return null != saved;
        }

        /// <summary>
        ///     Return the saved session token value
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            var saved = _dataService.GetData(SessionTokenKey);
            return saved;
        }

        /// <summary>
        ///     Verify if the local session token is valid
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Verify()
        {
            var saved = GetToken();
            if (null == saved) return false;
            var fhres = await CallRemote(VerifyPath, saved);
            var json = fhres.GetResponseAsJObject();
            var isValid = (bool) json["isValid"];
            return isValid;
        }

        /// <summary>
        ///     Clear the local session and delete from remote too.
        /// </summary>
        /// <returns></returns>
        public async Task Clear()
        {
            var saved = GetToken();
            if (null != saved)
            {
                _dataService.DeleteData(SessionTokenKey);
                await CallRemote(RevokePath, saved);
            }
        }

        private async Task<FHResponse> CallRemote(string path, string sessionToken)
        {
            var uri = new Uri(string.Format("{0}/{1}", FHConfig.GetInstance().GetHost(), path));
            var data = new Dictionary<string, object> {{SessionTokenKey, sessionToken}};
            var fhres = await _httpClient.SendAsync(uri, "POST", null, data, _timeout);
            return fhres;
        }
    }
}