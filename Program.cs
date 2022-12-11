using System;
using System.IO;

// TODO: Calculate for each line 1) the line endings count (check for all possible endings), 2) the leading and trailing whitespace (excluded line endings)

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // read the command line arguments
            string directoryPath = args[0];
            string fileExtension = args[1];
            string outputFilePath = args[2];

            // merge the files and print the summary information
            MergeFiles(directoryPath, fileExtension, outputFilePath);
        }

        static void MergeFiles(string directoryPath, string fileExtension, string outputFilePath)
        {
            // get all files with the specified file extension from the directory and its subdirectories
            var files = Directory.GetFiles(directoryPath, "*" + fileExtension, SearchOption.AllDirectories);

            // create a new summary object
            var summary = new Summary();

            // create a new output file
            using (var outputFile = new StreamWriter(outputFilePath))
            {
                // loop through all the files
                foreach (var file in files)
                {
                    // process the file and print its information
                    ProcessFile(file, outputFile, summary);
                }
            }

            // print the summary information
            summary.PrintSummary(files.Length);
        }

        static void ProcessFile(string filePath, StreamWriter outputFile, Summary summary)
        {
            Console.WriteLine($"Processing file: {filePath}");

            // read the contents of the file
            var fileContents = File.ReadAllLines(filePath);

            // write the contents of the file to the output file
            foreach (var line in fileContents)
            {
                outputFile.WriteLine(line);
            }

            Console.WriteLine($"File has {fileContents.Length} lines.");

            // increment the line count in the summary
            summary.TotalLineCount += fileContents.Length;

            // get the file size in bytes
            var fileInfo = new FileInfo(filePath);
            var fileSize = fileInfo.Length;

            // print the file size
            Console.WriteLine($"File size: {fileSize} bytes.");

            // increment the total file size
            summary.TotalFileSize += fileSize;

            // get the total character count for the file
            var characterCount = fileContents.Sum(line => line.Length);

            // print the character count
            Console.WriteLine($"Character count: {characterCount}.");

            // increment the total character count
            summary.TotalCharacterCount += characterCount;

            // get the total token count for the file
            var tokenCount = (long)Math.Ceiling(characterCount / 4.0);

            // print the token count
            Console.WriteLine($"Token count: {tokenCount}.");

            // increment the total token count
            summary.TotalTokenCount += tokenCount;

            // calculate the price for the file
            var price = 0.0200m * tokenCount / 1000;

            // print the price
            Console.WriteLine($"Price: ${price}.");

            // increment the total price
            summary.TotalPrice += price;
        }
    }

    class Summary
    {
        public int TotalLineCount { get; set; } = 0;
        public long TotalFileSize { get; set; } = 0;
        public long TotalCharacterCount { get; set; } = 0;
        public long TotalTokenCount { get; set; } = 0;
        public decimal TotalPrice { get; set; } = 0;

        public void PrintSummary(int fileCount)
        {
            Console.WriteLine($"Processed {fileCount} files with a total of {TotalLineCount} lines, {TotalCharacterCount} characters, {TotalTokenCount} tokens, {TotalFileSize} bytes, and ${TotalPrice}.");
        }
    }
}
