using System.Threading.Tasks;
using Android.Media;

namespace Xamarin.CommunityToolkit.UI.Views
{
	/// <summary>
	/// 	Processes Android camera preview frames.
	/// </summary>
	public interface ICameraPreviewProcessor
	{
		/// <summary>
		/// 	Processes the the provided camera preview image.
		/// </summary>
		/// <param name="image">The image.</param>
		/// <param name="rotationDegrees">The rotation in degrees.</param>
		Task Process(Image image, int rotationDegrees);
	}
}