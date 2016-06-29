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
            var sortSet = typeClient.SortedSets["TestSet1"];

            var list = GetUsers();

            set.Clear();
            list.ForEach(x => set.Add(x));
            list.ForEach(x => sortSet.Add(x));

            Console.WriteLine("写入完成");
            Console.ReadLine();

            var item = sortSet.Where(t => t.StaffId == "StaffId_7").ToList();

            //var result =  set.GetAll();
            
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

            var item = list.First(t => t.StaffId == "StaffId_8");

            Console.WriteLine(item);
            Console.ReadLine();
        }


        public void TestHash()
        {
            var client = cacheClient as RedisClient;
            IRedisTypedClient<UserInfoDto> typeClient = client.As<UserInfoDto>();

            var list = GetUsers();

            var hash = client.Hashes["TestHash"];


            Dictionary<string, string> stringMap = new Dictionary<string, string>();
             //   new Dictionary<string, string> {
             //    {,"a"}, {"two","b"}, {"three","c"}, {"four","d"}
             //};
            foreach (var item in list)
            {
                stringMap[item.Id.ToString()] = item.ToString();
            }

            stringMap.ForEach(hash.Add);


            //var result = typeClient.GetById('1');

        }


        public List<UserInfoDto> GetUsers()
        {
            List<UserInfoDto> list = new List<UserInfoDto>();
            DateTime dt = DateTime.Now;
            for (int i = 0; i < 10000; i++)
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
