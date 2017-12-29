using PrinterPlusPlusSDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailWeatherInCelcius
{
    public class Processor : PrinterPlusPlusSDK.IProcessor
    {


        public PrinterPlusPlusSDK.ProcessResult Process(string key, string psFilename)
        {


            //Convert PS to Text

            var pdfFilename = System.IO.Path.GetTempFileName();
            PSToPdf(psFilename, pdfFilename);
            var txtFilename = System.IO.Path.GetTempFileName();
            Convert(pdfFilename, txtFilename);


            //Process the converted Text File

            FileStream fs = new FileStream(txtFilename, FileMode.Open, FileAccess.Read);
            byte[] buf = new byte[fs.Length];
            fs.Read(buf, 0, buf.Length);
            fs.Close();
            //while (File.Exists(string.Format("e:\\test{0}.txt", intnum)))
            //{
            //    intnum++;
            //}
            ////成功出来的就是不同名的
            //File.Create(string.Format("e:\\test.txt", intnum));

            //string Path =  "e:\\"+pdfFilename + ".txt";
            //string SaveFileName = "e:\\SaveFileName.txt";
            //StreamWriter srd;
           
            
            //    srd = File.CreateText(SaveFileName);


            string path1 = @"E:\temp";
            
            string filename =  System.IO.Path.GetFileName(txtFilename);
            string path2 = filename + ".txt";
            string newPath = Path.Combine(path1, path2);
           
            FileStream newFile = new FileStream(newPath, FileMode.Create, FileAccess.Write);
        //FileStream newFile = new FileStream("e:\\test.txt",FileMode.Create, FileAccess.Write);
            newFile.Write(buf, 0, buf.Length);
            newFile.Flush();
            

            return new ProcessResult();
   
        }
        public static string PSToPdf(string psFilename, string pdfFilename)
        {
            //var retVal = string.Empty;
            //var errorMessage = string.Empty;
            //var command = "C:\\Ps2txt\\ps2txt\\ps2txt.exe";
            //var args = string.Format("-nolayout \"{0}\" \"{1}\"", psFilename, txtFilename);
            //retVal = Shell.ExecuteShellCommand(command, args, ref errorMessage);
            //return retVal;
            //var retVal = string.Empty;
            //var errorMessage = string.Empty;

            //var command = "C:\\PrinterPlusPlus\\Converters\\gs\\gswin32c.exe";
            //var args = string.Format("-q -dNODISPLAY -P- -dSAFER -dDELAYBIND -dWRITESYSTEMDICT -dSIMPLE \"c:\\PrinterPlusPlus\\Converters\\gs\\ps2ascii.ps\" \"{0}\"\"{1}\" -c quit", psFilename, txtFilename);
            //retVal = Shell.ExecuteShellCommand(command, args, ref errorMessage);
            //return retVal;
            var retVal = string.Empty;
            var errorMessage = string.Empty;

            var command = "C:\\PrinterPlusPlus\\Converters\\gs\\gs8.64\\bin\\gswin32c.exe";
            var args = string.Format("-q -dNOPAUSE -dBATCH -sDEVICE=pdfwrite -sOutputFile=\"{1}\" -c save pop -f \"{0}\"", psFilename, pdfFilename);
            retVal = Shell.ExecuteShellCommand(command, args, ref errorMessage);
            return retVal;
        }
        public static string Convert(string pdfFilename, string txtFilename)
        {
            var retVal = string.Empty;
            var errorMessage = string.Empty;

            var command = "C:\\PrinterPlusPlus\\Converters\\pdftotxt\\pdftotext.exe";
            var args = string.Format("-q -f 1 -l 100 \"{0}\" \"{1}\"", pdfFilename, txtFilename);
            retVal = Shell.ExecuteShellCommand(command, args, ref errorMessage);
            return retVal;
        }




    }




}

