using System;
using System.Net;
using System.Web;
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

        private string _messageErreur = string.Empty;

        public string messageErreur
        {
            get { return _messageErreur; }
            set
            {
                if(string.Compare(messageErreur, value) != 0)
                {
                    _messageErreur = value;
                    NotifierChangement("messageErreur");
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

            var maStatuedeCire = await monClientHttp.SauvegarderEquipeAsync(equipe);
            switch (maStatuedeCire)
            {
                case HttpStatusCode.Created:
                case HttpStatusCode.OK:
                case HttpStatusCode.NoContent:
                    messageErreur = "Réussite de la commande";
                    break;
                default:
                    messageErreur = string.Format("Une erreur est survenue; no de l'erreur : {0}.", (int)maStatuedeCire);
                    break;
            }
        }

        private CsBaseCommande _InitialiserNouvelleEquipe = null;

        public CsBaseCommande InitialiserNouvelleEquipe
        {
            get
            {
                if(_InitialiserNouvelleEquipe == null)
                {
                    Action<object> action = new Action<object>(InitialiserNouvelleEquipeRoutine);
                    _InitialiserNouvelleEquipe = new CsBaseCommande(action);
                }
                return _InitialiserNouvelleEquipe;
            }
        }

        private void InitialiserNouvelleEquipeRoutine(object objParametre)
        {
            equipe = new EquipeDto();
        }
    }
}
