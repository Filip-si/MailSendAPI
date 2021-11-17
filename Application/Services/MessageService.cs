﻿using Application.IServices;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
  public class MessageService : IMessageService
  {
    private readonly DatabaseContext _context;
    private readonly IConfiguration _configuration;

    public MessageService(DatabaseContext context, IConfiguration configuration)
    {
      _context = context;
      _configuration = configuration;
    }

    public async Task<IEnumerable<Message>> GetMessages()
    {
      return await _context.Messages.AsNoTracking()
        .ToListAsync();
    }

    public async Task<List<OutboxMessage>> SaveMessagesAsync(IEnumerable<string> recepients, Guid templateId)
    {
      await using var transaction = await _context.Database.BeginTransactionAsync();
      try
      {
        List<Message> messages = new();
        foreach (var recepient in recepients)
        {
          Message message = new(_configuration["EmailConfigurations:From"], recepient, templateId, DateTime.Now);
          messages.Add(message);
        }

        await _context.AddRangeAsync(messages);
        await _context.SaveChangesAsync();

        List<OutboxMessage> outboxMessages = new();
        foreach (var message in messages)
        {
          OutboxMessage outboxMessage = new(0);
          outboxMessage.MessageId = message.MessageId;
          outboxMessages.Add(outboxMessage);
        }

        await _context.AddRangeAsync(outboxMessages);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return outboxMessages;
      }
      catch (Exception)
      {
        await transaction.RollbackAsync();
        throw;
      }
      
    }

    public async Task AddMessage(string recepients, Guid templateId)
    {
      Message message = new(_configuration["EmailConfigurations:From"], recepients, templateId, DateTime.Now);

      await _context.AddRangeAsync(message);
      await _context.SaveChangesAsync();
    }

    public async Task DeleteMessagesBeforeDate(DateTime dateTime)
    {
      await using var transaction = await _context.Database.BeginTransactionAsync();
      try
      {
        await _context.Messages.AsNoTracking()
          .IsAnyRuleAsync(x => x.CreatedOn <= dateTime);

        var messages = await _context.Messages.Where(x => x.CreatedOn <= dateTime).ToListAsync();

        var messageIds = messages.Select(x => x.MessageId);

        _context.RemoveRange(messages);
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
