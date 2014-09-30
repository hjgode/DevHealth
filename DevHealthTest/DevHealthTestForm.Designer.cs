namespace DevHealthTest
{
    partial class DevHealthTestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.mnuExit = new System.Windows.Forms.MenuItem();
            this.btnBattery = new System.Windows.Forms.Button();
            this.txtOut = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnGet = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mnuExit);
            // 
            // mnuExit
            // 
            this.mnuExit.Text = "Exit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // btnBattery
            // 
            this.btnBattery.Location = new System.Drawing.Point(3, 3);
            this.btnBattery.Name = "btnBattery";
            this.btnBattery.Size = new System.Drawing.Size(88, 23);
            this.btnBattery.TabIndex = 0;
            this.btnBattery.Text = "Battery";
            this.btnBattery.Click += new System.EventHandler(this.btnBattery_Click);
            // 
            // txtOut
            // 
            this.txtOut.AcceptsReturn = true;
            this.txtOut.AcceptsTab = true;
            this.txtOut.Location = new System.Drawing.Point(0, 114);
            this.txtOut.Multiline = true;
            this.txtOut.Name = "txtOut";
            this.txtOut.ReadOnly = true;
            this.txtOut.Size = new System.Drawing.Size(240, 151);
            this.txtOut.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.Location = new System.Drawing.Point(0, 86);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(181, 22);
            this.comboBox1.TabIndex = 2;
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(188, 86);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(49, 21);
            this.btnGet.TabIndex = 3;
            this.btnGet.Text = "get";
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // DevHealthTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.txtOut);
            this.Controls.Add(this.btnBattery);
            this.Menu = this.mainMenu1;
            this.Name = "DevHealthTestForm";
            this.Text = "DevHealth Test";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBattery;
        private System.Windows.Forms.TextBox txtOut;
        private System.Windows.Forms.MenuItem mnuExit;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnGet;
    }
}

