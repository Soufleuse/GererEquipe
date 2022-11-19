using GererEquipe.Data.Dto;
using GererEquipe.MVVM;

namespace GererEquipe.Pages;

public partial class Equipes : ContentPage
{
	public Equipes()
    {
        InitializeComponent();
    }

    public Equipes(int noEquipe, IEnumerable<EquipeDto> listeEquipe) : this()
    {
        if(noEquipe > 0)
        {
            BindingContext = new LireEquipe();
            ((LireEquipe)BindingContext).LireUneEquipe(noEquipe, listeEquipe);
            nomEquipePicker.WidthRequest = 200;
        }
    }
}