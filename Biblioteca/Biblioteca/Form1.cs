using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Biblioteca
{
    public partial class aplicatie : Form
    {
        public aplicatie()
        {
            InitializeComponent();
            try
            {
                bibliotecaDatabaseConection = new MySqlConnection(conectionString);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

            foreach (ToolStripMenuItem item in menuStrip.Items)
            {
                if ("autentificareToolStripMenuItem" != item.Name)
                {
                    item.Visible = false;
                }
                else
                {
                    foreach (ToolStripMenuItem subItem in item.DropDown.Items)
                        if (subItem.Name == "logoutToolStripMenuItem")
                            subItem.Visible = false;
                }
            }

        }

        private void loginPage_loginButon_Click(object sender, EventArgs e)
        {
            MySqlCommand autentificare = new MySqlCommand("SELECT idUtilizator,Parola,idPrivilegiu FROM utilizator WHERE Username = @UsernameParam", bibliotecaDatabaseConection);
            MySqlCommand privilegiu = new MySqlCommand("SELECT Nume FROM Privilegii WHERE idPrivilegii = @idParam", bibliotecaDatabaseConection);
            MySqlDataAdapter dataAdapterAutentificare = new MySqlDataAdapter(autentificare);
            MySqlDataAdapter dataAdapterPrivilegiu = new MySqlDataAdapter(privilegiu);
            DataTable dateAutentificare = new DataTable();
            DataTable datePrivilegiu = new DataTable();
            
            bibliotecaDatabaseConection.Open();

            autentificare.Parameters.Add("@UsernameParam", MySqlDbType.VarChar, 45);
            privilegiu.Parameters.Add("@idParam", MySqlDbType.Int32);

            if (loginPage_usernameTextBox.Text == "" || loginPage_passwordTextBox.Text == "")
            {
                MessageBox.Show("Username sau Password incorecte!");
                bibliotecaDatabaseConection.Close();
                return;
            }
            autentificare.Parameters["@UsernameParam"].Value = loginPage_usernameTextBox.Text;

            dataAdapterAutentificare.Fill(dateAutentificare);

            if(dateAutentificare.Rows.Count == 0)
            {
                MessageBox.Show("Username incorect!");
                bibliotecaDatabaseConection.Close();
                return;
            }

            foreach (DataRow dra in dateAutentificare.Rows)
            {
                if (dra["Parola"] + "" == loginPage_passwordTextBox.Text)
                {
                    privilegiu.Parameters["@idParam"].Value = dra["idPrivilegiu"];

                    dataAdapterPrivilegiu.Fill(datePrivilegiu);

                    foreach (DataRow drp in datePrivilegiu.Rows)
                    {
                        logoutToolStripMenuItem.Visible = true;
                        Int32.TryParse(dra["idUtilizator"].ToString(),out userId);
                        if (drp["Nume"] + "" == "admin")
                            logareAdmin();
                        else
                            if (drp["Nume"] + "" == "bibliotecar")
                                logareBibliotecar();
                            else
                                logareUser();
                    }

                    tabControler.SelectedTab = home;
                }
                else
                {
                    MessageBox.Show("Password incorect!");
                    bibliotecaDatabaseConection.Close();
                    return;
                }
            }
            bibliotecaDatabaseConection.Close();
        }

        private void signin_inserareButton_Click(object sender, EventArgs e)
        {
            String username, password, nume, prenume, ocupatie, sex, oras, codPostal, strada, email, telefon, mesajOutput;
            MySqlCommand addUserFunctionCall = new MySqlCommand("adauga_utilizator");
            addUserFunctionCall.CommandType = System.Data.CommandType.StoredProcedure;
            addUserFunctionCall.Connection = bibliotecaDatabaseConection;

            addUserFunctionCall.Parameters.Add("_username", MySqlDbType.VarChar, 45);
            addUserFunctionCall.Parameters.Add("_password", MySqlDbType.VarChar, 45);
            addUserFunctionCall.Parameters.Add("_nume", MySqlDbType.VarChar, 45);
            addUserFunctionCall.Parameters.Add("_prenume", MySqlDbType.VarChar, 45);
            addUserFunctionCall.Parameters.Add("_ocupatie", MySqlDbType.VarChar, 45);
            addUserFunctionCall.Parameters.Add("_sex", MySqlDbType.Enum);
            addUserFunctionCall.Parameters.Add("_oras", MySqlDbType.VarChar, 45);
            addUserFunctionCall.Parameters.Add("_codPostal", MySqlDbType.VarChar, 6);
            addUserFunctionCall.Parameters.Add("_strada", MySqlDbType.VarChar, 45);
            addUserFunctionCall.Parameters.Add("_email", MySqlDbType.VarChar, 45);
            addUserFunctionCall.Parameters.Add("_telefon", MySqlDbType.VarChar, 10);
            addUserFunctionCall.Parameters.Add("_mesaj", MySqlDbType.VarChar, 45);
            addUserFunctionCall.Parameters.Add("_index", MySqlDbType.Int32);

            addUserFunctionCall.Parameters["_mesaj"].Direction = ParameterDirection.Output;
            addUserFunctionCall.Parameters["_index"].Direction = ParameterDirection.Output;

            if (signin_usenameTextBox.Text == "")
            {
                signin_usernameErrorLabel.Text = "Username-ul este obligatoriu!";
                signin_usernameErrorLabel.Visible = true;
                return;
            }
            else
                username = signin_usenameTextBox.Text;

            if (signin_passwordTextBox.Text == "")
            {
                signin_passwordErrorLabel.Text = "Parola este obligatorie!";
                signin_passwordErrorLabel.Visible = true;
                return;
            }
            else
                if (signin_confirmPassTextBox.Text != signin_passwordTextBox.Text)
                {
                    signin_comfirmPassErrorLabel.Text = "Password don't mach!";
                    signin_passwordErrorLabel.Text = "Password don't mach!";

                    signin_comfirmPassErrorLabel.Visible = true;
                    signin_passwordErrorLabel.Visible = true;
                    return;
                }
                else
                    password = signin_passwordTextBox.Text;

            if (signin_sexFemininRadioButton.Checked)
                sex = "F";
            else
                if (signin_sexMasculinRadioButton.Checked)
                    sex = "M";
                else
                    sex = null;

            if(signin_orasTextBox.Text == "")
            {
                oras = null;
                strada = null;
                codPostal = null;
                if (signin_stradaTextBox.Text != "")
                {
                    signin_orasErrorLabel.Text = "Daca ati introdus \"strada\", \"orasul\" devine obligatoriu!";
                    signin_orasErrorLabel.Visible = true;
                    return;
                }

                if(signin_codPostalTextBox.Text != "")
                {
                    signin_orasErrorLabel.Text = "Daca ati introdus \"codul postal\", \"orasul\" devine obligatoriu!";
                    signin_orasErrorLabel.Visible = true;
                    return;
                }
            }
            else
            {
                oras = signin_orasTextBox.Text;

                if (signin_stradaTextBox.Text == "")
                {
                    signin_stradaErrorLabel.Text = "Daca ati introdus \"orasul\", \"strada\" devine obligatorie!";
                    signin_stradaErrorLabel.Visible = true;
                    return;
                }
                else
                    strada = signin_stradaTextBox.Text;

                if (signin_codPostalTextBox.Text == "")
                {
                    signin_codPostalErrorLabel.Text = "Daca ati introdus \"orasul\", \"codul postal\" devine obligatoriu!";
                    signin_codPostalErrorLabel.Visible = true;
                    return;
                }
                else
                    codPostal = signin_codPostalTextBox.Text;
            }

            if (signin_corpEmailTextBox.Text == "")
            {
                if (signin_serverEmailTextBox.Text != "")
                {
                    MessageBox.Show("Email invalid!");
                    return;
                }
                else
                    email = null;
            }
            else
                if (signin_serverEmailTextBox.Text == "")
                {
                    MessageBox.Show("Email invalid!");
                    return;
                }
                else
                    email = signin_corpEmailTextBox.Text + "@" + signin_serverEmailTextBox.Text;

            if (signin_numeTextBox.Text == "")
                nume = null;
            else
                nume = signin_numeTextBox.Text;

            if (signin_prenumeTextBox.Text == "")
                prenume = null;
            else
                prenume = signin_prenumeTextBox.Text;

            if (signin_telefonTextBox.Text == "")
                telefon = null;
            else
                telefon = signin_telefonTextBox.Text;

            if ((String)signin_ocupatieComboBox.SelectedItem == "Alta ocupatie")
                if (signin_ocupatieTextBox.Text == "" || signin_ocupatieTextBox.Text == "Introduceti ocupatia")
                    ocupatie = null;
                else
                    ocupatie = signin_ocupatieTextBox.Text;
            else
                if ((String)signin_ocupatieComboBox.SelectedItem == "Selectati ocupatia")
                    ocupatie = null;
                else
                    ocupatie = (String)signin_ocupatieComboBox.SelectedItem;
            

            addUserFunctionCall.Parameters["_username"].Value = username;
            addUserFunctionCall.Parameters["_password"].Value = password;
            addUserFunctionCall.Parameters["_nume"].Value = nume;
            addUserFunctionCall.Parameters["_prenume"].Value = prenume;
            addUserFunctionCall.Parameters["_ocupatie"].Value = ocupatie;
            addUserFunctionCall.Parameters["_sex"].Value = sex;
            addUserFunctionCall.Parameters["_oras"].Value = oras;
            addUserFunctionCall.Parameters["_codPostal"].Value = codPostal;
            addUserFunctionCall.Parameters["_strada"].Value = strada;
            addUserFunctionCall.Parameters["_email"].Value = email;
            addUserFunctionCall.Parameters["_telefon"].Value = telefon;

            bibliotecaDatabaseConection.Open();
            addUserFunctionCall.ExecuteNonQuery();
            bibliotecaDatabaseConection.Close();


            mesajOutput = (String)addUserFunctionCall.Parameters["_mesaj"].Value;

            if (mesajOutput.ToLower() == "ok")
            {
                userId = (int)addUserFunctionCall.Parameters["_index"].Value;
                logareUser();
                tabControler.SelectedTab = home;
            }
            else
                if (mesajOutput == "Nume de utilizator existent")
                {
                    signin_usernameErrorLabel.Text = "Username-ul deja exista!";
                    signin_usernameErrorLabel.Visible = true;
                    return;
                }
                else
                    MessageBox.Show("Eroare neasteptata!\n" + mesajOutput);
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in menuStrip.Items)
            {
                if ("autentificareToolStripMenuItem" != item.Name)
                {
                    item.Visible = false;
                }
                else
                {
                    foreach (ToolStripMenuItem subItem in item.DropDown.Items)
                        if (subItem.Name == "logoutToolStripMenuItem")
                            subItem.Visible = false;
                }
            }
            userId = -1;
            tabControler.SelectedTab = home;
        }

        private void creareGenPage_creareButton_Click(object sender, EventArgs e)
        {

        }                
    }
}
