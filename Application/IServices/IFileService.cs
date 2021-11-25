using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IServices
{
  public interface IFileService
  {
    Task<Guid?> SaveFilesAsync(File files);

    Task DeleteFiles(Guid? filesId);
  }
}
