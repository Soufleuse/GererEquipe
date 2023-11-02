using System;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using GererEquipe.Data.Dto;
using SteveMAUI.Commun;

namespace GererEquipe.Data.Services
{
    internal class EquipeServices //: IEquipeService
    {
        internal const string _uriBase = "http://localhost:5245/api"; // En http pour ne pas se badrer avec le SSL.
        //internal const string _uriBase = "https://localhost:7166/api";
        //internal const string _uriBase = "http://10.0.0.5:5000/api";

        public async Task<EquipeDto> ObtenirEquipeAsync(long id)
        {
            var equipeDto = new EquipeDto();

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
                string innerMesg = ex.InnerException == null ? string.Empty : ex.InnerException.Message;
                TraitementMessages.ImprimerMessage(ex.Message, innerMesg, ex.StackTrace.ToString());
            }

            return equipeDto;
        }

        public async Task<string> ObtenirNomEquipeEstDevenu(long id)
        {
            var nomEquipeVille = string.Empty;

            var uriEquipe = new Uri(string.Concat(_uriBase + "/Equipe/nomequipeville/", id.ToString()));

            try
            {
                using (var htttpClient = new HttpClient())
                {
                    HttpResponseMessage reponse = await htttpClient.GetAsync(uriEquipe);
                    if (reponse.IsSuccessStatusCode)
                    {
                        nomEquipeVille = await reponse.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                string innerMesg = ex.InnerException == null ? string.Empty : ex.InnerException.Message;
                TraitementMessages.ImprimerMessage(ex.Message, innerMesg, ex.StackTrace.ToString());
            }

            return nomEquipeVille;
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
                string innerMesg = ex.InnerException == null ? string.Empty : ex.InnerException.Message;
                TraitementMessages.ImprimerMessage(ex.Message, innerMesg, ex.StackTrace.ToString());
            }

            return listeEquipeDto;
        }

        public async Task<HttpStatusCode> SauvegarderEquipeAsync(EquipeDto item)
        {
            HttpStatusCode retour = HttpStatusCode.Continue;

            try
            {
                bool blnFaireCreation = false;
                if (item.id < 1)
                {
                    blnFaireCreation = true;
                }
                else
                {
                    var monEquipe = await ObtenirEquipeAsync(item.id);
                    if (monEquipe == null)
                    {
                        blnFaireCreation = true;
                    }
                }

                if (blnFaireCreation)
                {
                    using (var htttpClient = new HttpClient())
                    {
                        var uriEquipe = new Uri(_uriBase + "/Equipe/");
                        var jesuisContent = JsonContent.Create(item);
                        HttpResponseMessage reponse = await htttpClient.PostAsync(uriEquipe, jesuisContent);
                        retour = reponse.StatusCode;
                    }
                }
                else
                {
                    using (var htttpClient = new HttpClient())
                    {
                        var uriEquipe = new Uri(_uriBase + "/Equipe/" + item.id.ToString());
                        var jesuisContent = JsonContent.Create(item);
                        HttpResponseMessage reponse = await htttpClient.PutAsync(uriEquipe, jesuisContent);
                        retour = reponse.StatusCode;
                    }
                }
            }
            catch (Exception ex)
            {
                string innerMesg = ex.InnerException == null ? string.Empty : ex.InnerException.Message;
                TraitementMessages.ImprimerMessage(ex.Message, innerMesg, ex.StackTrace.ToString());
            }

            return retour;
        }

        public async Task<List<StatsEquipeDto>> ObtenirListeStatsEquipe(short pAnnee)
        {
            var listeStatsEquipeDto = new List<StatsEquipeDto>();

            var uriEquipe = new Uri(string.Concat(_uriBase + "/StatsEquipe/parannee/", pAnnee.ToString()));

            try
            {
                using (var htttpClient = new HttpClient())
                {
                    HttpResponseMessage reponse = await htttpClient.GetAsync(uriEquipe);
                    if (reponse.IsSuccessStatusCode)
                    {
                        var content = await reponse.Content.ReadAsStringAsync();
                        listeStatsEquipeDto = JsonSerializer.Deserialize<List<StatsEquipeDto>>(content);
                    }
                }
            }
            catch (Exception ex)
            {
                string innerMesg = ex.InnerException == null ? string.Empty : ex.InnerException.Message;
                TraitementMessages.ImprimerMessage(ex.Message, innerMesg, ex.StackTrace.ToString());
            }

            return listeStatsEquipeDto;
        }
    
        public async Task<StatsEquipeDto> ObtenirStatsEquipe(int idEquipe, short annee)
        {
            StatsEquipeDto statsEquipe = null;
            
            var uriEquipe = new Uri(string.Format(_uriBase + "/StatsEquipe/{0}/{1}", idEquipe.ToString(), annee.ToString()));

            try
            {
                using(var htttpClient = new HttpClient())
                {
                    HttpResponseMessage reponse = await htttpClient.GetAsync(uriEquipe);
                    if (reponse.IsSuccessStatusCode)
                    {
                        var content = await reponse.Content.ReadAsStringAsync();
                        statsEquipe = JsonSerializer.Deserialize<StatsEquipeDto>(content);
                    }
                }
            }
            catch (Exception ex)
            {
                string innerMesg = ex.InnerException == null ? string.Empty : ex.InnerException.Message;
                TraitementMessages.ImprimerMessage(ex.Message, innerMesg, ex.StackTrace.ToString());
            }

            return statsEquipe;
        }

        public async Task<HttpStatusCode> SauvegarderStatsEquipeAsync(StatsEquipeDto item)
        {
            HttpStatusCode retour = HttpStatusCode.Continue;

            try
            {
                bool blnFaireCreation = false;

                var maStatsEquipe = await ObtenirStatsEquipe(item.equipeId, item.anneeStats);
                if (maStatsEquipe == null)
                {
                    blnFaireCreation = true;
                }

                if (blnFaireCreation)
                {
                    using (var htttpClient = new HttpClient())
                    {
                        var uriEquipe = new Uri(_uriBase + "/StatsEquipe/");
                        var jesuisContent = JsonContent.Create(item);
                        HttpResponseMessage reponse = await htttpClient.PostAsync(uriEquipe, jesuisContent);
                        retour = reponse.StatusCode;
                    }
                }
                else
                {
                    using (var htttpClient = new HttpClient())
                    {
                        var urlSauvegarde = string.Format(_uriBase + "/StatsEquipe/{0}/{1}", item.equipeId, item.anneeStats);
                        var uriEquipe = new Uri(urlSauvegarde);
                        var jesuisContent = JsonContent.Create(item);
                        HttpResponseMessage reponse = await htttpClient.PutAsync(uriEquipe, jesuisContent);
                        retour = reponse.StatusCode;
                    }
                }
            }
            catch (Exception ex)
            {
                string innerMesg = ex.InnerException == null ? string.Empty : ex.InnerException.Message;
                TraitementMessages.ImprimerMessage(ex.Message, innerMesg, ex.StackTrace.ToString());
            }

            return retour;
        }
    }
}
