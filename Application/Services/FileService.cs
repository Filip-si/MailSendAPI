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
  public class FileService : IFileService
  {
    private readonly DatabaseContext _context;
    private readonly IFileHeaderService _fileHeaderService;
    private readonly IFileAttachmentService _fileAttachmentService;

    public FileService(DatabaseContext context, IFileHeaderService fileHeaderService, IFileAttachmentService fileAttachmentService)
    {
      _context = context;
      _fileHeaderService = fileHeaderService;
      _fileAttachmentService = fileAttachmentService;
    }

    public async Task<IEnumerable<Files>> GetFiles()
    {
      return await _context.Files.AsNoTracking().ToListAsync();
    }

    public async Task<Guid?> SaveFilesAsync(Files files)
    {
      try
      {
        var newFiles = new Files()
        {
          FileHeaderId = files.FileHeaderId
        };

        await _context.AddAsync(newFiles);
        await _context.SaveChangesAsync();

        return newFiles.FilesId;
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task DeleteFiles(Guid? filesId)
    {
      await using var transaction = await _context.Database.BeginTransactionAsync();
      try
      {
        await _context.Files.AsNoTracking()
          .IsAnyRuleAsync(x => x.FilesId == filesId);

        var files = await _context.Files.AsNoTracking().SingleAsync(x => x.FilesId == filesId);

        await _fileHeaderService.DeleteFileHeader(files.FileHeaderId);

        foreach (var attachment in files.FilesAttachments)
        {
          await _fileAttachmentService.DeleteFileAttachment(attachment.FileAttachmentId);
        }

        _context.Remove(files);
        await _context.SaveChangesAsync();

        await transaction.CommitAsync();
      }
      catch (Exception)
      {
        await transaction.RollbackAsync();
        throw;
      }
    }
  }
}
