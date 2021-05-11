using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line_Production.Helper
{
    public static class FileHelper
    {
        public static void CreateFileTimer(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    // Create a new file     
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine("#1 Moc thoi gian 1");
                        sw.WriteLine("8:05:00,8:59:59");
                        sw.WriteLine("9:00:00,9:29:59");
                        sw.WriteLine("9:40:00,10:59:59");
                        sw.WriteLine("11:00:00,11:59:59");
                        sw.WriteLine("12:00:00,12:59:59");
                        sw.WriteLine("13:00:00,13:59:59");
                        sw.WriteLine("14:00:00,14:49:59");
                        sw.WriteLine("15:00:00,15:59:59");
                        sw.WriteLine("16:00:00,16:54:59");
                        sw.WriteLine("17:30:00,19:54:59");
                       
                    }

                   
                }


            }
            catch (Exception)
            {

                
            }
        }
    }
}
