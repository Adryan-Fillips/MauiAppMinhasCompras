using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();
    ObservableCollection<Produto> listaFiltrada = new ObservableCollection<Produto>();

    public ListaProduto()
    {
        InitializeComponent();
        lst_produtos.ItemsSource = listaFiltrada;
    }

    protected async override void OnAppearing()
    {
        List<Produto> tmp = await App.Db.GetAll();
        tmp.ForEach(i => lista.Add(i));
        AtualizarListaFiltrada();
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
        string q = e.NewTextValue;
        AtualizarListaFiltrada(q);
    }

    private void AtualizarListaFiltrada(string filtro = "")
    {
        listaFiltrada.Clear();
        var resultado = string.IsNullOrWhiteSpace(filtro)
            ? lista
            : new ObservableCollection<Produto>(lista.Where(p => p.Descricao.ToLower().Contains(filtro.ToLower())));
        resultado.ToList().ForEach(i => listaFiltrada.Add(i));
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);
        string msg = $"O total é {soma:C}";
        DisplayAlert("Total dos Produtos", msg, "OK");
    }

    private void MenuItem_Clicked(object sender, EventArgs e)
    {
        if (sender is MenuItem menuItem && menuItem.BindingContext is Produto produto)
        {
            lista.Remove(produto);
            listaFiltrada.Remove(produto);
        }
    }
}
