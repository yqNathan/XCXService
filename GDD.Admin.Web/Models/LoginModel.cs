using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDD.Admin.Web.Models
{
    public class LoginModel
    {
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; }

        [Display(Name = "密码")]
        [Required(ErrorMessage = "密码不能为空")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^\w+$", ErrorMessage = "密码格式有误,只能是字母数字或者下划线")]
        public string Password { get; set; }


        [Display(Name = "记住登陆?")]
        public bool RememberMe { get; set; }

        public string Login()
        {
            //该方法应从数据库中对比用户名和密码,并取得用户权限列表
            //这里为了简单直接对比字符串并返回权限列表,返回NULL则说明用户名或者密码错误
            //权限列表即为用,分割的权限名称
            string result = null;

            if (this.UserName == "guest" & this.Password == "guest")
                result = "Add";

            if (this.UserName == "admin" & this.Password == "admin")
                result = "Add,Edit";

            return result;
        }
    }
}