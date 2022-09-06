using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace AzureFuntionsBlobTrigger
{
    public static class GetImageAndResize
    {
        [FunctionName("GetImageAndResize")]
        public static void Run(
            [BlobTrigger("product/{name}", Connection = "AzureBlobStorage")] Stream inputBlob,
            [Blob("product-thumb2/{name}", FileAccess.Write, Connection = "AzureBlobStorage")] Stream outputBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {inputBlob.Length} Bytes");

            IImageFormat format;

            using (Image<Rgba32> input = Image.Load<Rgba32>(inputBlob, out format))
            {
                input.Mutate(x => x.Resize(320, 200));
                input.Save(outputBlob, format);
            }
        }
    }
}
