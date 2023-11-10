using GererEquipe.Data.Dto;
using GererEquipe.Data.Services;
using GererEquipe.MVVM;

namespace GererEquipe.Pages;

public partial class Equipes : ContentPage
{
	public Equipes()
    {
        InitializeComponent();
        BindingContext = new LireEquipe(ConfigGlobale.Instance.AnneeCourante);
    }

    public Equipes(int noEquipe, IEnumerable<EquipeDto> listeEquipe) : this()
    {
        if(noEquipe > 0)
        {
            ((LireEquipe)BindingContext).LireUneEquipe(noEquipe, listeEquipe);
        }
    }
}