using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IStrojService
    {
        Task<bool> CreateStroj(string naziv_stroja);
        Task<List<Stroj>> GetStrojList();
        Task<StrojIspis> GetStroj(int id_stroja);
        Task<Stroj> UpdateStroj(Stroj stroj);
        Task<bool> DeleteStroj(int key);
    }
}
