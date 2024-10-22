using GamerZone.MVC.Models;
using GamerZone.MVC.ViewModel;

namespace GamerZone.MVC.Services
{
    public interface IGameServiecs
    {
        Task Create(CreateGameFormViewModel model, string UserID);
        Task<Game?> Upadete(EditGameFromViewMmodel model);
        IEnumerable<GameViewModel> GetAll(string UserID);
        GameViewModel? GetById(int id, string UserID);
        bool Delete(int id);
    }
}
