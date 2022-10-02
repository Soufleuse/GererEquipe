using GererEquipe.Data.Dto;
using GererEquipe.MVVM;
using GererEquipe.Pages;

namespace GererEquipe;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new ListerEquipe();
    }

    private async void listeEquipe_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        //e.SelectedItem
        await Navigation.PushAsync(new Equipes(((EquipeDto)e.SelectedItem).id));
    }
}
