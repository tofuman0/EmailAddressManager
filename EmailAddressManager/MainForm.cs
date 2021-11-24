using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Windows.Forms;

namespace EmailAddressManager
{
    public partial class MainForm : Form
    {
        private String LdapDomain = "";
        private String UserAccount = "";
        public MainForm()
        {
            InitializeComponent();
        }

        private SearchResultCollection GetUserSearchResult(String user)
        {
            SearchResultCollection result = null;
            Forest forest = Forest.GetCurrentForest();
            GlobalCatalog gc = forest.FindGlobalCatalog();

            using (DirectorySearcher userSearcher = gc.GetDirectorySearcher())
            {
                userSearcher.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(samAccountName=" + user + "))";

                userSearcher.PropertiesToLoad.AddRange(new[] {
                       "ProxyAddresses",
                       "mail"
                   });

                if (userSearcher.FindAll().Count == 1)
                {
                    result = userSearcher.FindAll();
                }
                else
                {
                    userSearcher.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(name=*" + user + "*))";
                    result = userSearcher.FindAll();
                }
                
                LdapDomain = gc.Domain.Name;
                UserAccount = user;
            }
            return result;
        }

        private SearchResult GetUser(String user)
        {
            SearchResult result = null;
            Forest forest = Forest.GetCurrentForest();
            GlobalCatalog gc = forest.FindGlobalCatalog();

            using (DirectorySearcher userSearcher = gc.GetDirectorySearcher())
            {
                userSearcher.Filter =
          "(&((&(objectCategory=Person)(objectClass=User)))(samAccountName=" + user + "))";

                userSearcher.PropertiesToLoad.AddRange(new[] {
                       "ProxyAddresses",
                       "mail"
                   });

                result = userSearcher.FindOne();

                LdapDomain = gc.Domain.Name;
                UserAccount = user;
            }
            return result;
        }

        private void UpdateAttribute(List<String> addresses, String user)
        {
            try
            {
                if(String.IsNullOrEmpty(user) || String.IsNullOrEmpty(UserAccount) || String.IsNullOrEmpty(LdapDomain))
                {
                    MessageBox.Show(this, "Username or Domain is not detected", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string connectionPrefix = "LDAP://" + LdapDomain;
                DirectoryEntry entry = new DirectoryEntry(connectionPrefix);
                DirectorySearcher mySearcher = new DirectorySearcher(entry);
                mySearcher.Filter = "(SamAccountName=" + user + ")";
                mySearcher.PropertiesToLoad.Add("proxyaddresses");
                mySearcher.PropertiesToLoad.Add("mail");
                SearchResult result = mySearcher.FindOne();
                if (result != null)
                {
                    DirectoryEntry entryToUpdate = result.GetDirectoryEntry();
                    if (addresses.Count == 0)
                    {
                        if (MessageBox.Show(this, "No addresses in list. Would you like to empty the users addresses?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            entry.Close();
                            entry.Dispose();
                            mySearcher.Dispose();
                            return;
                        }
                    }
                    if (entryToUpdate.Properties.Contains("proxyaddresses"))
                    {
                        entryToUpdate.Properties["proxyaddresses"].Value = addresses.ToArray();
                    }
                    else
                    {
                        entryToUpdate.Properties["proxyaddresses"].Add(addresses.ToArray());
                    }
                    foreach (String address in addresses)
                    {
                        String[] add = address.Split(':');
                        if (add[0] == "SMTP")
                        {
                            entryToUpdate.Properties["mail"].Value = add[1];
                            break;
                        }
                    }
                    entryToUpdate.CommitChanges();
                    entryToUpdate.Close();
                }
                else
                    MessageBox.Show(this, "Unabled to find user " + UserAccount, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                entry.Close();
                entry.Dispose();
                mySearcher.Dispose();
                MessageBox.Show(this, "Email addresses updated", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbUser.TextLength > 0)
            {
                lvAddresses.Items.Clear();
               
                    SearchResultCollection sr = GetUserSearchResult(tbUser.Text.ToString());
                    if(sr.Count == 1)
                    {
                        SearchResult found = sr[0];
                        for (int i = 0; i < found.Properties["ProxyAddresses"].Count; i++)
                        {
                            String[] values = found.Properties["ProxyAddresses"][i].ToString().Split(':');
                            ListViewItem row = new ListViewItem(new[] { values[0], values[1] });
                            if(values[0] == "SMTP") row.Font = new Font(row.Font, FontStyle.Bold);
                            lvAddresses.Items.Add(row);
                        }
                        lvAddresses.Sorting = SortOrder.Ascending;
                        lvAddresses.Sort();
                        btnAddrAdd.Enabled = true;
                    }
                    else
                    {
                        SearchUser searchUserDialog = new SearchUser(tbUser.Text);
                        searchUserDialog.ShowDialog();
                        if(searchUserDialog.DialogResult == DialogResult.OK && searchUserDialog.User != "")
                        {
                            SearchResult srUser = GetUser(searchUserDialog.User);
                            UserAccount = tbUser.Text = searchUserDialog.User;
                            if (srUser != null)
                            {
                                for (int i = 0; i < srUser.Properties["ProxyAddresses"].Count; i++)
                                {
                                    String[] values = srUser.Properties["ProxyAddresses"][i].ToString().Split(':');
                                    ListViewItem row = new ListViewItem(new[] { values[0], values[1] });
                                    if (values[0] == "SMTP") row.Font = new Font(row.Font, FontStyle.Bold);
                                    lvAddresses.Items.Add(row);
                                }
                                lvAddresses.Sorting = SortOrder.Ascending;
                                lvAddresses.Sort();
                                btnAddrAdd.Enabled = true;
                            }
                        }
                        else if(searchUserDialog.DialogResult != DialogResult.Cancel)
                            MessageBox.Show(tbUser.Text + " not found");
                    }
                }
                else
                {
                    SearchUser searchUserDialog = new SearchUser(null);
                    searchUserDialog.ShowDialog();
                    if (searchUserDialog.DialogResult == DialogResult.OK && searchUserDialog.User != "")
                    {
                        SearchResult srUser = GetUser(searchUserDialog.User);
                        UserAccount = tbUser.Text = searchUserDialog.User;
                        if (srUser != null)
                        {
                            for (int i = 0; i < srUser.Properties["ProxyAddresses"].Count; i++)
                            {
                                String[] values = srUser.Properties["ProxyAddresses"][i].ToString().Split(':');
                                ListViewItem row = new ListViewItem(new[] { values[0], values[1] });
                                if (values[0] == "SMTP") row.Font = new Font(row.Font, FontStyle.Bold);
                                lvAddresses.Items.Add(row);
                            }
                            lvAddresses.Sorting = SortOrder.Ascending;
                            lvAddresses.Sort();
                            btnAddrAdd.Enabled = true;
                        }
                    }
                    else if (searchUserDialog.DialogResult != DialogResult.Cancel)
                        MessageBox.Show(tbUser.Text + " not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TbUser_TextChanged(object sender, EventArgs e)
        {
            //if (tbUser.TextLength > 0)
            //{
            //    btnSearch.Enabled = true;
            //}
            //else
            //{
            //    btnSearch.Enabled = false;
            //}
        }

        private void LvAddresses_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnAddrAdd.Enabled = true;
                if (lvAddresses.Items.Count > 0)
                {
                    btnUpdate.Enabled = true;
                }
                else
                {
                    btnUpdate.Enabled = false;
                    btnAddrDelete.Enabled = false;
                    btnAddrDefault.Enabled = false;
                    btnAddrEdit.Enabled = false;
                    return;
                }
                if(lvAddresses.FocusedItem == null) return;
                if (lvAddresses.FocusedItem.Index >= 0)
                {
                    if (lvAddresses.FocusedItem.Text.ToLower() == "smtp")
                    {
                        btnAddrDelete.Enabled = true;
                        btnAddrEdit.Enabled = true;
                    }
                    else
                    {
                        btnAddrDelete.Enabled = false;
                        btnAddrEdit.Enabled = false;
                    }
                    if (lvAddresses.Items[lvAddresses.FocusedItem.Index].SubItems[0].Text.ToString() == "smtp")
                    {
                        btnAddrDefault.Enabled = true;
                    }
                    else
                    {
                        btnAddrDefault.Enabled = false;
                    }
                }
                else
                {
                    btnAddrDelete.Enabled = false;
                    btnAddrDefault.Enabled = false;
                    btnAddrEdit.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
}

        private void BtnAddrDelete_Click(object sender, EventArgs e)
        {
            if(lvAddresses.SelectedItems.Count > 0 && lvAddresses.FocusedItem != null)
            {
                lvAddresses.Items.RemoveAt(lvAddresses.FocusedItem.Index);
                LvAddresses_SelectedIndexChanged(sender, e);
                btnAddrDelete.Enabled = false;
                btnAddrDefault.Enabled = false;
                btnAddrEdit.Enabled = false;
            }
        }

        private void LvAddresses_SizeChanged(object sender, EventArgs e)
        {
            LvAddresses_SelectedIndexChanged(sender, e);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAddrAdd_Click(object sender, EventArgs e)
        {
            EditAdd addAddress = new EditAdd("Add", null, false);
            addAddress.ShowDialog();
            if (addAddress.DialogResult == DialogResult.OK)
            {
                if (lvAddresses.FindItemWithText(addAddress.address) == null)
                {
                    if (addAddress.makedefault == true)
                    {
                        ClearDefault();
                    }
                    ListViewItem row = new ListViewItem(new[] { (addAddress.makedefault == true) ? "SMTP" : "smtp", addAddress.address });
                    lvAddresses.Items.Add(row);
                    lvAddresses.FocusedItem = row;
                    if (addAddress.makedefault == true)
                    {
                        lvAddresses.FocusedItem.Font = new Font(lvAddresses.FocusedItem.Font, FontStyle.Bold);
                    }
                }
                else
                {
                    MessageBox.Show(this, "Address already exists", "Error adding address", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnAddrEdit_Click(object sender, EventArgs e)
        {
            if (lvAddresses.SelectedItems.Count > 0 && lvAddresses.FocusedItem != null)
            {
                EditAdd editAddress = new EditAdd("Edit", lvAddresses.Items[lvAddresses.FocusedItem.Index].SubItems[1].Text, lvAddresses.Items[lvAddresses.FocusedItem.Index].SubItems[0].Text == "SMTP");
                editAddress.ShowDialog();
                if (editAddress.DialogResult == DialogResult.OK)
                {
                    if (editAddress.changed == true && lvAddresses.FindItemWithText(editAddress.address) != null)
                    {
                        MessageBox.Show(this, "Address already exists", "Error adding address", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (editAddress.makedefault == true)
                        {
                            ClearDefault();
                            lvAddresses.FocusedItem.SubItems[0].Text = "SMTP";
                            lvAddresses.FocusedItem.Font = new Font(lvAddresses.FocusedItem.Font, FontStyle.Bold);
                        }
                        lvAddresses.Items[lvAddresses.FocusedItem.Index].SubItems[1].Text = editAddress.address;
                    }
                }
            }
        }

        private void LvAddresses_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BtnAddrEdit_Click(sender, e);
        }

        private void ClearDefault()
        {
            foreach(ListViewItem line in lvAddresses.Items)
            {
                if(line.SubItems[0].Text == "SMTP")
                {
                    line.SubItems[0].Text = "smtp";
                    line.Font = new Font(line.Font, FontStyle.Regular);
                }
            }
        }

        private void BtnAddrDefault_Click(object sender, EventArgs e)
        {
            if (lvAddresses.SelectedItems.Count > 0 && lvAddresses.FocusedItem != null)
            {
                ClearDefault();
                lvAddresses.FocusedItem.SubItems[0].Text = "SMTP";
                lvAddresses.FocusedItem.Font = new Font(lvAddresses.FocusedItem.Font, FontStyle.Bold);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                List<String> addresses = new List<string>();
                foreach (ListViewItem row in lvAddresses.Items)
                {
                    addresses.Add(row.SubItems[0].Text + ":" + row.SubItems[1].Text);
                }
                if (addresses.Count > 0)
                {
                    UpdateAttribute(addresses, UserAccount);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TbUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && tbUser.TextLength > 0)
                BtnSearch_Click(null, null);
        }
    }
}
