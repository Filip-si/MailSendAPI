using Application.IServices;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
  public class FileBodyService : IFileBodyService
  {
    private readonly DatabaseContext _context;

    public FileBodyService(DatabaseContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<FileBody>> GetFileBodies()
    {
      return await _context.FileBodies.AsNoTracking().ToListAsync();
    }

    public async Task<Guid?> SaveFileBody(FileBody fileBody)
    {
      try
      {
        var newFileBody = new FileBody(fileBody.FileName, fileBody.ContentType, fileBody.DataFiles);

        await _context.AddAsync(newFileBody);
        await _context.SaveChangesAsync();
        return newFileBody.FileBodyId;
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task DeleteFileBody(Guid? fileBodyId)
    {
      if (await _context.FileBodies.AnyAsync(x => x.FileBodyId == fileBodyId))
      {
        var fileBody = await _context.FileBodies.SingleAsync(x => x.FileBodyId == fileBodyId);

        _context.Remove(fileBody);
        await _context.SaveChangesAsync();
      }
    }
  }
}
