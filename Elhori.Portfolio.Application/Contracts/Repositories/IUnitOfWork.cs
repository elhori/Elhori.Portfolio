namespace Elhori.Portfolio.Application.Contracts.Repositories;

public interface IUnitOfWork : IDisposable
{
    ISkillRepository Skills { get; }
    IProjectRepository Projects { get; }
    IInfoRepository Information { get; }

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}