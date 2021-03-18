using System.Threading.Tasks;
using CoreMedia;

namespace Xamarin.CommunityToolkit.UI.Views
{
	public interface ICameraPreviewProcessor
	{
		Task Process(CMSampleBuffer sampleBuffer);
	}
}