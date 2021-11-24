using Application.IServices;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
  public class FileFooterService : IFileFooterService
  {
    private readonly DatabaseContext _context;

    public FileFooterService(DatabaseContext context)
    {
      _context = context;
    }

    public async Task<Guid?> SaveFileFooter(FileFooter fileFooter)
    {
      var newFileFooter = new FileFooter(fileFooter.FileName, fileFooter.ContentType, fileFooter.DataFiles);

      await _context.AddAsync(newFileFooter);
      await _context.SaveChangesAsync();
      return newFileFooter.FileFooterId;
    }

    public async Task DeleteFileFooter(Guid? fileFooterId)
    {
      if (await _context.FileFooters.AnyAsync(x => x.FileFooterId == fileFooterId))
      {
        var fileFooter = await _context.FileFooters.SingleAsync(x => x.FileFooterId == fileFooterId);

        _context.Remove(fileFooter);
        await _context.SaveChangesAsync();
      }
    }
  }
}
