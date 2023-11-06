using System;
using GererEquipe.Data.Services;

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

        private ConfigGlobale() { }

        internal async void AllerChercherAnneeCourante()
        {
            var monParamHttp = new ParametresServices();
            var monAnneeHttp = await monParamHttp.ObtenirParametreAsync("anneeCourante", DateTime.Now);

            _anneeCourante = Convert.ToInt16(monAnneeHttp.First().valeur);
        }
    }
}
