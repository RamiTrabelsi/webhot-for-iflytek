using System;
using System.Configuration;
using System.Windows.Forms;

namespace WebHost
{
    class LoginHelper
    {
        #region Fields

        private static FormWeb _form = null;

        /// <summary>
        /// 默认登录地址
        /// </summary>
        public static string LoginUrl { get; private set; }

        public static string UserName { get; private set; }

        public static string PassWord { get; private set; }

        public static bool IsSaveInfo { get; private set; }

        public static string TxtUserName { get; private set; }

        public static string TxtPwd { get; private set; }

        public static string CbxIsSave { get; private set; }
        /// <summary>
        /// 登录的js函数
        /// </summary>
        public static string LoginInJsMethod { get; private set; }
        /// <summary>
        /// 登出的js函数
        /// </summary>
        public static string LoginOutJsMethod { get; private set; }

        private static UserCmdMode _loginMode = UserCmdMode.Login;

        /// <summary>
        /// 需要进行登录的模式
        /// </summary>
        public static UserCmdMode LoginMode
        {
            get { return _loginMode; }
            set { _loginMode = value; }
        }
        #endregion

        /// <summary>
        /// 登录数据初始化
        /// </summary>
        public static void LoginDataIni(FormWeb form)
        {
            try
            {
                _form = form;

                UserName = ConfigurationManager.AppSettings["UserName"].ToString();
                PassWord = ConfigurationManager.AppSettings["PassWord"].ToString();
                IsSaveInfo = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSaveInfo"]);
                TxtUserName = ConfigurationManager.AppSettings["TxtUserName"].ToString();
                TxtPwd = ConfigurationManager.AppSettings["TxtPwd"].ToString();
                CbxIsSave = ConfigurationManager.AppSettings["CbxIsSave"].ToString();
                LoginUrl = ConfigurationManager.AppSettings["LoginUrl"].ToString();
                LoginInJsMethod = ConfigurationManager.AppSettings["LoginInJsMethod"].ToString();
                LoginOutJsMethod = ConfigurationManager.AppSettings["LoginOutJsMethod"].ToString();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// 导向URL
        /// </summary> 
        /// <param name="url"></param>
        public static void GoToUrl(string url = null)
        {
            string urlInput = url == null ? LoginHelper.LoginUrl : url;
            _form.textBox1.Text = urlInput;
            _form.webBrowser1.Url = new Uri(urlInput);
        }

        /// <summary>
        /// 重新登录
        /// </summary>
        public static void ReLogin()
        {
            LoginMode = UserCmdMode.ReLogin;
            GoToUrl();
        }

        /// <summary>
        /// 登出
        /// </summary>
        public static void Logout()
        {
            LoginMode = UserCmdMode.Logout;
            GoToUrl();
        }

        /// <summary>
        /// 对网页进行响应登陆模式操作
        /// </summary>
        /// <param name="isLogin"></param>
        public static void LoginModeResponse(UserCmdMode mode)
        {
            switch (mode)
            {
                case UserCmdMode.Login:
                    _form.webBrowser1.Document.InvokeScript(LoginHelper.LoginInJsMethod);
                    break;
                case UserCmdMode.Logout:
                    _form.webBrowser1.Document.InvokeScript(LoginHelper.LoginOutJsMethod);
                    break;
            }
        }

        /// <summary>
        /// 填充表单
        /// </summary>
        public static void FillFormInfos()
        {
            HtmlDocument doc = _form.webBrowser1.Document;
            HtmlElement elemuserName = doc.All[TxtUserName];
            HtmlElement elemuserPWD = doc.All[TxtPwd];
            HtmlElement elemuserSaveID = doc.All[CbxIsSave];

            elemuserName.SetAttribute("value", LoginHelper.UserName);
            elemuserPWD.SetAttribute("value", LoginHelper.PassWord);
            if (IsSaveInfo)
            {
                elemuserSaveID.SetAttribute("value", "on");
            }
        }


        /// <summary>
        /// 检查是否表单元素填写完毕
        /// </summary>
        /// <returns></returns>
        public static bool CheckFormFillState()
        {
            return _form.webBrowser1.Document.All[TxtUserName] != null &&
                   string.IsNullOrWhiteSpace(_form.webBrowser1.Document.All[TxtUserName].GetAttribute("value"));
        }

        /// <summary>
        /// 检查浏览器状态是否完成
        /// </summary>
        /// <returns></returns>
        public static bool CheckBrowserFinish()
        {
            return (string.Equals(_form.webBrowser1.StatusText, "完成") || (string.Equals(_form.webBrowser1.StatusText, "Finish")));
        }

    }


    #region 用户登录模式

    /// <summary>
    /// 用户登录模式
    /// </summary>
    public enum UserCmdMode
    {
        /// <summary>
        /// 登录
        /// </summary>
        Login,
        /// <summary>
        /// 登出
        /// </summary>
        Logout,
        /// <summary>
        /// 重新登录
        /// </summary>
        ReLogin
    }

    #endregion
}
