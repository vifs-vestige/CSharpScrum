using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FileWriter.Handles
{
    abstract class IFileHandle : IDisposable
    {
        public static IFileHandle GetInstance(string path)
        {
            throw new NotImplementedException();
        }

        public abstract void Create(object toAdd);
        public abstract void Update(List<string> text);
        public abstract void Delete(List<string> values);
        public abstract List<string> Read();
        public abstract FileStream Open(string path);

        public abstract void Dispose();
    }
}
