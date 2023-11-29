using GererEquipe.Data.Dto;
using GererEquipe.MVVM;

namespace GererEquipe.Pages;

public partial class Equipes : ContentPage
{
	public Equipes(IEnumerable<EquipeDto> listeEquipe)
    {
        InitializeComponent();
        BindingContext = new LireEquipe(ConfigGlobale.Instance.AnneeCourante, listeEquipe);
    }

    public Equipes(int noEquipe, IEnumerable<EquipeDto> listeEquipe) : this(listeEquipe)
    {
        if(noEquipe > 0)
        {
            ((LireEquipe)BindingContext).LireUneEquipe(noEquipe);
        }
    }
}