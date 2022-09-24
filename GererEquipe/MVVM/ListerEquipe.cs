using System;
using System.Collections.Generic;
using GererEquipe.Data.Dto;
using GererEquipe.Data.Services;
using GererEquipe.Pages;
using SteveMAUI.MVVM;

namespace GererEquipe.MVVM
{
    public class ListerEquipe : CsBaseContexte
    {
        private CsBaseCommande _listerLesEquipes = null;

        private List<EquipeDto> _listeEquipe = new List<EquipeDto>();
        public List<EquipeDto> listeEquipe
        {
            get { return _listeEquipe; }
        }

        public ListerEquipe()
        {
            ListerEquipeRoutine(null);
        }

        public CsBaseCommande LireUneEquipe
        {
            get
            {
                if (_listerLesEquipes == null)
                {
                    Action<object> action = new Action<object>(ListerEquipeRoutine);
                    _listerLesEquipes = new CsBaseCommande(action);
                }

                return _listerLesEquipes;
            }
        }

        private async void ListerEquipeRoutine(object objParametre)
        {
            var monClientHttp = new EquipeServices();

            var listeEquipe = await monClientHttp.ObtenirListeEquipeAsync();
            _listeEquipe = listeEquipe;
            NotifierChangement("listeEquipe");
        }
    }
}
