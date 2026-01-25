using AfishaVoenmeh.AuthService.Application.Common.Interfaces.Persistence;
using AfishaVoenmeh.AuthService.Domain.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfishaVoenmeh.AuthService.Infrastructure.Data.Repositories;

public class RefreshSessionRepository : IRefreshSessionRepository
{
    private readonly ApplicationDbContext _dbContext;

    public RefreshSessionRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(RefreshSession refreshSession, CancellationToken ct = default)
    {
        await _dbContext.RefreshSessions.AddAsync(refreshSession, ct);

        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<RefreshSession?> GetByTokenAsync(string token, CancellationToken ct = default)
    {
        return await _dbContext.RefreshSessions
            .FirstOrDefaultAsync(rs => rs.Token == token, ct);
    }

    public async Task RemoveAsync(RefreshSession refreshSession, CancellationToken ct = default)
    {
        _dbContext.RefreshSessions.Remove(refreshSession);

        await _dbContext.SaveChangesAsync(ct);
    }
}
