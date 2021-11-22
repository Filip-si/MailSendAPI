using Application.IServices;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
  public class FileHeaderService : IFileHeaderService
  {
    private readonly DatabaseContext _context;

    public FileHeaderService(DatabaseContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<FileHeader>> GetFileHeaders()
    {
      return await _context.FileHeaders.AsNoTracking().ToListAsync();
    }

    public async Task<Guid?> SaveFileHeader(FileHeader fileHeader)
    {
      try
      {
        var newFileHeader = new FileHeader(fileHeader.FileName, fileHeader.ContentType, fileHeader.DataFiles);

        await _context.AddAsync(newFileHeader);
        await _context.SaveChangesAsync();
        return newFileHeader.FileHeaderId;
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task DeleteFileHeader(Guid? fileHeaderId)
    {
      if(await _context.FileHeaders.AnyAsync(x => x.FileHeaderId == fileHeaderId))
      {
        var fileHeader = await _context.FileHeaders.SingleAsync(x => x.FileHeaderId == fileHeaderId);

        _context.Remove(fileHeader);
        await _context.SaveChangesAsync();
      }
    }
  }
}
