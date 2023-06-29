using GererEquipe.Data.Dto;
using GererEquipe.MVVM;

namespace GererEquipe.Pages;

public partial class Equipes : ContentPage
{
	public Equipes()
    {
        InitializeComponent();
        BindingContext = new LireEquipe();
    }

    public Equipes(int noEquipe, IEnumerable<EquipeDto> listeEquipe) : this()
    {
        if(noEquipe > 0)
        {
            ((LireEquipe)BindingContext).LireUneEquipe(noEquipe, listeEquipe);
            nomEquipePicker.WidthRequest = 200;
        }
    }
}