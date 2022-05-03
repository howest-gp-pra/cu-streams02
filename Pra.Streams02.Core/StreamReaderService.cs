using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Pra.Streams02.Core
{
    public class StreamReaderService
    {
        public static string ReadFileToString(string path, string fileName, Encoding encoding=null)
        {
            string fileContent = "";
            string filePath = path + "\\" + fileName;
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            try
            {
                using (StreamReader streamReader = new StreamReader(filePath, encoding))
                {
                    fileContent = streamReader.ReadToEnd();
                }
            }
            catch (IOException)
            {
                throw new IOException($"Het bestand {filePath} kan niet geopend worden.\nProbeer het te sluiten.");
            }
            catch (Exception e)
            {
                throw new Exception($"Er is een fout opgetreden. {e.Message}");
            }
            return fileContent;
        }
    }
}
