using GererEquipe.Data.Dto;
using GererEquipe.MVVM;

namespace GererEquipe.Pages;

public partial class UneStatsEquipe : ContentPage
{
	public UneStatsEquipe(StatsEquipeDto statsEquipeSelectionnee, IEnumerable<EquipeDto> pListeEquipe)
	{
		InitializeComponent();
		BindingContext = new LireStatsEquipe(statsEquipeSelectionnee, pListeEquipe);
	}
}