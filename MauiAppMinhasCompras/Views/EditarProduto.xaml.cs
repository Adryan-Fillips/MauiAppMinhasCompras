using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class EditarProduto : ContentPage
{
    public EditarProduto()
    {
        InitializeComponent();
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Obt�m o produto atualmente vinculado ao contexto da p�gina
            Produto produto_anexado = BindingContext as Produto;

            // Cria um novo objeto Produto com os valores atualizados
            Produto p = new Produto
            {
                Id = produto_anexado.Id,
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text)
            };

            // Atualiza o produto no banco de dados
            await App.Db.Update(p);

            // Exibe uma mensagem de sucesso ao usu�rio
            await DisplayAlert("Sucesso!", "Registro Atualizado", "OK");

            // Retorna para a p�gina anterior
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            // Captura e exibe qualquer erro ocorrido
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}