using System;
using System.Net;
using GererEquipe.Data.Dto;
using GererEquipe.Data.Services;
using SteveMAUI.MVVM;

namespace GererEquipe.MVVM
{
    public class LireEquipe : CsBaseContexte
    {
        public LireEquipe(int pAnneeCourante, IEnumerable<EquipeDto> listeEquipe)
        {
            _AnneeCourante = pAnneeCourante;
            NotifierChangement("AnneeCourante");

            _listeEquipeEstDevenu = listeEquipe;
        }

        public async void LireUneEquipe(int noEquipe)
        {
            equipe = null;
            var monClientHttp = new EquipeServices();
            equipe = await monClientHttp.ObtenirEquipeAsync(noEquipe);

            selectedIndexEstDevenueEquipe = -1;
            if (equipe.estDevenueEquipe.HasValue)
            {
                selectedIndexEstDevenueEquipe = equipe.estDevenueEquipe.Value;
            }
        }

        private EquipeDto _equipe = new EquipeDto();
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

        public bool _EstInitNouvlEquipeDejaPresse = false;

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

        private int _selectedIndexEstDevenueEquipe = -1;
        public int selectedIndexEstDevenueEquipe
        {
            get { return _selectedIndexEstDevenueEquipe; }
            set
            {
                if (selectedIndexEstDevenueEquipe != value)
                {
                    _selectedIndexEstDevenueEquipe = value;
                    NotifierChangement("selectedIndexEstDevenueEquipe");
                }
            }
        }

        private EquipeDto _estDevenueCetteEquipe = null;
        public EquipeDto estDevenueCetteEquipe
        {
            get { return _estDevenueCetteEquipe; }
            set
            {
                _estDevenueCetteEquipe = value;
                NotifierChangement("estDevenueCetteEquipe");
            }
        }

        public int AnnneMinimum { get { return 1800; } }
        private bool _EstNomEquipeValide;
        public bool EstNomEquipeValide
        {
            get { return _EstNomEquipeValide; }
            set
            {
                if (EstNomEquipeValide != value)
                {
                    _EstNomEquipeValide = value;
                    NotifierChangement("EstNomEquipeValide");
                    SauvegarderEquipe.ChangeCanExecute();
                }
            }
        }

        public bool _EstVilleValide { get; set; }
        public bool EstVilleValide
        {
            get { return _EstVilleValide; }
            set
            {
                if (EstVilleValide != value)
                {
                    _EstVilleValide = value;
                    NotifierChangement("EstVilleValide");
                    SauvegarderEquipe.ChangeCanExecute();
                }
            }
        }

        private int _AnneeCourante = 0;
        public int AnneeCourante { get { return _AnneeCourante; } }

        private Command _SauvegarderEquipe = null;
        public Command SauvegarderEquipe
        {
            get
            {
                if(_SauvegarderEquipe == null)
                {
                    Action<object> action = new Action<object>(SauvegarderEquipeRoutine);
                    Func<object, bool> predicat = new Func<object, bool>(PredicatSauvegarderEquipeRoutine);
                    _SauvegarderEquipe = new Command(action, predicat);
                }

                return _SauvegarderEquipe;
            }
        }

        private bool PredicatSauvegarderEquipeRoutine(object objParametre)
        {
            return EstNomEquipeValide && EstVilleValide;
        }

        private async void SauvegarderEquipeRoutine(object objParametre)
        {
            equipe.estDevenueEquipe = null;
            if (estDevenueCetteEquipe != null)
            {
                equipe.estDevenueEquipe = estDevenueCetteEquipe.id;
            }

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

        private Command _InitialiserNouvelleEquipe = null;
        public Command InitialiserNouvelleEquipe
        {
            get
            {
                if(_InitialiserNouvelleEquipe == null)
                {
                    Action<object> action = new Action<object>(InitialiserNouvelleEquipeRoutine);
                    Func<object, bool> predicat = new Func<object, bool>(ModifierEtatBoutonInit);
                    _InitialiserNouvelleEquipe = new Command(action, predicat);
                }
                return _InitialiserNouvelleEquipe;
            }
        }

        private void InitialiserNouvelleEquipeRoutine(object objParametre)
        {
            equipe = new EquipeDto();
            equipe.anneeDebut = ConfigGlobale.Instance.AnneeCourante;
            _EstInitNouvlEquipeDejaPresse = true;
            InitialiserNouvelleEquipe.ChangeCanExecute();
        }

        private bool ModifierEtatBoutonInit(object pParametres) { return !_EstInitNouvlEquipeDejaPresse; }
    }
}
