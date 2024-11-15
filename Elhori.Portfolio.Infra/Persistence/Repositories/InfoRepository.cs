using Elhori.Portfolio.Application.Contracts.Repositories;
using Elhori.Portfolio.Domain;
using Elhori.Portfolio.Domain.Dtos;
using Elhori.Portfolio.Domain.Entities;

namespace Elhori.Portfolio.Infra.Persistence.Repositories;

public class InfoRepository(AppDbContext context) : AsyncRepository<Info>(context), IInfoRepository
{

}