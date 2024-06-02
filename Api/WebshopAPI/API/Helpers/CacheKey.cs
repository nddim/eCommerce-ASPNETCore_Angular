using WebAPI.Endpoints.Proizvod.PretragaByBrends;

namespace WebAPI.Helpers
{
    public class CacheKey
    {
        public static string ProductsByBrendsGenerator(ProizvodPretragaByBrendRequest obj)
        {
            var key = $"pretragaByBrends" +
                $"-{obj.Min}" +
                $"-{obj.SortID}" +
                $"-{obj.PageNumber}" +
                $"-{obj.Max}" +
                $"-{obj.PotkategorijaID}" +
                $"-{obj.PageSize}";
            key += "-";
            if (obj.Brendovi != null)
            {
                foreach (var brend in obj.Brendovi)
                    key += $"{brend}.";
            }
            return key;
        }
    }
}
