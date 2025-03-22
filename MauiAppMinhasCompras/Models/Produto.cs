using SQLite;

namespace MauiAppMinhasCompras.Models
{
    public class Produto
    {
        string _descricao;

        // Definição da chave primária com auto incremento
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // Propriedade com validação para evitar valores nulos
        public string Descricao
        {
            get => _descricao;
            set
            {
                if (value == null)
                {
                    throw new Exception("Por favor, preencha a descrição");
                }
                _descricao = value;
            }
        }

        // Quantidade do produto armazenada como número decimal
        public double Quantidade { get; set; }

        // Preço unitário do produto
        public double Preco { get; set; }

        // Propriedade calculada para obter o total (quantidade * preço)
        public double Total { get => Quantidade * Preco; }
    }
}