using System;
using AVFoundation;
using CoreMedia;
using Xamarin.Forms;

namespace Xamarin.CommunityToolkit.UI.Views
{
	public class PreviewCameraOutputDelegate : AVCaptureVideoDataOutputSampleBufferDelegate
	{
		private ICameraPreviewProcessor cameraPreviewProcessor;

		public PreviewCameraOutputDelegate()
			: base()
		{
			cameraPreviewProcessor = DependencyService.Get<ICameraPreviewProcessor>();
		}

		public override async void DidOutputSampleBuffer(AVCaptureOutput captureOutput, CMSampleBuffer sampleBuffer,
			AVCaptureConnection connection)
		{
			if (cameraPreviewProcessor != null)
			{
				await cameraPreviewProcessor.Process(sampleBuffer);
			}

			// If we don't garbage collect, it seems to build up and eventually stops calling DidOutputSampleBuffer()
			// See: https://stackoverflow.com/q/30850676
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}
	}
}