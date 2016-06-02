using System.Runtime.CompilerServices;
using FHSDK.Services.Network;
using FHSDKPhone.Services.Network;
using System.Threading.Tasks;
using FHSDK.FHHttpClient;
using FHSDK.Services;
using FHSDK.Services.Auth;
using FHSDK.Services.Data;
using FHSDK.Services.Device;
using FHSDK.Services.Hash;
using FHSDK.Services.Log;
using FHSDK.Services.Monitor;
using Microsoft.Practices.Unity;

namespace FHSDK
{
    /// <summary>
    ///     Contains the entry class of the FeedHenry SDK for Windows Phone 8 platform. It's defined in the FHSDKPhone.dll.
    ///     To use the FeedHenry SDK, both FHSDK.dll and FHSDKPhone.dll should be referenced by your WP8 project, and
    ///     initialise the SDK using the FHClient class in this name space.
    /// </summary>
    [CompilerGenerated]
    internal class NamespaceDoc
    {
    }

    /// <summary>
    ///     Initialise the FeedHenry SDK. This should be called before any other API functions are invoked. Usually this should
    ///     be called after the app finish intialising.
    /// </summary>
    /// <example>
    ///     <code>
    ///   public MainPage()
    ///     {
    ///         InitializeComponent();
    ///         InitApp();
    ///     }
    /// 
    ///     private async void InitApp()
    ///     {
    ///         try
    ///         {
    ///             bool inited = await FHClient.Init();
    ///             if(inited)
    ///             {
    ///               //Initialisation is successful
    ///             }
    ///        }
    ///        catch(FHException e)
    ///        {
    ///            //Initialisation failed, handle exception
    ///        }
    ///     }
    ///  
    ///  </code>
    /// </example>
    /// <returns>If Init is success or not</returns>
    /// <exception cref="FHException"></exception>
    public class FHClient : FH
    {
        public FHClient(ILogService logService, IPush push, IDataService dataService) : base(logService, push, dataService)
        {
        }

        public new static async Task<bool> Init()
        {
            InitClient();
            return await FH.Init();
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
            container.RegisterType<FH>();
            Instance = container.Resolve<FH>();
        }
    }
}