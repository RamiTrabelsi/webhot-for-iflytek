using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace WebHost
{
    class ReminderHelper
    {

        /// <summary>
        /// 创建新的提醒实例
        /// </summary>
        /// <param name="groupBox1"></param>
        /// <param name="dateTimePicker1"></param>
        /// <param name="dateTimePicker2"></param>
        /// <param name="txtTitle"></param>
        /// <param name="txtContent"></param>
        /// <param name="txtAddress"></param>
        /// <returns></returns>
        public static Reminder CreateNewReminder(GroupBox groupBox1, DateTimePicker dateTimePicker1, DateTimePicker dateTimePicker2, TextBox txtTitle, TextBox txtContent, TextBox txtAddress)
        {
           
            string type = string.Empty;

            foreach (RadioButton ctrl in groupBox1.Controls)
            {
                if (ctrl.Checked)
                {
                    type = ctrl.Tag.ToString();
                    break;
                }
            }

            var reminder = new Reminder(DateTime.Parse(dateTimePicker1.Text),
                                                  DateTime.Parse(dateTimePicker2.Text),
                                                 (RemindType)Enum.Parse(typeof(RemindType), type, false),
                                                  txtTitle.Text,
                                                  txtContent.Text,
                                                  txtAddress.Text);
            return reminder;
        }
    }
}
