using Dapper;
using Domain.Core.Entities;
using Domain.Core.Repository;
using MySql.Data.MySqlClient;

namespace Infra.Data.Repository
{
    public class NotaRepository : INotaRepository
    {
        private readonly LankContext _context;

        public NotaRepository(LankContext context)
        {
            _context = context;
        }
        public async Task<int> CreateAsync(Notas nota)
        {
            using (var cn = new MySqlConnection(_context.GetConnectionString()))
            {
                try
                {
                    cn.Open();
                    nota.Id = Guid.NewGuid().ToString();
                    var sql = "INSERT INTO Notas (Id,  Valor,  ClienteId) " +
                        "VALUES (@Id,  @Valor,  @ClienteId)";

                    return await cn.ExecuteAsync(sql, nota);
                }
                catch (Exception)
                {

                    throw new ArgumentException("Erro ao inserir nota");
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

                    var sql = "DELETE FROM Notas WHERE Id = @Id";

                    return await cn.ExecuteAsync(sql, id) > 0;
                }
                catch (Exception)
                {

                    throw new ArgumentException("Erro ao deletar nota");
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        public async Task<IEnumerable<Notas>> GetAllAsync()
        {
            
            using (var cn = new MySqlConnection(_context.GetConnectionString()))
            {
                try
                {
                    cn.Open();

                    var sql = "SELECT * FROM Notas";

                    return await cn.QueryAsync<Notas>(sql);
                }
                catch (Exception)
                {

                    throw new ArgumentException("Erro ao buscar notas");
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        public async Task<Notas> GetByIdAsync(string id)
        {
            
            using (var cn = new MySqlConnection(_context.GetConnectionString()))
            {
                try
                {
                    cn.Open();

                    var sql = "SELECT * FROM Notas WHERE Id = @Id";

                    return await cn.QueryFirstOrDefaultAsync<Notas>(sql, new { Id = id });
                }
                catch (Exception)
                {

                    throw new ArgumentException("Erro ao buscar nota");
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        public async Task<Notas> UpdateAsync(Notas nota)
        {
            
            using (var cn = new MySqlConnection(_context.GetConnectionString()))
            {
                try
                {
                    cn.Open();

                    var sql = "UPDATE Notas SET Numero = @Numero, Serie = @Serie, DataEmissao = @DataEmissao, DataEntrada = @DataEntrada, " +
                        "Valor = @Valor, ChaveAcesso = @ChaveAcesso, ClienteId = @ClienteId WHERE Id = @Id";

                    await cn.ExecuteAsync(sql, nota);

                    return nota;
                }
                catch (Exception)
                {

                    throw new ArgumentException("Erro ao atualizar nota");
                }
                finally
                {
                    cn.Close();
                }
            }
        }
    }
}
