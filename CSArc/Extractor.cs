using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace CSArc
{
    class Extractor
    {

        private string fileOutputName { get; set; }
        private string path { get; set; }

        private List<ArchiveStructure> ExtractedArchives
        {
            get
            {
                return extractedArchives;
            }

            set
            {
                extractedArchives = value;
            }
        }

        private List<ArchiveStructure> extractedArchives;

        public Extractor(string[] arg)
        {
             ExtractedArchives = new List<ArchiveStructure>();
             StartExtraction(arg);   
        }

        private ArchiveStructure FindFileInArchive(string fileName)
        {

            foreach (var file in ExtractedArchives)
            {
                if (file.fileName == fileName)
                {
                    Console.WriteLine("The file " + fileName + " was found.");
                    return file;
                }
                 
            }

            Console.WriteLine("The file " + fileName + " was not found.");
            return null;
        }
 
        public List<ArchiveStructure> Deserialize(string archiveName)
        {
            var deskPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            try
            {
                using (Stream stream = File.Open(@""+deskPath+"\\"+archiveName +".csarc", FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    return ExtractedArchives = (List<ArchiveStructure>)bin.Deserialize(stream);
                   
                }
            }
            catch (IOException e) { 
                Console.WriteLine("Bad archive Name ,Deserialize aborted",e.Message);
                Environment.Exit(0);
            }
            
            return ExtractedArchives;
        }

        private void ExtractFileFromArchive(ArchiveStructure archive,string locationToExtract,Boolean allFiles)
        {
            string file;

            if (allFiles == true) {
                 file = @"" + archive.filePath + "\\" + archive.fileName;
            }
            else{
                 file = @"" + locationToExtract + "\\" + archive.fileName;
            }
            
            try
            {
                BinaryWriter Writer = new BinaryWriter(File.OpenWrite(file));

                //writes the Binary data to the rawData byte array.
                Writer.Write(archive.rawBinaryData);
                Writer.Flush();
                Writer.Close();

            }
            catch(Exception e)
            {
               Console.WriteLine("Error BuildArchive" +e);
            }

        }
        
        private void StartExtraction(string[] arg )
        {
            if (arg.Length == 2){
               UnpackArchive(arg);
            }else if (arg.Length == 3)
            {              
                var archiveName = arg[1];
                var file = arg[2];
                extractedArchives = Deserialize(archiveName);
                var fileFound = FindFileInArchive(file);


                if (fileFound != null)
                {
                    var deskPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    ExtractFileFromArchive(fileFound, @"" + deskPath + "\\", false);
                }
                else
                {
                    Environment.Exit(0);
                }
                
            }

        }

        private void UnpackArchive(string[] arg){

            //extract whole archive.
            this.fileOutputName = arg[1];
            extractedArchives = Deserialize(fileOutputName);

            foreach (var file in extractedArchives)
            {
                var dir = file.filePath;

                if (!Directory.Exists(file.filePath))
                    dir = Directory.CreateDirectory(file.filePath).ToString();
                ExtractFileFromArchive(file, dir,true);
            }


        }

    }
}

