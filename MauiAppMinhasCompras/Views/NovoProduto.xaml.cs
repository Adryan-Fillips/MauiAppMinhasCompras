using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
    public NovoProduto()
    {
        InitializeComponent();
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Cria um novo produto com os dados informados pelo usuário
            Produto p = new Produto
            {
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text)
            };

            // Insere o produto no banco de dados
            await App.Db.Insert(p);

            // Exibe uma mensagem de sucesso
            await DisplayAlert("Sucesso!", "Produto cadastrado com sucesso", "OK");

            // Retorna para a tela anterior
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            // Exibe erro caso algo dê errado na inserção do produto
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}