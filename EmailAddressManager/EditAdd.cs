using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmailAddressManager
{
    public partial class EditAdd : Form
    {
        public String address { get; set; }
        public bool makedefault { get; set; }
        public bool changed { get; set; }

        private String origAddress;

        public EditAdd(String type, String address, bool isdefault)
        {
            InitializeComponent();
            origAddress = address;
            if (type != "")
            {
                this.Text = type;
            }
            if (address != null)
            {
                if (address.Length > 0)
                {
                    this.tbAddress.Text = address;
                }
            }
            if(isdefault == true)
            {
                this.cbDefault.Checked = isdefault;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbAddress.Text.Contains("@") && tbAddress.Text.Contains("."))
            {
                address = tbAddress.Text;
                makedefault = cbDefault.Checked;
                if (origAddress != null)
                {
                    changed = (tbAddress.Text.ToLower() == origAddress.ToLower()) ? false : true;
                }
                else
                {
                    changed = false;
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(this, "Invalid email address", "Error adding address", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && tbAddress.TextLength > 0)
                btnSave_Click(null, null);
        }
    }
}
