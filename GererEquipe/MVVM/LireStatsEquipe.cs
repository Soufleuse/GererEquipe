using System;
using System.Net;
using GererEquipe.Data.Dto;
using GererEquipe.Data.Services;
using SteveMAUI.MVVM;

namespace GererEquipe.MVVM
{
    public class LireStatsEquipe : CsBaseContexte
    {
        private Page m_page;

        public LireStatsEquipe(Page pPage)
        {
            m_page = pPage;
        }

        public async void LireUneStatsEquipe(int idEquipe, short anneeStats)
        {
            var monClientHttp = new EquipeServices();
            _statsEquipe = await monClientHttp.ObtenirStatsEquipe(idEquipe, anneeStats);
        }

        private StatsEquipeDto _statsEquipe = null;
        public StatsEquipeDto statsEquipe
        {
            get { return _statsEquipe; }
            set
            {
                _statsEquipe = value;
                NotifierChangement("statsEquipe");
            }
        }

        private string _messageErreur = string.Empty;

        public string messageErreur
        {
            get { return _messageErreur; }
            set
            {
                if (string.Compare(messageErreur, value) != 0)
                {
                    _messageErreur = value;
                    NotifierChangement("messageErreur");
                }
            }
        }

        private CsBaseCommande _SauvegarderStatsEquipe = null;
        public CsBaseCommande SauvegarderStatsEquipe
        {
            get
            {
                if (_SauvegarderStatsEquipe == null)
                {
                    Action<object> action = new Action<object>(SauvegarderStatsEquipeRoutine);
                    _SauvegarderStatsEquipe = new CsBaseCommande(action);
                }

                return _SauvegarderStatsEquipe;
            }
        }

        private /*async*/ void SauvegarderStatsEquipeRoutine(object objParametre)
        {
            m_page.DisplayAlert("Info", "En construction", "Annuler");
            /*var monClientHttp = new EquipeServices();
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
            }*/
        }

        private CsBaseCommande _InitialiserNouvelleStatsEquipe = null;

        public CsBaseCommande InitialiserNouvelleStatsEquipe
        {
            get
            {
                if (_InitialiserNouvelleStatsEquipe == null)
                {
                    Action<object> action = new Action<object>(InitialiserNouvelleStatsEquipeRoutine);
                    _InitialiserNouvelleStatsEquipe = new CsBaseCommande(action);
                }
                return _InitialiserNouvelleStatsEquipe;
            }
        }

        private void InitialiserNouvelleStatsEquipeRoutine(object objParametre)
        {
            statsEquipe = new StatsEquipeDto();
        }
    }
}
