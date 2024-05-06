using Dapper;
using Domain.Core.Entities;
using Domain.Core.Repository;
using MySql.Data.MySqlClient;

namespace Infra.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly LankContext _context;

        public ClienteRepository(LankContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Cliente cliente)
        {
            using (var cn = new MySqlConnection(_context.GetConnectionString()))
            {
                try
                {
                    cn.Open();
                    cliente.Id = Guid.NewGuid().ToString();
                    var sql = "INSERT INTO Clientes (Id, Nome, CNPJ) VALUES (@Id, @Nome, @CNPJ)";

                    await cn.ExecuteAsync(sql,cliente);
                }
                catch (Exception ex)
                {

                    throw new ArgumentException(ex.Message);
                }
                finally
                {
                    cn.Close();
                }


            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            using (var cn = new MySqlConnection(_context.GetConnectionString()))
            {
                try
                {
                    cn.Open();

                    var sql = $"DELETE FROM Clientes WHERE Id = '{id}'";

                    return await cn.ExecuteAsync(sql) > 0;
                }
                catch (Exception ex)
                {

                    throw new ArgumentException(ex.Message);
                }
                finally
                {
                    cn.Close();
                }


            }
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            using (var cn = new MySqlConnection(_context.GetConnectionString()))
            {
                try
                {
                    cn.Open();

                    var sql = "SELECT * FROM Clientes";

                    return await cn.QueryAsync<Cliente>(sql);
                }
                catch (Exception ex)
                {

                    throw new ArgumentException($"{ex.Message} - _{_context.GetConnectionString()}");
                }
                finally
                {
                    cn.Close();
                }


            }
        }

        public async Task<Cliente> GetByCNPJAsync(string cnpj)
        {
            using (var cn = new MySqlConnection(_context.GetConnectionString()))
            {
                try
                {
                    cn.Open();

                    var sql = "SELECT * FROM Clientes WHERE CNPJ = @CNPJ";

                    var cliente = await cn.QueryAsync<Cliente>(sql,cnpj);

                    return cliente.FirstOrDefault();
                }
                catch (Exception ex)
                {

                    throw new ArgumentException(ex.Message);
                }
                finally
                {
                    cn.Close();
                }


            }
        }

        public async Task<Cliente> GetByIdAsync(string id)
        {
            using (var cn = new MySqlConnection(_context.GetConnectionString()))
            {
                try
                {
                    cn.Open();

                    var sql = $"SELECT * FROM Clientes WHERE Id ='{id}'";

                    var cliente = await cn.QueryAsync<Cliente>(sql);

                    return cliente.FirstOrDefault();
                }
                catch (Exception ex)
                {

                    throw new ArgumentException(ex.Message);
                }
                finally
                {
                    cn.Close();
                }


            }
        }

        public async Task<Cliente> UpdateAsync(Cliente cliente)
        {
            
            using (var cn = new MySqlConnection(_context.GetConnectionString()))
            {
                try
                {
                    cn.Open();

                    var sql = "UPDATE Clientes SET Nome = @Nome, CNPJ = @CNPJ WHERE Id = @Id";

                    await cn.ExecuteAsync(sql, cliente);

                    return cliente;
                }
                catch (Exception ex)
                {

                    throw new ArgumentException(ex.Message);
                }
                finally
                {
                    cn.Close();
                }
            }
        }
    }
}
