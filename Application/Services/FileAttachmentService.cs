using Application.IServices;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
  public class FileAttachmentService : IFileAttachmentService
  {
    private readonly DatabaseContext _context;

    public FileAttachmentService(DatabaseContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<FileAttachment>> GetFileAttachments()
    {
      return await _context.FileAttachments.AsNoTracking().ToListAsync();
    }

    public async Task<Guid?> SaveFileAttachment(FileAttachment fileAttachment)
    {
      try
      {
        var newFileAttachment = new FileAttachment(fileAttachment.FileName, fileAttachment.ContentType, fileAttachment.DataFiles);

        await _context.AddAsync(newFileAttachment);
        await _context.SaveChangesAsync();
        return newFileAttachment.FileAttachmentId;
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task DeleteFileAttachment(Guid? fileAttachmentId)
    {
      await _context.FileAttachments.AsNoTracking()
        .IsAnyRuleAsync(x => x.FileAttachmentId == fileAttachmentId);

      var fileAttachment = await _context.FileAttachments.SingleAsync(x => x.FileAttachmentId == fileAttachmentId);

      _context.Remove(fileAttachment);
      await _context.SaveChangesAsync();
    }
  }
}
