using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FileWriter.Objects;

namespace FileWriter.Handles
{
    sealed class FileHandle : IFileHandle
    {
        /// <summary>
        /// The currently opened FileStream
        /// </summary>
        private static string _filePath;

        /// <summary>
        /// Provides the current instance as a IFileHandle
        /// </summary>
        private IFileHandle Instance
        {
            get
            {
                return ((IFileHandle)this);
            }
        }

        ///// <summary>
        ///// Indicates whether the FileStream is open
        ///// </summary>
        //private Boolean IsOpen
        //{
        //    get
        //    {
        //        return _fileStream != null && !_fileStream.SafeFileHandle.IsClosed;
        //    }
        //}

        private FileHandle(string path)
        {
            _filePath = path;
        }

        public static new IFileHandle GetInstance(string path)
        {
            return new FileHandle(path);
        }

        public override void Create(object toAdd)
        {
            using (StreamWriter writer = File.AppendText(_filePath))
            {
                writer.WriteLine(toAdd);
            }
        }

        public override List<string> Read()
        {
            List<string> lines = File.ReadAllLines(_filePath).ToList();
            return lines;
        }

        public override void Update(List<string> text)
        {
            Delete(text);
        }

        public override void Delete(List<string> values)
        {
            File.Delete(_filePath);
            //using (File.Create(_filePath))
            //{}
            File.WriteAllLines(_filePath, values);
        }

        /// <summary>
        /// Opens a file
        /// </summary>
        /// <param name="path">The file to open</param>
        /// <returns>The filestream for the opened file</returns>
        public override FileStream Open(string path)
        {
            return File.Open(path, FileMode.OpenOrCreate);
        }

        /// <summary>
        /// Closes and Disposes
        /// </summary>
        public override void Dispose()
        {
        }
    }
}
