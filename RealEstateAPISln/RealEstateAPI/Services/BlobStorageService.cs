

using Azure.Storage.Blobs;
using RealEstateAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace RealEstateAPI.Services
{
    [ExcludeFromCodeCoverage]
    public class BlobStorageService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName = "mv-67acres-container";

        public BlobStorageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("No file uploaded");
            }

            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            await blobContainerClient.CreateIfNotExistsAsync();

            var blobClient = blobContainerClient.GetBlobClient(file.FileName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            return blobClient.Uri.ToString();
        }

        public async Task<string> GetFileUrlAsync(string fileName)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = blobContainerClient.GetBlobClient(fileName);

            if (await blobClient.ExistsAsync())
            {
                return blobClient.Uri.ToString();
            }

            throw new FileNotFoundException("The requested file does not exist.");
        }
    }
}

