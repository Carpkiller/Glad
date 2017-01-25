//using System;
//using System.Windows.Forms;

//namespace Glad.Udalosti
//{
//    public class KlikLogo : Udalost
//    {
//        public KlikLogo(TimeSpan cas, WebBrowser webBrowser)
//        {
//            CasSimulacie = cas;
//            wb = webBrowser;
//            TypAktivity = TypAktivityEnum.Logo;
//        }

//        public override void Vykonaj()
//        {
//            var c = wb.Document.GetElementsByTagName("a");
//            foreach (HtmlElement htmlElement in c)
//            {
//                if (htmlElement.GetAttribute("id") == "logo")
//                {
//                    //htmlElement.InvokeMember("Click");

//                    var position = ElementPostions.GetCoordinatesX(wb, htmlElement);
//                    MouseEvents.MouseClick(position.X, position.Y);
//                }
//            }
//        }
//    }
//}
