using System;
using GererEquipe.Data.Dto;
using GererEquipe.Data.Services;
using SteveMAUI.MVVM;

namespace GererEquipe.MVVM
{
    public class ListerEquipe : CsBaseContexte
    {
        private List<EquipeDto> _listeEquipe = new List<EquipeDto>();
        public List<EquipeDto> listeEquipe
        {
            get { return _listeEquipe; }
        }

        private Command _ListerEquipeAction = null;
        public Command ListerEquipeAction
        {
            get
            {
                if (_ListerEquipeAction == null)
                {
                    var monAction = new Action<object>(ListerEquipeRoutine);
                    _ListerEquipeAction = new Command(monAction);
                }
                return _ListerEquipeAction;
            }
        }

        public ListerEquipe()
        {
            ListerEquipeRoutine(null);
        }

        private async void ListerEquipeRoutine(object objParametre)
        {
            var monClientHttp = new EquipeServices();

            var listeEquipe = await monClientHttp.ObtenirListeEquipeAsync();

            foreach (var item in listeEquipe)
            {
                if (item.estDevenueEquipe.HasValue)
                {
                    item.nomEquipeVilleEstDevenueEquipe = await monClientHttp.ObtenirNomEquipeEstDevenu(item.estDevenueEquipe.Value);
                }
            }

            _listeEquipe = listeEquipe;
            NotifierChangement("listeEquipe");
        }
    }
}
