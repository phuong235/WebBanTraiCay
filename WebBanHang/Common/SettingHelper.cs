using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHang.Context;
using WebBanHang.Models;

namespace WebBanHang.Common
{
    public class SettingHelper
    {
        private static  WebBanHangEntities db = new WebBanHangEntities();
        public static string GetValue(string key)
        {
            var item = db.SystemSettings.SingleOrDefault(x => x.SettingKey.Equals(key));
            if (item != null)
            {
                return item.SettingValue;
            }
            return "";
        }
    }
}