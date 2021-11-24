using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchandiseRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Exceptions;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.Infrastructure.Interfaces;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.Interfaces;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.ModelsDb;
using OzonEdu.MerchandiseService.Infrastukture.Contracts;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainServices.Implementation
{
    public class MerchandiseRequestRepository: IMerchandiseRequestRepository
    {
        public IUnitOfWork UnitOfWork { get; }
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int Timeout = 5;

        public MerchandiseRequestRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IChangeTracker changeTracker, IUnitOfWork unitOfWork)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
            UnitOfWork = unitOfWork;
        }

        public async Task<MerchandiseRequest> FindByEmployeeIdAsync(long id, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT employees.id, employees.first_name, employees.last_name, employees.email,
                       employees.phone_number, employees.clothing_size
                       merchandise_requests.id, merchandise_requests.status,merchandise_requests.employees_id,
                       merchandise_requests.manager_id, merchandise_requests.merchandise_items_id,
                       merchandise_items.id, merchandise_items.merch_pack_id, merchandise_items.items,
                       merch_packes.id, merch_packes.name
                FROM employees 
                INNER JOIN merchandise_requests on merchandise_requests.employee_id = employees.id 
                LEFT JOIN merchandise_items on merchandise_requests.merchandise_items_id = merchandise_items.id  
                INNER JOIN merch_packes on merchandise_items.merch_pack_id = merch_packes.id
                WHERE merchandise_requests.employee_id = @EmployeeId;";
            
            var parameters = new
            {
                EmployeeId = id,
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var merchandiseRequests = await connection.QueryAsync<
                EmployeeDb, MerchandiseRequestDb, MerchandiseItemDb, MerchPackDb, MerchandiseRequest>
            (commandDefinition, (employee, merchandiseRequest, merchandiseItem, merchPack) => new MerchandiseRequest(
                merchandiseRequest.Id,
                merchandiseRequest.Status,
                employee.Id,
                new PhoneNumber(employee.PhoneNumber),
                merchandiseRequest?.MenagerId,
                merchandiseItem?.Id is not null ? 
                    new MerchandiseItem(merchandiseItem.Id.Value, new MerchPack(new MerchType(merchPack.Id, merchPack.Name)),
                    merchandiseItem.Items.Select(x => new Sku(x)).ToList()) : null
            ));
            // Добавление после успешно выполненной операции.
            var merchandiseRequest = merchandiseRequests.First();
            _changeTracker.Track(merchandiseRequest);
            return merchandiseRequest;
        }

        public async Task<MerchandiseRequest> FindByEmployeeIdAndMerchPackAsync(long id, int merchPack, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT employees.id, employees.first_name, employees.last_name, employees.email,
                       employees.phone_number, employees.clothing_size
                       merchandise_requests.id, merchandise_requests.status,merchandise_requests.employees_id,
                       merchandise_requests.manager_id, merchandise_requests.merchandise_items_id,
                       merchandise_items.id, merchandise_items.merch_pack_id, merchandise_items.items,
                       merch_packes.id, merch_packes.name
                FROM employees 
                INNER JOIN merchandise_requests on merchandise_requests.employee_id = employees.id 
                LEFT JOIN merchandise_items on merchandise_requests.merchandise_items_id = merchandise_items.id  
                INNER JOIN merch_packes on merchandise_items.merch_pack_id = merch_packes.id
                WHERE merchandise_requests.employee_id = @EmployeeId AND merch_packes.id =@MerchPackId;";
            
            var parameters = new
            {
                EmployeeId = id,
                MerchPackId = merchPack
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var merchandiseRequests = await connection.QueryAsync<
                EmployeeDb, MerchandiseRequestDb, MerchandiseItemDb, MerchPackDb, MerchandiseRequest>
            (commandDefinition, (employee, merchandiseRequest, merchandiseItem, merchPack) => new MerchandiseRequest(
                merchandiseRequest.Id,
                merchandiseRequest.Status,
                employee.Id,
                new PhoneNumber(employee.PhoneNumber),
                merchandiseRequest?.MenagerId,
                merchandiseItem?.Id is not null ? 
                    new MerchandiseItem(merchandiseItem.Id.Value, new MerchPack(new MerchType(merchPack.Id, merchPack.Name)),
                    merchandiseItem.Items.Select(x => new Sku(x)).ToList()) : null
            ));
            // Добавление после успешно выполненной операции.
            var merchandiseRequest = merchandiseRequests.First();
            _changeTracker.Track(merchandiseRequest);
            return merchandiseRequest;
        }

        
        public async Task<MerchandiseRequest> CreateAsync(MerchandiseRequest itemToCreate, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                INSERT INTO merchandise_items (id, merch_pack_id)
                VALUES (@MerchandiseItemId, @MerchPackId);
                INSERT INTO merchandise_requests (status, employee_id, merchandise_item_id)
                VALUES (@Status, @EmployeeId, @MerchandiseItemId);                
                ";

            var parameters = new
            {
                MerchandiseItemId = itemToCreate.MerchandiseItem.Id,
                MerchPackId = itemToCreate.MerchandiseItem.MerchPack.Id,
                Status = itemToCreate.Status,
                EmployeeId = itemToCreate.EmployeeId
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            await connection.ExecuteAsync(commandDefinition);
            _changeTracker.Track(itemToCreate);
            return itemToCreate;
        }

        public async Task<MerchandiseRequest> UpdateAsync(MerchandiseRequest itemToUpdate, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                UPDATE merchandise_items
                SET merch_pack_id =@MerchPackId, items = @Items
                WHERE id = @MerchandiseItemId;
                UPDATE merchandise_requests
                SET status = @Status, employee_id = @EmployeeId, 
                    merchandise_item_id = @MerchandiseItemId, manager_id = @ManagerId
                WHERE id = @Id;              
                ";

            var parameters = new
            {
                MerchandiseItemId = itemToUpdate.MerchandiseItem.Id,
                MerchPackId = itemToUpdate.MerchandiseItem.MerchPack.Id,
                Items = itemToUpdate.MerchandiseItem.Items.Select(x=>x.Value).ToArray(),
                Status = itemToUpdate.Status,
                EmployeeId = itemToUpdate.EmployeeId,
                Id = itemToUpdate.Id,
                ManagerId = itemToUpdate.ResponsibleManagerId
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            await connection.ExecuteAsync(commandDefinition);
            _changeTracker.Track(itemToUpdate);
            return itemToUpdate;
        }
    }
}