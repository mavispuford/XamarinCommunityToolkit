using Android.Content;
using Android.Media;
using Android.Views;
using Java.Interop;
using Xamarin.Forms;

namespace Xamarin.CommunityToolkit.UI.Views
{
	public class OnPreviewImageAvailableListener : Java.Lang.Object, ImageReader.IOnImageAvailableListener
	{
		readonly ICameraPreviewProcessor cameraPreviewProcessor;
		readonly Context context;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public OnPreviewImageAvailableListener()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
			cameraPreviewProcessor = DependencyService.Get<ICameraPreviewProcessor>();
		}

		public OnPreviewImageAvailableListener(Context context)
            : this()
		{
			this.context = context;
		}

		public async void OnImageAvailable(ImageReader? reader)
		{
			using var frame = reader?.AcquireNextImage();

			try
			{
				if (frame == null || cameraPreviewProcessor == null)
				{
					return;
				}

				await cameraPreviewProcessor.Process(frame, GetDisplayRotationDegrees());
			}
			finally
			{
				frame?.Close();
			}
		}

		SurfaceOrientation GetDisplayRotation()
			=> context?.GetSystemService(Context.WindowService)?.JavaCast<IWindowManager>()?.DefaultDisplay?.Rotation ?? SurfaceOrientation.Rotation90;

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