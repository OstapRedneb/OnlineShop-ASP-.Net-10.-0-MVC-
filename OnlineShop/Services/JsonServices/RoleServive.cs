using System;
using Newtonsoft.Json;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Services.JsonServices;

public class RoleService : IRoleService
{
    private const string _path = "roles.json";
    public List<Role> GetAll()
    {
        string blob = GetRolesBlob();

        return JsonConvert.DeserializeObject<List<Role>>(blob) ?? new List<Role>();
    }
    public Role? GetById(Guid id)
    {
        return GetAll().FirstOrDefault(role => role.Id == id);
    }
    public Role? GetByName(string name)
    {
        return GetAll().FirstOrDefault(role => role.Name == name);
    }
    public bool Add(Role role)
    {
        bool answer = false;

        List<Role> roles = GetAll();

        if (role != null && !roles.Any(roleFromMemory => roleFromMemory.Id == role.Id))
        {
            roles.Add(role);
            WriteIntoMemory(roles);
            answer = true;
        }

        return answer;
    }
    public void AddRange(params List<Role> roles)
    {
        List<Role> memoryRoles = GetAll();
        List<Role> rolesToAdd = memoryRoles
            .Union(
                roles.Where(role => role != null),
                new RoleIdEqualityComparer()
            )
            .ToList();

        WriteIntoMemory(rolesToAdd);
    }
    public bool Remove(Role role) 
    {
        Role? roleToRemove = GetById(role.Id);

        if (roleToRemove is null || roleToRemove.Name == "User" || roleToRemove.Name == "Admin")
            return false;

        List<Role> updatedRoles = new List<Role>();

        foreach (Role roleFromMemory in GetAll()) 
        {
            if (roleFromMemory.Id != roleToRemove.Id)
                updatedRoles.Add(roleFromMemory);
        }

        WriteIntoMemory(updatedRoles);
        return true;
    }
    public bool Update(Role role)
    {
        if (role is null)
            return false;

        List<Role> roles = GetAll();

        bool wasFound = false;
        for (int i = 0; i < roles.Count; i++)
        {
            if (roles[i].Id == role.Id)
            {
                roles[i] = role;
                wasFound = true;
                break;
            }
        }

        if (!wasFound)
            Add(role);
        else
            WriteIntoMemory(roles);

        return true;
    }
    public void Clear()
    {
        if (File.Exists(_path))
            File.Delete(_path);
    }
    private void WriteIntoMemory(List<Role> roles)
    {
        string blob = JsonConvert.SerializeObject(roles);

        using (StreamWriter writer = new StreamWriter(_path, false))
        {
            writer.Write(blob);
        }
    }
    private string GetRolesBlob()
    {
        if (File.Exists(_path))
            using (StreamReader reader = new StreamReader(_path, false))
                return reader.ReadToEnd();
        return "";
    }
}
