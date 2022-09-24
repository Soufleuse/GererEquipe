using GererEquipe.MVVM;

namespace GererEquipe.Pages;

public partial class Equipes : ContentPage
{
	public Equipes()
	{
		InitializeComponent();
        BindingContext = new LireEquipe();
    }
}