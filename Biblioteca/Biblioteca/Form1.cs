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
            }

        }

        private void loginPage_loginButon_Click(object sender, EventArgs e)
        {
            MySqlCommand autentificare = new MySqlCommand("SELECT Parola,idPrivilegiu FROM utilizator WHERE Username = @UsernameParam", bibliotecaDatabaseConection);
            MySqlCommand privilegiu = new MySqlCommand("SELECT Nume FROM Privilegii WHERE idPrivilegii = @idParam", bibliotecaDatabaseConection);
            MySqlDataAdapter dataAdapterAutentificare = new MySqlDataAdapter(autentificare);
            MySqlDataAdapter dataAdapterPrivilegiu = new MySqlDataAdapter(privilegiu);
            DataTable dateAutentificare = new DataTable();
            DataTable datePrivilegiu = new DataTable();
            
            bibliotecaDatabaseConection.Open();

            autentificare.Parameters.Add("@UsernameParam", MySqlDbType.VarChar, 45);
            privilegiu.Parameters.Add("@idParam", MySqlDbType.Int32);
            autentificare.Parameters["@UsernameParam"].Value = loginPage_usernameTextBox.Text;

            dataAdapterAutentificare.Fill(dateAutentificare);

            foreach (DataRow dra in dateAutentificare.Rows)
            {
                if (dra["Parola"] + "" == loginPage_passwordTextBox.Text)
                {
                    privilegiu.Parameters["@idParam"].Value = dra["idPrivilegiu"];

                    dataAdapterPrivilegiu.Fill(datePrivilegiu);

                    foreach (DataRow drp in datePrivilegiu.Rows)
                    {
                        if (drp["Nume"] + "" == "admin")
                            logareAdmin();
                        else
                            if (drp["Nume"] + "" == "bibliotecar")
                                logareBibliotecar();
                            else
                                logareUser();
                    }

                    MessageBox.Show("Atatttttt!");
                }
            }
            bibliotecaDatabaseConection.Close();
        }

       
    }
}
