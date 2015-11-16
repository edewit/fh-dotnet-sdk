using AeroGear.Push;
using System.Threading.Tasks;
using Android.Support.V7.App;
using Android.Gms.Common;
using Android.Content;
using Android.Support.V4.Content;
using Android.App;
using Android.Gms.Gcm.Iid;
using Android.Gms.Gcm;

namespace FHSDK.Services.Network
{
	/// <summary>
	/// Service to register 
	/// </summary>
	public class Push : PushBase
	{
		/// <summary>
		/// Creates the registration.
		/// </summary>
		/// <returns>The registration.</returns>
		protected override Registration CreateRegistration()
		{
			return new GcmRegistration();
		}
	}

	/// <summary>
	/// Gcm registration.
	/// </summary>
	internal class GcmRegistration : Registration {
		/// <summary>
		/// Initializes a new instance of the <see cref="FHSDK.Services.Network.GcmRegistration"/> class.
		/// </summary>
		public GcmRegistration() 
		{
			var intent = new Intent (Application.Context, typeof (UnifiedPushInstanceIDListenerService));
			Application.Context.StartService (intent);
		}
		
		/// <summary>
		/// Creates the installation.
		/// </summary>
		/// <returns>The installation.</returns>
		/// <param name="pushConfig">Push config.</param>
		protected override Installation CreateInstallation(PushConfig pushConfig)
		{
			var installation = new Installation
			{
				alias = pushConfig.Alias,
				operatingSystem = "android",
				osVersion = Android.OS.Build.VERSION.Release,
				categories = pushConfig.Categories
			};
			return installation;
		}

		/// <summary>
		/// Channels the URI.
		/// </summary>
		/// <returns>The URI.</returns>
		protected override async Task<string> ChannelUri()
		{

			BroadcastReceiver receiver = new Receiver ();
			LocalBroadcastManager.GetInstance (Application.Context);

			return UnifiedPushInstanceIDListenerService.Token;

		}

		internal class Receiver : BroadcastReceiver {
			public override void OnReceive (Context context, Intent intent)
			{
				//PushReceivedEvent (null);
			}
		}
			
		/// <summary>
		/// Creates the channel store.
		/// </summary>
		/// <returns>The channel store.</returns>
		protected override ILocalStore CreateChannelStore()
		{
			return new LocalStore();
		}

		/// <summary>
		/// Loads the config json.
		/// </summary>
		/// <returns>The config json.</returns>
		/// <param name="filename">Filename.</param>
		public override async Task<PushConfig> LoadConfigJson(string filename)
		{
			var file = new Java.IO.File(filename);
			return null;
		}		
	}

	internal class LocalStore : ILocalStore {
		
		public void Save(string key, string value)
		{
			ServiceFinder.Resolve<DataService> ().SaveData (key, value);
		}

		public string Read(string key)
		{
			return ServiceFinder.Resolve<DataService> ().GetData (key);
		}
	}

	[Service(Exported = false)]
	internal class UnifiedPushInstanceIDListenerService : InstanceIDListenerService 
	{
		public static string Token { get; private set; }
		public override void OnTokenRefresh ()
		{
			var instanceId = InstanceID.GetInstance (Application.Context);
			Token = instanceId.GetToken ("senderId", GoogleCloudMessaging.InstanceIdScope);
		}
	}
}

