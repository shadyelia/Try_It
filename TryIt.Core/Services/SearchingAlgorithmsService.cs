using TryIt.Core.Enums;
using TryIt.Core.Interfaces;

namespace TryIt.Core.Services
{
    public class SearchingAlgorithmsService : ISearchingAlgorithmsService
    {
        private readonly long _numOfRecords = 1000000;
        public string Search(SearchAlgorithmTypesEnum type)
        {
            int[] data = InitData();

            var watch = System.Diagnostics.Stopwatch.StartNew();
            var randNumber = LongRandom(0, _numOfRecords - 1);
            long index = -1;

            if (type == SearchAlgorithmTypesEnum.Linear)
                index = LinearSearch(data, randNumber);
            if (type == SearchAlgorithmTypesEnum.Greedy)
                index = GreedySearch(data, randNumber);
            else if (type == SearchAlgorithmTypesEnum.BinarySearchTree)
                index = BinarySearchTree(data, randNumber);

            watch.Stop();
            string result = $"time = {watch.ElapsedMilliseconds} ms " +
                $"Found {randNumber} at index : {index}";
            return result;
        }
        private long LinearSearch(int[] data, long target)
        {
            long index = -1;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == target) index = i;
            }
            return index;
        }
        private long GreedySearch(int[] data, long target)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == target) return i;
            }
            return -1;
        }
        private long BinarySearchTree(int[] data, long target)
        {
            int start = 0, end = data.Length - 1;
            while (start <= end)
            {
                int mid = start + (end - start) / 2;
                if (data[mid] == target) return mid;
                else if (data[mid] < target)
                {
                    start = mid + 1;
                }
                else if (data[mid] > target)
                {
                    end = mid - 1;
                }
            }
            return -1;
        }
        private long LongRandom(long min, long max)
        {
            Random rnd = new Random();
            byte[] buf = new byte[8];
            rnd.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (max - min)) + min);
        }
        private int[] InitData()
        {
            int[] data = new int[_numOfRecords];
            for (int i = 0; i < _numOfRecords; i++)
            {
                data[i] = i;
            }
            return data;
        }
    }
}
