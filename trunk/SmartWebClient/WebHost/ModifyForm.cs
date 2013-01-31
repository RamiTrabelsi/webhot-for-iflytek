using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebHost
{
    public partial class ModifyForm : Form
    {
        public event ActHandler<Reminder> SubmintModified;
        private bool _isNeedSave = false;

        public ModifyForm()
        {
            InitializeComponent();
        }

        public ModifyForm(Reminder reminder)
            : this()
        {
            this.txtAddress.Text = reminder.Address;
            this.txtContent.Text = reminder.Content;
            this.txtTitle.Text = reminder.Title;
            this.dateTimePicker1.Text = reminder.BeginTime.ToString();
            this.dateTimePicker2.Text = reminder.EndTime.ToString();
            foreach (RadioButton rbBtn in groupBox1.Controls)
            {
                if (string.Equals(rbBtn.Tag, reminder.Type))
                {
                    rbBtn.Checked = true;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SubmitChange();
        }
          
        private void controls_Validating(object sender, CancelEventArgs e)
        {
            _isNeedSave = true;
        }

        private void ModifyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isNeedSave)
            {
                if (MessageBox.Show("已经更改,是否提交?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
              {
                  SubmitChange();
              } 
            }
        }

        /// <summary>
        /// 提交更改
        /// </summary>
        private void SubmitChange()
        {
            if (SubmintModified != null)
            {
                var reminder = ReminderHelper.CreateNewReminder(this.groupBox1, dateTimePicker1, dateTimePicker2, txtTitle, txtContent, txtAddress);

                SubmintModified(reminder);

                _isNeedSave = false;
            }
        }
    } 
}
