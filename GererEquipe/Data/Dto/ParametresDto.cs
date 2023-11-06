using System;
using SteveMAUI.MVVM;

namespace GererEquipe.Data.Dto
{
    public class ParametresDto : CsBaseContexte
    {
        private string _nom = string.Empty;
        public string nom
        {
            get { return _nom; }
            set
            {
                if(string.Compare(nom, value) != 0)
                {
                    _nom = value;
                    NotifierChangement("nom");
                }
            }
        }

        public DateTime _dateDebut = DateTime.MinValue;
        public DateTime dateDebut
        {
            get { return _dateDebut; }
            set
            {
                if(DateTime.Compare(dateDebut, value) != 0)
                {
                    _dateDebut = value;
                    NotifierChangement("dateDebut");
                }
            }
        }
        
        private DateTime? _dateFin = null;
        public DateTime? dateFin
        {
            get { return _dateFin; }
            set
            {
                bool blnValeursDifferentes = false;
                if((dateFin.HasValue && !value.HasValue) || (!dateFin.HasValue && value.HasValue))
                {
                    blnValeursDifferentes = true;
                }
                else if(dateFin.HasValue && value.HasValue)
                {
                    if (DateTime.Compare(dateFin.Value, value.Value) != 0)
                    {
                        blnValeursDifferentes = true;
                    }
                }

                if (blnValeursDifferentes)
                {
                    _dateFin = value;
                    NotifierChangement("dateFin");
                }
            }
        }
        
        private string _valeur = string.Empty;
        public string valeur
        {
            get { return _valeur; }
            set
            {
                if(string.Compare(valeur, value) != 0)
                {
                    _valeur = value;
                    NotifierChangement("valeur");
                }
            }
        }
    }
}
