using Dapper;
using Microsoft.Data.SqlClient;

namespace Api.Clientes.Entities;

public class ClienteRepository (string connectionString)
{

    public void Inserir(Cliente cliente)
    {
        var sql = """
                INSERT INTO CLIENTES(ID, NOME, EMAIL, CPF, DATACADASTRO)
                VALUES (@Id, @Nome, @Email, @Cpf, @DataCadastro)
            """;

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Execute(sql, cliente);
        }
    }

    public void Atualizar(Cliente cliente)
    {
        var sql = """
                UPDATE CLIENTES
                SET
                    NOME = @Nome,
                    EMAIL = @Email,
                    CPF = @Cpf
                WHERE
                    ID = @Id
            """;

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Execute(sql, cliente);
        }
    }

    public void Excluir(Guid id)
    {
        var sql = """
                DELETE FROM CLIENTES
                WHERE ID = @Id
            """;

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Execute(sql, new { @Id = id });
        }
    }

    public List<Cliente> ObterTodos()
    {
        var sql = """
                SELECT * FROM CLIENTES
                ORDER BY NOME
            """;

        using (var connection = new SqlConnection(connectionString))
        {
            return connection.Query<Cliente>(sql).ToList();
        }
    }

    public Cliente? ObterPorId(Guid id)
    {
        var sql = """
                SELECT * FROM CLIENTES
                WHERE ID = @Id
            """;

        using (var connection = new SqlConnection(connectionString))
        {
            return connection.QuerySingleOrDefault<Cliente>(sql, new { @Id = id });
        }
    }

}
