using WebApplication1.Services;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class KvarService : IKvarService
    {
        private readonly IDbService _dbService;

        public KvarService(IDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<bool> CreateKvar(Kvar kvar)
        {
            var result =
                await _dbService.EditData(
                    "INSERT INTO public.\"KVAROVI\" (\"ID_KVARA\",\"NAZIV_KVARA\") VALUES (@Id_kvara, @Naziv_kvara)",
                    kvar);
            return true;
        }

        public async Task<List<Kvar>> GetKvarList()
        {
            var kvarList = await _dbService.GetAll<Kvar>("SELECT * FROM public.\"KVAROVI\"", new { });
            return kvarList;
        }


        public async Task<StrojIspis> GetKvar(int id_stroja)
        {
            var kvarList = await _dbService.GetAll<Kvar>("SELECT * FROM public.\"KVAROVI\" where \"ID_STROJA\"=@id_stroja", new { id_stroja });
            var strojIspis = new StrojIspis();
            var stroj1 = await _dbService.GetAsync<Stroj>("SELECT \"NAZIV_STROJA\" FROM public.\"STROJEVI\" where \"ID_STROJA\"=@id_stroja", new { id_stroja });
            strojIspis.Naziv_stroja = stroj1.Naziv_stroja;
            strojIspis.Ukupno_trajanje_kvarova = new TimeSpan(0,0,0,0);
            foreach (var kvar in kvarList)
            {
                if (kvar.Vrijeme_zavrsetka != null)
                {
                    DateTime VrijemeZ = kvar.Vrijeme_zavrsetka.Value;
                    strojIspis.Ukupno_trajanje_kvarova += VrijemeZ - kvar.Vrijeme_pocetka;
                }
                else
                {
                    strojIspis.Ukupno_trajanje_kvarova +=  DateTime.Now - kvar.Vrijeme_pocetka;
                }
            }
            strojIspis.kvarovi = kvarList;
            return strojIspis;
        }

        public async Task<Kvar> UpdateKvar(Kvar kvar)
        {
            var updateKvar =
                await _dbService.EditData(
                    "Update public.\"KVAROVI\" SET \"NAZIV_KVARA\"=@Naziv_kvara WHERE \"ID_KVARA\"=@Id_kvara",
                    kvar);
            return kvar;
        }

        public async Task<bool> DeleteKvar(int id_kvara)
        {
            var deleteKvar = await _dbService.EditData("DELETE FROM public.\"KVAROVI\" WHERE \"ID_KVARA\"=@Id", new { id_kvara });
            return true;
        }
    }
}
