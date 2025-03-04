using MauiAppMinhasCompras.Models;
using SQLite;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn; // _conn para gerar conexão
        public SQLiteDatabaseHelper(string path) 
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait(); //Cria uma conexão com a tabela produto, o path é como um "Caminho"
        }

        public Task<int> Insert(Produto p) 
        {
            return _conn.InsertAsync(p); //Irá colocar dentro da tabela produto, Método CRUD = Create
        } 

        public Task<List<Produto>> Update(Produto p) 
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=?, WHERE Id=?";

            return _conn.QueryAsync<Produto>(
                sql, p.Descricao, p.Quantidade, p.Preco, p.Id //Atualiza as coisas listadas na tabela produto, Método CRUD = Update
            );
        }

        public Task<int> Delete(int id) 
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id); //Deleta as coisas que foram listadas na tabela produto, Método CRUD = Delete
        }

        public Task<List<Produto>> GetAll() 
        {
            return _conn.Table<Produto>().ToListAsync(); //Pega todos os produtos
        }

        public Task<List<Produto>> Search(string q) 
        {
            string sql = "SELECT * Produto WHERE Descricao LIKE '%"+ q + "%'";

            return _conn.QueryAsync<Produto>(sql); //Seleciona da tabela Produto, ou seja Método CRUD = Read
        }

    }
}
