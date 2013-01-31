namespace WebHost
{
    partial class ModifyForm
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
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnForever = new System.Windows.Forms.RadioButton();
            this.rbtnJustOnce = new System.Windows.Forms.RadioButton();
            this.rbtnByDay = new System.Windows.Forms.RadioButton();
            this.rbtnByWeek = new System.Windows.Forms.RadioButton();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(120, 403);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnForever);
            this.groupBox1.Controls.Add(this.rbtnJustOnce);
            this.groupBox1.Controls.Add(this.rbtnByDay);
            this.groupBox1.Controls.Add(this.rbtnByWeek);
            this.groupBox1.Location = new System.Drawing.Point(16, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(301, 73);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "周期";
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
            this.rbtnForever.Validating += new System.ComponentModel.CancelEventHandler(this.controls_Validating);
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
            this.rbtnJustOnce.Validating += new System.ComponentModel.CancelEventHandler(this.controls_Validating);
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
            this.rbtnByDay.Validating += new System.ComponentModel.CancelEventHandler(this.controls_Validating);
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
            this.rbtnByWeek.Validating += new System.ComponentModel.CancelEventHandler(this.controls_Validating);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(134, 49);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(141, 21);
            this.dateTimePicker2.TabIndex = 26;
            this.dateTimePicker2.Value = new System.DateTime(2013, 1, 30, 0, 0, 0, 0);
            this.dateTimePicker2.Validating += new System.ComponentModel.CancelEventHandler(this.controls_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(73, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 25;
            this.label6.Text = "To   : ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(73, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 24;
            this.label5.Text = "From : ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(134, 22);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(141, 21);
            this.dateTimePicker1.TabIndex = 23;
            this.dateTimePicker1.Validating += new System.ComponentModel.CancelEventHandler(this.controls_Validating);
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(75, 238);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(200, 140);
            this.txtContent.TabIndex = 22;
            this.txtContent.Validating += new System.ComponentModel.CancelEventHandler(this.controls_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 238);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 21;
            this.label4.Text = "内容 : ";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(75, 210);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(200, 21);
            this.txtAddress.TabIndex = 20;
            this.txtAddress.Validating += new System.ComponentModel.CancelEventHandler(this.controls_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 213);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "地点 : ";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(75, 176);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(200, 21);
            this.txtTitle.TabIndex = 18;
            this.txtTitle.Validating += new System.ComponentModel.CancelEventHandler(this.controls_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "标题 : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "时间 : ";
            // 
            // ModifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 438);
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
            this.Controls.Add(this.btnSave);
            this.Name = "ModifyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ModifyForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModifyForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnForever;
        private System.Windows.Forms.RadioButton rbtnJustOnce;
        private System.Windows.Forms.RadioButton rbtnByDay;
        private System.Windows.Forms.RadioButton rbtnByWeek;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}