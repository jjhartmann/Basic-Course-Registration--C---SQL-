namespace CMPT354_TermProject
{
    partial class LoginWindow
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Anonymous = new System.Windows.Forms.Button();
            this.Username = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.Label();
            this.textbox_username = new System.Windows.Forms.TextBox();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(240, 87);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Login";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.loginbutton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(140, 87);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.cancelbutton_click);
            // 
            // Anonymous
            // 
            this.Anonymous.Location = new System.Drawing.Point(12, 87);
            this.Anonymous.Name = "Anonymous";
            this.Anonymous.Size = new System.Drawing.Size(88, 23);
            this.Anonymous.TabIndex = 2;
            this.Anonymous.Text = "Anonymous";
            this.Anonymous.UseVisualStyleBackColor = true;
            this.Anonymous.Click += new System.EventHandler(this.anonymousbutton_click);
            // 
            // Username
            // 
            this.Username.AutoSize = true;
            this.Username.Location = new System.Drawing.Point(287, 36);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(55, 13);
            this.Username.TabIndex = 3;
            this.Username.Text = "Username";
            // 
            // Password
            // 
            this.Password.AutoSize = true;
            this.Password.Location = new System.Drawing.Point(287, 65);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(53, 13);
            this.Password.TabIndex = 4;
            this.Password.Text = "Password";
            // 
            // textbox_username
            // 
            this.textbox_username.Location = new System.Drawing.Point(12, 29);
            this.textbox_username.Name = "textbox_username";
            this.textbox_username.Size = new System.Drawing.Size(269, 20);
            this.textbox_username.TabIndex = 5;
            // 
            // textBox_password
            // 
            this.textBox_password.Location = new System.Drawing.Point(12, 58);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(269, 20);
            this.textBox_password.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Login into the Course Registration System";
            // 
            // LoginWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 118);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_password);
            this.Controls.Add(this.textbox_username);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.Username);
            this.Controls.Add(this.Anonymous);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.MaximumSize = new System.Drawing.Size(356, 145);
            this.MinimumSize = new System.Drawing.Size(356, 145);
            this.Name = "LoginWindow";
            this.Text = "Login Window";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button Anonymous;
        private System.Windows.Forms.Label Username;
        private System.Windows.Forms.Label Password;
        private System.Windows.Forms.TextBox textbox_username;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Label label1;
    }
}

