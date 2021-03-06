using Android.App;
using Android.OS;
using Xunit.Runners.UI;
using System.Reflection;
using Android.Content.PM;

namespace Xamarin.Platform.Handlers.DeviceTests
{
	[Activity(
		Name = "com.xamarin.handlers.devicetests.MainActivity",
		Label = "@string/app_name",
		Theme = "@style/MainTheme",
		MainLauncher = true,
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : RunnerActivity
	{
        protected override void OnCreate(Bundle bundle)
		{
			Essentials.Platform.Init(this, bundle);
			Platform.Init(this);

			// Tests can be inside the main assembly
			AddTestAssembly(Assembly.GetExecutingAssembly());
			AddTestAssembly(typeof(SliderHandlerTests).Assembly);

			base.OnCreate(bundle);
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}
}