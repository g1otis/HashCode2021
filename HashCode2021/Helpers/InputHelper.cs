using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HashCode2021.Models;

namespace HashCode2021.Helpers
{
    public static class InputHelper
    {
        public static async Task<InputDataModel> InitAsync(string filePath)
        {
            Console.WriteLine($"[{filePath}]: reading..");

            string line;
            var model = new InputDataModel();
            using (StreamReader file = new System.IO.StreamReader(@$"Resources\Inputs\{filePath}"))
            {
                var metadataLine = await file.ReadLineAsync();
                var metadataTable = metadataLine!.Split(" ");

                model.TotalDuration = int.Parse(metadataTable[0]);
                
                var intersectionsCounter = int.Parse(metadataTable[1]);
                model.IntersectionIds = Enumerable.Range(0, intersectionsCounter).ToHashSet();
                
                var streetsCounter = int.Parse(metadataTable[2]);
                var carsCounter = int.Parse(metadataTable[3]);
                
                //read streets
                for (int i = 0; i < streetsCounter; i++)
                {
                    line = await file.ReadLineAsync();
                    var streetLineArray = line.Split(" ");
                    var street = new Street
                    {
                        StartIntersection = int.Parse(streetLineArray[0]),
                        EndIntersection = int.Parse(streetLineArray[1]),
                        Name = streetLineArray[2],
                        Duration = int.Parse(streetLineArray[3])
                    };

                    model.Streets.Add(street.Name, street);
                }
                // read cars
                for (int i = 0; i < carsCounter; i++)
                {
                    line = await file.ReadLineAsync();
                    var streetLineArray = line.Split(" ");
                    var car = new Car
                    {
                       JourneyStreets = streetLineArray.Skip(1).ToList()
                    };

                    model.Cars.Add(car);
                }

                Console.WriteLine($"[{filePath}]: read!");

                return model;
            }
        }
    }
}
