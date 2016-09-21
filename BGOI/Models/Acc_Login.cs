using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class Acc_Login
    {
        //[Required]  
        //[Display(Name = "用户名")]  
        public string UserName { get; set; }

        //[Required]  
        //[DataType(DataType.Password)]  
        //[Display(Name = "密码")]  
        public string Password { get; set; }

        //[Display(Name = "下次自动登陆")]  
        public bool RememberMe { get; set; }
    }
}
