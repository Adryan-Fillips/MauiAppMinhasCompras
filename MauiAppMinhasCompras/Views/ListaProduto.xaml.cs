using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    public ListaProduto()
    {
        InitializeComponent();

        lst_produtos.ItemsSource = lista;
    }

    protected async override void OnAppearing()
    {
        try
        {
            // Limpa a lista antes de recarregar os dados do banco
            lista.Clear();

            // Busca todos os produtos cadastrados no banco
            List<Produto> tmp = await App.Db.GetAll();

            // Adiciona os produtos na ObservableCollection para exibição na tela
            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            // Exibe erro caso algo dê errado no carregamento
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());

        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            // Captura o texto digitado no campo de busca
            string q = e.NewTextValue;

            // Limpa a lista atual
            lista.Clear();

            // Filtra os produtos no banco de dados com base na pesquisa
            List<Produto> tmp = await App.Db.Search(q);

            // Adiciona os produtos filtrados à lista exibida
            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            // Exibe uma mensagem de erro caso ocorra algum problema
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        // Calcula a soma total de todos os produtos listados
        double soma = lista.Sum(i => i.Total);

        // Exibe o total calculado em um alerta
        string msg = $"O total é {soma:C}";

        DisplayAlert("Total dos Produtos", msg, "OK");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Obtém o item selecionado no menu de contexto
            MenuItem selecionado = sender as MenuItem;

            // Converte o contexto para um objeto Produto
            Produto p = selecionado.BindingContext as Produto;

            // Exibe um alerta de confirmação antes da remoção
            bool confirm = await DisplayAlert(
                "Tem Certeza?", $"Remover {p.Descricao}?", "Sim", "Não");

            if (confirm)
            {
                // Remove o produto do banco de dados
                await App.Db.Delete(p.Id);

                // Remove o produto da lista exibida na tela
                lista.Remove(p);
            }
        }
        catch (Exception ex)
        {
            // Exibe uma mensagem de erro caso algo dê errado
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }


    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Produto p = e.SelectedItem as Produto;

            Navigation.PushAsync(new Views.EditarProduto
            {
                BindingContext = p,
            });
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}