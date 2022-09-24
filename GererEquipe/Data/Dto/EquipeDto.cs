using System;
using System.Collections.Generic;

namespace GererEquipe.Data.Dto
{
    public class EquipeDto
    {
        public int id { get; set; }

        public string nomEquipe { get; set; } = default!;

        public string ville { get; set; } = default!;

        public Int32 anneeDebut { get; set; }

        public Int32? anneeFin { get; set; }

        public int? estDevenueEquipe { get; set; }
    }
}
