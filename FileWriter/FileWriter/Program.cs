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
                String toWrite = "Done writing to file! fodslighroiufhgdsoughfdoiugnhiugfneoakndx";
                Console.WriteLine("Writing " + toWrite + " to file: " + path);
                /* writes stuff to the opened file */
                StreamWriter writer = new StreamWriter(fileStream);
                writer.WriteLine(toWrite);
                /* makes sure its sent to be written */
                writer.Flush();
                /* closes the file */
                writer.Close();
                fileStream.Close();
                /* and done */
                Console.WriteLine("Done writing to file: " + path);
                Console.WriteLine("Closing file: " + path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
