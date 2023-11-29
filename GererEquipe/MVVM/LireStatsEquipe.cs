using System;
using System.Net;
using GererEquipe.Data.Dto;
using GererEquipe.Data.Services;
using SteveMAUI.MVVM;
using System.ComponentModel;

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

            statsEquipe.anneeStats = ConfigGlobale.Instance.AnneeCourante;
            this.nbPartiesJoueesMax = ConfigGlobale.Instance.nbPartiesJoueesMax;
            SauvegarderStatsEquipe.ChangeCanExecute();
            statsEquipe.PropertyChanged += StatsEquipe_PropertyChanged;
        }

        private void StatsEquipe_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SauvegarderStatsEquipe.ChangeCanExecute();
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

        private bool _estEquipeSelectionnee = false;
        public bool estEquipeSelectionnee
        {
            get { return _estEquipeSelectionnee; }
            set
            {
                if (estEquipeSelectionnee != value)
                {
                    _estEquipeSelectionnee = value;
                    NotifierChangement("estEquipeSelectionnee");
                    SauvegarderStatsEquipe.ChangeCanExecute();
                }
            }
        }

        private Command _SauvegarderStatsEquipe = null;
        public Command SauvegarderStatsEquipe
        {
            get
            {
                if (_SauvegarderStatsEquipe == null)
                {
                    Action<object> action = new Action<object>(SauvegarderStatsEquipeRoutine);
                    Func<object, bool> predisMoi = new Func<object, bool>(SauvegarderStatsEquipePredicat);
                    _SauvegarderStatsEquipe = new Command(action, predisMoi);
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

        private bool SauvegarderStatsEquipePredicat(object pParametres)
        {
            bool retour = estEquipeSelectionnee && estNbPartieJoueeValide && estNbVictoiresValide &&
                estNbDefaitesValide && estNbDefProloValide;
            if (retour)
            {
                // Tests unitaires réussis, on vérifie que le nombre de victoires, de défaites et de défaites en
                // prolongation est égal au nombre de parties jouées.
                int totalParties = statsEquipe.nbVictoires + statsEquipe.nbDefaites + statsEquipe.nbDefProlo;
                retour = totalParties == statsEquipe.nbPartiesJouees;    // Retournera faux si le total diffère du nombre de parties jouées.
                messageErreur = string.Empty;
                if (!retour)
                {
                    messageErreur = "Le total de victoires, de défaites et de défaites en prolongation doit égaler le nombre de parties jouées.";
                }
            }

            return retour;
        }

        private int _nbPartiesJoueesMax;
        public int nbPartiesJoueesMax
        {
            get { return _nbPartiesJoueesMax; }
            set
            {
                _nbPartiesJoueesMax = value;
                NotifierChangement(nameof(nbPartiesJoueesMax));
            }
        }

        private bool _estNbPartieJoueeValide;
        public bool estNbPartieJoueeValide
        {
            get { return _estNbPartieJoueeValide; }
            set
            {
                _estNbPartieJoueeValide = value;
                SauvegarderStatsEquipe.ChangeCanExecute();
            }
        }

        private bool _estNbVictoiresValide;
        public bool estNbVictoiresValide
        {
            get { return _estNbVictoiresValide; }
            set
            {
                _estNbVictoiresValide = value;
                SauvegarderStatsEquipe.ChangeCanExecute();
            }
        }

        private bool _estNbDefaitesValide;
        public bool estNbDefaitesValide
        {
            get { return _estNbDefaitesValide; }
            set
            {
                _estNbDefaitesValide = value;
                SauvegarderStatsEquipe.ChangeCanExecute();
            }
        }

        private bool _estNbDefProloValide;
        public bool estNbDefProloValide
        {
            get { return _estNbDefProloValide; }
            set
            {
                _estNbDefProloValide = value;
                SauvegarderStatsEquipe.ChangeCanExecute();
            }
        }
    }
}
