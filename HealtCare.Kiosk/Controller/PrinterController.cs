using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;


namespace HealtCare.Kiosk.Controller {

    internal static class PrinterController {
        
        public static void Print(string doctorNme, string no, string healtCareName) {
            PrintDocument pd = new PrintDocument();
            PrintDialog yazdir = new PrintDialog {
                PrinterSettings = {
                    Collate = true,
                    Copies = 1
                },
                Document = pd
            };
            pd.PrintPage += (sender, e) => {
                Font myFontBig   = new Font("Arial Black", 30);
                Font myFontSmall = new Font("Arial Black", 15);
                Font myFontTiny  = new Font("Arial Black", 8);
                Brush myBrush    = Brushes.Black;
                Point myPoint    = new Point(5, 20);
                e.Graphics.DrawString($"{healtCareName}", myFontTiny, myBrush, myPoint.X - 5, myPoint.Y - 15);
                e.Graphics.DrawString($"    {no}"       , myFontBig, myBrush, myPoint);
                e.Graphics.DrawString($"{doctorNme}"         , myFontSmall, myBrush, myPoint.X - 5, myPoint.Y + 50);
                e.Graphics.DrawString($"{DateTime.Now}" , myFontSmall, myBrush, myPoint.X - 5, myPoint.Y + 75);
                e.Graphics.DrawString(" "               , myFontSmall, myBrush, myPoint.X - 5, myPoint.Y + 75);
            };
            yazdir.Document.Print();
        }
    }

}