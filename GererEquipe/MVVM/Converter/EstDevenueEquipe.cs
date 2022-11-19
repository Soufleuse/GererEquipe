using GererEquipe.Data.Dto;
using GererEquipe.Data.Services;
using GererEquipe.Pages;
using System;
using System.Globalization;

namespace GererEquipe.MVVM.Converter
{
    public class EstDevenueEquipe : IValueConverter
    {
        private EquipeDto _equipeDto;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string retour = string.Empty;

            if(value != null)
            {
                if(value.GetType() == typeof(Int32))
                {
                    int maValeur = (int)value;
                    LireUneEquipe(maValeur);
                    retour = string.Concat(_equipeDto.nomEquipe, " ", _equipeDto.ville);
                }
            }

            return retour;
        }

        public async void LireUneEquipe(int noEquipe)
        {
            var monClientHttp = new EquipeServices();
            _equipeDto = await monClientHttp.ObtenirEquipeAsync(noEquipe);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //throw new NotImplementedException();
            return null;
        }
    }
}
