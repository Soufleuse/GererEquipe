using System;
using SteveMAUI.MVVM;

namespace GererEquipe.Data.Dto
{
    public class EquipeDto : CsBaseContexte
    {
        private int _id = default(int);
        public int id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    NotifierChangement("id");
                }
            }
        }

        private string _nomEquipe = default!;
        public string nomEquipe
        {
            get { return _nomEquipe; }
            set
            {
                if (string.Compare(nomEquipe, value) != 0)
                {
                    _nomEquipe = value;
                    NotifierChangement(nomEquipe);
                }
            }
        }

        private string _ville = default!;
        public string ville
        {
            get { return _ville; }
            set
            {
                if(string.Compare(ville, value) != 0)
                {
                    _ville = value;
                    NotifierChangement("ville");
                }
            }
        }

        private Int32 _anneeDebut = 1800/*DateTime.Now.Year*/;
        public Int32 anneeDebut
        {
            get { return _anneeDebut; }
            set
            {
                if (anneeDebut != value)
                {
                    _anneeDebut = value;
                    NotifierChangement("anneeDebut");
                }
            }
        }

        private Int32? _anneeFin = null;
        public Int32? anneeFin
        {
            get { return _anneeFin; }
            set
            {
                if(!Nullable<Int32>.Equals(anneeFin, value))
                {
                    _anneeFin = value;
                    NotifierChangement("anneeFin");
                }
            }
        }

        private int? _estDevenueEquipe = null;
        public int? estDevenueEquipe
        {
            get { return _estDevenueEquipe; }
            set
            {
                if(!Nullable<int>.Equals(estDevenueEquipe, value))
                {
                    _estDevenueEquipe = value;
                    NotifierChangement("estDevenueEquipe");
                }
            }
        }

        private string _nomEquipeVilleEstDevenueEquipe = string.Empty;
        public string nomEquipeVilleEstDevenueEquipe
        {
            get { return _nomEquipeVilleEstDevenueEquipe; }
            set
            {
                if(string.Compare(nomEquipeVilleEstDevenueEquipe, value) != 0)
                {
                    _nomEquipeVilleEstDevenueEquipe = value;
                    NotifierChangement("nomEquipeVilleEstDevenueEquipe");
                }
            }
        }

        public override string ToString()
        {
            return string.Concat(this.nomEquipe, " ", this.ville);
        }
    }
}
