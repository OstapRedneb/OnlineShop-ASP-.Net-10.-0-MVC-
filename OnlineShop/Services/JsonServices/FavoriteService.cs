using Newtonsoft.Json;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Services.JsonServices
{
    public class FavoriteService : IFavoriteService
    {
        public const string _path = "favorites.json";

        public List<Favorite> GetAll()
        {
            string blob = GetFavoritesBlob();

            return JsonConvert
                    .DeserializeObject<List<FavoriteData>>(blob)
                    ?.OfType<FavoriteData>()
                    ?.Select(favoriteData => (Favorite)favoriteData)
                    ?.ToList() ?? new List<Favorite>();
        }
        public Favorite? GetById(Guid id)
        {
            List<Favorite> favorites = GetAll();

            return favorites.FirstOrDefault(favorite => favorite.Id == id);
        }
        public bool Add(Favorite favorite)
        {
            List<Favorite> favorites = GetAll();

            if (favorite is null || favorites.Any(favoriteFromMemory => favoriteFromMemory.Id == favorite.Id))
                return false;

            favorites.Add(favorite);
            WriteIntoMemory(favorites);

            return true;
        }
        public void AddRange(params List<Favorite> favorites)
        {
            List<Favorite> favoritesFromMemory = GetAll();

            List<Favorite> newFavorites = favoritesFromMemory.Union(favorites, new FavoriteIdEqualityComparer()).ToList();
            WriteIntoMemory(newFavorites);
        }
        public bool Update(Favorite favorite)
        {
            List<Favorite> favorites = GetAll();

            if (favorite is null)
                return false;

            bool wasFound = false;

            for (int i = 0; i < favorites.Count; i++)
            {
                if (favorites[i].Id == favorite.Id)
                {
                    favorites[i] = favorite;
                    wasFound = true;
                    break;
                }
            }

            if (!wasFound)
                favorites.Add(favorite);

            WriteIntoMemory(favorites);
            return true;
        }
        public void Clear()
        {
            if (File.Exists(_path))
                File.Delete(_path);
        }
        private void WriteIntoMemory(List<Favorite> favorites)
        {
            string blob = JsonConvert.SerializeObject(favorites.OfType<Favorite>().Select(favorite => (FavoriteData)favorite).ToList());

            using (StreamWriter writer = new StreamWriter(_path, false))
            {
                writer.Write(blob);
            }
        }
        private string GetFavoritesBlob()
        {
            if (File.Exists(_path))
                using (StreamReader reader = new StreamReader(_path, false))
                    return reader.ReadToEnd();
            return "";
        }
    }
}
