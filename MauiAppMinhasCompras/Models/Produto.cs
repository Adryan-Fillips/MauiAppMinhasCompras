using SQLite;

namespace MauiAppMinhasCompras.Models
{
    public class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Quantidade { get; set; }
        public double Preco { get; set; }
        //Declara a classe produto, e dentro dela declara suas variáveis, como o Id como número, Descrição em String, Quantidade e preço em números também
    }
}
