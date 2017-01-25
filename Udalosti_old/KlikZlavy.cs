//using System;
//using System.Windows.Forms;

//namespace Glad.Udalosti
//{
//    public class KlikZlavy : Udalost
//    {

//        public KlikZlavy(TimeSpan cas, WebBrowser webBrowser)
//        {
//            CasSimulacie = cas;
//            wb = webBrowser;
//            TypAktivity = TypAktivityEnum.Zlavy;
//        }

//        public override void Vykonaj()
//        {
//            var c = wb.Document.GetElementsByTagName("a");
//            foreach (HtmlElement htmlElement in c)
//            {
//                if (htmlElement.InnerText == "Nové zľavy")
//                {
//                    //htmlElement.InvokeMember("Click");

//                    var position = ElementPostions.GetCoordinatesX(wb, htmlElement);
//                    MouseEvents.MouseClick(position.X, position.Y);
//                }
//            }
//        }
//    }
//}