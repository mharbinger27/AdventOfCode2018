using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class FabricSubmission
    {
        private string incomingText;
        private int[,] availableFabric;
        private int identifier;
        private int horizontalOffset;
        private int verticalOffset;
        private int width;
        private int height;

        public int Identifier
        {
            get => identifier;
            set => identifier = value;
        }

        List<Tuple<int, int>> coordinatesList = new List<Tuple<int, int>>();

        public FabricSubmission(string incomingText, int[,] grid)
        {
            availableFabric = grid;

            string[] firstSplit = incomingText.Split();
            string identificationChunk = firstSplit[0].TrimStart('#');
            string coordinatesChunk = firstSplit[2].Trim(':');
            string dimensionsChunk = firstSplit[3];
            string[] coordinates = coordinatesChunk.Split(',');
            string[] dimensions = dimensionsChunk.Split('x');

            identifier = int.Parse(identificationChunk);
            horizontalOffset = int.Parse(coordinates[0]);
            verticalOffset = int.Parse(coordinates[1]);
            width = int.Parse(dimensions[0]);
            height = int.Parse(dimensions[1]);

            for (int i = horizontalOffset; i < horizontalOffset + width; i++)
            {
                for (int j = verticalOffset; j < verticalOffset + height; j++)
                {
                    availableFabric[i, j]++;
                    coordinatesList.Add(Tuple.Create(i, j));
                }
            }
        }

        public bool ReportFabric()
        {
            bool status = true;

            foreach (var location in coordinatesList)
            {
                int x = location.Item1;
                int y = location.Item2;

                if (availableFabric[x, y] != 1)
                {
                    status = false;
                }
            }

            return status;
        }
    }
}
