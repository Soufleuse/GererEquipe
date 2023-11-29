using System;

namespace GererEquipe
{
    internal class ConfigGlobale
    {
        private static object _objLock = new object();

        private static ConfigGlobale _instance = null;
        internal static ConfigGlobale Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_objLock)
                    {
                        _instance = new ConfigGlobale();
                    }
                }

                return _instance;
            }
        }

        private short _anneeCourante = short.MinValue;
        internal short AnneeCourante
        {
            get { return _anneeCourante; }
            set { _anneeCourante = value; }
        }

        private short _nbPartiesJoueesMax = 0;
        public short nbPartiesJoueesMax
        {
            get { return _nbPartiesJoueesMax; }
            set { _nbPartiesJoueesMax = value; }
        }


        private ConfigGlobale() { }
    }
}
