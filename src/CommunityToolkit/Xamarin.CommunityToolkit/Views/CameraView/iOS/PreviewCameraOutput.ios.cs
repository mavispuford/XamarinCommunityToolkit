using AVFoundation;
using CoreMedia;
using Xamarin.Forms;

namespace Xamarin.CommunityToolkit.UI.Views
{
	public class PreviewCameraOutputDelegate : AVCaptureVideoDataOutputSampleBufferDelegate
	{
		private IIOSCameraPreviewProcessor cameraPreviewProcessor;

		public PreviewCameraOutputDelegate() : base()
		{
			cameraPreviewProcessor = DependencyService.Get<IIOSCameraPreviewProcessor>();
		}

		public override async void DidOutputSampleBuffer(AVCaptureOutput captureOutput, CMSampleBuffer sampleBuffer,
			AVCaptureConnection connection)
		{
			if (cameraPreviewProcessor != null)
			{
				await cameraPreviewProcessor.Process();
			}

			base.DidOutputSampleBuffer(captureOutput, sampleBuffer, connection);
		}
	}
}