using BLL;
using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class LoginController : BaseHandler
    {
        //
        // GET: /Login/

        public ActionResult Login()
        {
            //如果曾经记录了cooke信息，则从cookie信息中加载
            if (ExistCookie("userName"))
            {
                ViewData["userName"] = GetCookie("userName");
            }
            if (ExistCookie("pwd"))
            {
                ViewData["pwd"] = GetCookie("pwd");
            }
            if (ExistCookie("checked"))
            {
                ViewData["checked"] = GetCookie("checked");
            }
            //如果是账号密码错误重定向到登录页面,则给出提示消息
            if (!string.IsNullOrEmpty(Request["verify"]))
            {
                Response.Write("<script> var resultmsg='用户名或密码错误'</script>");
            }
            else
            {
                Response.Write("<script> var resultmsg=''</script>");
            }
            return View();
        }

        /// <summary>
        /// 登录操作
        /// </summary>
        /// <returns></returns>
        public ActionResult DoLogin()
        {
            //获取表单提交的信息
            var check = Request.Form["rememberMe"];
            string name = Request.Form["name"].ToString();
            string pwd = Request.Form["pwd"].ToString();
            //创建参数字典
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("AccountName", name);
            dic.Add("Pwd", Encrypt.Md5(pwd));
            T_UserBLL bll = new T_UserBLL();
            T_User user = bll.DoLogin(dic);
            if (user != null)
            {//登录成功
                Session["LoginUser"] = user;

                if (check == "true")
                {//如果用户选择了记住密码，则记录账号和密码
                    SetCookie("userName", name);
                    SetCookie("pwd", pwd);
                    SetCookie("checked", check);
                }
                else
                {//如果用户选择了不记住密码，则删除cookie
                    DelCookie("userName");
                    DelCookie("pwd");
                    DelCookie("checked");

                }
                return Redirect("/Home/Index");
            }
            else
            {
                return Redirect("/Login/Login?verify=false");
            }

        }

    }
}
