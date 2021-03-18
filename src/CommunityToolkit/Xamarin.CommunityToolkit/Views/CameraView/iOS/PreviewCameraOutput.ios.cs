using System;
using AVFoundation;
using CoreMedia;
using Xamarin.Forms;

namespace Xamarin.CommunityToolkit.UI.Views
{
	/// <summary>
	/// 	Output sample buffer delegate used to pass sample buffer data to the registered <see cref="ICameraPreviewProcessor"/>.
	/// </summary>
	public class PreviewCameraOutputDelegate : AVCaptureVideoDataOutputSampleBufferDelegate
	{
		readonly ICameraPreviewProcessor cameraPreviewProcessor;

		/// <summary>
		/// 	Initializes a new instance of the <see cref="PreviewCameraOutputDelegate"/> class.
		/// </summary>
		public PreviewCameraOutputDelegate()
			: base()
		{
			cameraPreviewProcessor = DependencyService.Get<ICameraPreviewProcessor>();
		}

		/// <summary>
		/// 	Called each time there is a new camera frame.
		/// </summary>
		/// <param name="captureOutput">The capture output.</param>
		/// <param name="sampleBuffer">The sample buffer.</param>
		/// <param name="connection">The connection.</param>
		public override async void DidOutputSampleBuffer(AVCaptureOutput captureOutput, CMSampleBuffer sampleBuffer,
			AVCaptureConnection connection)
		{
			if (cameraPreviewProcessor != null)
			{
				await cameraPreviewProcessor.Process(sampleBuffer, connection.VideoOrientation);
			}

			// If we don't garbage collect, memory seems to build up and eventually DidOutputSampleBuffer() stops being called
			// See: https://stackoverflow.com/q/30850676
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}
	}
}