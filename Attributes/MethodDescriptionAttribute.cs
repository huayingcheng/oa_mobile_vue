using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZhejiangGovernmentDingTalkServer.Attributes
{
    public class MethodDescriptionAttribute : Attribute
    {
        public string Description { get; set; }
        public string MethodName { get; set; }
    }
}