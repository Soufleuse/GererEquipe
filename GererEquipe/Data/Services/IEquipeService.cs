using System;
using System.Net;
using System.Threading.Tasks;
using GererEquipe.Data.Dto;

namespace GererEquipe.Data.Services
{
    internal interface IEquipeService
    {
        Task<EquipeDto> ObtenirEquipeAsync(long id);

        Task<string> ObtenirNomEquipeEstDevenu(long id);

        Task<List<EquipeDto>> ObtenirListeEquipeAsync();

        Task<HttpStatusCode> SauvegarderEquipeAsync(EquipeDto item);
    }
}
