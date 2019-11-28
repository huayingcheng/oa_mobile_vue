using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Services;


namespace ZhejiangGovernmentDingTalkServer.Config.Webservices.Home
{
    /// <summary>
    /// Modules 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class Modules : WebService
    {
        [WebMethod]
        public void GetConfigs()
        {
            List<Entities.Home.Config> configs = new List<Entities.Home.Config>();

            string path = AppDomain.CurrentDomain.BaseDirectory + @"Extensions\ZhejiangGovernmentDingTalkServer\Config\Home\modules";
            DirectoryInfo root = new DirectoryInfo(path);

            foreach (FileInfo f in root.GetFiles())
            {
                if (f.Name.IndexOf("base.json") > -1 || f.Name.IndexOf("readme.md") > -1)
                {
                    continue;
                }
                configs.Add(new Entities.Home.Config
                {
                    Name = f.Name,
                    Content = JsonConvert.DeserializeObject<List<Entities.Home.Module>>(File.ReadAllText(f.FullName))
                });
            }

            Context.Response.Charset = "UTF-8"; //设置字符集类型  
            Context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            Context.Response.Write(JsonConvert.SerializeObject(configs));
            Context.Response.End();
        }

        [WebMethod(Description = "获取服务端所有的图标")]
        public void GetIcons()
        {
            List<string> icons = new List<string>();
            string path = AppDomain.CurrentDomain.BaseDirectory + @"Extensions\ZhejiangGovernmentDingTalkServer\Img\Home\Modules";
            DirectoryInfo root = new DirectoryInfo(path);

            foreach (FileInfo f in root.GetFiles())
            {
                icons.Add("Extensions/ZhejiangGovernmentDingTalkServer/Img/Home/Modules/" + f.Name);
            }

            Context.Response.Charset = "UTF-8"; //设置字符集类型  
            Context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            Context.Response.Write(JsonConvert.SerializeObject(icons));
            Context.Response.End();
        }

        [WebMethod(Description = "获取计算右上角小标的方法")]
        public void GetInfoMethods()
        {
            List<Entities.Home.Method> methodsList = new List<Entities.Home.Method>();

            Type methods = typeof(ZhejiangGovernmentDingTalkServer.Webservices.HomeInfoCaculationMethods);

            Attributes.MethodDescriptionAttribute ma = null;
            foreach (System.Reflection.MethodInfo methodInfo in methods.GetMethods())
            {
                var attributes = methodInfo.GetCustomAttributes(typeof(Attributes.MethodDescriptionAttribute), true);
                if (attributes.Length > 0)
                {
                    ma = (Attributes.MethodDescriptionAttribute)attributes[0];
                    methodsList.Add(new Entities.Home.Method()
                    {
                        Description = ma.Description,
                        MethodName = ma.MethodName
                    });
                }
            }
            Context.Response.Charset = "UTF-8"; //设置字符集类型  
            Context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            Context.Response.Write(JsonConvert.SerializeObject(methodsList));
            Context.Response.End();
        }

        [WebMethod(Description = "保存配置")]
        public void SaveConfig(string configName,string configContent)
        {
            
            
            string result = "";

            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + @"Extensions\ZhejiangGovernmentDingTalkServer\Config\Home\modules\" + configName;
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                File.WriteAllText(path, configContent);
                result = "保存成功";

                Common.Common.handleResult(Context, result, false);
            }
            catch (Exception ex)
            {
                result = "保存失败：" + ex.Message;
                Common.Common.handleResult(Context, result, true);
            }
        }

        [WebMethod(Description = "上传配置")]
        public void UploadConfig()
        {
            try {
                Context.Request.Files[0].SaveAs(AppDomain.CurrentDomain.BaseDirectory + @"Extensions\ZhejiangGovernmentDingTalkServer\Config\Home\modules\" + Context.Request.Files[0].FileName);

            }catch(Exception ex)
            {

            }

            Common.Common.handleResult(Context, "上传成功", false);
        }

        [WebMethod(Description = "增加一个新的配置")]
        public void AddNewConfig(string configName)
        {
            var baseFile = AppDomain.CurrentDomain.BaseDirectory + @"Extensions\ZhejiangGovernmentDingTalkServer\Config\Home\modules\base.json";
            var newFile = AppDomain.CurrentDomain.BaseDirectory + @"Extensions\ZhejiangGovernmentDingTalkServer\Config\Home\modules\" + configName + ".json";
            File.Copy(baseFile, newFile);
            Common.Common.handleResult(Context, "增加配置成功", false);
        }
    }
}
