using System;
using System.IO;
using System.Threading.Tasks;
using HashCode2021.Models;

namespace HashCode2021.Helpers
{
    class OutputHelper
    {
        public static async Task PrintAsync(string fileName, OutputDataModel output)
        {
            Console.WriteLine($"[{fileName}]: writing..");

            // if not exist then create
            Directory.CreateDirectory(@"Resources\Outputs");

            var outpFilePath = @$"Resources\Outputs\{fileName}";
            
            // Because in next runs the file might remain opened and the  StreamWriter fails
            File.Delete(outpFilePath);
            
            await using var fileStream = File.Create(outpFilePath);
            await using var streamWriter = new StreamWriter(fileStream);
            
            await streamWriter.WriteLineAsync(output.Intersections.Count.ToString());

            //output.Intersections.ForEach(async options => await IntersectionOptionsPrintAction(options, streamWriter));

            foreach (var intersectionOptions in output.Intersections.Values)
            {
                await IntersectionOptionsPrintAction(intersectionOptions, streamWriter);
            }

            Console.WriteLine($"[{fileName}]: wrote!");
        }

        private static async Task IntersectionOptionsPrintAction(IntersectionOptions options, StreamWriter streamWriter)
        {
            await streamWriter.WriteLineAsync(options.Id.ToString());
            await streamWriter.WriteLineAsync(options.IncomingStreets.Count.ToString());

            //options.IncomingStreets.ForEach(async streetOptions => await StreetOptionsPrintAction(streetOptions, streamWriter));
            foreach (var streetOptions in options.IncomingStreets)
            {
                await StreetOptionsPrintAction(streetOptions, streamWriter);
            }
        }

        private static async Task StreetOptionsPrintAction(StreetOptions options, StreamWriter streamWriter)
        {
            await streamWriter.WriteLineAsync($"{options.StreetName} {options.GreenLightDuration}");
        }
    }
}
