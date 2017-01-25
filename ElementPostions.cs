using System;
using System.Drawing;
using System.Windows.Forms;

namespace Glad
{
    public class ElementPostions
    {
        public static Point GetCoordinatesX(WebBrowser wb, HtmlElement htmlElement)
        {
            var locWbX = wb.Location.X;
            var locWbY = wb.Location.Y;
            var fmx = wb.Parent.Location.X;
            var fmy = wb.Parent.Location.Y;

            var xx = GetXoffset(htmlElement);
            var yy = GetYoffset(htmlElement);

            Random rnd = new Random(DateTime.Now.Millisecond);
            var x = fmx + xx + locWbX + 8 + rnd.Next(5, htmlElement.OffsetRectangle.Width - 5);
            var y = fmy + SystemInformation.CaptionHeight + yy + locWbY + 8 + rnd.Next(2, htmlElement.OffsetRectangle.Height - 2);

            return new Point(x, y);
        }

        public static int GetXoffset(HtmlElement el)
        {
            //get element pos
            int xPos = el.OffsetRectangle.Left;

            //get the parents pos
            HtmlElement tempEl = el.OffsetParent;
            while (tempEl != null)
            {
                xPos += tempEl.OffsetRectangle.Left;
                tempEl = tempEl.OffsetParent;
            }

            return xPos;
        }

        public static int GetYoffset(HtmlElement el)
        {
            //get element pos
            int yPos = el.OffsetRectangle.Top;

            //get the parents pos
            HtmlElement tempEl = el.OffsetParent;
            while (tempEl != null)
            {
                yPos += tempEl.OffsetRectangle.Top;
                tempEl = tempEl.OffsetParent;
            }

            return yPos;
        }
    }
}
