using System;
using System.CodeDom;
using System.IO;
using System.Runtime.CompilerServices;

namespace CSArc
{
	class MainClass{
		public static void Main (string[] args){

            Start();
		    
		}
	    public static void Start(){

            InputOutput io = new InputOutput();

            io.Prompt("Welcome To CSArchive\nPlease choose your option\n");
            io.Prompt("To Extract a File Use :extract <input file> [<file to extract>]\n");
	        io.Prompt("To Archive a File Use:archive <folder name> <output file>\n");
            io.Prompt("To quit the Program type 'exit'\n");


            do
	        {

	            string input = io.ReadLine();
	            var commands = io.SplitStringByDelimiter(' ', input);


	            switch (commands[0].ToLower())
	            {

	                case "extract":
	                    Console.WriteLine("Extracting File...");
	                    Extractor csExt = new Extractor(commands);
                        Console.WriteLine("Extracting File finished");

                        break;
	                case "archive":
                        if (commands.Length == 3){
                            Console.WriteLine("Archiveing File...");
                            Archiver csArc = new Archiver(commands);
                            Console.WriteLine("Archiveing File finished");
                        }
                        
                        break;

                    case "exit":

                        Environment.Exit(0);
	                    break;

	                default:
                        Console.WriteLine("Bad Parameters...");
                        break;

	            }

	        } while (true);



	    }

	}
}
