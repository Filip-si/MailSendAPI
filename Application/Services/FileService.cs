using Application.IServices;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Application.Services
{
  public class FileService : IFileService
  {
    private readonly DatabaseContext _context;

    public FileService(DatabaseContext context)
    {
      _context = context;
    }

    public async Task<Domain.Entities.File> UploadFiles(IFormFile file, Guid templateId)
    {
      int index = file.FileName.LastIndexOf("\\");
      var shortName = file.FileName.Substring(index + 1);

      var newFile = new Domain.Entities.File
      {
        ContentType = file.ContentType,
        FileName = shortName,
        MailMessageTemplateId = templateId
      };

      using (var target = new MemoryStream())
      {
        file.CopyTo(target);
        newFile.DataFiles = target.ToArray();
      }
      _context.Files.Add(newFile);

      await _context.SaveChangesAsync();
      return newFile;
    }


  }
}
