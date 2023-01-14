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

        public async Task<bool> CreateStroj(Stroj stroj)
        {
            var strojList = await _dbService.GetAsync<Stroj>("SELECT * FROM public.\"STROJEVI\" where \"NAZIV_STROJA\"=@Naziv_stroja", stroj);
            if (strojList == null)
            {
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


        public async Task<Stroj> GetStroj(int id_stroja)
        {
            var strojList = await _dbService.GetAsync<Stroj>("SELECT * FROM public.\"STROJEVI\" where \"ID_STROJA\"=@id_stroja", new { id_stroja });
            return strojList;
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
