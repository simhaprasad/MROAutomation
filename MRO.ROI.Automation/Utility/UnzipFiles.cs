using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;


namespace MRO.ROI.Automation.Utility
{
   public class UnzipFiles
    {
        public static void UnzipFile()
        {
            
            string SourceZipFile = @"C:\Users\SS PRASAD\Downloads";

            ReadFileFromDirectory(SourceZipFile);


        }

        public static void ReadFileFromDirectory(string path)
        {
            DirectoryInfo Dir = new DirectoryInfo(path);
           List<FileInfo> files = Dir.GetFiles().ToList();
            foreach (FileInfo fileinfo in files)
            {
                if(fileinfo.Extension == ".zip" && fileinfo.Name.Contains("ROIInvoice"))
                {
                    ZipFile.ExtractToDirectory(path + "\\" + fileinfo.Name, path);
                    break;
                }
            }

        }

    }
}
