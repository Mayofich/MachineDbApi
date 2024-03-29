﻿using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IKvarService
    {
        Task<bool> CreateKvar(KvarCreate kvar);
        Task<List<Kvar>> GetKvarList();
        Task<List<Kvar>> GetKvarPagination(int limit );
        Task<Kvar> GetKvar(int id_stroja);
        Task<Kvar> UpdateKvar(Kvar kvar);
        Task<Kvar> UpdateStatus(KvarStatusChange kvarStatus);
        Task<List<Kvar>> UpdatePagination(KvarPagination kvarPagination);
        Task<bool> DeleteKvar(int key);
    }
}
