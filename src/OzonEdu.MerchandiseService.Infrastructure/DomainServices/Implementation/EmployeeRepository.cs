using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchandiseRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.Infrastructure.Interfaces;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.Interfaces;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.ModelsDb;
using OzonEdu.MerchandiseService.Infrastukture.Contracts;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainServices.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public IUnitOfWork UnitOfWork { get; }
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int Timeout = 5;

        public EmployeeRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
            IChangeTracker changeTracker, IUnitOfWork unitOfWork)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
            UnitOfWork = unitOfWork;
        }

        public async Task<Employee> CreateAsync(Employee itemToCreate, CancellationToken cancellationToken)
        {
            const string sql = @"
                INSERT INTO employee (first_name, last_name, email, phone_number, clothing_size)
                VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @ClothingSize);                               
                ";

            var parameters = new
            {
                FirstName = itemToCreate.Name.FirstName,
                LastName =itemToCreate.Name.LastName,
                Email = itemToCreate.Email,
                PhoneNumber = itemToCreate.PhoneNumber,
                ClothingSize = itemToCreate.Size.Id
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

        public Task<Employee> UpdateAsync(Employee itemToUpdate, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Employee> FindByIdAsync(long id, CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT employees.id, first_name, last_name, email, phone_number, clothing_size,
                       clothing_sizes.id, clothing_sizes.name
                FROM employees 
                INNER JOIN clothing_sizes ON clothing_size = clothing_sizes.id
                WHERE id = @EmployeeId;";
            
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
            var findEmployees = await connection.QueryAsync<EmployeeDb, ClothingSize, Employee>
                (commandDefinition, ((employeeDb, size) => Employee.Create(
                    new PersonName(employeeDb.FirstName, employeeDb.LastName),
                    new Email(employeeDb.Email),
                    new PhoneNumber(employeeDb.Phone),
                    size)));
            // Добавление после успешно выполненной операции.
            var findEmployee = findEmployees.First();
           
            _changeTracker.Track(findEmployee);
            return findEmployee;
        }
    }
}