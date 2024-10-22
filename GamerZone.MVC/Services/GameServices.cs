using GamerZone.MVC.Data;
using GamerZone.MVC.Models;
using GamerZone.MVC.Settings;
using GamerZone.MVC.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GamerZone.MVC.Services
{
    public class GameServices(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment) : IGameServiecs
    {
        private readonly string _imagePath = $"{webHostEnvironment.WebRootPath}{FileSettings.ImagePath}";
        public IEnumerable<GameViewModel> GetAll(string userID)
        {
            var Usergames = (from ug in dbContext.ApplicationUsersGames
                             join g in dbContext.Games
                             on ug.GameId equals g.Id

                             join c in dbContext.Categories
                             on g.CategoryId equals c.Id

                             where ug.ApplicationUserId == userID
                             select new GameViewModel
                             {
                                 Id = g.Id,
                                 Name = g.Name,
                                 Description = g.Description,
                                 Cover = g.Cover,
                                 Category = c.Name,
                                 //Devices = new List<string> {d.Icon }
                                 Devices = (from d in dbContext.Devices
                                            join gd in dbContext.GameDevices
                                            on d.Id equals gd.DeviceId
                                            where gd.GameId == g.Id
                                            select d.Icon
                                            ).ToList()
                             }).ToList();
            return Usergames;
        }
        public GameViewModel? GetById(int id, string userID)
        {
            var game = (from ug in dbContext.ApplicationUsersGames
                             join g in dbContext.Games
                             on ug.GameId equals g.Id

                             join c in dbContext.Categories
                             on g.CategoryId equals c.Id

                             where ug.ApplicationUserId == userID
                             select new GameViewModel
                             {
                                 Id = g.Id,
                                 Name = g.Name,
                                 Description = g.Description,
                                 Cover = g.Cover,
                                 Category = c.Name,
                                 //Devices = new List<string> {d.Icon }
                                 Devices = (from d in dbContext.Devices
                                            join gd in dbContext.GameDevices
                                            on d.Id equals gd.DeviceId
                                            where gd.GameId == g.Id
                                            select d.Icon
                                            ).ToList(),
                                 DeviceIds = (from d in dbContext.Devices
                                              join gd in dbContext.GameDevices
                                              on d.Id equals gd.DeviceId
                                              where gd.GameId == g.Id
                                              select d.Id
                                            ).ToList()

                             }).SingleOrDefault(g => g.Id == id);
            return game;
        }
        public async Task Create(CreateGameFormViewModel model, string UserID)
        {
            var coverName = await SaveImage(model.Cover);
            Game game = new()
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cover = coverName,
                GameDevices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()

            };
            
            dbContext.Games.Add(game);
            dbContext.SaveChanges();
            ApplicationUserGame userGame = new()
            {
                GameId = game.Id,
                ApplicationUserId = UserID
            };
            dbContext.ApplicationUsersGames.Add(userGame);
            dbContext.SaveChanges();
        }
        public async Task<Game?> Upadete(EditGameFromViewMmodel model)
        {
            var game = dbContext.Games.Include(g => g.GameDevices).SingleOrDefault(g => g.Id == model.Id);
            if (game is null)
                return null;
            var hasNewCover = model.Cover is not null;
            var currentCOver = game.Cover;
            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.GameDevices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();
            if (hasNewCover)
            {
                game.Cover = await SaveImage(model.Cover!);
            }
            var result = dbContext.SaveChanges();
            if(result > 0)
            {
                if (hasNewCover)
                {
                    var cover = Path.Combine(_imagePath, currentCOver);
                    File.Delete(cover);
                }
                return game;
            }
            else
            {
                var cover = Path.Combine(_imagePath, game.Cover);
                File.Delete(cover);
                return null;
            }
        }
        public bool Delete(int id)
        {
            var isDeleted = false;
            var game = dbContext.Games.Find(id);
            if (game is null)
                return isDeleted;
            dbContext.Remove(game);
            var effectedROws = dbContext.SaveChanges();
            if (effectedROws > 0)
            {
                var cover = Path.Combine(_imagePath, game.Cover);
                File.Delete(cover);
                isDeleted = true;
            }
            return isDeleted;
        }
        private async Task<string> SaveImage(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var path = Path.Combine(_imagePath, coverName);
            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);
            return coverName;
        }
    }
}
