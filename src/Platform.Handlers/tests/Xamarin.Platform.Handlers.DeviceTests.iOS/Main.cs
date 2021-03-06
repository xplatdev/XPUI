using UIKit;

namespace Xamarin.Platform.Handlers.DeviceTests
{
    public class Application
    {
        static void Main(string[] args)
        {
            if (args?.Length > 0) // usually means this is from xharness
                UIApplication.Main(args, null, nameof(TestApplicationDelegate));
            else
                UIApplication.Main(args, null, nameof(AppDelegate));
        }
    }
}
