using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Services;
using System.Web.Services;
using ZhejiangGovernmentDingTalkServer.Entities;
using ZhejiangGovernmentDingTalkServer.Entities.Home;

namespace ZhejiangGovernmentDingTalkServer.Webservices
{
    /// <summary>
    /// Home 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class Home : WebService
    {
        [WebMethod(Description = "获取home页面上显示的模块数据")]
        public void GetModules(long userId,string address)
        {
            //0.初始化返回结果
            var result = new Result();

            try
            {
                //1.根据address读取配置文件
                var _path = AppDomain.CurrentDomain.BaseDirectory + @"Extensions\ZhejiangGovernmentDingTalkServer\Config\Home\modules\" + address + ".json";
                var dataStr = File.ReadAllText(_path);
                List<Module> modules = JsonConvert.DeserializeObject<List<Module>>(dataStr);
                
                Random random = new Random();
                foreach (Module m in modules)
                {
                    m.Key = Guid.NewGuid().ToString();
                    if (!string.IsNullOrEmpty(m.MethodName))
                    {
                        var assembly = System.Reflection.Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory+ @"bin\ZhejiangGovernmentDingTalkServer.dll");
                        var type = assembly.GetType("ZhejiangGovernmentDingTalkServer.Webservices.HomeInfoCaculationMethods");
                        var method = type.GetMethod(m.MethodName);
                        object returnValue = method.Invoke(null, new object[] { userId });

                        m.Info = returnValue.ToString();
                    }
                }
                result.Data = modules;
            }
            catch (Exception ex)
            {
                result.ErrorCount = 1;
                result.ErrorDetail = ex.Message;
            }
            Context.Response.Charset = "UTF-8"; //设置字符集类型  
            Context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            Context.Response.Write(JsonConvert.SerializeObject(result));
            Context.Response.End();
        }
    }
}
