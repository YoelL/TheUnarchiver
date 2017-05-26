using System;

namespace CSArc
{
    [Serializable]
    public class ArchiveStructure
    {

        public string fileName { get; set; }
        public string filePath { get; set; }
        public int fileEntry { get; set; }
        public byte[] rawBinaryData { get; set; }

        public ArchiveStructure()
        {
            this.filePath = "";
            this.fileName = "";
            rawBinaryData = new byte[0];
        }
        public void PrintArcStructure(long totalSize){
          Console.WriteLine(filePath + "\\" + fileName + " Size: " + fileEntry + " bytes " + Percentage(totalSize, fileEntry) + "%");
        }

        private long Percentage(long total, long file)
        {
            var res = file/total * (long)100.0;
            return ConvertFileSize(res);
        }

        private static long ConvertFileSize (long size)
        {
            if (size < 1024)
            {
                return size/100;
            }
            else if ((size >> 10) < 1024)
            {
                return size / 1000;
            }
            else if ((size >> 20) < 1024)
            {
                return size / 10000;
            }
            else 
            {
                return size / 100000;
            }
            
           
        }


    }
}

