using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace WebHost
{
    public partial class StartTaskForm : Form
    {
        private List<Reminder> reminderList = new List<Reminder>();

        public StartTaskForm()
        {
            InitializeComponent();
        }

        private void StartTaskForm_Load(object sender, EventArgs e)
        {
            //加载选项,从服务器还是从本地

            //多线程起步
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var reminder = ReminderHelper.CreateNewReminder(this.groupBox1, dateTimePicker1, dateTimePicker2, txtTitle, txtContent, txtAddress);

            listItems.Items.Add(reminder);

        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (listItems.SelectedItem is Reminder)
            {
                var reminder = (Reminder)listItems.SelectedItem;
                ModifyForm form = new ModifyForm(reminder);
                form.SubmintModified += new ActHandler<Reminder>(form_SubmintModified);
                form.ShowDialog();
            }
        }

        private void form_SubmintModified(Reminder reminder)
        {
            // 提交数据库还是提交到服务器
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listItems.SelectedItem is Reminder)
            {
                if (MessageBox.Show("是否删除选中的选项?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    listItems.Items.Remove(listItems.SelectedItem);
            }

        }
    }
}
