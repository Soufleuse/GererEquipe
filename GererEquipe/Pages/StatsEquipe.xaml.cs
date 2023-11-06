using GererEquipe.Data.Dto;
using GererEquipe.MVVM;

namespace GererEquipe.Pages;

public partial class StatsEquipe : ContentPage
{
	public StatsEquipe()
	{
		InitializeComponent();
		BindingContext = new ListerStatsEquipe();
	}

    private async void listeStatsEquipe_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        await Navigation.PushAsync(new UneStatsEquipe((StatsEquipeDto)e.SelectedItem, ((ListerStatsEquipe)BindingContext).listeEquipe));
    }

    private async void btnNouvelleStatsEquipe_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreerStatsEquipe(((ListerStatsEquipe)BindingContext).listeEquipe));
    }
}