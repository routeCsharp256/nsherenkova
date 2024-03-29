﻿using System.Threading;
using System.Threading.Tasks;

namespace OzonEdu.MerchandiseService.Infrastukture.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}