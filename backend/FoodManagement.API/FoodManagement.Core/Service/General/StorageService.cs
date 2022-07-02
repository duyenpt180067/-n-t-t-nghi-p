using Azure.Storage.Blobs;
using FoodManagement.Core.Interface.IService.General;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManagement.Core.Service.General
{
    public class StorageService : IStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IConfiguration _configuration;

        public StorageService(BlobServiceClient blobServiceClient, IConfiguration configuration)
        {
            _blobServiceClient = blobServiceClient;
            _configuration = configuration;
        }

        public string UploadFile(IFormFile file)
        {
            var containerName = _configuration.GetSection("Storage:ContainerName").Value;

            var containterClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containterClient.GetBlobClient(file.FileName);

            using var stream = file.OpenReadStream();
            blobClient.Upload(stream, true);
            return blobClient.Uri.AbsoluteUri;
        }

        public List<string> UploadMultipleFile(List<IFormFile> files)
        {
            List<string> listUrl = new List<string>();
            var containerName = _configuration.GetSection("Storage:ContainerName").Value;

            var containterClient = _blobServiceClient.GetBlobContainerClient(containerName);
            foreach (var file in files)
            {
                var blobClient = containterClient.GetBlobClient(file.FileName);

                using var stream = file.OpenReadStream();
                blobClient.Upload(stream, true);
                listUrl.Add(blobClient.Uri.AbsoluteUri);
            }
            return listUrl;
        }
    }
}
