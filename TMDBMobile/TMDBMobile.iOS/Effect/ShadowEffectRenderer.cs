using CoreGraphics;
using System;
using System.ComponentModel;
using TMDBMobile.Core.Effects;
using TMDBMobile.iOS.Effect;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("TMDBMovie")]
[assembly: ExportEffect(typeof(ShadowEffectRenderer), "ShadowEffect")]
namespace TMDBMobile.iOS.Effect
{
    public class ShadowEffectRenderer : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                UpdateRadius();
                UpdateColor();
                UpdateOffset();
                Control.Layer.ShadowOpacity = 1.0f;
                Control.Layer.ShouldRasterize = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == ShadowEffect.RadiusProperty.PropertyName)
            {
                UpdateRadius();
            }
            else if (args.PropertyName == ShadowEffect.ColorProperty.PropertyName)
            {
                UpdateColor();
            }
            else if (args.PropertyName == ShadowEffect.DistanceXProperty.PropertyName ||
                     args.PropertyName == ShadowEffect.DistanceYProperty.PropertyName)
            {
                UpdateOffset();
            }
        }

        protected override void OnDetached() { }

        void UpdateRadius()
        {
            Control.Layer.CornerRadius = (nfloat)ShadowEffect.GetRadius(Element);
        }

        void UpdateColor()
        {
            Control.Layer.ShadowColor = ShadowEffect.GetColor(Element).ToCGColor();
        }

        void UpdateOffset()
        {
            Control.Layer.ShadowOffset = new CGSize(
                (double)ShadowEffect.GetDistanceX(Element),
                (double)ShadowEffect.GetDistanceY(Element));
        }

    }
}
