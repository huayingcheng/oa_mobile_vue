using Gisquest.Data.Client;
using System;
using System.Text;
using System.Web.Services;
using ZhejiangGovernmentDingTalkServer.Attributes;

namespace ZhejiangGovernmentDingTalkServer.Webservices
{
    /// <summary>
    /// HomeInfoCaculationMethods 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class HomeInfoCaculationMethods : WebService
    {
        [MethodDescription(Description = "获取未读邮件数量",MethodName = "GetNotReadEmailNumber")]
        [WebMethod]
        public string GetNotReadEmailNumber(string userId)
        {
            GisqDataSource _ds = null;

            try
            {
                _ds = GisqFrameFound.AppCode.AppDataSource.CreateOADatasource();
                var _sb = new StringBuilder();
                _sb.Append("Select count(*)");
                _sb.Append(" From office.T_Email a,office.T_EMail_Rec_Stf b");
                _sb.AppendFormat(" Where b.EmailFolderID =0 AND a.Mail_ID = b.Mail_ID AND b.Read_State = '1' AND b.Mail_State = '2' AND b.USER_ID = '{0}'", userId);
                return Verifier.VerifyString(_ds.ExecuteScalar(_sb.ToString()));
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (_ds!=null){
                    _ds.Close();
                    _ds.Dispose();
                }
            }
        }
    }
}
