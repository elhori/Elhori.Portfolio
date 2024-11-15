using Elhori.Portfolio.Application.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elhori.Portfolio.Infra.Persistence.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private ISkillRepository _skills = null!;

    public ISkillRepository Skills
    {
        get
        {
            return _skills ??= new SkillRepository(context);
        }
    }

    private IProjectRepository _projects = null!;

    public IProjectRepository Projects
    {
        get
        {
            return _projects ??= new ProjectRepository(context);
        }
    }

    private IInfoRepository _information = null!;

    public IInfoRepository Information
    {
        get
        {
            return _information ??= new InfoRepository(context);
        }
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        context.Dispose();
    }
}