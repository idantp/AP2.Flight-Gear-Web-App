using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Web;

namespace EvenDerech_4_.Models
{
    public class FileHandler
    {
        // Singleton
        private static FileHandler fileHandlerInstance = null;
        private bool firstWrite;
        private string filePath;
        private string detailsLine;

        private FileHandler()
        { }


        public static FileHandler GetFileHandlerInstance
        {
            get {
                if (fileHandlerInstance == null) {
                    fileHandlerInstance = new FileHandler();
                }
                return fileHandlerInstance;
            }
        }

        public bool FirstWrite {
            get {
                return firstWrite;
            }
            set {
                firstWrite = value;
            }
        }
        
        public string FilePath
        {
            get => filePath;
            set => filePath = value;
        }
        public string DetailsLine
        {
            get => detailsLine;
            set => detailsLine = value;
        }

        public void SaveDataToFile()
        {
            if (firstWrite)
            {
                firstWrite = false;
            }

            if (File.Exists(FilePath) && firstWrite) {
                File.Delete(FilePath);
            }


            using (StreamWriter streamWriter = File.AppendText(FilePath))
            {
                streamWriter.WriteLine(this.detailsLine);
            }
        }

    }
}