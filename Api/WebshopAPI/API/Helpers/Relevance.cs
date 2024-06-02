namespace WebAPI.Helpers
{
    public class Relevance
    {
        public static int RelevanceHelp(string search, string naziv)
        {
            int brojac = 0;
            var rijeci = search.Split(' ');
            foreach (var rijec in rijeci)
            {
                if(naziv.Contains(rijec))
                    brojac++;
            }

            return brojac;
        }
    }
}
