using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContactsAndCountries_BusinessLayer;

namespace Contacts_WindowsForms
{
    public partial class frmListContacts : Form
    {
        public frmListContacts()
        {
            InitializeComponent();
            _RefreshDataViewList();
        }

        private void _RefreshDataViewList()
        {
            dgvAllContacts.DataSource = clsContact.GetAllContacts();
        }

        private void btnAddNewContact_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditContact(-1);
            frm.ShowDialog();
            _RefreshDataViewList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Control sourceControl = ((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl;
            Form frm = new frmAddEditContact((int)dgvAllContacts.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshDataViewList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete contact [" + dgvAllContacts.CurrentRow.Cells[0].Value + "]",
                "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (clsContact.DeleteContact((int)dgvAllContacts.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Contact Deleted Successfully.");
                    _RefreshDataViewList();
                }
                else
                {
                    MessageBox.Show("Contact is not deleted.");
                }
            }
        }
    }
}
