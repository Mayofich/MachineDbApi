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


        public async Task<List<Kvar>> GetKvar(int id_stroja)
        {
            var kvarList = await _dbService.GetAll<Kvar>("SELECT * FROM public.\"KVAROVI\" where \"ID_STROJA\"=@id_stroja", new { id_stroja });
            return kvarList;
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
