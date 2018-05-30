﻿using System;
using System.ComponentModel;
using System.Linq;

using Xamarin.Forms.Platform.Android;
using TMDBMobile.Core.Effects;
using Xamarin.Forms;
using TMDBMobile.Droid.Effects;

[assembly: ResolutionGroupName("TMDBMovie")]
[assembly: ExportEffect(typeof(ShadowEffectRenderer), "ShadowEffect")]
namespace TMDBMobile.Droid.Effects
{
    public class ShadowEffectRenderer: PlatformEffect
    {
        Android.Widget.TextView control;
        Android.Graphics.Color color;
        float radius, distanceX, distanceY;

        protected override void OnAttached()
        {
            try
            {
                control = Control as Android.Widget.TextView;
                UpdateRadius();
                UpdateColor();
                UpdateOffset();
                UpdateControl();
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
                UpdateControl();
            }
            else if (args.PropertyName == ShadowEffect.ColorProperty.PropertyName)
            {
                UpdateColor();
                UpdateControl();
            }
            else if (args.PropertyName == ShadowEffect.DistanceXProperty.PropertyName ||
                     args.PropertyName == ShadowEffect.DistanceYProperty.PropertyName)
            {
                UpdateOffset();
                UpdateControl();
            }
        }

        protected override void OnDetached() { }

        void UpdateControl()
        {
            if (control != null)
                control.SetShadowLayer(radius, distanceX, distanceY, color);
        }

        void UpdateRadius()
        {
            radius = (float)ShadowEffect.GetRadius(Element);
        }

        void UpdateColor()
        {
            color = ShadowEffect.GetColor(Element).ToAndroid();
        }

        void UpdateOffset()
        {
            distanceX = (float)ShadowEffect.GetDistanceX(Element);
            distanceY = (float)ShadowEffect.GetDistanceY(Element);
        }
    }
}