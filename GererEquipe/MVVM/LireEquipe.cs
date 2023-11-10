﻿using System;
using System.Net;
using GererEquipe.Data.Dto;
using GererEquipe.Data.Services;
using SteveMAUI.MVVM;

namespace GererEquipe.MVVM
{
    public class LireEquipe : CsBaseContexte
    {
        public LireEquipe(int pAnneeCourante)
        {
            _AnneeCourante = pAnneeCourante;
            NotifierChangement("AnneeCourante");
        }

        public async void LireUneEquipe(int noEquipe, IEnumerable<EquipeDto> listeEquipe)
        {
            equipe = null;
            var monClientHttp = new EquipeServices();
            equipe = await monClientHttp.ObtenirEquipeAsync(noEquipe);

            _listeEquipeEstDevenu = listeEquipe;
            NotifierChangement("listeEquipeEstDevenu");

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

        private int AnnneMinimum { get {  return 1800; } }

        private int _AnneeCourante = 0;
        public int AnneeCourante { get { return _AnneeCourante; } }

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
            equipe.estDevenueEquipe = null;
            if(estDevenueCetteEquipe != null)
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
            equipe.anneeDebut = ConfigGlobale.Instance.AnneeCourante;
        }
    }
}
