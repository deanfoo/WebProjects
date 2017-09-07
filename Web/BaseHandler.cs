using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web
{
    public class BaseHandler : Controller
    {
        //全局当前登录对象
        protected T_User CurrentUser { get; set; }
        // 全局Action执行前的验证操作：类似于AoP的思想，面向切面编程
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            //如果不是登录操作,就进行Session验证
            if (((filterContext.ActionDescriptor).ControllerDescriptor).ControllerName != "Login")
            {
                if (Session["LoginUser"] == null)
                {
                    //如果session不存在,就是没有登录,跳转到登录页面
                    filterContext.Result = new RedirectResult("/Login/Login");
                }
                else
                {
                    //如果登录了,就记录下登录对象
                    CurrentUser = Session["LoginUser"] as T_User;
                }
            }
        }

        //设置Cookie
        public void SetCookie(string name, string val)
        {
            HttpCookie cookie = new HttpCookie(name);
            cookie.Value = val;
            HttpContext.Response.Cookies.Add(cookie);
        }

        //读取Cookie
        public string GetCookie(string name)
        {
            return HttpContext.Request.Cookies[name].Value;
        }

        //判断Cookie是否存在
        public bool ExistCookie(string name)
        {
            if (HttpContext.Request.Cookies[name] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
            {

            }
        }

        //删除Cookie
        public void DelCookie(string name)
        {

            HttpCookie aCookie = new HttpCookie(name);
            aCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(aCookie);
        }

    }
}