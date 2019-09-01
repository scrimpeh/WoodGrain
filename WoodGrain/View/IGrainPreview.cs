using System.Collections.Generic;
using System.Drawing;

namespace WoodGrain
{
	public interface IGrainPreview
	{
		/// <summary>
		/// Creates and draws a preview image from the grain pattern. The caller
		/// is responsible for freeing the output image.
		/// </summary>
		Image GetPreviewImage(IList<(int layer, int temp)> grain, int width, int zoom, out Color finalColor); 
	}
}
