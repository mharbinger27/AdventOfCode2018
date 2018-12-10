using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("No program identifier passed.");
            }
            else if (args.Length < 2)
            {
                Console.WriteLine("No file path passed.");
            }
            else
            {
                int programSelector = int.Parse(args[0]);
                string filePath;

                if (File.Exists(args[1]))
                {
                    filePath = args[1];
                }
                else
                {
                    Console.WriteLine("Cannot find file at specified path.");
                    return;
                }

                Worker letsDoWork = new Worker();

                switch (programSelector)
                {
                    case 1:
                        Console.WriteLine("Beginning 01: Sum...");
                            letsDoWork.SumNumbers(filePath);
                        break;
                    case 2:
                        Console.WriteLine("Beginning 02: Duplicates...");
                            letsDoWork.FindDuplicates(filePath);
                        break;
                    case 3:
                        Console.WriteLine("Beginning 03: Inventory...");
                            letsDoWork.Inventory(filePath);
                        break;
                    case 4:
                        Console.WriteLine("Beginning 04: Find close inventory match...");
                            letsDoWork.FindCloseMatch(filePath);
                        break;
                    case 5:
                        Console.WriteLine("Beginning 05: Fabric...");
                        letsDoWork.Fabric(filePath);
                        break;
                    case 6:
                        Console.WriteLine("Beginning 06: Fabric revisited...");
                        letsDoWork.FabricRevisited(filePath);
                        break;
                    default:
                        Console.WriteLine("Program identifier passed was not found.");
                        break;
                }
            }

            Console.Write("Press any key to exit: ");
            Console.ReadKey();
        }
    }
}
