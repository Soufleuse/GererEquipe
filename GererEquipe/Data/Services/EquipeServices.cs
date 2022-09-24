using System;
using GererEquipe.Data.Dto;
using System.Text.Json;
//using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace GererEquipe.Data.Services
{
    internal class EquipeServices //: IEquipeService
    {
        internal const string _uriBase = "https://localhost:7166/api";

        public async Task<EquipeDto> ObtenirEquipeAsync(long id)
        {
            var equipeDto = new EquipeDto();

            //var uriEquipe = new Uri(string.Concat(_uriBase + "/EquipeBds/", id.ToString()));
            var uriEquipe = new Uri(string.Concat(_uriBase + "/Equipe/", id.ToString()));

            try
            {
                using (var htttpClient = new HttpClient())
                {
                    HttpResponseMessage reponse = await htttpClient.GetAsync(uriEquipe);
                    if (reponse.IsSuccessStatusCode)
                    {
                        var content = await reponse.Content.ReadAsStringAsync();
                        equipeDto = JsonSerializer.Deserialize<EquipeDto>(content);
                    }
                }
            }
            catch( Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return equipeDto;
        }

        public async Task<List<EquipeDto>> ObtenirListeEquipeAsync()
        {
            var listeEquipeDto = new List<EquipeDto>();

            var uriEquipe = new Uri(_uriBase + "/Equipe/");

            try
            {
                using (var htttpClient = new HttpClient())
                {
                    HttpResponseMessage reponse = await htttpClient.GetAsync(uriEquipe);
                    if (reponse.IsSuccessStatusCode)
                    {
                        var content = await reponse.Content.ReadAsStringAsync();
                        listeEquipeDto = JsonSerializer.Deserialize<List<EquipeDto>>(content);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return listeEquipeDto;
        }

        public async Task<ActionResult> CreerEquipeAsync(EquipeDto item)
        {
            var uriEquipe = new Uri(_uriBase + "/Equipe/");
            ActionResult retour = new OkResult();

            try
            {
                using (var htttpClient = new HttpClient())
                {
                    var equipeEnjson = JsonSerializer.Serialize(item, item.GetType());
                    var jesuisContent = new StringContent(equipeEnjson);
                    HttpResponseMessage reponse = await htttpClient.PostAsync(uriEquipe, jesuisContent);
                    if (!reponse.IsSuccessStatusCode)
                    {
                        retour = new StatusCodeResult((int)reponse.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return retour;
        }
    }
}
