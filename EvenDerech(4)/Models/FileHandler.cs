using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvenDerech_4_.Models
{
    public class FileHandler
    {
        // Singleton
        private static FileHandler fileHandlerInstance = null;
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
            System.IO.File.WriteAllText(FilePath,DetailsLine);
        }

    }
}