using System;
using System.Net;
using GererEquipe.Data.Dto;
using GererEquipe.Data.Services;
using SteveMAUI.MVVM;

namespace GererEquipe.MVVM
{
    public class LireStatsEquipe : CsBaseContexte
    {
        public LireStatsEquipe(StatsEquipeDto pStatsEquipe, IEnumerable<EquipeDto> pListeEquipe)
        {
            _listeEquipe = pListeEquipe;

            StatsEquipeDto statsEquipeLocale = pStatsEquipe;
            if (pStatsEquipe == null)
            {
                statsEquipeLocale = new StatsEquipeDto();
            }
            this.statsEquipe = statsEquipeLocale;

            // Voir le summary de la fonction
            AllerChercherAnneeCourante();
            statsEquipe.anneeStats = ConfigGlobale.Instance.AnneeCourante;
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

        private bool _AfficherBoutonNouvelleStats = true;
        public bool AfficherBoutonNouvelleStats
        {
            get { return _AfficherBoutonNouvelleStats; }
            set
            {
                _AfficherBoutonNouvelleStats = value;
                NotifierChangement("AfficherBoutonNouvelleStats");
            }
        }

        private IEnumerable<EquipeDto> _listeEquipe = null;
        public IEnumerable<EquipeDto> listeEquipe { get { return _listeEquipe; } }

        private EquipeDto _equipeSelectionnee = null;
        public EquipeDto equipeSelectionnee
        {
            get { return _equipeSelectionnee; }
            set
            {
                _equipeSelectionnee = value;
                NotifierChangement("equipeSelectionnee");
            }
        }

        private short _anneeStats = short.MinValue;
        public short anneeStats
        {
            get { return _anneeStats; }
            set
            {
                if(anneeStats != value)
                {
                    _anneeStats = value;
                    NotifierChangement("anneeStats");
                }
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

        private bool _estBtnSauvegarderEnabled = true;
        public bool estBtnSauvegarderEnabled
        {
            get { return _estBtnSauvegarderEnabled; }
            set
            {
                if (estBtnSauvegarderEnabled != value)
                {
                    _estBtnSauvegarderEnabled = value;
                    NotifierChangement("estBtnSauvegarderEnabled");
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

        private async void SauvegarderStatsEquipeRoutine(object objParametre)
        {
            var monClientHttp = new EquipeServices();
            var maStatuedeCire = await monClientHttp.SauvegarderStatsEquipeAsync(statsEquipe);
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

        /// <summary>
        /// Juste pour faire sûr qu'on a été chercher l'année courante
        /// </summary>
        private async void AllerChercherAnneeCourante()
        {
            if (ConfigGlobale.Instance.AnneeCourante == short.MinValue)
            {
                var monParamHttp = new ParametresServices();
                var monAnneeHttp = await monParamHttp.ObtenirParametreAsync("anneeCourante", DateTime.Now);

                ConfigGlobale.Instance.AnneeCourante = Convert.ToInt16(monAnneeHttp.First().valeur);
            }

            anneeStats = ConfigGlobale.Instance.AnneeCourante;
        }
    }
}
