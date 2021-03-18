using System.Threading.Tasks;
using AVFoundation;
using CoreMedia;

namespace Xamarin.CommunityToolkit.UI.Views
{
	/// <summary>
	/// 	Processes iOS camera preview frames.
	/// </summary>
	public interface ICameraPreviewProcessor
	{
		/// <summary>
		/// 	Processes the provided camera sample buffer.
		/// </summary>
		/// <param name="sampleBuffer">The sample buffer.</param>
		/// <param name="orientation">The orientation.</param>
		Task Process(CMSampleBuffer sampleBuffer, AVCaptureVideoOrientation orientation);
	}
}