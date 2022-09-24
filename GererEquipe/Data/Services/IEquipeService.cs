using System;
using System.Threading.Tasks;
using GererEquipe.Data.Dto;

namespace GererEquipe.Data.Services
{
    internal interface IEquipeService
    {
        Task<List<EquipeDto>> ObtenirListeEquipeAsync();

        Task<EquipeDto> ObtenirEquipeAsync(long id);

        Task CreerEquipeAsync(EquipeDto item);

        Task MAJEquipeAsync(EquipeDto item);
    }
}
