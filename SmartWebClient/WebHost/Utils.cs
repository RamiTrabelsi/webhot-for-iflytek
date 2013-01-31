using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WebHost
{
    class Utils
    {
        /// <summary>
        /// 开机启动程序
        /// </summary>
        /// <param name="sender"></param>
        public static void StartEXEAfterBoot(object sender)
        {
            ToolStripMenuItem startMenu = sender as ToolStripMenuItem;
            string strName = Application.ExecutablePath;
            string strnewName = strName.Substring(strName.LastIndexOf("\\") + 1);
            if (startMenu.Checked)
            {
                //修改注册表，使程序开机时不自动执行。  
                startMenu.Checked = false;
                Microsoft.Win32.RegistryKey Rkey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                Rkey.DeleteValue(strnewName, false);
                MessageBox.Show("程序设置完成,取消开机启动.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                startMenu.Checked = true;
                if (!File.Exists(strName))//指定文件是否存在  
                    return;
                Microsoft.Win32.RegistryKey Rkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (Rkey == null)
                    Rkey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                Rkey.SetValue(strnewName, strName);//修改注册表，使程序开机时自动执行。  
                MessageBox.Show("程序设置完成，重新启动计算机后即可生效！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 用API释放内存
        /// </summary>
        public static void ReleaseMemory()
        {
            IntPtr pHandle = WinAPIHelper.GetCurrentProcess();
            WinAPIHelper.SetProcessWorkingSetSize(pHandle, -1, -1);
        }

    }

    public delegate void ActHandler<T>(T objs);
}
