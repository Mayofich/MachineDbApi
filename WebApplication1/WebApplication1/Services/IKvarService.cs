using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IKvarService
    {
        Task<bool> CreateKvar(KvarCreate kvar);
        Task<List<Kvar>> GetKvarList();
        Task<Kvar> GetKvar(int id_stroja);
        Task<Kvar> UpdateKvar(Kvar kvar);
        Task<bool> DeleteKvar(int key);
    }
}
