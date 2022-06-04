using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MedikTapp.Renderers
{
    public class BorderlessEntry : Entry
    {
        public BorderlessEntry()
        {
            FontFamily = "MR";
            IsSpellCheckEnabled = false;
            IsTextPredictionEnabled = false;
            PlaceholderColor = Color.Gray;
        }
    }
}
