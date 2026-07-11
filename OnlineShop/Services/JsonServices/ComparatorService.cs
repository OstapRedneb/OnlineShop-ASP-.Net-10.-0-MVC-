using Newtonsoft.Json;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Services.JsonServices
{
    public class ComparatorService : IComparatorService
    {
        public const string _path = "comparators.json";

        public List<Comparator> GetAll()
        {
            string blob = GetComparatorsBlob();

            return JsonConvert
                    .DeserializeObject<List<ComparatorData>>(blob)
                    ?.OfType<ComparatorData>()
                    ?.Select(comparatorData => (Comparator)comparatorData)
                    ?.ToList() ?? new List<Comparator>();
        }
        public Comparator? GetById(Guid id)
        {
            List<Comparator> comparators = GetAll();

            return comparators.FirstOrDefault(comparator => comparator.Id == id);
        }
        public bool Add(Comparator comparator)
        {
            List<Comparator> comparators = GetAll();

            if (comparator is null || comparators.Any(comparatorFromMemory => comparatorFromMemory.Id == comparator.Id))
                return false;

            comparators.Add(comparator);
            WriteIntoMemory(comparators);

            return true;
        }
        public void AddRange(params List<Comparator> comparators)
        {
            List<Comparator> comparatorsFromMemory = GetAll();

            List<Comparator> newComparators = comparatorsFromMemory.Union(comparators, new ComparatorIdEqualityComparer()).ToList();
            WriteIntoMemory(newComparators);
        }
        public bool Update(Comparator comparator)
        {
            List<Comparator> comparators = GetAll();

            if (comparator is null)
                return false;

            bool wasFound = false;

            for (int i = 0; i < comparators.Count; i++)
            {
                if (comparators[i].Id == comparator.Id)
                {
                    comparators[i] = comparator;
                    wasFound = true;
                    break;
                }
            }

            if (!wasFound)
                comparators.Add(comparator);

            WriteIntoMemory(comparators);
            return true;
        }
        public void Clear()
        {
            if (File.Exists(_path))
                File.Delete(_path);
        }
        private void WriteIntoMemory(List<Comparator> comparators)
        {
            string blob = JsonConvert.SerializeObject(comparators.OfType<Comparator>().Select(comparator => (ComparatorData)comparator).ToList());

            using (StreamWriter writer = new StreamWriter(_path, false))
            {
                writer.Write(blob);
            }
        }
        private string GetComparatorsBlob()
        {
            if (File.Exists(_path))
                using (StreamReader reader = new StreamReader(_path, false))
                    return reader.ReadToEnd();
            return "";
        }
    }
}
