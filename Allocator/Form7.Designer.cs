namespace Allocator
{
    partial class Form7
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
            this.confirmpassword = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.newpassword = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.currentpassword = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialFlatButton1 = new MaterialSkin.Controls.MaterialFlatButton();
            this.SuspendLayout();
            // 
            // confirmpassword
            // 
            this.confirmpassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.confirmpassword.Depth = 0;
            this.confirmpassword.Hint = "Confirm Password";
            this.confirmpassword.Location = new System.Drawing.Point(153, 247);
            this.confirmpassword.MouseState = MaterialSkin.MouseState.HOVER;
            this.confirmpassword.Name = "confirmpassword";
            this.confirmpassword.PasswordChar = '*';
            this.confirmpassword.SelectedText = "";
            this.confirmpassword.SelectionLength = 0;
            this.confirmpassword.SelectionStart = 0;
            this.confirmpassword.Size = new System.Drawing.Size(202, 23);
            this.confirmpassword.TabIndex = 5;
            this.confirmpassword.UseSystemPasswordChar = false;
            // 
            // newpassword
            // 
            this.newpassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.newpassword.Depth = 0;
            this.newpassword.Hint = "New Password";
            this.newpassword.Location = new System.Drawing.Point(153, 187);
            this.newpassword.MouseState = MaterialSkin.MouseState.HOVER;
            this.newpassword.Name = "newpassword";
            this.newpassword.PasswordChar = '*';
            this.newpassword.SelectedText = "";
            this.newpassword.SelectionLength = 0;
            this.newpassword.SelectionStart = 0;
            this.newpassword.Size = new System.Drawing.Size(202, 23);
            this.newpassword.TabIndex = 4;
            this.newpassword.UseSystemPasswordChar = false;
            // 
            // currentpassword
            // 
            this.currentpassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.currentpassword.Depth = 0;
            this.currentpassword.Hint = "Current Password";
            this.currentpassword.Location = new System.Drawing.Point(153, 131);
            this.currentpassword.MouseState = MaterialSkin.MouseState.HOVER;
            this.currentpassword.Name = "currentpassword";
            this.currentpassword.PasswordChar = '*';
            this.currentpassword.SelectedText = "";
            this.currentpassword.SelectionLength = 0;
            this.currentpassword.SelectionStart = 0;
            this.currentpassword.Size = new System.Drawing.Size(202, 23);
            this.currentpassword.TabIndex = 3;
            this.currentpassword.UseSystemPasswordChar = false;
            // 
            // materialFlatButton1
            // 
            this.materialFlatButton1.AutoSize = true;
            this.materialFlatButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialFlatButton1.Depth = 0;
            this.materialFlatButton1.Location = new System.Drawing.Point(360, 358);
            this.materialFlatButton1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialFlatButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFlatButton1.Name = "materialFlatButton1";
            this.materialFlatButton1.Primary = false;
            this.materialFlatButton1.Size = new System.Drawing.Size(141, 36);
            this.materialFlatButton1.TabIndex = 6;
            this.materialFlatButton1.Text = "Update Password";
            this.materialFlatButton1.UseVisualStyleBackColor = true;
            this.materialFlatButton1.Click += new System.EventHandler(this.materialFlatButton1_Click);
            // 
            // Form7
            // 
            this.AcceptButton = this.materialFlatButton1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(508, 400);
            this.Controls.Add(this.materialFlatButton1);
            this.Controls.Add(this.confirmpassword);
            this.Controls.Add(this.newpassword);
            this.Controls.Add(this.currentpassword);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form7";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change Password";
            this.Load += new System.EventHandler(this.Form7_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialSingleLineTextField confirmpassword;
        private MaterialSkin.Controls.MaterialSingleLineTextField newpassword;
        private MaterialSkin.Controls.MaterialSingleLineTextField currentpassword;
        private MaterialSkin.Controls.MaterialFlatButton materialFlatButton1;
    }
}