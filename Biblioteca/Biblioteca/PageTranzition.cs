using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace Biblioteca
{
    public partial class aplicatie : Form
    {
        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControler.SelectedTab = loginPage;
        }

        private void signinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MySqlCommand ocupatiiCommand = new MySqlCommand("SELECT * FROM Ocupatii;", bibliotecaDatabaseConection);
            MySqlDataAdapter ocupatiiAdaptor = new MySqlDataAdapter(ocupatiiCommand);
            DataTable tabelaOcupatii = new DataTable();

            bibliotecaDatabaseConection.Open();
            ocupatiiAdaptor.Fill(tabelaOcupatii);
            bibliotecaDatabaseConection.Close();

            foreach (DataRow tupla in tabelaOcupatii.Rows)
                signin_ocupatieComboBox.Items.Add(tupla["Nume"]);

            signin_ocupatieComboBox.Items.Add("Alta ocupatie");
            signin_ocupatieComboBox.SelectedIndex = 0;

            signin_usenameTextBox.Text = "";
            signin_passwordTextBox.Text = "";
            signin_confirmPassTextBox.Text = "";
            signin_numeTextBox.Text = "";
            signin_prenumeTextBox.Text = "";
            signin_ocupatieTextBox.Text = "Introduceti ocupatia";
            signin_sexFemininRadioButton.Checked = false;
            signin_sexMasculinRadioButton.Checked = false;
            signin_orasTextBox.Text = "";
            signin_codPostalTextBox.Text = "";
            signin_stradaTextBox.Text = "";
            signin_corpEmailTextBox.Text = "";
            signin_serverEmailTextBox.Text = "";
            signin_telefonTextBox.Text = "";

            signin_ocupatieTextBox.Visible = false;
            signin_usernameErrorLabel.Visible = false;
            signin_passwordErrorLabel.Visible = false;
            signin_comfirmPassErrorLabel.Visible = false;
            signin_orasErrorLabel.Visible = false;
            signin_codPostalErrorLabel.Visible = false;
            signin_stradaErrorLabel.Visible = false;
            signin_emailErrorLabel.Visible = false;

            tabControler.SelectedTab = signinPage;
        }

        private void dupaTitluToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControler.SelectedTab = cautareTitluPage;
        }

        private void dupaAutorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControler.SelectedTab = cautareAutorPage;
        }

        private void dupaColectieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControler.SelectedTab = cautareColectiePage;
        }

        private void avansataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControler.SelectedTab = cautareAvansataPage;
        }

        private void cautareTitluPage_cautaButon_Click(object sender, EventArgs e)
        {
            tabControler.SelectedTab = rezultateleCautariiPage;
        }

        private void cautareAutorPage_cautaButon_Click(object sender, EventArgs e)
        {
            tabControler.SelectedTab = rezultateleCautariiPage;
        }

        private void cautareColectiePage_cautaButon_Click(object sender, EventArgs e)
        {
            tabControler.SelectedTab = rezultateleCautariiPage;
        }

        private void cautareAvansataPage_cautaButon_Click(object sender, EventArgs e)
        {
            tabControler.SelectedTab = rezultateleCautariiPage;
        }

        private void acordaFunctieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControler.SelectedTab = acordareFunctiePage;
        }

        private void introducereCartiiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControler.SelectedTab = inserareCartePage;
        }

        private void creareColectieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControler.SelectedTab = creareColectiePage;
        }

        private void creareGenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            creareGenPage_numeTextBox.Text = "";
            creareGenPage_numeErrorLabel.Visible = false;
            tabControler.SelectedTab = creareGenPage;
        }

        private void modificareCarteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControler.SelectedTab = modificareCartePage;
        }

        private void imprumutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControler.SelectedTab = imprumutPage;
        }

        private void vizualizareIstoricToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControler.SelectedTab = rezultateleCautariiPage;
        }
    }
}