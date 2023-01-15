using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IKvarService
    {
        Task<bool> CreateKvar(Kvar kvar);
        Task<List<Kvar>> GetKvarList();
        Task<StrojIspis> GetKvar(int id_stroja);
        Task<Kvar> UpdateKvar(Kvar kvar);
        Task<bool> DeleteKvar(int key);
    }
}
