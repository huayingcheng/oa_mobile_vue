using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZhejiangGovernmentDingTalkServer.Entities
{
    public class Result
    {
        public Object Data { get; set; }
        public int ErrorCount { get; set; }
        public string ErrorDetail { get; set; }
    }
}