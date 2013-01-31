using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace WebHost
{
    public partial class FormWeb : Form
    {

        #region Methods

        public FormWeb()
        {
            InitializeComponent();

            LoginHelper.LoginDataIni(this);
            AppIni();
        }

        /// <summary>
        /// 应用程序初始化
        /// </summary>
        private void AppIni()
        {
            webBrowser1.StatusTextChanged += webBrowser1_StatusTextChanged;
            this.FormClosing += FormWeb_FormClosing;
            webBrowser1.NewWindow += webBrowser1_NewWindow;

            BootStartupToolStripMenuItem.Checked = true;

            LoginHelper.GoToUrl();
        }

        #endregion

        #region Events

        private void FormWeb_MinimumSizeChanged(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void FormWeb_SizeChanged(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;  //不显示在系统任务栏
            notifyIcon1.Visible = true;  //托盘图标可见

        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;  //显示在系统任务栏
            this.WindowState = FormWindowState.Normal;  //还原窗体
            WinAPIHelper.SetForegroundWindow(this.Handle);
            //notifyIcon1.Visible = false;  //托盘图标隐藏

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                this.Visible = true;
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginHelper.Logout();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!this.Visible)
            {
                this.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginHelper.GoToUrl(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoginHelper.ReLogin();
        }

        void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// 页面跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webBrowser1_StatusTextChanged(object sender, EventArgs e)
        {
            //当前页面的响应状态
            this.toolStripStatusLabel1.Text = string.Format("当前状态 : {0} . 当前登陆用户 : {1} ,切换用户请更改配置信息...", this.webBrowser1.StatusText, LoginHelper.UserName);

            if (LoginHelper.CheckBrowserFinish())
            {
                if (LoginHelper.CheckFormFillState())
                {
                    switch (LoginHelper.LoginMode)
                    {
                        case UserCmdMode.Login:
                            LoginHelper.FillFormInfos();
                            LoginHelper.LoginModeResponse(UserCmdMode.Login);
                            break;
                        case UserCmdMode.ReLogin:
                            LoginHelper.LoginModeResponse(UserCmdMode.Logout);
                            Thread.Sleep(700);
                            LoginHelper.FillFormInfos();
                            LoginHelper.LoginModeResponse(UserCmdMode.Login);
                            //this.Visible = false;
                            break;
                        case UserCmdMode.Logout:
                            LoginHelper.LoginModeResponse(UserCmdMode.Logout);
                            Thread.Sleep(700);
                            MessageBox.Show("已经登出成功!");
                            //this.Visible = true;
                            return;
                    }

                    Utils.ReleaseMemory();
                }
            }
        }

        private void FormWeb_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (MessageBox.Show("   是否退出登陆客户端?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel = true;
                this.Visible = false;
            }
        }

        private void BootToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utils.StartEXEAfterBoot(sender);
        }

        private void ReleaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utils.ReleaseMemory();
            MessageBox.Show("缓存清理成功.", "标题", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void StartTastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartTaskForm form = new StartTaskForm();
            form.ShowDialog();
        }

        private void ShutDownTastToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoginHelper.Logout();
        }

        #endregion

    }
}
