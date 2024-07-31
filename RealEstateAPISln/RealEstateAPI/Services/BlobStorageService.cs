//using Azure.Storage.Blobs;
//using RealEstateAPI.Interfaces;

//namespace RealEstateAPI.Services
//{
//    public class BlobStorageService : IBlobService
//    {
//        private readonly BlobServiceClient _blobServiceClient;
//        private readonly string _containerName = "mv-products";

//        public BlobStorageService(BlobServiceClient blobServiceClient)
//        {
//            _blobServiceClient = blobServiceClient;

//        }

//        public async Task<string> UploadImageAsync(IFormFile imageFile)
//        {
//            if (imageFile == null || imageFile.Length == 0)
//            {
//                throw new ArgumentException("No file uploaded");
//            }

//            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
//            await blobContainerClient.CreateIfNotExistsAsync();

//            var blobClient = blobContainerClient.GetBlobClient(imageFile.FileName);

//            using (var stream = imageFile.OpenReadStream())
//            {
//                await blobClient.UploadAsync(stream, true);
//            }

//            return blobClient.Uri.ToString();
//        }

//        public async Task<string> GetImageUrlAsync(string imageName)
//        {
//            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
//            var blobClient = blobContainerClient.GetBlobClient(imageName);

//            if (await blobClient.ExistsAsync())
//            {
//                return blobClient.Uri.ToString();
//            }

//            throw new FileNotFoundException("The requested image does not exist.");
//        }

//    }
//}

using Azure.Storage.Blobs;
using RealEstateAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RealEstateAPI.Services
{
    public class BlobStorageService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName = "mv-products";

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

