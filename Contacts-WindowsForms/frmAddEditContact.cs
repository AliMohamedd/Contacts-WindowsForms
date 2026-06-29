using ContactsAndCountries_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contacts_WindowsForms
{
    public partial class frmAddEditContact : Form
    {
        private enum enMode { addNew = 0, Update = 1}
        private enMode _Mode = enMode.addNew;

        private int _ContactID;
        private clsContact _Contact;

        public frmAddEditContact(int ContactID)
        {
            InitializeComponent();
            _ContactID = ContactID;
            if (_ContactID == -1)
                _Mode = enMode.addNew;
            else
                _Mode = enMode.Update;
        }

        private void _FillCountriesInComboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();
            
            foreach(DataRow row in dtCountries.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }

        }

        private void _OnLoad()
        {
            _FillCountriesInComboBox();
            cbCountry.SelectedIndex = 0;

            if (_Mode == enMode.addNew)
            {
                lblTitle.Text = "Add New Contact";
                lblContactID.Visible = false;
                lbllnkRemove.Visible = false;
                _Contact = new clsContact();
                return;
            }

            _Contact = clsContact.Find(_ContactID);

            if (_Contact == null)
            {
                MessageBox.Show("");
                this.Close();
                return;
            }

            lblTitle.Text = "Edit contact ID = " + _ContactID;
            lblIdValue.Text = _ContactID.ToString();
            tbFirstName.Text = _Contact.FirstName;
            tbLastName.Text = _Contact.LastName;
            tbEmail.Text = _Contact.Email;
            tbPhone.Text = _Contact.Phone;
            tbAddress.Text = _Contact.Address;
            dtpDateOfBirth.Value = _Contact.DateOfBirth;
           

            if(_Contact.ImagePath != "")
            {
                pbContact.Load(_Contact.ImagePath);
            }

            lbllnkRemove.Visible = (_Contact.ImagePath != "");

            cbCountry.SelectedIndex = cbCountry.FindString(clsCountry.Find(_Contact.CountryID).CountryName);
        }

        private void frmAddEditContact_Load(object sender, EventArgs e)
        {
            _OnLoad();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int CountryID = clsCountry.Find(cbCountry.Text).ID;

            _Contact.FirstName = tbFirstName.Text;
            _Contact.LastName = tbLastName.Text;
            _Contact.Email = tbEmail.Text;
            _Contact.Phone = tbPhone.Text;
            _Contact.Address = tbAddress.Text;
            _Contact.DateOfBirth = dtpDateOfBirth.Value;
            _Contact.CountryID = CountryID;

            if (pbContact.ImageLocation != null)
                _Contact.ImagePath = pbContact.ImageLocation;
            else
                _Contact.ImagePath = "";

            if(_Contact.Save())
            {
                MessageBox.Show("Data Saved Successfully.");
            }
            else
            {
                MessageBox.Show("Error: Failed to save.");
            }

            _Mode = enMode.Update;
            lblTitle.Text = "Edit Contact ID = " + _Contact.ID;
            lblContactID.Text = _Contact.ID.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbllnkSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ofdContactPic.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            ofdContactPic.FilterIndex = 1;
            ofdContactPic.RestoreDirectory = true;

            if(ofdContactPic.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = ofdContactPic.FileName;
                pbContact.Load(selectedFilePath);
                lbllnkRemove.Visible = true;
            }
        }

        private void lbllnkRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbContact.ImageLocation = null;
            lbllnkRemove.Visible = false;
        } 
    }
}
