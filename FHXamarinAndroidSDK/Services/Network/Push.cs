using AeroGear.Push;
using FHSDK.Services.Device;
using FHSDK.Services.Log;
using FHSDK.Services.Network;

namespace FHSDK.Services
{
    public class Push : PushBase
    {
        public Push(ILogService logService, IDeviceService deviceService) : base(logService, deviceService)
        {
        }

        protected override Registration CreateRegistration()
        {
            return new GcmRegistration(_deviceService);
        }
    }
}