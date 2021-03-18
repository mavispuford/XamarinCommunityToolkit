using System.Threading.Tasks;
using Android.Graphics;

namespace Xamarin.CommunityToolkit.UI.Views
{
	/// <summary>
	/// 	Processes Android camera preview frames.
	/// </summary>
	public interface ICameraPreviewProcessor
	{
		/// <summary>
		/// 	Processes the the provided camera preview bitmap.
		/// </summary>
		/// <param name="bitmap">The bitmap.</param>
		/// <param name="rotationDegrees">The rotation in degrees.</param>
		Task Process(Bitmap bitmap, int rotationDegrees);
	}
}