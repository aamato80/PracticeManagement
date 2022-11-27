using Microsoft.AspNetCore.Mvc.ModelBinding;
using DossierManagement.Dal.Models;
using System.Runtime.CompilerServices;
using System.Linq;

namespace DossierManagement.Api.Utils
{
    public static class Extensions
    {

        public static T GetNewValue<T>(this T @enum) where T : Enum
        {
            var intValue = Convert.ToInt32(@enum);
            var newValue =Enum.Parse(@enum.GetType(), (intValue+1).ToString());
            var lastValue = Enum.GetValues(@enum.GetType()).Cast<int>().ToArray().Max();

            if (Convert.ToInt32(newValue) > lastValue)
            {
                throw new ArgumentOutOfRangeException();
            }

            return (T)newValue;
        }
    }
}
