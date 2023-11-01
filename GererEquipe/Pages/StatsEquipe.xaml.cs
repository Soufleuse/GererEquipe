using GererEquipe.MVVM;

namespace GererEquipe.Pages;

public partial class StatsEquipe : ContentPage
{
	public StatsEquipe()
	{
		InitializeComponent();
		BindingContext = new ListerStatsEquipe();
	}
}