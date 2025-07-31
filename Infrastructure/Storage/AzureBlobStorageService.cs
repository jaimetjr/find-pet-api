using Application.Interfaces.Services;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Storage
{
    public class AzureBlobStorageService : IFileStorageService
    {
        private readonly string _containerName;
        private readonly string _connectionString;
        private readonly string _accountKey;
        private readonly BlobContainerClient _containerClient;

        public AzureBlobStorageService(IConfiguration configuration, BlobContainerClient containerClient)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration), "Configuration cannot be null.");
            }
            _containerName = configuration["AzureBlobStorage:ContainerName"]!;
            _connectionString = configuration["AzureBlobStorage:ConnectionString"]!;
            _containerClient = containerClient;
            if (string.IsNullOrEmpty(_containerName) || string.IsNullOrEmpty(_connectionString))
            {
                throw new ArgumentException("Azure Blob Storage configuration is not set properly.");
            }
        }

        public async Task<string> UploadAsync(string file, string folder)
        {
            //var fileName = $"{folder}/{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var blobClient = _containerClient.GetBlobClient(file);
            //using var stream = file.OpenReadStream();
            //await blobClient.UploadAsync(stream, overwrite: true);

            return blobClient.Uri.ToString();
        }

        public async Task<bool> DeleteAsync(string file, string folder)
        {
            var fileName = $"{folder}/{Guid.NewGuid()}{Path.GetExtension(file)}";

            var blobClient = _containerClient.GetBlobClient(fileName);
            return await blobClient.DeleteIfExistsAsync();
        }
    }
}
