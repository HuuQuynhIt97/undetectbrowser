namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            label_website = new Label();
            label_chrome = new Label();
            label_after = new Label();
            button_attack = new Button();
            button_stop = new Button();
            numeric_after = new NumericUpDown();
            textBox_website = new TextBox();
            textBox_chrome = new TextBox();
            progressBar = new ProgressBar();
            lblCountdown = new Label();
            timer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)numeric_after).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(286, 9);
            label1.Name = "label1";
            label1.Size = new Size(262, 37);
            label1.TabIndex = 0;
            label1.Text = "Phần mềm auto click";
            label1.Click += label1_Click;
            // 
            // label_website
            // 
            label_website.AutoSize = true;
            label_website.BackColor = SystemColors.ActiveCaption;
            label_website.BorderStyle = BorderStyle.Fixed3D;
            label_website.Location = new Point(133, 81);
            label_website.Name = "label_website";
            label_website.Size = new Size(63, 17);
            label_website.TabIndex = 1;
            label_website.Text = "Trang web";
            // 
            // label_chrome
            // 
            label_chrome.AutoSize = true;
            label_chrome.BackColor = SystemColors.ActiveCaption;
            label_chrome.BorderStyle = BorderStyle.Fixed3D;
            label_chrome.Location = new Point(44, 148);
            label_chrome.Name = "label_chrome";
            label_chrome.Size = new Size(152, 17);
            label_chrome.TabIndex = 3;
            label_chrome.Text = "Đường dẫn google chrome";
            label_chrome.Click += label_content_Click;
            // 
            // label_after
            // 
            label_after.AutoSize = true;
            label_after.BackColor = SystemColors.ActiveCaption;
            label_after.Location = new Point(57, 215);
            label_after.Name = "label_after";
            label_after.Size = new Size(137, 15);
            label_after.TabIndex = 4;
            label_after.Text = "Thời gian sau mỗi (phút)";
            // 
            // button_attack
            // 
            button_attack.BackColor = Color.Lime;
            button_attack.Cursor = Cursors.Hand;
            button_attack.Location = new Point(547, 205);
            button_attack.Name = "button_attack";
            button_attack.Size = new Size(75, 35);
            button_attack.TabIndex = 5;
            button_attack.Text = "Bắt đầu";
            button_attack.UseVisualStyleBackColor = false;
            button_attack.Click += button_attack_Click;
            // 
            // button_stop
            // 
            button_stop.BackColor = Color.Red;
            button_stop.Cursor = Cursors.Hand;
            button_stop.Location = new Point(628, 205);
            button_stop.Name = "button_stop";
            button_stop.Size = new Size(75, 35);
            button_stop.TabIndex = 6;
            button_stop.Text = "Dừng";
            button_stop.UseVisualStyleBackColor = false;
            button_stop.Click += button_stop_Click;
            // 
            // numeric_after
            // 
            numeric_after.Location = new Point(218, 211);
            numeric_after.Name = "numeric_after";
            numeric_after.Size = new Size(160, 23);
            numeric_after.TabIndex = 8;
            numeric_after.Value = new decimal(new int[] { 60, 0, 0, 0 });
            // 
            // textBox_website
            // 
            textBox_website.Location = new Point(218, 75);
            textBox_website.Name = "textBox_website";
            textBox_website.ReadOnly = true;
            textBox_website.Size = new Size(482, 23);
            textBox_website.TabIndex = 9;
            textBox_website.Text = "https://freebitco.in/";
            // 
            // textBox_chrome
            // 
            textBox_chrome.Location = new Point(218, 142);
            textBox_chrome.Name = "textBox_chrome";
            textBox_chrome.Size = new Size(482, 23);
            textBox_chrome.TabIndex = 10;
            textBox_chrome.Text = "C:\\Users\\Administrator\\AppData\\Local\\Google\\Chrome\\User Data";
            // 
            // progressBar
            // 
            progressBar.Location = new Point(242, 373);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(442, 17);
            progressBar.TabIndex = 11;
            // 
            // lblCountdown
            // 
            lblCountdown.AutoSize = true;
            lblCountdown.Location = new Point(408, 215);
            lblCountdown.Name = "lblCountdown";
            lblCountdown.Size = new Size(0, 15);
            lblCountdown.TabIndex = 12;
            // 
            // timer
            // 
            timer.Enabled = true;
            timer.Interval = 1000;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            ClientSize = new Size(820, 414);
            Controls.Add(lblCountdown);
            Controls.Add(progressBar);
            Controls.Add(textBox_chrome);
            Controls.Add(textBox_website);
            Controls.Add(numeric_after);
            Controls.Add(button_stop);
            Controls.Add(button_attack);
            Controls.Add(label_after);
            Controls.Add(label_chrome);
            Controls.Add(label_website);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            RightToLeftLayout = true;
            Text = "Auto click by Leo Ver1.0";
            ((System.ComponentModel.ISupportInitialize)numeric_after).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label_website;
        private Label label_chrome;
        private Label label_after;
        private Button button_attack;
        private Button button_stop;
        private NumericUpDown numeric_after;
        private TextBox textBox_website;
        private TextBox textBox_chrome;
        private ProgressBar progressBar;
        private Label lblCountdown;
        private System.Windows.Forms.Timer timer;
    }
}
