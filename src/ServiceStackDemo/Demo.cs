using ServiceStack.Caching;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStackDemo
{
    public class Demo
    {
        ICacheClientExtended cacheClient = new RedisClient("127.0.0.1", 6379);

        public void TestByteValue()
        {
            const string key = "bytesKey";

            var value = new byte[256];
            for (var i = 0; i < value.Length; i++)
            {
                value[i] = (byte)i;
            }

            cacheClient.Set(key, value);
            var resultValue = cacheClient.Get<byte[]>(key);
            foreach (var item in resultValue)
                Console.WriteLine(item);
            Console.WriteLine("写入完成");
            Console.ReadLine();
        }

        public void TestSet()
        {
            var client = cacheClient as RedisClient;
            IRedisTypedClient<UserInfoDto> typeClient = client.As<UserInfoDto>();
            typeClient.DeleteAll();
            IRedisSet<UserInfoDto> set = typeClient.Sets["TestSet"];

            var list = GetUsers();

            set.Clear();
            list.ForEach(x => set.Add(x));

            Console.WriteLine("写入完成");
            Console.ReadLine();

            var item = set.First(t => t.Id == 1);
            Console.WriteLine(item.ToString());
            Console.ReadLine();
        }

        public void TestList()
        {
            var client = cacheClient as RedisClient;
            IRedisTypedClient<UserInfoDto> typeClient = client.As<UserInfoDto>();
            IRedisList<UserInfoDto> list = typeClient.Lists["TestList"];

            var listUser = GetUsers();
            list.Clear();
            listUser.ForEach(t => list.Add(t));

            Console.WriteLine(list.First(t => t.StaffId == "StaffId_8"));
            Console.ReadLine();
        }


        public void TestHash()
        {
            var client = cacheClient as RedisClient;
            IRedisTypedClient<UserInfoDto> typeClient = client.As<UserInfoDto>();

            var hash = client.Hashes["TestHash"];

            Dictionary<string, string>  stringMap = new Dictionary<string, string> {
                 {"one","a"}, {"two","b"}, {"three","c"}, {"four","d"}
             };

            stringMap.ForEach(hash.Add);
        }


        public List<UserInfoDto> GetUsers()
        {
            List<UserInfoDto> list = new List<UserInfoDto>();
            DateTime dt = DateTime.Now;
            for (int i = 0; i < 10; i++)
            {
                list.Add(new UserInfoDto()
                {
                    Id = i,
                    LastLoginTime = dt,
                    Password = "password" + i.ToString(),
                    StaffId = "StaffId_" + i.ToString(),
                    StaffName = "StaffName_" + i.ToString()
                });
            }
            return list;
        }


    }
}
