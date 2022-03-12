using Microsoft.AspNetCore.Mvc;
using System.Collections;
using TryIt.SharedKernel.Authorization;

namespace TryIt.Web.Controllers
{
    [Route("api/InitializationData")]
    [ApiController]
    [Authorize]
    public class InitializationData : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("init_Array")]
        public ActionResult<string> Init_Array()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            long initialMemory = GC.GetTotalMemory(true);
            int[] data = new int[100000000];
            for (int i = 0; i < 100000000; i++)
            {
                data[i] = i;
            }
            watch.Stop();
            long finalMemory = GC.GetTotalMemory(true);
            GC.KeepAlive(data);
            long size = finalMemory - initialMemory;

            string result = $"time = {watch.ElapsedMilliseconds}ms ,size = {(double)size / 100000}";
            return Ok(result);
        }

        [HttpGet("init_List")]
        public ActionResult<string> Init_List()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            long initialMemory = GC.GetTotalMemory(true);
            List<int> data = new List<int>();
            for (int i = 0; i < 100000000; i++)
            {
                data.Add(i);
            }
            watch.Stop();
            long finalMemory = GC.GetTotalMemory(true);
            GC.KeepAlive(data);
            long size = finalMemory - initialMemory;

            string result = $"time = {watch.ElapsedMilliseconds}ms ,size = {(double)size / 100000}";
            return Ok(result);
        }

        [HttpGet("Init_HashSet")]
        public ActionResult<string> Init_HashSet()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            long initialMemory = GC.GetTotalMemory(true);
            HashSet<int> data = new HashSet<int>();
            for (int i = 0; i < 100000000; i++)
            {
                data.Add(i);
            }
            watch.Stop();
            long finalMemory = GC.GetTotalMemory(true);
            GC.KeepAlive(data);
            long size = finalMemory - initialMemory;

            string result = $"time = {watch.ElapsedMilliseconds}ms ,size = {(double)size / 100000}";
            return Ok(result);
        }

        [HttpGet("Init_Dictionary")]
        public ActionResult<string> Init_Dictionary()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            long initialMemory = GC.GetTotalMemory(true);
            Dictionary<int, int> data = new Dictionary<int, int>();
            for (int i = 0; i < 100000000; i++)
            {
                data.Add(i, i);
            }
            watch.Stop();
            long finalMemory = GC.GetTotalMemory(true);
            GC.KeepAlive(data);
            long size = finalMemory - initialMemory;

            string result = $"time = {watch.ElapsedMilliseconds}ms ,size = {(double)size / 100000}";
            return Ok(result);
        }

        [HttpGet("Init_Hashtable")]
        public ActionResult<string> Init_HashtTable()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            long initialMemory = GC.GetTotalMemory(true);
            Hashtable data = new Hashtable();
            for (int i = 0; i < 100000000; i++)
            {
                data.Add(i, i);
            }
            watch.Stop();
            long finalMemory = GC.GetTotalMemory(true);
            GC.KeepAlive(data);
            long size = finalMemory - initialMemory;

            string result = $"time = {watch.ElapsedMilliseconds}ms ,size = {(double)size / 100000}";
            return Ok(result);
        }

    }
}
