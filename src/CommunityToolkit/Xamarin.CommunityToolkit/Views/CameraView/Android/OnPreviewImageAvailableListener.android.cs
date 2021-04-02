using Android.Content;
using Android.Media;
using Android.Views;
using Java.Interop;
using Xamarin.Forms;

namespace Xamarin.CommunityToolkit.UI.Views
{
	public class OnPreviewImageAvailableListener : Java.Lang.Object, ImageReader.IOnImageAvailableListener
	{
		private readonly ICameraPreviewProcessor _cameraPreviewProcessor;
		private readonly Context _context;

		public OnPreviewImageAvailableListener()
		{
			_cameraPreviewProcessor = DependencyService.Get<ICameraPreviewProcessor>();
		}

		public OnPreviewImageAvailableListener(Context context)
            : this()
		{
			_context = context;
		}

		public async void OnImageAvailable(ImageReader reader)
		{
			using var frame = reader?.AcquireNextImage();

			try
			{
				if (frame == null || _cameraPreviewProcessor == null)
				{
					return;
				}

				await _cameraPreviewProcessor.Process(frame, GetDisplayRotationDegrees());
			}
			finally
			{
				frame.Close();
			}
		}

		SurfaceOrientation GetDisplayRotation()
			=> _context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>().DefaultDisplay.Rotation;

		int GetDisplayRotationDegrees() =>
			GetDisplayRotation() switch
			{
				SurfaceOrientation.Rotation90 => 90,
				SurfaceOrientation.Rotation180 => 180,
				SurfaceOrientation.Rotation270 => 270,
				_ => 0,
			};
	}
}