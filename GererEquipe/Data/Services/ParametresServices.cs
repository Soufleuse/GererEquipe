using System;
using System.Text.Json;
using GererEquipe.Data.Dto;
using SteveMAUI.Commun;

namespace GererEquipe.Data.Services
{
    internal class ParametresServices
    {
        internal const string _uriBase = "http://localhost:5245/api"; // En http pour ne pas se badrer avec le SSL.
                                                                      //internal const string _uriBase = "https://localhost:7166/api";
                                                                      //internal const string _uriBase = "http://10.0.0.5:5000/api";

        public async Task<List<ParametresDto>> ObtenirParametreAsync(string nom, DateTime dateValidite)
        {
            var parametresDto = new List<ParametresDto>();

            var monUrl = string.Concat(_uriBase, "/Parametres/{0}/{1}");
            var uriEquipe = new Uri(string.Format(monUrl, nom, dateValidite));

            try
            {
                using (var htttpClient = new HttpClient())
                {
                    HttpResponseMessage reponse = await htttpClient.GetAsync(uriEquipe);
                    if (reponse.IsSuccessStatusCode)
                    {
                        var content = await reponse.Content.ReadAsStringAsync();
                        parametresDto = JsonSerializer.Deserialize<List<ParametresDto>>(content);
                    }
                }
            }
            catch (Exception ex)
            {
                string innerMesg = ex.InnerException == null ? string.Empty : ex.InnerException.Message;
                TraitementMessages.ImprimerMessage(ex.Message, innerMesg, ex.StackTrace.ToString());
            }

            return parametresDto;
        }
    }
}
