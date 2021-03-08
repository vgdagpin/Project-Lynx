using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lynx.MobileApp.Portable.Common
{
    [ContentProperty("BackgroundImage")]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string BackgroundImage { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(this.BackgroundImage))
            {
                return null;
            }

            var imageSource = ImageSource.FromResource
                (
                    resource: $"Lynx.MobileApp.Portable.Images.{this.BackgroundImage}", 
                    sourceAssembly: typeof(ImageResourceExtension).GetTypeInfo().Assembly
                );

            return imageSource;
        }
    }
}
