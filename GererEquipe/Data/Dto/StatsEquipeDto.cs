using System;
using SteveMAUI.MVVM;

namespace GererEquipe.Data.Dto
{
    public class StatsEquipeDto : CsBaseContexte
    {
        private short _anneeStats = default(short);
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

        private short _nbPartiesJouees = default(short);
        public short nbPartiesJouees
        {
            get { return _nbPartiesJouees; }
            set
            {
                if (nbPartiesJouees != value)
                {
                    _nbPartiesJouees = value;
                    NotifierChangement("nbPartiesJouees");
                }
            }
        }

        private short _nbVictoires = default(short);
        public short nbVictoires
        {
            get { return nbVictoires; }
            set
            {
                if(nbVictoires != value)
                {
                    _nbVictoires = value;
                    NotifierChangement("nbVictoires");
                }
            }
        }

        private short _nbDefaites = default(short);
        public short nbDefaites
        {
            get { return _nbDefaites; }
            set
            {
                if (nbDefaites != value)
                {
                    _nbDefaites = value;
                    NotifierChangement("nbDefaites");
                }
            }
        }

        private short _nbDefProlo = default(short);
        public short nbDefProlo
        {
            get { return _nbDefProlo; }
            set
            {
                if(nbDefProlo != value)
                {
                    _nbDefProlo = value;
                    NotifierChangement("nbDefProlo");
                }
            }
        }

        private short _nbButsPour = default(short);
        public short nbButsPour
        {
            get { return _nbButsPour; }
            set
            {
                if(nbButsPour != value)
                {
                    _nbButsPour = value;
                    NotifierChangement("nbButsPour");
                }
            }
        }

        private short _nbButsContre = default(short);
        public short nbButsContre
        {
            get { return _nbButsContre; }
            set
            {
                if(nbButsContre != value)
                {
                    _nbButsContre = value;
                    NotifierChangement("nbButsContre");
                }
            }
        }

        private int _equipeId = default(int);
        public int equipeId
        {
            get { return _equipeId; }
            set
            {
                if (equipeId != value)
                {
                    _equipeId = value;
                    NotifierChangement("equipeId");
                }
            }
        }
    }
}
