using FHSDK.Services.Network;
using AeroGear.Push;
using FHSDK.Services.Device;
using FHSDK.Services.Log;
using UIKit;

namespace FHSDK.Services
{
	public class Push : PushBase
	{
	    public Push(ILogService logService, IDeviceService deviceService) : base(logService, deviceService)
	    {
	    }

	    protected override Registration CreateRegistration() 
		{
			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
				var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes (UIUserNotificationType.Sound |
					UIUserNotificationType.Alert | UIUserNotificationType.Badge, null);

				UIApplication.SharedApplication.RegisterUserNotificationSettings (notificationSettings);
				UIApplication.SharedApplication.RegisterForRemoteNotifications ();
			} else {
				UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(UIRemoteNotificationType.Badge |
					UIRemoteNotificationType.Sound | UIRemoteNotificationType.Alert);
			}

			return new IosRegistration();
		}
	}
}

