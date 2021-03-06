using System;

namespace Xamarin.Forms.Platform.Tizen.Renderers
{
	[Obsolete("MasterDetailContainer is obsolete as of version 5.0.0. Please use FlyoutContainer instead.")]
	public class MasterDetailContainer : ElmSharp.Box, IDisposable
	{
#pragma warning disable CS0618 // Type or member is obsolete
		readonly MasterDetailPage _parent;
#pragma warning restore CS0618 // Type or member is obsolete
		readonly bool _isMaster;

		VisualElement _childView;
		bool _disposed;
		bool _hasAppearedToParent;

		IPageController PageController => ChildView as IPageController;

		IMasterDetailPageController MasterDetailPageController => _parent as IMasterDetailPageController;

#pragma warning disable CS0618 // Type or member is obsolete
		public MasterDetailContainer(MasterDetailPage parentElement, bool isMaster) : base(Forms.NativeParent)
#pragma warning restore CS0618 // Type or member is obsolete
		{
			_parent = parentElement;
			_isMaster = isMaster;

			SetLayoutCallback(OnLayoutUpdated);
			Show();
		}

		~MasterDetailContainer()
		{
			Dispose(false);
		}

		public VisualElement ChildView
		{
			get { return _childView; }
			set
			{
				if (_childView == value)
					return;

				if (_childView != null)
				{
					RemoveChildView();
				}

				_childView = value;

				if (_childView == null)
					return;

				AddChildView(_childView);

				if (_hasAppearedToParent)
				{
					Device.BeginInvokeOnMainThread(() =>
					{
						if (!_disposed && _hasAppearedToParent)
							PageController?.SendAppearing();
					});
				}
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected void RemoveChildView()
		{
			IVisualElementRenderer childRenderer = Platform.GetRenderer(_childView);
			if (childRenderer != null)
			{
				UnPack(childRenderer.NativeView);
				childRenderer.Dispose();
			}
		}

		protected void AddChildView(VisualElement childView)
		{
			IVisualElementRenderer renderer = Platform.GetOrCreateRenderer(childView);
			this.PackEnd(renderer.NativeView);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed)
				return;

			if (disposing)
			{
				if (_childView != null)
				{
					RemoveChildView();
				}
				SetLayoutCallback(null);
			}
			_disposed = true;
		}

		void OnLayoutUpdated()
		{
			if (_childView != null)
			{
				if (_isMaster)
					MasterDetailPageController.MasterBounds = this.Geometry.ToDP();
				else
					MasterDetailPageController.DetailBounds = this.Geometry.ToDP();

				IVisualElementRenderer renderer = Platform.GetRenderer(_childView);
				renderer.NativeView.Geometry = this.Geometry;
			}
		}

		public void SendAppearing()
		{
			if (_hasAppearedToParent)
				return;

			_hasAppearedToParent = true;

			PageController?.SendAppearing();
		}

		public void SendDisappearing()
		{
			if (!_hasAppearedToParent)
				return;

			_hasAppearedToParent = false;

			PageController?.SendDisappearing();
		}
	}
}