using System;
using System.ComponentModel;
using System.Threading.Tasks;
using CoreGraphics;
using TMDBMobile.Core.Controls;
using TMDBMobile.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ImageButton), typeof(ImageButtonRenderer))]
namespace TMDBMobile.iOS.Renderer
{
    public partial class ImageButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            UpdatTintColor();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ImageButton.ImageTintColorProperty.PropertyName)
                UpdatTintColor();
        }

        private void UpdatTintColor()
        {
            var imageButton = Element as ImageButton;

            if (imageButton?.ImageTintColor == null)
                return;

            Control.SetImage(Control.ImageView.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate), UIControlState.Normal);
            Control.TintColor = imageButton.ImageTintColor.ToUIColor();
        }
    }
}
