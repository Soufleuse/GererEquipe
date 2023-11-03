using GererEquipe.Data.Dto;
using GererEquipe.MVVM;

namespace GererEquipe.Pages;

public partial class UneStatsEquipe : ContentPage
{
	public UneStatsEquipe(StatsEquipeDto statsEquipeSelectionnee)
	{
		InitializeComponent();
		BindingContext = new LireStatsEquipe(statsEquipeSelectionnee);
	}

    private void pckEquipe_SelectedIndexChanged(object sender, EventArgs e)
    {
		var monContexte = (LireStatsEquipe)BindingContext;

		monContexte.estBtnSauvegarderEnabled = false;
		monContexte.statsEquipe.equipeId = 0;
        if (monContexte.equipeSelectionnee != null)
		{
			monContexte.statsEquipe.equipeId = monContexte.equipeSelectionnee.id;
			monContexte.estBtnSauvegarderEnabled = true;
        }
    }
}