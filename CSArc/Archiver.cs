using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace CSArc
{
    public class Archiver
    {

        private ArchiveStructure archiveStructure;
        public List<ArchiveStructure> archives = new List<ArchiveStructure>();

        public ArchiveStructure ArchiveStructure
        {
            get { return archiveStructure; }

            set { archiveStructure = value; }
        }

        private string[] filePath { get; }
        private string outputFile { get; }
        private static long globalArchiveSize;

        public Archiver(string[] arg)
        {
            filePath = new[] {@""+ arg[1]};
            outputFile = arg[2];

            BuildFileEntris(filePath);
            globalArchiveSize = GetDirectorySize(arg[1]);
            Serialize(arg[1]);
            printAllArchives();
        }

        private void BuildFileEntris(string[] arg)
        {

            foreach (string path in arg)
            {
                if (File.Exists(path))
                {
                    // This path is a file
                    ProcessFile(path);
                }
                else if (Directory.Exists(path))
                {
                    // This path is a directory
                    ProcessDirectory(path);
                }
                else
                {
                    Console.WriteLine("{0} is not a valid file or directory.", path);
                    Console.WriteLine("Program Ended.");
                    Environment.Exit(0);
                }
            }
        }

        // Process all files in the directory passed in, recurse on any directories 
        // that are found, and process the files they contain.
        private void ProcessDirectory(string targetDirectory)
        {

            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);


            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
            {
                //Console.WriteLine("Processed Subdirectory '{0}'.", subdirectory);
                ProcessDirectory(subdirectory);
            }

        }

        // Insert logic for processing found files here.
        private void ProcessFile(string path)
        {
            //	Console.WriteLine("Processed file '{0}'.", path);
            string fileName = Path.GetFileName(path);
            string directoryName = Path.GetDirectoryName(path);

            archiveStructure = new ArchiveStructure();
            var csArc = BuildArchiveStructure(fileName, directoryName);
            archives.Add(csArc);
        }

        private static long GetFileSize(string fileName)
        {
            // Calculate total bytes of a file.
            long b = 0;
            FileInfo info = new FileInfo(fileName);
            b += info.Length;
            return b;
        }

        private static long GetDirectorySize(string p)
        {
            // 1.
            // Get array of all file names.
            string[] a = Directory.GetFiles(p, "*.*");

            // 2.
            // Calculate total bytes of all files in a loop.
            long b = 0;
            foreach (string name in a)
            {
                // 3.
                // Use FileInfo to get length of each file.
                b += GetFileSize(name);
            }
            // 4.
            // Return total size
            return b;
        }

        private ArchiveStructure BuildArchiveStructure(string fileName, string filePath)
        {

            var fullPath = @"" + filePath + "\\" + fileName;
            var rawData = File2BinaryArray(fullPath);

            ArchiveStructure.fileName = fileName;
            ArchiveStructure.filePath = filePath;
            ArchiveStructure.fileEntry = rawData.Length;
            ArchiveStructure.rawBinaryData = rawData;
            return ArchiveStructure;
        }

        private byte[] File2BinaryArray(string filePath)
        {
            byte[] buffer;

            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            try
            {
                int length = (int) fileStream.Length;
                buffer = new byte[length];
                int count;
                int sum = 0;

                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;

        }

        public void Serialize(string archivePathToSave)
        {

            string deskPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            try
            {
                using (Stream stream = File.Open(@""+deskPath+"\\"+outputFile+".csarc", FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, archives);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Problem Serializing archives " + e.Message);
            }
            finally
            {
                Console.WriteLine("Serializing archive was succesfull.");

            }

        }

        public void printAllArchives()
        {

            Console.WriteLine("Total Size : " + globalArchiveSize + " bytes  - File Count " + archives.Count);

            foreach (var archive in archives)
            {

                archive.PrintArcStructure(globalArchiveSize);
            }
        }

    }


}


