using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Worker
    {
        // 01
        // Opens a file containing a list of numbers and sums them.
        internal void SumNumbers(string filePath)
        {
            if (File.Exists(filePath))
            {
                int sum = 0;

                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (reader.Peek() > 0)
                    {
                        sum += int.Parse(reader.ReadLine());
                    }
                }

                Console.WriteLine($"Sum of numbers: {sum}");
            }
            else
            {
                Console.WriteLine("File does not exist.");
                return;
            }
        }

        // 02
        // Builds on 01. Logs the new sum at each step, and continues
        // until a duplicate sum is found.
        internal void FindDuplicates(string filePath)
        {
            if (File.Exists(filePath))
            {
                int sum = 0;
                int? duplicate = null;
                List<int> sumLog = new List<int>();
                sumLog.Add(sum);

                while (duplicate == null)
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        while (reader.Peek() > 0)
                        {
                            sum += int.Parse(reader.ReadLine());

                            if (sumLog.Contains(sum))
                            {
                                duplicate = sum;
                                break;
                            }
                            else
                            {
                                sumLog.Add(sum);
                            }
                        }
                    }

                    sumLog.Sort();
                }

                Console.WriteLine($"Duplicate sum: {duplicate}");
                Console.WriteLine($"Entries in list: {sumLog.Count}");
            }
            else
            {
                Console.WriteLine("File does not exist.");
                return;
            }
        }

        // 03
        // Opens a file and reads in strings. Each string is checked
        // to see if any letters are duplicated 2 or 3 times. The total
        // number of strings with 2 and total number of strings with 3
        // are multipied.
        public void Inventory(string filePath)
        {
            if (File.Exists(filePath))
            {
                int twoLetters = 0;
                int threeLetters = 0;
                bool twoFlag;
                bool threeFlag;

                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (reader.Peek() > 0)
                    {
                        twoFlag = false;
                        threeFlag = false;

                        string incomingText = reader.ReadLine();
                        char[] characters = incomingText.ToCharArray();
                        List<char> characterList = characters.ToList();

                        var q = characterList.GroupBy(x => x)
                            .Select(g => new {Value = g.Key, Count = g.Count()})
                            .OrderByDescending(x => x.Count);

                        foreach (var x in q)
                        {
                            if (x.Count == 2)
                            {
                                twoFlag = true;
                            }

                            if (x.Count == 3)
                            {
                                threeFlag = true;
                            }
                        }

                        if (twoFlag)
                        {
                            twoLetters++;
                        }

                        if (threeFlag)
                        {
                            threeLetters++;
                        }
                    }
                }

                Console.WriteLine($"Two letters: {twoLetters}.");
                Console.WriteLine($"Three letters: {threeLetters}.");
                Console.WriteLine($"Sum of numbers: {twoLetters * threeLetters}");
            }
            else
            {
                Console.WriteLine("File does not exist.");
                return;
            }
        }

        // 04
        // Builds on 03. Compares IDs looking for two that only have different
        // characters in a single location.
        public void FindCloseMatch(string filePath)
        {
            List<char[]> inventoryNumbers = new List<char[]>();
            char[] inventoryA;
            char[] inventoryB;
            int shared = 0;

            using (StreamReader reader = new StreamReader(filePath))
            {
                while (reader.Peek() > 0)
                {
                    inventoryNumbers.Add(reader.ReadLine().ToCharArray());
                }
            }

            for (int i = 0; i < inventoryNumbers.Count; i++)
            {
                for (int j = (i + 1); j < inventoryNumbers.Count; j++)
                {
                    for (int k = 0; k < inventoryNumbers[i].Length; k++)
                    {
                        if (inventoryNumbers[i][k] == inventoryNumbers[j][k])
                        {
                            shared++;
                        }
                    }

                    if (shared == (inventoryNumbers[i].Length - 1))
                    {
                        inventoryA = inventoryNumbers[i];
                        inventoryB = inventoryNumbers[j];

                        StringBuilder matches = new StringBuilder();

                        for (int l = 0; l < inventoryA.Length; l++)
                        {
                            if (inventoryA[l] == inventoryB[l])
                            {
                                matches.Append(inventoryA[l]);
                            }
                        }

                        Console.WriteLine($"Matching characters: {matches}");

                        return;
                    }
                    else
                    {
                        shared = 0;
                    }
                }
            }
        }

        // 05
        // Parses strings to get coordinates in a grid. Sums individual 
        // coordinates each time they are claimed. Finds total number
        // of coordinates claimed more than once.
        public void Fabric(string filePath)
        {
            int[,] squareInches = new int[1000, 1000];
            string incomingText;
            int contestedSquares = 0;

            using (StreamReader reader = new StreamReader(filePath))
            {
                while (reader.Peek() > 0)
                {
                    incomingText = reader.ReadLine();
                    string[] firstSplit = incomingText.Split();
                    string coordinatesChunk = firstSplit[2].Trim(':');
                    string dimensionsChunk = firstSplit[3];
                    string[] coordinates = coordinatesChunk.Split(',');
                    string[] dimensions = dimensionsChunk.Split('x');

                    int horizontalOffset = int.Parse(coordinates[0]);
                    int verticalOffset = int.Parse(coordinates[1]);
                    int width = int.Parse(dimensions[0]);
                    int height = int.Parse(dimensions[1]);

                    for (int i = horizontalOffset; i < horizontalOffset + width; i++)
                    {
                        for (int j = verticalOffset; j < verticalOffset + height; j++)
                        {
                            squareInches[i, j]++;
                        }
                    }
                }
            }

            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    if (squareInches[i, j] > 1)
                    {
                        contestedSquares++;
                    }
                }
            }

            Console.WriteLine($"Contested squares: {contestedSquares}");
        }

        // 06
        // Builds on 05. Maintains a list of submissions, each with references
        // to coordinates on the grid. Asks the references to check the
        // coordinates on the grid for their values. Identifies submission
        // where all those values equal 1.
        public void FabricRevisited(string filePath)
        {
            int[,] squareInches = new int[1000, 1000];
            List<FabricSubmission> submissionList = new List<FabricSubmission>();
            FabricSubmission uncontested = null;

            using (StreamReader reader = new StreamReader(filePath))
            {
                while (reader.Peek() > 0)
                {
                    FabricSubmission newSubmission = new FabricSubmission(reader.ReadLine(), squareInches);
                    submissionList.Add(newSubmission);
                }
            }

            foreach (FabricSubmission submission in submissionList)
            {
                if (submission.ReportFabric())
                {
                    uncontested = submission;
                    break;
                }
            }

            if (uncontested == null)
            {
                Console.WriteLine("Could not find uncontested submissions.");
            }
            else
            {
                Console.WriteLine($"Uncontested submission: {uncontested.Identifier}");

            }
        }
    }
}
