using System;
using System.Text.RegularExpressions;

namespace CSArc
{

	/// <summary>
	/// Input output class helps the user display messages and intercat with the user, 
	/// manipulate strings, and verify if an email address is a valid email .
	/// </summary>
	/// 
	/// 
	public class InputOutput
	{

		//Displays Message to screen
		public void Prompt(string prompt){
			Console.WriteLine(prompt);
		}

		//Displays underline sign to screen
		public void PromptUnderLine(int UnderLineLength){
			for (int i = 0; i < UnderLineLength; i++) {
				Console.Write("_ ");
			}
			Console.WriteLine(" ");
		}
	
		/// <summary>
		/// Displays the message/prompt and gets text from user
		/// EX - var shouldContinue = PromptAndGetText(“Continue (y/n)?”);
		/// </summary>
		/// <returns>The and get text.</returns>
		/// <param name="prompt">Prompt.</param>
		public string PromptAndGetText(string prompt){
			Console.Write(prompt + " ");
			return Console.ReadLine();
		}

		/// <summary>
		/// Reads the line .
		/// gets text from the user.
		/// </summary>
		/// <returns>The line.</returns>
		public string ReadLine(){

		return Console.ReadLine();
		
		}

		// returns True or False if the Text is Yes or No 
		public bool IsYesOrNo(string text){
			
			var upperText = text.ToUpperInvariant();

			return
				upperText == "Y" || upperText == "N" ||
				upperText == "YES" || upperText == "NO";
			
		}

		// Checks if the string enterd is not null or empty 
		public static bool IsValid(string input){
			return !string.IsNullOrEmpty(input);
		}


		/// <summary>
		/// Splits the string by delimiter.
		/// 
		/// <returns>array of strings.</returns>
		/// <param name="delimiter">Delimiter.</param>
		/// 
		///EX-  string s = "a,b, b, c";
		///		string[] val =	io.SplitStringByDelimiter (',', s);
		///		for (int i = 0; i < 4; i++) {
		///			Console.Write (val [i] + " ");
		///		}
		/// 
		/// </summary>
		/// 
		public string[] SplitStringByDelimiter(char delimiter ,string stringToSplit){
			
			string[] values = stringToSplit.Split(delimiter);
			for(int i = 0; i < values.Length; i++){
				values[i] = values[i].Trim();
			}
			return values;
		}
			

		/// <summary>
		/// Splits the string to parts.
		///
		/// <returns> an array with splited to X parts .</returns>
		/// <param name="parts"> integer .</param>
		/// <param name="text"> a string .</param>
		/// 
		/// Ex-
		/// InputOutput io = new InputOutput ();
		/// string[] val = io.SplitStringToParts (2, "Hello my name is");
		///  Console.Write (val[0]);
		///  Console.WriteLine (" ");	
	    ///  Console.Write (val[1]);
		/// 
		///  </summary>
		public string[] SplitStringToParts(int parts , string text){
			
			string[] splitedArray = text.Split(new char[]{' '},parts);

			return splitedArray;


		}

	    public Boolean Argument(int numberOfArgumet,string[] args) {



	        return true;
	    } 

	}
}

