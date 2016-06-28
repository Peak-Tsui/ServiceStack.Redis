using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServiceStackDemo
{
    public class UserInfoDto
    {
        public int Id { get; set; }
        public string StaffId { get; set; }
        public string StaffName { get; set; }
        public string Password { get; set; }
        public System.DateTime LastLoginTime { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
