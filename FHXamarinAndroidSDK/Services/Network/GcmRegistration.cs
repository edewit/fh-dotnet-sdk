using System.Collections.Generic;
using System.Threading.Tasks;
using AeroGear.Push;
using Android.App;
using Android.Gms.Gcm;
using Android.Gms.Gcm.Iid;
using Android.OS;
using FHSDK.Services.Device;
using Task = System.Threading.Tasks.Task;

namespace FHSDK.Services
{
    public class GcmRegistration : Registration
    {
        private AndroidPushConfig _config;
        private readonly IDeviceService _deviceService;

        public GcmRegistration(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        public override Task<PushConfig> LoadConfigJson(string filename)
        {
            return Task.Run(() =>_deviceService.ReadPushConfig());
        }

        protected override Installation CreateInstallation(PushConfig pushConfig)
        {
            _config = (AndroidPushConfig) pushConfig;
            return new Installation
            {
                alias = pushConfig.Alias,
                categories = pushConfig.Categories,
                operatingSystem = "android",
                osVersion = Build.VERSION.Release
            };
        }

        protected override ILocalStore CreateChannelStore()
        {
            return new AndroidStore();
        }

        protected override Task<string> ChannelUri()
        {
            return Task.Run(() =>
            {
                var token = InstanceID.GetInstance(Application.Context)
                    .GetToken(_config.SenderId, GoogleCloudMessaging.InstanceIdScope, new Bundle());

                return token;
            });
        }

        public void OnPushNotification(string message, Dictionary<string, string> messageData)
        {
            base.OnPushNotification(message, messageData);
        }
    }
}