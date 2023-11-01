using System;
using GererEquipe.Data.Dto;
using GererEquipe.Data.Services;
using SteveMAUI.MVVM;

namespace GererEquipe.MVVM
{
    public class ListerStatsEquipe : CsBaseContexte
    {
        private List<StatsEquipeDto> _listeStatsEquipe = new List<StatsEquipeDto>();
        public List<StatsEquipeDto> listeStatsEquipe
        {
            get { return _listeStatsEquipe; }
        }

        public ListerStatsEquipe()
        {
            ListerStatsEquipeRoutine(null);
        }

        private async void ListerStatsEquipeRoutine(object objParametre)
        {
            var monClientHttp = new EquipeServices();

            var listeStatsEquipe = await monClientHttp.ObtenirListeStatsEquipe(2023);

            _listeStatsEquipe = listeStatsEquipe;
            NotifierChangement("listeStatsEquipe");
        }
    }
}
