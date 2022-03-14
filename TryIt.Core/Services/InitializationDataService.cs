using System.Collections;
using TryIt.Core.Enums;
using TryIt.Core.Interfaces;

namespace TryIt.Core.Services
{
    public class InitializationDataService : IInitializationDataService
    {
        private readonly long _numOfRecords = 1000000;
        public string InitDataStrucutre(DataStructuresTypesEnum type)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            long initialMemory = GC.GetTotalMemory(true);
            dynamic data = InitData(type);
            watch.Stop();
            long finalMemory = GC.GetTotalMemory(true);
            GC.KeepAlive(data);
            long size = finalMemory - initialMemory;

            string result = $"time = {watch.ElapsedMilliseconds} ms " +
                $",size = {(double)size / 100000} mb with {_numOfRecords} records";
            return result;
        }
        private dynamic InitData(DataStructuresTypesEnum type)
        {
            dynamic data;
            if (type == DataStructuresTypesEnum.Array)
                data = InitArray();
            else if (type == DataStructuresTypesEnum.List)
                data = InitList();
            else if (type == DataStructuresTypesEnum.HashSet)
                data = InitHashSet();
            else if (type == DataStructuresTypesEnum.Dictionary)
                data = InitDictionary();
            else if (type == DataStructuresTypesEnum.HashTable)
                data = InitHashTable();
            else 
                data = null;

            return data;
        }
        private int[] InitArray()
        {
            int[] data = new int[_numOfRecords];
            for (int i = 0; i < _numOfRecords; i++)
            {
                data[i] = i;
            }
            return data;
        }
        private List<int> InitList()
        {
            List<int> data = new List<int>();
            for (int i = 0; i < _numOfRecords; i++)
            {
                data.Add(i);
            }
            return data;
        }
        private HashSet<int> InitHashSet()
        {
            HashSet<int> data = new HashSet<int>();
            for (int i = 0; i < _numOfRecords; i++)
            {
                data.Add(i);
            }
            return data;
        }
        private Dictionary<int, int> InitDictionary()
        {
            Dictionary<int, int> data = new Dictionary<int, int>();
            for (int i = 0; i < _numOfRecords; i++)
            {
                data.Add(i, i);
            }
            return data;
        }
        private Hashtable InitHashTable()
        {
            Hashtable data = new Hashtable();
            for (int i = 0; i < _numOfRecords; i++)
            {
                data.Add(i, i);
            }
            return data;
        }
    }
}
