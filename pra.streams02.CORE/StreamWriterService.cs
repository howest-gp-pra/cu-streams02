using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace pra.streams02.CORE
{
    public class StreamWriterService
    {
        public static bool WriteStringToFile(string content, string path, string fileName, Encoding encoding = null, bool overWrite=false)
        {
            bool successfull = false;
            if (encoding == null) encoding = Encoding.Default;
            if (!Directory.Exists(path))
                throw new Exception($"Het opgegeven pad {path} bestaat niet!");
            string fullPath = path + "\\" + fileName;

            if (!overWrite)
            {
                if (File.Exists(fullPath))
                    throw new Exception($"De bestandsnaam {fileName} is in de map {path} reeds in gebruik.\nGebruik de knop Overschrijf bestand");
            }
            else
            {
                try
                {
                    File.Delete(fullPath);
                }
                catch
                {
                    throw new Exception($"De bestandsnaam {fileName} in de map {path} is momenteel in gebruik.\nProbeer later opnieuw");
                }
            }
            try
            {
                using (FileStream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream, encoding))
                    {
                        streamWriter.Write(content);
                        streamWriter.Close();
                    }
                }
                successfull = true;
            }
            catch(Exception e)
            {
                throw new Exception($"Er is een fout opgetreden. {e.Message}");
            }
            return successfull;
        }

 
    }
}
