using GererEquipe.MVVM;

namespace GererEquipe.Pages;

public partial class Equipes : ContentPage
{
	public Equipes()
    {
        InitializeComponent();
    }

    public Equipes(int noEquipe) : this()
    {
        if(noEquipe > 0)
            BindingContext = new LireEquipe(noEquipe);
    }
}