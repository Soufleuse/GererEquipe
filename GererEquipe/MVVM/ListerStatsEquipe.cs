using System;
using GererEquipe.Data.Dto;
using GererEquipe.Data.Services;
using SteveMAUI.MVVM;

namespace GererEquipe.MVVM
{
    public class ListerStatsEquipe : CsBaseContexte
    {
        private List<StatsEquipeDto> _listeStatsEquipe = null;
        public List<StatsEquipeDto> listeStatsEquipe
        {
            get { return _listeStatsEquipe; }
        }

        private IEnumerable<EquipeDto> _listeEquipe = null;
        public IEnumerable<EquipeDto> listeEquipe { get { return _listeEquipe; } }

        public ListerStatsEquipe()
        {
            ListerStatsEquipeRoutine(null);
        }

        private async void ListerStatsEquipeRoutine(object objParametre)
        {
            var monClientHttp = new EquipeServices();

            var listeStatsEquipeLocale = await monClientHttp.ObtenirListeStatsEquipe(ConfigGlobale.Instance.AnneeCourante);

            _listeStatsEquipe = listeStatsEquipeLocale;
            NotifierChangement("listeStatsEquipe");

            var listeEquipeLocale = await monClientHttp.ObtenirListeEquipeAsync();
            _listeEquipe = listeEquipeLocale;
            NotifierChangement("listeEquipe");
        }
    }
}
