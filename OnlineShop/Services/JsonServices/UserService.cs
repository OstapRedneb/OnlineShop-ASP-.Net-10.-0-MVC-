using System;
using Newtonsoft.Json;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Services.JsonServices;

public class UserService : IUserService
{
    private const string _path = "users.json";
    public List<User> GetAll()
    {
        string blob = GetUsersBlob();

        return JsonConvert.DeserializeObject<List<User>>(blob) ?? new List<User>();
    }
    public User? GetById(Guid id)
    {
        return GetAll().FirstOrDefault(user => user.Id == id);
    }
    public bool Add(User user)
    {
        bool answer = false;

        List<User> users = GetAll();

        if (user != null && !users.Any(userFromMemory => userFromMemory.Id == user.Id))
        {
            users.Add(user);
            WriteIntoMemory(users);
            answer = true;
        }

        return answer;
    }
    public void AddRange(params List<User> users)
    {
        List<User> memoryUsers = GetAll();
        List<User> usersToAdd = memoryUsers
            .Union(
                users.Where(user => user != null),
                new UserIdEqualityComparer()
            )
            .ToList();

        WriteIntoMemory(usersToAdd);
    }
    public bool Update(User user)
    {
        if (user is null)
            return false;

        List<User> users = GetAll();

        bool wasFound = false;
        for (int i = 0; i < users.Count; i++)
        {
            if (users[i].Id == user.Id)
            {
                users[i] = user;
                wasFound = true;
                break;
            }
        }

        if (!wasFound)
            Add(user);
        else
            WriteIntoMemory(users);

        return true;
    }
    public void Clear()
    {
        if (File.Exists(_path))
            File.Delete(_path);
    }
    private void WriteIntoMemory(List<User> users)
    {
        string blob = JsonConvert.SerializeObject(users);

        using (StreamWriter writer = new StreamWriter(_path, false))
        {
            writer.Write(blob);
        }
    }
    private string GetUsersBlob()
    {
        if (File.Exists(_path))
            using (StreamReader reader = new StreamReader(_path, false))
                return reader.ReadToEnd();
        return "";
    }
}
