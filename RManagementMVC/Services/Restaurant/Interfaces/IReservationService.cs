using RManagementMVC.Models;

namespace RManagementMVC.Services.Restaurant.Interfaces
{
    public interface IReservationService
    {
        Task<bool> CreateAsync(Reservation reservation);
        Task<bool> DeleteAsync(Guid id);
        Task<Reservation?> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(Reservation reservation);
        Task<IEnumerable<Reservation>> GetAllAsync();

    }
}