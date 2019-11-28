using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZhejiangGovernmentDingTalkServer.Entities.Home
{
    public class Config
    {
        public string Name { get; set; }
        public List<Module> Content { get; set; }
    }
}