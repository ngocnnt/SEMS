using Foundation;
using Syncfusion.SfNumericUpDown.XForms.iOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SEMS_APP.Views.CustomNumericUpDown), typeof(SEMS_APP.iOS.Renderer.NumericUpDown))]
namespace SEMS_APP.iOS.Renderer
{
    class NumericUpDown : SfNumericUpDownRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Syncfusion.SfNumericUpDown.XForms.SfNumericUpDown> e)
        {
            base.OnElementChanged(e);
            if (this.Control != null)
            {
                /// For Achieving Borderwidth customization.
                this.Control.Layer.BorderWidth = 0f;
            }
        }
    }
}