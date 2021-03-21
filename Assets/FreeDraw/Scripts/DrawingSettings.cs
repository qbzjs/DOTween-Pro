using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FreeDraw
{
    // Helper methods used to set drawing settings
    public class DrawingSettings : MonoBehaviour
    {
        public float Transparency = 1f;

        // new_width is radius in pixels
        public void SetMarkerWidth(int new_width)
        {
            Drawable.Pen_Width = new_width;
        }
        public void SetMarkerWidth(float new_width)
        {
            SetMarkerWidth((int)new_width);
        }

        public void SetTransparency(float amount)
        {
            Transparency = amount;
            Color c = Drawable.Pen_Colour;
            c.a = amount;
            Drawable.Pen_Colour = c;
        }
        // Call these these to change the pen settings

        public void SetColorCustom(Color inputColor)
        {
            Color c = inputColor;
            c.a = Transparency;
            print("Input color: " + c);
            Drawable.Pen_Colour = c;
            Drawable.drawable.SetPenBrush();
        }

        public void SetEraser()
        {
            Drawable.Pen_Colour = new Color(255f, 255f, 255f, 0f);
        }
    }
}