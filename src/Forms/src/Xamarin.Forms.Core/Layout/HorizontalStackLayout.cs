using Xamarin.Platform.Layouts;

namespace Xamarin.Forms
{
	// Don't panic, Layout2.StackLayout is the temporary name for the abstract base class until
	// we rename everything and move the legacy layouts
	public class HorizontalStackLayout : Layout2.StackLayout
	{
		protected override ILayoutManager CreateLayoutManager() => new HorizontalStackLayoutManager(this);
	}
}
