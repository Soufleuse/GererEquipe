using GererEquipe.Data.Dto;
using GererEquipe.MVVM;

namespace GererEquipe.Pages;

public partial class CreerStatsEquipe : ContentPage
{
	public CreerStatsEquipe(IEnumerable<EquipeDto> pListeEquipe)
	{
		InitializeComponent();
        BindingContext = new LireStatsEquipe(null, pListeEquipe);
    }

    private void pckEquipe_SelectedIndexChanged(object sender, EventArgs e)
    {
        var monContexte = (LireStatsEquipe)BindingContext;

        monContexte.estEquipeSelectionnee = false;
        monContexte.statsEquipe.equipeId = 0;
        if (monContexte.equipeSelectionnee != null)
        {
            monContexte.statsEquipe.equipeId = monContexte.equipeSelectionnee.id;
            monContexte.estEquipeSelectionnee = true;
        }
    }
}