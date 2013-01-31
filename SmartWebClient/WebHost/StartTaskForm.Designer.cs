namespace WebHost
{
    partial class StartTaskForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAdd = new System.Windows.Forms.Button();
            this.listItems = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnJustOnce = new System.Windows.Forms.RadioButton();
            this.rbtnByDay = new System.Windows.Forms.RadioButton();
            this.rbtnByWeek = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.rbtnForever = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(333, 130);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(86, 76);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "增加提醒 >>";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // listItems
            // 
            this.listItems.FormattingEnabled = true;
            this.listItems.ItemHeight = 12;
            this.listItems.Location = new System.Drawing.Point(436, 48);
            this.listItems.Name = "listItems";
            this.listItems.ScrollAlwaysVisible = true;
            this.listItems.Size = new System.Drawing.Size(372, 292);
            this.listItems.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "时间 : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "标题 : ";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(73, 185);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(200, 21);
            this.txtTitle.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 222);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "地点 : ";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(73, 219);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(200, 21);
            this.txtAddress.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 247);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "内容 : ";
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(73, 247);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(200, 140);
            this.txtContent.TabIndex = 9;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(132, 31);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(141, 21);
            this.dateTimePicker1.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(71, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "From : ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(71, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "To   : ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(132, 58);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(141, 21);
            this.dateTimePicker2.TabIndex = 13;
            this.dateTimePicker2.Value = new System.DateTime(2013, 1, 30, 0, 0, 0, 0);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnForever);
            this.groupBox1.Controls.Add(this.rbtnJustOnce);
            this.groupBox1.Controls.Add(this.rbtnByDay);
            this.groupBox1.Controls.Add(this.rbtnByWeek);
            this.groupBox1.Location = new System.Drawing.Point(14, 89);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(301, 73);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "周期";
            // 
            // rbtnJustOnce
            // 
            this.rbtnJustOnce.AutoSize = true;
            this.rbtnJustOnce.Location = new System.Drawing.Point(168, 39);
            this.rbtnJustOnce.Name = "rbtnJustOnce";
            this.rbtnJustOnce.Size = new System.Drawing.Size(59, 16);
            this.rbtnJustOnce.TabIndex = 21;
            this.rbtnJustOnce.Tag = "2";
            this.rbtnJustOnce.Text = "就一次";
            this.rbtnJustOnce.UseVisualStyleBackColor = true;
            // 
            // rbtnByDay
            // 
            this.rbtnByDay.AutoSize = true;
            this.rbtnByDay.Checked = true;
            this.rbtnByDay.Location = new System.Drawing.Point(23, 39);
            this.rbtnByDay.Name = "rbtnByDay";
            this.rbtnByDay.Size = new System.Drawing.Size(47, 16);
            this.rbtnByDay.TabIndex = 20;
            this.rbtnByDay.TabStop = true;
            this.rbtnByDay.Tag = "0";
            this.rbtnByDay.Text = "按天";
            this.rbtnByDay.UseVisualStyleBackColor = true;
            // 
            // rbtnByWeek
            // 
            this.rbtnByWeek.AutoSize = true;
            this.rbtnByWeek.Location = new System.Drawing.Point(90, 39);
            this.rbtnByWeek.Name = "rbtnByWeek";
            this.rbtnByWeek.Size = new System.Drawing.Size(59, 16);
            this.rbtnByWeek.TabIndex = 19;
            this.rbtnByWeek.Tag = "1";
            this.rbtnByWeek.Text = "按星期";
            this.rbtnByWeek.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(434, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "记录 : ";
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(532, 354);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(75, 33);
            this.btnModify.TabIndex = 17;
            this.btnModify.Text = "修改";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(675, 354);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 33);
            this.btnDelete.TabIndex = 18;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // rbtnForever
            // 
            this.rbtnForever.AutoSize = true;
            this.rbtnForever.Location = new System.Drawing.Point(246, 39);
            this.rbtnForever.Name = "rbtnForever";
            this.rbtnForever.Size = new System.Drawing.Size(47, 16);
            this.rbtnForever.TabIndex = 22;
            this.rbtnForever.TabStop = true;
            this.rbtnForever.Tag = "3";
            this.rbtnForever.Text = "永远";
            this.rbtnForever.UseVisualStyleBackColor = true;
            // 
            // StartTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 399);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listItems);
            this.Controls.Add(this.btnAdd);
            this.Name = "StartTaskForm";
            this.Text = "StartTaskForm";
            this.Load += new System.EventHandler(this.StartTaskForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox listItems;
        private System.Windows.Forms.RadioButton rbtnJustOnce;
        private System.Windows.Forms.RadioButton rbtnByDay;
        private System.Windows.Forms.RadioButton rbtnByWeek;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.RadioButton rbtnForever;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox txtTitle;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox txtAddress;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox txtContent;
        internal System.Windows.Forms.DateTimePicker dateTimePicker1;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.DateTimePicker dateTimePicker2;
        internal System.Windows.Forms.GroupBox groupBox1;
    }
}