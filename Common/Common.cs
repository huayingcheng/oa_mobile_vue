using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZhejiangGovernmentDingTalkServer.Common
{
    public class Common
    {
        public static void handleResult(HttpContext Context, Object result, bool isError)
        {
            Entities.Result res = new Entities.Result();

            if (isError)
            {
                res.ErrorCount = 1;
                res.ErrorDetail = result.ToString();
            }
            else
            {
                res.Data = result;
                res.ErrorCount = 0;
                res.ErrorDetail = "";
            }

            Context.Response.Charset = "UTF-8"; //设置字符集类型  
            Context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            Context.Response.Write(JsonConvert.SerializeObject(res));
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}