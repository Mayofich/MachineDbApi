using WebApplication1.Services;
using WebApplication1.Models;
using System.Collections.Generic;
using WebApplication1.Controllers;

namespace WebApplication1.Services
{
    public class KvarService : IKvarService
    {
        private readonly IDbService _dbService;

        public KvarService(IDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<bool> CreateKvar(KvarCreate kvar)
        {
            var kvarList = await _dbService.GetAll<Kvar>("SELECT * FROM public.\"KVAROVI\"", new { });
            var kvarList1 = await _dbService.GetAll<Kvar>("SELECT * FROM public.\"KVAROVI\" where \"ID_STROJA\"=@Id_stroja", kvar);
            bool check = true;
            foreach (Kvar kvar2 in kvarList1)
            { if (kvar2.Aktivan_kvar == true)
                    check = false;
            }

            if (kvar.Detaljni_opis != null & check)
            {
                Kvar kvar1 = new Kvar();
                if (kvar.Aktivan_kvar == true)
                {
                    kvar1.Vrijeme_zavrsetka = null;
                }
                else 
                {
                    kvar1.Vrijeme_zavrsetka = DateTime.Now;
                }
                kvar1.Naziv_kvara = kvar.Naziv_kvara;
                kvar1.Id_stroja = kvar.Id_stroja;
                kvar1.Vrijeme_pocetka = kvar.Vrijeme_pocetka;
                kvar1.Prioritet = kvar.Prioritet;
                kvar1.Aktivan_kvar = kvar.Aktivan_kvar;
                kvar1.Detaljni_opis = kvar.Detaljni_opis;
                kvar1.Id_kvara = kvarList[kvarList.Count - 1].Id_kvara + 1;
                
                var result =
                await _dbService.EditData(
                    "INSERT INTO public.\"KVAROVI\" (\"ID_KVARA\",\"ID_STROJA\",\"NAZIV_KVARA\",\"PRIORITET\",\"VRIJEME_POCETKA\",\"VRIJEME_ZAVRSETKA\",\"DETALJNI_OPIS\",\"AKTIVAN_KVAR\") VALUES (@Id_kvara, @Id_stroja, @Naziv_kvara, @Prioritet, @Vrijeme_pocetka, @Vrijeme_zavrsetka, @Detaljni_opis, @Aktivan_kvar)",
                    kvar1);

                return true;
            }
            else
            { return false; }
        }

        public async Task<List<Kvar>> GetKvarList()
        {
            var kvarList = await _dbService.GetAll<Kvar>("SELECT * FROM public.\"KVAROVI\"", new { });
            return kvarList;
        }
        public async Task<List<Kvar>> GetKvarPagination(int limit)
        {
            var kvarList = await _dbService.GetAll<Kvar>("SELECT * FROM PUBLIC.\"KVAROVI\" ORDER BY \"PRIORITET\", \"VRIJEME_POCETKA\" DESC LIMIT @limit OFFSET 0", new {limit});
            return kvarList;
        }

        public async Task<Kvar> GetKvar(int id_kvara)
        {
            var kvar = await _dbService.GetAsync<Kvar>("SELECT * FROM public.\"KVAROVI\" where \"ID_KVARA\"=@id_kvara", new { id_kvara });
           
            return kvar;
        }

        public async Task<Kvar> UpdateKvar(Kvar kvar)
        {
            var updateKvar =
                await _dbService.EditData(
                    "Update public.\"KVAROVI\" SET \"ID_STROJA\"=@Id_stroja,\"NAZIV_KVARA\"=@Naziv_Kvara,\"PRIORITET\"=@Prioritet,\"VRIJEME_POCETKA\"=@Vrijeme_pocetka,\"VRIJEME_ZAVRSETKA\"=@Vrijeme_zavrsetka,\"DETALJNI_OPIS\"=@Detaljni_opis,\"AKTIVAN_KVAR\"=@Aktivan_kvar WHERE \"ID_KVARA\"=@Id_kvara",
                    kvar);
            return kvar;
        }
        public async Task<Kvar> UpdateStatus(KvarStatusChange kvarStatus)
        {   
            var kvar = await _dbService.GetAsync<Kvar>("SELECT * FROM public.\"KVAROVI\" where \"ID_KVARA\"=@Id_kvara", kvarStatus);
            kvar.Vrijeme_zavrsetka = kvarStatus.Vrijeme_zavrsetka;
            kvar.Aktivan_kvar = false;
            var updateKvar =
                await _dbService.EditData(
                    "Update public.\"KVAROVI\" SET \"VRIJEME_ZAVRSETKA\"=@Vrijeme_zavrsetka,\"AKTIVAN_KVAR\"=@Aktivan_kvar where \"ID_KVARA\"=@Id_kvara",
                    kvar);
            return kvar;
        }
        public async Task<List<Kvar>> UpdatePagination(KvarPagination kvarPagination)
        {
            var kvarList = await _dbService.GetAll<Kvar>("SELECT * FROM PUBLIC.\"KVAROVI\" ORDER BY \"PRIORITET\", \"VRIJEME_POCETKA\" DESC LIMIT @Limit OFFSET @Offset", kvarPagination);
            return kvarList;
        }

        public async Task<bool> DeleteKvar(int id_kvara)
        {
            var deleteKvar = await _dbService.EditData("DELETE FROM public.\"KVAROVI\" WHERE \"ID_KVARA\"=@id_kvara", new { id_kvara });
            return true;
        }
    }
}
