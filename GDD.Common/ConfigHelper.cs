using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Xml;

namespace GDD.Common
{
    public static class ConfigHelper
    {
        #region 修改config文件  
        /// <summary>  
        /// 获取AppSetting节点) 
        /// </summary>  
        /// <param name="key">键</param>  
        /// <param name="value">要修改成的值</param>  
        public static string GetAppSettingValue(string key)
        {
            return WebConfigurationManager.AppSettings[key];
        }

        /// <summary>  
        /// 设置AppSetting节点
        /// </summary>  
        /// <param name="name">键</param>  
        /// <param name="value">要修改成的值</param>  
        public static void SetAppSettingValue(string key, string value)
        {
            WebConfigurationManager.AppSettings.Set(key, value);
        }
        #endregion
    }
}
