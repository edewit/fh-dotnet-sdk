using System;
using System.Threading.Tasks;
using FHSDK.Services;
using FHSDK.Services.Auth;
using FHSDK.Services.Data;
using FHSDK.Services.Device;
using FHSDK.Services.Hash;
using FHSDK.Services.Log;
using FHSDK.Services.Monitor;
using FHSDK.Services.Network;
using Foundation;
using Microsoft.Practices.Unity;

namespace FHSDK
{
    /// <summary>
    /// Contains the entry class of the FeedHenry Xamarin SDK for iOS platform. It's defined in the FHXamarinIOSSDK.dll.
    /// To use the FeedHenry SDK, both FHSDK.dll and FHXamarinIOSSDK.dll should be referenced by your Xamarain iOS project, and initialise the SDK using the FHClient class in this name space.
    /// </summary>
    [System.Runtime.CompilerServices.CompilerGenerated]
    class NamespaceDoc
    {

    }

	public class FHClient: FH
	{
	    public FHClient(ILogService logService, IPush push, IDataService dataService) : base(logService, push, dataService)
	    {
	    }

	    /// <summary>
        /// Initialise the FeedHenry SDK. This should be called before any other API functions are invoked. Usually this should be called after the app finish intialising.
        /// </summary>
        /// <example>
        /// <code>
        ///  public override void ViewDidLoad ()
        ///  {
        ///      //Other app init work
        ///      InitApp();
        ///  }
        ///
        ///  private async void InitApp()
        ///  {
        ///      try
        ///      {
        ///          bool inited = await FHClient.Init();
        ///          if(inited)
        ///          {
        ///            //Initialisation is successful
        ///          }
        ///       }
        ///       catch(FHException e)
        ///       {
        ///           //Initialisation failed, handle exception
        ///       }
        ///    }
        /// 
        /// </code>
        /// </example>
        /// <returns>If Init is success or not</returns>
        /// <exception cref="FHException"></exception>
		public new static async Task<bool> Init()
		{
            InitClient();
			return await FH.Init ();
		}

	    public static void FinishRegistration(NSData deviceToken)
		{
			var notification = NSNotification.FromName("sucess_registered", deviceToken);
			NSNotificationCenter.DefaultCenter.PostNotification (notification);
		}

	    public static void OnMessageReceived(NSDictionary userInfo) 
		{
			var notification = NSNotification.FromName("message_received", userInfo);
			NSNotificationCenter.DefaultCenter.PostNotification (notification);
		}

        private new static void InitClient()
        {
            var container = new UnityContainer();
            container.RegisterType<IOAuthClientHandlerService, OAuthClientHandlerService>();
            container.RegisterType<IDataService, DataService>();
            container.RegisterType<IIOService, IOService>();
            container.RegisterType<IDeviceService, DeviceService>();
            container.RegisterType<IHashService, HashService>();
            container.RegisterType<ILogService, LogService>();
            container.RegisterType<IMonitorService, MonitorService>();
            container.RegisterType<INetworkService, NetworkService>();
            container.RegisterType<IPush, Push>();
            container.RegisterType<FHClient>();
            Instance = container.Resolve<FHClient>();
        }
    }
}

