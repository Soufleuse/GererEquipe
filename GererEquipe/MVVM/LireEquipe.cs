using System;
using GererEquipe.Data.Dto;
using GererEquipe.Data.Services;
using SteveMAUI.MVVM;

namespace GererEquipe.MVVM
{
    public class LireEquipe : CsBaseContexte
    {
        private LireEquipe() { }

        public LireEquipe(int noEquipe)
        {
            var monClientHttp = new EquipeServices();

            var monEquipe = monClientHttp.ObtenirEquipeAsync(noEquipe);
            monEquipe.Wait();
            equipe = monEquipe.Result;
        }

        private CsBaseCommande _lireUneEquipe = null;

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

        public CsBaseCommande LireUneEquipe
        {
            get
            {
                if(_lireUneEquipe == null)
                {
                    Action<object> action = new Action<object>(LireEquipeRoutine);
                    _lireUneEquipe = new CsBaseCommande(action);
                }

                return _lireUneEquipe;
            }
        }

        private async void LireEquipeRoutine(object objParametre)
        {
            /*var monClientHttp = new EquipeServices();

            var monEquipe = await monClientHttp.ObtenirEquipeAsync(1);
            equipe = monEquipe;*/
        }
    }
}
