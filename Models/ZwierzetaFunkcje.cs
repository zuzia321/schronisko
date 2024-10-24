using System.Linq;

namespace Schronisko.Models
{
    public class ZwierzetaFunkcje
    {
        public List<Zwierze> FiltrowanieSortowanieZwierza(
            List<Zwierze> zwierzeta,
            string szukaj = null,
            string rodzajZwierzecia=null,
            string gatunek =null,
            string stan=null,
            string sortowanie=null
           )
        {
            if(!string.IsNullOrWhiteSpace(szukaj))
            {
                var filtrZwierze=new List<Zwierze>();
                for(int i=0;i<zwierzeta.Count;i++)
                {
                    var zwierze = zwierzeta[i];
                    if(zwierze.Imie.Contains(szukaj,StringComparison.OrdinalIgnoreCase))
                        filtrZwierze.Add(zwierze);
                }
                zwierzeta=filtrZwierze;
            }
            if (!string.IsNullOrEmpty(rodzajZwierzecia))
            {
                zwierzeta = zwierzeta.Where(z => z.RodzajZwierzecia == rodzajZwierzecia).ToList();
            }
            if (!string.IsNullOrEmpty(gatunek))
            {
                zwierzeta = zwierzeta.Where(z => z.Gatunek == gatunek).ToList();
            }
            if (!string.IsNullOrEmpty(stan))
            {
                zwierzeta = zwierzeta.Where(z => z.Stan == stan).ToList();
            }
            switch (sortowanie)
            {
                case "imie":
                    zwierzeta = zwierzeta.OrderBy(z => z.Imie).ToList();
                    break;
                case "data":
                    zwierzeta = zwierzeta.OrderBy(z => z.OdKiedyWSchronisku).ToList();
                    break;
                case "data_od_tylu":
                    zwierzeta = zwierzeta.OrderByDescending(z => z.OdKiedyWSchronisku).ToList();
                    break;
                default:
                    break;
            }
            return zwierzeta;
        }
    }
}
