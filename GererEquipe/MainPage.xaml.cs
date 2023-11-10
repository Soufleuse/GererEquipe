using GererEquipe.Data.Dto;
using GererEquipe.Data.Services;
using GererEquipe.MVVM;
using GererEquipe.Pages;

namespace GererEquipe;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        AllerChercherAnneeCourante();
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

    private async void btnListerStatsEquipe_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new StatsEquipe());
    }

    internal void AllerChercherAnneeCourante()
    {
        if (ConfigGlobale.Instance.AnneeCourante == short.MinValue)
        {
            var maThread = new Thread(async () => {
                var monParamHttp = new ParametresServices();
                var monAnneeHttp = await monParamHttp.ObtenirParametreAsync("anneeCourante", DateTime.Now);

                ConfigGlobale.Instance.AnneeCourante = Convert.ToInt16(monAnneeHttp.First().valeur);
            });

            maThread.Start();
            while (maThread.ThreadState == ThreadState.Running)
            {
                Thread.Sleep(500);
            }
        }
    }
}
