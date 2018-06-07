using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using TMDBMobile.Core.Controls;
using TMDBMobile.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Xamarin.Forms.Color;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(ImageButton), typeof(ImageButtonRenderer))]
namespace TMDBMobile.Droid.Renderers
{
    public class ImageButtonRenderer : ButtonRenderer
    {
        public ImageButtonRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            UpdateTintColor();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing && Control != null)
                Control.Dispose();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ImageButton.ImageTintColorProperty.PropertyName)
                UpdateTintColor();
        }

        private void UpdateTintColor()
        {
            var imageButton = Element as ImageButton;

            if (imageButton?.ImageTintColor == null)
                return;

            int[][] states = new int[][] {
                new int[] { global::Android.Resource.Attribute.StateEnabled },
            };

            int[] colors = new int[] {
                imageButton.ImageTintColor.ToAndroid(),
            };

            Control.CompoundDrawableTintList = new ColorStateList(states, colors);
            Control.CompoundDrawableTintMode = PorterDuff.Mode.SrcIn;
        }

    }

}
