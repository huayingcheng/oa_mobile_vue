using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZhejiangGovernmentDingTalkServer.Entities.Home
{
    [JsonObject(Newtonsoft.Json.MemberSerialization.OptOut)]
    public class Module
    {
        /// <summary>
        /// 元素的key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 图标显示的文字
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 图标内的图片
        /// </summary>
        public string ImgSrc { get; set; }
        /// <summary>
        /// 显示的时候的背景色
        /// </summary>
        public string Bg { get; set; }
        /// <summary>
        /// 右上角是否要显示信息
        /// </summary>
        public bool IfInfo { get; set; }
        /// <summary>
        /// 右上角显示的文字
        /// </summary>
        public string Info { get; set; }
        /// <summary>
        /// 计算info的方法名,要求该方法是个静态方法，参数是userId
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// 路由名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// 路由参数
        /// </summary>
        public Object Query { get; set; }
    }
}