using System.Threading.Tasks;
using Android.Graphics;

namespace Xamarin.CommunityToolkit.UI.Views
{
	public interface IAndroidCameraPreviewProcessor
	{
		Task Process(SurfaceTexture surfaceTexture);
	}
}