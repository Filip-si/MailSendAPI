using Application.IServices;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
  public class FileService : IFileService
  {
    private readonly DatabaseContext _context;
    private readonly IFileHeaderService _fileHeaderService;
    private readonly IFileBodyService _fileBodyService;
    private readonly IFileFooterService _fileFooterService;
    private readonly IFileAttachmentService _fileAttachmentService;

    public FileService(
      DatabaseContext context, 
      IFileHeaderService fileHeaderService, 
      IFileBodyService fileBodyService, 
      IFileFooterService fileFooterService,
      IFileAttachmentService fileAttachmentService)
    {
      _context = context;
      _fileHeaderService = fileHeaderService;
      _fileBodyService = fileBodyService;
      _fileFooterService = fileFooterService;
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
          FileHeaderId = files.FileHeaderId,
          FileBodyId = files.FileBodyId,
          FileFooterId = files.FileFooterId
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
      try
      {
        await _context.Files.AsNoTracking()
          .IsAnyRuleAsync(x => x.FilesId == filesId);

        var files = await _context.Files.AsNoTracking()
          .SingleAsync(x => x.FilesId == filesId);

        var filesAttachments = await _context.FileAttachments
          .Where(x => x.FilesId == files.FilesId)
          .ToListAsync();

        foreach (var attachment in filesAttachments)
        {
          await _fileAttachmentService.DeleteFileAttachment(attachment.FileAttachmentId);
        }

        _context.Remove(files);
        await _fileHeaderService.DeleteFileHeader(files.FileHeaderId);
        await _fileBodyService.DeleteFileBody(files.FileBodyId);
        await _fileFooterService.DeleteFileFooter(files.FileFooterId);

        await _context.SaveChangesAsync();
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}
