using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class T_UserDAL
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public T_User DoLogin(Dictionary<string, string> dic)
        {
            WebDbEntities db = new WebDbEntities();
            string accountName = dic["AccountName"];
            string pwd = dic["Pwd"];
            T_User user = db.T_User.Where(x => x.AccountName == accountName).Where(a => a.Pwd == pwd).SingleOrDefault();
            return user;
        }
    }
}
