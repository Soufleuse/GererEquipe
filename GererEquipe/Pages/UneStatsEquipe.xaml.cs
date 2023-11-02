using GererEquipe.MVVM;

namespace GererEquipe.Pages;

public partial class UneStatsEquipe : ContentPage
{
	public UneStatsEquipe()
	{
		InitializeComponent();
		BindingContext = new ListerStatsEquipe();
	}
}