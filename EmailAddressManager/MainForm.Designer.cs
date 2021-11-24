namespace EmailAddressManager
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tbUser = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblUser = new System.Windows.Forms.Label();
            this.lvAddresses = new System.Windows.Forms.ListView();
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Address = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblAddresses = new System.Windows.Forms.Label();
            this.btnAddrAdd = new System.Windows.Forms.Button();
            this.btnAddrEdit = new System.Windows.Forms.Button();
            this.btnAddrDelete = new System.Windows.Forms.Button();
            this.btnAddrDefault = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(50, 17);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(313, 20);
            this.tbUser.TabIndex = 0;
            this.tbUser.TextChanged += new System.EventHandler(this.TbUser_TextChanged);
            this.tbUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbUser_KeyPress);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(369, 15);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "&Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(12, 22);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(32, 13);
            this.lblUser.TabIndex = 2;
            this.lblUser.Text = "User:";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lvAddresses
            // 
            this.lvAddresses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Type,
            this.Address});
            this.lvAddresses.FullRowSelect = true;
            this.lvAddresses.Location = new System.Drawing.Point(12, 69);
            this.lvAddresses.MultiSelect = false;
            this.lvAddresses.Name = "lvAddresses";
            this.lvAddresses.Size = new System.Drawing.Size(351, 235);
            this.lvAddresses.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvAddresses.TabIndex = 3;
            this.lvAddresses.UseCompatibleStateImageBehavior = false;
            this.lvAddresses.View = System.Windows.Forms.View.Details;
            this.lvAddresses.SelectedIndexChanged += new System.EventHandler(this.LvAddresses_SelectedIndexChanged);
            this.lvAddresses.SizeChanged += new System.EventHandler(this.LvAddresses_SizeChanged);
            this.lvAddresses.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LvAddresses_MouseDoubleClick);
            // 
            // Type
            // 
            this.Type.Text = "Type";
            this.Type.Width = 81;
            // 
            // Address
            // 
            this.Address.Text = "Address";
            this.Address.Width = 260;
            // 
            // lblAddresses
            // 
            this.lblAddresses.AutoSize = true;
            this.lblAddresses.Location = new System.Drawing.Point(15, 50);
            this.lblAddresses.Name = "lblAddresses";
            this.lblAddresses.Size = new System.Drawing.Size(59, 13);
            this.lblAddresses.TabIndex = 4;
            this.lblAddresses.Text = "Addresses:";
            // 
            // btnAddrAdd
            // 
            this.btnAddrAdd.Enabled = false;
            this.btnAddrAdd.Location = new System.Drawing.Point(369, 69);
            this.btnAddrAdd.Name = "btnAddrAdd";
            this.btnAddrAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAddrAdd.TabIndex = 2;
            this.btnAddrAdd.Text = "&Add";
            this.btnAddrAdd.UseVisualStyleBackColor = true;
            this.btnAddrAdd.Click += new System.EventHandler(this.BtnAddrAdd_Click);
            // 
            // btnAddrEdit
            // 
            this.btnAddrEdit.Enabled = false;
            this.btnAddrEdit.Location = new System.Drawing.Point(369, 98);
            this.btnAddrEdit.Name = "btnAddrEdit";
            this.btnAddrEdit.Size = new System.Drawing.Size(75, 23);
            this.btnAddrEdit.TabIndex = 3;
            this.btnAddrEdit.Text = "&Edit";
            this.btnAddrEdit.UseVisualStyleBackColor = true;
            this.btnAddrEdit.Click += new System.EventHandler(this.BtnAddrEdit_Click);
            // 
            // btnAddrDelete
            // 
            this.btnAddrDelete.Enabled = false;
            this.btnAddrDelete.Location = new System.Drawing.Point(369, 127);
            this.btnAddrDelete.Name = "btnAddrDelete";
            this.btnAddrDelete.Size = new System.Drawing.Size(75, 23);
            this.btnAddrDelete.TabIndex = 4;
            this.btnAddrDelete.Text = "&Delete";
            this.btnAddrDelete.UseVisualStyleBackColor = true;
            this.btnAddrDelete.Click += new System.EventHandler(this.BtnAddrDelete_Click);
            // 
            // btnAddrDefault
            // 
            this.btnAddrDefault.Enabled = false;
            this.btnAddrDefault.Location = new System.Drawing.Point(369, 156);
            this.btnAddrDefault.Name = "btnAddrDefault";
            this.btnAddrDefault.Size = new System.Drawing.Size(75, 23);
            this.btnAddrDefault.TabIndex = 5;
            this.btnAddrDefault.Text = "De&fault";
            this.btnAddrDefault.UseVisualStyleBackColor = true;
            this.btnAddrDefault.Click += new System.EventHandler(this.BtnAddrDefault_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Location = new System.Drawing.Point(369, 252);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(369, 281);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 316);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAddrDefault);
            this.Controls.Add(this.btnAddrDelete);
            this.Controls.Add(this.btnAddrEdit);
            this.Controls.Add(this.btnAddrAdd);
            this.Controls.Add(this.lblAddresses);
            this.Controls.Add(this.lvAddresses);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.tbUser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Email Address Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.ListView lvAddresses;
        private System.Windows.Forms.Label lblAddresses;
        private System.Windows.Forms.Button btnAddrAdd;
        private System.Windows.Forms.Button btnAddrEdit;
        private System.Windows.Forms.Button btnAddrDelete;
        private System.Windows.Forms.Button btnAddrDefault;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.ColumnHeader Address;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnClose;
    }
}

