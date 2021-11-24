using Application.IServices;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
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

    public async Task<Guid?> SaveFileAttachment(FileAttachment fileAttachment)
    {
      var newFileAttachment = new FileAttachment(fileAttachment.FileName, fileAttachment.ContentType, fileAttachment.DataFiles);

      await _context.AddAsync(newFileAttachment);
      await _context.SaveChangesAsync();
      return newFileAttachment.FileAttachmentId;
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
