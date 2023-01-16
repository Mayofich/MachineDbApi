using WebApplication1.Services;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class StrojService : IStrojService
    {
        private readonly IDbService _dbService;

        public StrojService(IDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<bool> CreateStroj(string naziv_stroja)
        {
            var strojList = await _dbService.GetAsync<Stroj>("SELECT * FROM public.\"STROJEVI\" where \"NAZIV_STROJA\"=@naziv_stroja", new { naziv_stroja });
            var strojList1 = await _dbService.GetAll<Stroj>("SELECT * FROM public.\"STROJEVI\"", new { });
            if (strojList == null)
            {
                Stroj stroj = new Stroj();
                stroj.Id_stroja = strojList1[strojList1.Count-1].Id_stroja + 1;
                stroj.Naziv_stroja = naziv_stroja;
                var result =
                await _dbService.EditData(
                    "INSERT INTO public.\"STROJEVI\" (\"ID_STROJA\",\"NAZIV_STROJA\") VALUES (@Id_stroja, @Naziv_stroja)",
                    stroj);
            }
            return true;
        }

        public async Task<List<Stroj>> GetStrojList()
        {
            var strojList = await _dbService.GetAll<Stroj>("SELECT * FROM public.\"STROJEVI\"", new { });
            return strojList;
        }


        public async Task<StrojIspis> GetStroj(int id_stroja)
        {

            var kvarList = await _dbService.GetAll<Kvar>("SELECT * FROM public.\"KVAROVI\" where \"ID_STROJA\"=@id_stroja", new { id_stroja });
            var strojIspis = new StrojIspis();
            var stroj1 = await _dbService.GetAsync<Stroj>("SELECT \"NAZIV_STROJA\" FROM public.\"STROJEVI\" where \"ID_STROJA\"=@id_stroja", new { id_stroja });
            strojIspis.Naziv_stroja = stroj1.Naziv_stroja;
            strojIspis.Ukupno_trajanje_kvarova = new TimeSpan(0, 0, 0, 0);
            foreach (var kvar in kvarList)
            {
                if (kvar.Vrijeme_zavrsetka != null)
                {
                    DateTime VrijemeZ = kvar.Vrijeme_zavrsetka.Value;
                    strojIspis.Ukupno_trajanje_kvarova += VrijemeZ - kvar.Vrijeme_pocetka;
                }
                else
                {
                    strojIspis.Ukupno_trajanje_kvarova += DateTime.Now - kvar.Vrijeme_pocetka;
                }
            }
            strojIspis.kvarovi = kvarList;
            return strojIspis;
        }

        public async Task<Stroj> UpdateStroj(Stroj stroj)
        {
            var strojList = await _dbService.GetAsync<Stroj>("SELECT * FROM public.\"STROJEVI\" where \"NAZIV_STROJA\"=@Naziv_stroja", stroj);
            if (strojList == null)
            {
                var updateStroj =
                await _dbService.EditData(
                    "Update public.\"STROJEVI\" SET \"NAZIV_STROJA\"=@Naziv_stroja WHERE \"ID_STROJA\"=@Id_stroja",
                    stroj);
            }
            return stroj;
        }

        public async Task<bool> DeleteStroj(int id_stroja)
        {
            var deleteStroj = await _dbService.EditData("DELETE FROM public.\"STROJEVI\" WHERE \"ID_STROJA\"=@Id_stroja", new { id_stroja });
            return true;
        }
    }
}
