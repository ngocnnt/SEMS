using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Syncfusion.SfNumericUpDown.XForms.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SEMS_APP.Views.CustomNumericUpDown), typeof(SEMS_APP.Droid.Renderer.NumericUpDown))]
namespace SEMS_APP.Droid.Renderer
{
    class NumericUpDown : SfNumericUpDownRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Syncfusion.SfNumericUpDown.XForms.SfNumericUpDown> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                for (int i = 0; i < Control.ChildCount; i++)
                {
                    var child = Control.GetChildAt(i);
                    if (child is EditText)
                    {
                        var control = child as EditText;
                        control.Background = null;
                    }
                }
            }
        }
    }
}