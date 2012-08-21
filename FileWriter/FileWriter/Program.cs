using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FileWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            Program prog = new Program();
            prog.Run();
            Console.ReadLine();
        }

        /* writes to a file called 1.txt in the root directory */
        public void Run()
        {
            try
            {
                String rootDirectory = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));

                String path = rootDirectory + "1.txt";
                FileStream fileStream = OpenFile(path);
                String toWrite = "I Wrote Stuff fodslighroiufhgdsoughfdoiugnhiugfneoakndx";
                Console.WriteLine("Writing: " + toWrite + ", to file: " + path);

                Write(fileStream, toWrite);
                CloseStream(fileStream);

                /* and done */
                Console.WriteLine("Done writing to file: " + path);
                Console.WriteLine("Closing file: " + path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public FileStream OpenFile(string path)
        {
            FileStream fileStream;
            /* if file at the path above doesn't exist it, creates it then opens it, otherwise it just opens it*/
            if (!File.Exists(path))
            {
                Console.WriteLine("Creating file: " + path);
                fileStream = File.Create(path);
            }
            else
            {
                Console.WriteLine("Opening file: " + path);
                fileStream = File.OpenWrite(path);
            }
            return fileStream;
        }

        public void Write(FileStream fileStream, string toWrite)
        {
            /* writes stuff to the opened file */
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.WriteLine(toWrite);
                /* makes sure its sent to be written */
                writer.Flush();
            }
        }

        public string Read(FileStream fileStream)
        {
            string data = null;
            if (fileStream != null)
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    data = reader.ReadToEnd();
                }
            }
            return data;
        }

        public void Delete(FileStream fileStream)
        {

        }

        public void Update(FileStream fileStream, string newData)
        {

        }

        public void CloseStream(FileStream fileStream)
        {
            if (fileStream != null)
                fileStream.Close();
        }
    }
}
