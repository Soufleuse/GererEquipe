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
}