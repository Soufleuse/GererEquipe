using System;
using GererEquipe.Data.Dto;
using GererEquipe.Data.Services;
using SteveMAUI.MVVM;

namespace GererEquipe.MVVM
{
    public class LireEquipe : CsBaseContexte
    {
        public LireEquipe() { }

        public async void LireUneEquipe(int noEquipe, IEnumerable<EquipeDto> listeEquipe)
        {
            var monClientHttp = new EquipeServices();
            equipe = await monClientHttp.ObtenirEquipeAsync(noEquipe);

            _listeEquipeEstDevenu = listeEquipe;
            NotifierChangement("listeEquipeEstDevenu");
        }

        private EquipeDto _equipe = default;
        public EquipeDto equipe
        {
            get { return _equipe; }
            private set
            {
                if(equipe != value)
                {
                    _equipe = value;
                    NotifierChangement("equipe");
                }
            }
        }

        private IEnumerable<EquipeDto> _listeEquipeEstDevenu = null;

        public IEnumerable<EquipeDto> listeEquipeEstDevenu
        {
            get { return _listeEquipeEstDevenu; }
        }

        private CsBaseCommande _SauvegarderEquipe = null;

        public CsBaseCommande SauvegarderEquipe
        {
            get
            {
                if(_SauvegarderEquipe == null)
                {
                    Action<object> action = new Action<object>(SauvegarderEquipeRoutine);
                    _SauvegarderEquipe = new CsBaseCommande(action);
                }

                return _SauvegarderEquipe;
            }
        }

        private async void SauvegarderEquipeRoutine(object objParametre)
        {
            var monClientHttp = new EquipeServices();

            var maStatuedeCire = await monClientHttp.CreerEquipeAsync(equipe);
        }
    }
}
