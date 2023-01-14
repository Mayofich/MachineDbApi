using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IStrojService
    {
        Task<bool> CreateStroj(Stroj stroj);
        Task<List<Stroj>> GetStrojList();
        Task<Stroj> GetStroj(int id_stroja);
        Task<Stroj> UpdateStroj(Stroj stroj);
        Task<bool> DeleteStroj(int key);
    }
}
