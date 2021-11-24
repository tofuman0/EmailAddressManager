using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;

namespace EmailAddressManager
{
    public partial class SearchUser : Form
    {
        public String User = "";

        private void findUser(String _user)
        {
            lvUsers.Items.Clear();
            Forest forest = Forest.GetCurrentForest();
            GlobalCatalog gc = forest.FindGlobalCatalog();

            using (DirectorySearcher userSearcher = gc.GetDirectorySearcher())
            {
                userSearcher.Filter =
          "(&((&(objectCategory=Person)(objectClass=User)))(name=*" + _user + "*))";

                userSearcher.PropertiesToLoad.AddRange(new[] {
                       "samAccountName",
                       "mail",
                       "name"
                   });
                foreach (SearchResult entry in userSearcher.FindAll())
                {
                    DirectoryEntry found = entry.GetDirectoryEntry();
                    if (found.Properties["name"].Value != null && found.Properties["samaccountname"].Value != null && found.Properties["mail"].Value != null)
                    {
                        ListViewItem row = new ListViewItem(new[] { found.Properties["name"].Value.ToString(), found.Properties["samaccountname"].Value.ToString(), found.Properties["mail"].Value.ToString() });
                        lvUsers.Items.Add(row);
                    }
                }
            }
        }

        public SearchUser(String _user)
        {
            InitializeComponent();
            if (_user != null)
            {
                User = _user;
                tbUserSearch.Text = User;
                findUser(User);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (lvUsers.SelectedItems.Count > 0)
            {
                User = lvUsers.Items[lvUsers.FocusedItem.Index].SubItems[1].Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(this, "No User selected.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            findUser(tbUserSearch.Text.ToString());
        }

        private void tbUserSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(tbUserSearch.TextLength > 0 && e.KeyChar == (char)Keys.Enter)
                findUser(tbUserSearch.Text.ToString());
        }

        private void lvUsers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSelect_Click(sender, e);
        }
    }
}
