using System;
using System.Threading;
using System.Threading.Tasks;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using Bumptech.Glide.Load.Resource.Gif;

namespace Microsoft.Maui.Platform
{
	public static class ImageViewExtensions
	{
		public static void Clear(this ImageView imageView)
		{
			// stop the animation
			if (imageView.Drawable is IAnimatable animatable)
				animatable.Stop();

			// clear the view and release any bitmaps
			imageView.SetImageResource(global::Android.Resource.Color.Transparent);
		}

		public static void UpdateAspect(this ImageView imageView, IImage image)
		{
			imageView.SetScaleType(image.Aspect.ToScaleType());
		}

		public static void UpdateIsAnimationPlaying(this ImageView imageView, IImageSourcePart image) =>
			imageView.Drawable.UpdateIsAnimationPlaying(image);

		public static void UpdateIsAnimationPlaying(this Drawable? drawable, IImageSourcePart image)
		{
			// IMPORTANT:
			// The linker will remove the interface from the concrete type if we don't force
			// the linker to be aware of both the concrete and interface types.

			if (drawable is IAnimatable animatable)
				Update(image, animatable);
			else if (drawable is AnimationDrawable ad)
				Update(image, ad);
			else if (drawable is GifDrawable gif)
				Update(image, gif);
			else if (OperatingSystem.IsAndroidVersionAtLeast(28) && drawable is AnimatedImageDrawable aid)
				Update(image, aid);

			static void Update(IImageSourcePart image, IAnimatable animatable)
			{
				if (image.IsAnimationPlaying)
				{
					if (!animatable.IsRunning)
						animatable.Start();
				}
				else
				{
					if (animatable.IsRunning)
						animatable.Stop();
				}
			}
		}
	}
}