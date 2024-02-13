using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraping.Model;

namespace WebScraping.Validation
{
    public static class CheckPrice
    {
        public static bool CheckPriceValidation(Item item, int priceLimit)
        {
            var price = item.price.Replace(".", "");
            return int.Parse(price) < priceLimit;
        }
    }
}
