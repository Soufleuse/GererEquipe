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
        var monContexte = (ListerEquipe)BindingContext;
        var idEquipe = ((EquipeDto)e.SelectedItem).id;
        List<EquipeDto> listeEquipeFiltree = (from item in monContexte.listeEquipe where item.id != idEquipe || item.anneeFin != null select item).ToList();
        listeEquipeFiltree.Insert(0, new EquipeDto());
        await Navigation.PushAsync(new Equipes(idEquipe, listeEquipeFiltree));
    }

    private async void btnNouvelleEquipe_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Equipes());
    }
}
