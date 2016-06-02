using AeroGear.Push;
using FHSDK.Services.Device;
using FHSDK.Services.Log;

namespace FHSDK.Services.Network
{
    public class Push: PushBase
    {
        public Push(ILogService logService, IDeviceService deviceService) : base(logService, deviceService)
        {
        }

        protected override Registration CreateRegistration()
        {
            return new WnsRegistration();
        }
    }
}
