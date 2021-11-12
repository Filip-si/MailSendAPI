using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Application.IServices
{
  public interface IFileService
  {
    Task<File> UploadFiles(IFormFile file, Guid templateId);
  }
}
