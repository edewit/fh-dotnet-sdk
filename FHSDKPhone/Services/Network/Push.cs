using AeroGear.Push;
using FHSDK.Services.Network;
using System;
using System.Threading.Tasks;
using FHSDK.Services.Device;
using FHSDK.Services.Log;

namespace FHSDKPhone.Services.Network
{
    public class Push: PushBase
    {
        public Push(ILogService logService, IDeviceService deviceService) : base(logService, deviceService)
        {
        }

        protected override Registration CreateRegistration()
        {
            return new MpnsRegistration();
        }
    }
}
