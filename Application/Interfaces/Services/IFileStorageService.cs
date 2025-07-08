using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IFileStorageService
    {
        Task<string> UploadAsync(IFormFile file, string folder);
        Task<bool> DeleteAsync(IFormFile file, string folder);
    }
}
