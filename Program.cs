using System;
using System.IO;
using System.Collections.Generic;

namespace MazeRatsNPCGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // First check if there are any text files: without them, nothing will be generated
            string currentDir = Directory.GetCurrentDirectory();
            string appdataDir = currentDir + "\\appdata";
            var files = Directory.GetFiles(currentDir, "*.txt");

            if (files.Length == 0)
            {
                Console.WriteLine("ERROR: No text files found, can't run generator!");
                Environment.Exit(2);
            }

            // Initialize variables
            var listofLists = new List<List<string>>();
            var listofResults = new List<string>();
            int numberofNPCs = 0;
            string help = "\n" + "Usage: MazeRatsNPCGenerator.exe number" + "\n" + "\n" +
                "Example: MazeRatsNPCGenerator.exe 3 --> will create 3 NPCs" + "\n" + "\n" +
                "If you run without arguments, the default number of 10 will be used";

            if (args.Length == 0)
            {
                Console.WriteLine("INFO: No CLI arguments given, running with default parameters");
                numberofNPCs = 10;
            }
            else if (args.Length == 1)
            {
                if (args[0] == "/?")
                {
                    Console.WriteLine(help);
                    Environment.Exit(0);
                }
                else
                {
                    Int32.TryParse(args[0], out numberofNPCs);
                    if (numberofNPCs == 0)
                    {
                        Console.WriteLine("INFO: Argument couldn't be parsed as integer, using default setting of 10 NPCs");
                        numberofNPCs = 10;
                    }
                }
            }
            else
            {
                Console.WriteLine("INFO: More than 1 CLI argument given, need only 1!");
                Console.WriteLine(help);
                Environment.Exit(0);
            }

            var generator = new RandomGenerator();
            string resultPath = currentDir + "\\NPCs.log";
            Console.WriteLine($"Generating {numberofNPCs} NPCs...");

            for (int i = 0; i < numberofNPCs; i++)
            {
                listofResults.Add($"NPC {i + 1}");
                foreach (var file in files)
                {
                    listofLists = new List<List<string>>();
                    string filePath = file;
                    string[] lines = File.ReadAllLines(filePath);
                    string fileName = Path.GetFileNameWithoutExtension(filePath);
                    var tempList = new List<string>();

                    foreach (var line in lines)
                    {
                        if (line != "")
                        {
                            tempList.Add(line);
                        }
                        else
                        {
                            listofLists.Add(tempList);
                            tempList = new List<string>();
                        }
                    }

                    var columnRolled = generator.RandomNumber(1, 6);
                    var rowRolled = generator.RandomNumber(1, 6);
                    var columnResult = listofLists[columnRolled - 1]; // index starts at 0
                    var rowResult = columnResult[rowRolled - 1];
                    listofResults.Add(fileName + ": " + rowResult);
                }

                listofResults.Add("");
                File.WriteAllLines(resultPath, listofResults);
            }

            Console.WriteLine($"Randomly generated NPCs can be found in {resultPath}");
        }
    }
}
