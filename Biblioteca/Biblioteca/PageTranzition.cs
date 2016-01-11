using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.IO;


namespace Biblioteca
{
    public partial class aplicatie : Form
    {
        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loginPage_usernameTextBox.Text = "";
            loginPage_passwordTextBox.Text = "";
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
            cautareTitluPage_TitluTextbox.Text = "Introduceti titlul";
            cautareTitluPage_titluErrorLabel.Visible = false;
            tabControler.SelectedTab = cautareTitluPage;
        }

        private void dupaAutorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cautareAutorPage_numeTextBox.Text = "Introduceti autorul";
            cautareAutorPage_numeErrorLabel.Visible = false;
            tabControler.SelectedTab = cautareAutorPage;
        }

        private void dupaColectieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MySqlCommand colectiiCommand = new MySqlCommand("select Nume from colectii order by Nume asc;",bibliotecaDatabaseConection);
            MySqlDataAdapter colectiiDataAdaptor = new MySqlDataAdapter(colectiiCommand);
            DataTable colectiiDataTable = new DataTable();

            bibliotecaDatabaseConection.Open();
            colectiiDataAdaptor.Fill(colectiiDataTable);
            bibliotecaDatabaseConection.Close();

            cautareColectiePage_colectieComboBox.Items.Clear();
            cautareColectiePage_colectieComboBox.Items.Add("Selectati colectia");
            foreach (DataRow row in colectiiDataTable.Rows)
                cautareColectiePage_colectieComboBox.Items.Add(row["Nume"].ToString());
            cautareColectiePage_colectieComboBox.SelectedIndex = 0;

            cautareColectiePage_caolectiiErrorLabel.Visible = false;

            tabControler.SelectedTab = cautareColectiePage;
        }

        private void avansataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MySqlCommand autoriCommand = new MySqlCommand("select * from autor order by nume asc;", bibliotecaDatabaseConection);
            MySqlCommand genuriCommand = new MySqlCommand("select * from gen order by nume asc;", bibliotecaDatabaseConection);
            MySqlCommand edituriCommand = new MySqlCommand("select * from editura order by nume asc;", bibliotecaDatabaseConection);
            MySqlCommand colectiiCommand = new MySqlCommand("select * from colectii order by nume asc;", bibliotecaDatabaseConection);
            MySqlDataAdapter autoriDataAdaptor = new MySqlDataAdapter(autoriCommand);
            MySqlDataAdapter genuriDataAdapter = new MySqlDataAdapter(genuriCommand);
            MySqlDataAdapter edituriDataAdapter = new MySqlDataAdapter(edituriCommand);
            MySqlDataAdapter colectiiDataAdapter = new MySqlDataAdapter(colectiiCommand);
            DataTable autoriDataTable = new DataTable();
            DataTable genuriDataTable = new DataTable();
            DataTable edituriDataTable = new DataTable();
            DataTable colectiiDataTable = new DataTable();

            bibliotecaDatabaseConection.Open();
            autoriDataAdaptor.Fill(autoriDataTable);
            genuriDataAdapter.Fill(genuriDataTable);
            edituriDataAdapter.Fill(edituriDataTable);
            colectiiDataAdapter.Fill(colectiiDataTable);
            bibliotecaDatabaseConection.Close();

            cautareAvansataPage_autorCheckListBox.Items.Clear();
            foreach (DataRow row in autoriDataTable.Rows)
                cautareAvansataPage_autorCheckListBox.Items.Add(row["Nume"].ToString());

            cautareAvansataPage_genCheckListBox.Items.Clear();
            foreach (DataRow row in genuriDataTable.Rows)
                cautareAvansataPage_genCheckListBox.Items.Add(row["Nume"].ToString());

            cautareAvansataPage_edituraCheckListBox.Items.Clear();
            foreach (DataRow row in edituriDataTable.Rows)
                cautareAvansataPage_edituraCheckListBox.Items.Add(row["Nume"].ToString());

            cautareAvansataPage_colectiiComboBox.Items.Clear();
            cautareAvansataPage_colectiiComboBox.Items.Add("Selectati colectia");
            foreach (DataRow row in colectiiDataTable.Rows)
                cautareAvansataPage_colectiiComboBox.Items.Add(row["Nume"].ToString());
            cautareAvansataPage_colectiiComboBox.SelectedIndex = 0;

            cautareAvansataPage_dataDateTimePicker.Value = cautareAvansataPage_dataDateTimePicker.MinDate;
            cautareAvansataPage_titluTextBox.Text = "";
            cautareAvansataPage_isbnTextBox.Text = "";
            cautareAvansataPage_notaMaskTextBox.Text = "";
            cautareAvansataPage_notaMinimaNumericUpDown.Value = -1;
            cautareAvansataPage_notaMaximaNumericUpDown.Value = -1;

            tabControler.SelectedTab = cautareAvansataPage;
        }

        private void cautareTitluPage_cautaButon_Click(object sender, EventArgs e)
        {
            MySqlCommand cautareCommand = new MySqlCommand("select * from carte where idCarte in (SELECT idCarte FROM carte WHERE Titlu REGEXP @titlu);", bibliotecaDatabaseConection);
            MySqlDataAdapter cautareDataAdapter = new MySqlDataAdapter(cautareCommand);

            cautareCommand.Parameters.Add("@titlu", MySqlDbType.VarChar, 45);


            if (cautareTitluPage_TitluTextbox.Text == "Introduceti titlul" || cautareTitluPage_TitluTextbox.Text == "")
            {
                cautareTitluPage_titluErrorLabel.Text = "Introduceti titlul";
                cautareTitluPage_titluErrorLabel.Visible = true;
                return;
            }
            else
                cautareTitluPage_titluErrorLabel.Visible = false;

            cautareCommand.Parameters["@titlu"].Value = cautareTitluPage_TitluTextbox.Text;
            rezultatDataTable = new DataTable();

            bibliotecaDatabaseConection.Open();
            cautareDataAdapter.Fill(rezultatDataTable);
            bibliotecaDatabaseConection.Close();

            indexRexultat = 0;

            if (rezultatDataTable.Rows.Count == 0)
            {
                MessageBox.Show("Nici o carte gasita!");
                return;
            }

            incarcaCatea(indexRexultat);

            if (rezultatDataTable.Rows.Count == 1)
                rezultateleCautariiPage_urmatorButton.Visible = false;
            else
                rezultateleCautariiPage_urmatorButton.Visible = true;
            rezultateleCautariiPage_anteriorButton.Visible = false;

            tabControler.SelectedTab = rezultateleCautariiPage;
        }

        private void cautareAutorPage_cautaButon_Click(object sender, EventArgs e)
        {
            MySqlCommand cautareCommand = new MySqlCommand("select * from carte where idCarte in (select Carte_idCarte from carteautor where Autor_idAutor in (SELECT idAutor FROM autor WHERE Nume REGEXP @numeAutor));", bibliotecaDatabaseConection);
            MySqlDataAdapter cautareDataAdapter = new MySqlDataAdapter(cautareCommand);

            cautareCommand.Parameters.Add("@numeAutor", MySqlDbType.VarChar, 45);

            if (cautareAutorPage_numeTextBox.Text == "Introduceti autorul" || cautareAutorPage_numeTextBox.Text == "")
            {
                cautareAutorPage_numeErrorLabel.Text = "Introduceti autorul";
                cautareAutorPage_numeErrorLabel.Visible = true;
                return;
            }
            else
                cautareAutorPage_numeErrorLabel.Visible = false;

            cautareCommand.Parameters["@numeAutor"].Value = cautareAutorPage_numeTextBox.Text;
            rezultatDataTable = new DataTable();

            bibliotecaDatabaseConection.Open();
            cautareDataAdapter.Fill(rezultatDataTable);
            bibliotecaDatabaseConection.Close();

            indexRexultat = 0;

            if (rezultatDataTable.Rows.Count == 0)
            {
                MessageBox.Show("Nici o carte gasita!");
                return;
            }

            if (rezultatDataTable.Rows.Count == 1)
                rezultateleCautariiPage_urmatorButton.Visible = false;
            else
                rezultateleCautariiPage_urmatorButton.Visible = true;
            rezultateleCautariiPage_anteriorButton.Visible = false;
                
            incarcaCatea(indexRexultat);

            tabControler.SelectedTab = rezultateleCautariiPage;
        }

        private void cautareColectiePage_cautaButon_Click(object sender, EventArgs e)
        {
            MySqlCommand cautareCommand = new MySqlCommand("select * from carte where idCarte in (SELECT idCarte FROM carte WHERE idColectie in (select idColectii from colectii where Nume = @nume));", bibliotecaDatabaseConection);
            MySqlDataAdapter cautareDataAdapter = new MySqlDataAdapter(cautareCommand);

            cautareCommand.Parameters.Add("@nume", MySqlDbType.VarChar, 45);


            if (cautareColectiePage_colectieComboBox.SelectedIndex == 0)
            {
                cautareColectiePage_caolectiiErrorLabel.Text = "Selectati colectia!";
                cautareColectiePage_caolectiiErrorLabel.Visible = true;
                return;
            }
            else
                cautareColectiePage_caolectiiErrorLabel.Visible = false;

            cautareCommand.Parameters["@nume"].Value = cautareColectiePage_colectieComboBox.SelectedItem;
            rezultatDataTable = new DataTable();

            bibliotecaDatabaseConection.Open();
            cautareDataAdapter.Fill(rezultatDataTable);
            bibliotecaDatabaseConection.Close();

            indexRexultat = 0;

            if (rezultatDataTable.Rows.Count == 0)
            {
                MessageBox.Show("Nici o carte gasita!");
                return;
            }

            incarcaCatea(indexRexultat);

            if (rezultatDataTable.Rows.Count == 1)
                rezultateleCautariiPage_urmatorButton.Visible = false;
            else
                rezultateleCautariiPage_urmatorButton.Visible = true;
            rezultateleCautariiPage_anteriorButton.Visible = false;

            tabControler.SelectedTab = rezultateleCautariiPage;
        }

        private void cautareAvansataPage_cautaButon_Click(object sender, EventArgs e)
        {
            tabControler.SelectedTab = rezultateleCautariiPage;
        }

        private void acordaFunctieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MySqlCommand privilegiiCommand = new MySqlCommand("select Nume from privilegii;",bibliotecaDatabaseConection);
            MySqlDataAdapter privilegiiDataAdapter = new MySqlDataAdapter(privilegiiCommand);
            DataTable privilegiiDataTable = new DataTable();

            bibliotecaDatabaseConection.Open();
            privilegiiDataAdapter.Fill(privilegiiDataTable);
            bibliotecaDatabaseConection.Close();

            acordareFunctiePage_functieComboBox.Items.Clear();
            acordareFunctiePage_functieComboBox.Items.Add("Selectati functia");
            foreach (DataRow row in privilegiiDataTable.Rows)
                acordareFunctiePage_functieComboBox.Items.Add(row["Nume"].ToString());
            acordareFunctiePage_functieComboBox.SelectedIndex = 0;

            acordareFunctiePage_functieErrorLabel.Visible = false;
            acordareFunctiePage_usernameErrorLabel.Visible = false;

            tabControler.SelectedTab = acordareFunctiePage;
        }

        private void introducereCartiiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MySqlCommand autoriCommand = new MySqlCommand("select * from autor order by nume asc;", bibliotecaDatabaseConection);
            MySqlCommand genuriCommand = new MySqlCommand("select * from gen order by nume asc;", bibliotecaDatabaseConection);
            MySqlCommand edituriCommand = new MySqlCommand("select * from editura order by nume asc;", bibliotecaDatabaseConection);
            MySqlCommand colectiiCommand = new MySqlCommand("select * from colectii order by nume asc;", bibliotecaDatabaseConection);
            MySqlDataAdapter autoriDataAdaptor = new MySqlDataAdapter(autoriCommand);
            MySqlDataAdapter genuriDataAdapter = new MySqlDataAdapter(genuriCommand);
            MySqlDataAdapter edituriDataAdapter = new MySqlDataAdapter(edituriCommand);
            MySqlDataAdapter colectiiDataAdapter = new MySqlDataAdapter(colectiiCommand);
            DataTable autoriDataTable = new DataTable();
            DataTable genuriDataTable = new DataTable();
            DataTable edituriDataTable = new DataTable();
            DataTable colectiiDataTable = new DataTable();

            bibliotecaDatabaseConection.Open();
            autoriDataAdaptor.Fill(autoriDataTable);
            genuriDataAdapter.Fill(genuriDataTable);
            edituriDataAdapter.Fill(edituriDataTable);
            colectiiDataAdapter.Fill(colectiiDataTable);
            bibliotecaDatabaseConection.Close();

            inserareCartePage_autoriCheckList.Items.Clear();
            foreach (DataRow row in autoriDataTable.Rows)
                inserareCartePage_autoriCheckList.Items.Add(row["Nume"].ToString());

            inserareCartePage_genuriCheckList.Items.Clear();
            foreach (DataRow row in genuriDataTable.Rows)
                inserareCartePage_genuriCheckList.Items.Add(row["Nume"].ToString());

            inserareCartePage_edituriCheckList.Items.Clear();
            foreach (DataRow row in edituriDataTable.Rows)
                inserareCartePage_edituriCheckList.Items.Add(row["Nume"].ToString());

            inserareCartePage_colectieComboBox.Items.Clear();
            inserareCartePage_colectieComboBox.Items.Add("Selectati colectia");
            foreach (DataRow row in colectiiDataTable.Rows)
                inserareCartePage_colectieComboBox.Items.Add(row["Nume"].ToString());
            inserareCartePage_colectieComboBox.SelectedIndex = 0;

            inserareCartePage_titluTextBox.Text = "";
            inserareCartePage_isbnTextBox.Text = "";
            inserareCartePage_numarPaginiTextBox.Text = "";
            inserareCartePage_notaCarteTextBox.Text = "";
            inserareCartePage_rezumatTextBox.Text = "";
            inserareCartePage_numarExemplareTextBox.Text = "";
            inserareCartePage_imaginePanou.BackgroundImage = null;

            inserareCartePage_autorErrorLabel.Visible = false;
            inserareCartePage_edituraErrorLabel.Visible = false;
            inserareCartePage_exemplareErrorLabel.Visible = false;
            inserareCartePage_genErrorLabel.Visible = false;
            inserareCartePage_isbnErrorLabel.Visible = false;
            inserareCartePage_notaErrorLabel.Visible = false;
            inserareCartePage_nrPaginiErrorLabel.Visible = false;
            inserareCartePage_titluErrorLabel.Visible = false;
            

            tabControler.SelectedTab = inserareCartePage;
        }

        private void inserareAutorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inserareAutorPage_numeTextBox.Text = "";
            inserareAutorPage_numeErrorLabel.Visible = false;
            tabControler.SelectedTab = inserareAutorPage;
        }

        private void creareEdituraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            creareEdituraPage_numeTextBox.Text = "";
            creareEdituraPage_numeErrorLabel.Visible = false;
            tabControler.SelectedTab = creareEdituraPage;
        }

        private void creareColectieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            creareColectiePage_numeTextBox.Text = "";
            creareColectiePage_numeErrorLabel.Visible = false;
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
            MySqlCommand wishlistCommand = new MySqlCommand("select idCarte from wishlist where idUtilizator = @userId;",bibliotecaDatabaseConection);
            MySqlCommand autoriCommand = new MySqlCommand("select Nume from autor where idAutor in (select Autor_idAutor from carteautor where Carte_idCarte = @idCarte)", bibliotecaDatabaseConection);
            MySqlCommand numeCarteCommand = new MySqlCommand("select Titlu from carte where idCarte = @idCarte;",bibliotecaDatabaseConection);
            MySqlCommand carteCommand = new MySqlCommand("select idCarte,Titlu from carte;", bibliotecaDatabaseConection);
            MySqlCommand imprumuturiCommand = new MySqlCommand("select count(*) from imprumuturi where idUtilizator = @userId;", bibliotecaDatabaseConection);
            MySqlDataAdapter imprumuturiDataAdapter = new MySqlDataAdapter(imprumuturiCommand);
            MySqlDataAdapter autoriDataAdapter = new MySqlDataAdapter(autoriCommand);
            MySqlDataAdapter wishlistDataAdapter = new MySqlDataAdapter(wishlistCommand);
            MySqlDataAdapter numeCarteDataAdapter = new MySqlDataAdapter(numeCarteCommand);
            MySqlDataAdapter carteDataAdapter = new MySqlDataAdapter(carteCommand);
            DataTable imprumuturiDataTable = new DataTable();
            DataTable wishlistDataTable = new DataTable();
            DataTable autoriDataTable;
            DataTable numeCarteDataTable;
            DataTable carteDataTable = new DataTable();

            autoriCommand.Parameters.Add("@idCarte", MySqlDbType.Int32);
            numeCarteCommand.Parameters.Add("@idCarte", MySqlDbType.Int32);
            wishlistCommand.Parameters.Add("@userId", MySqlDbType.Int32);
            imprumuturiCommand.Parameters.Add("@userId", MySqlDbType.Int32);

            wishlistCommand.Parameters["@userId"].Value = userId;
            imprumuturiCommand.Parameters["@userId"].Value = userId;

            bibliotecaDatabaseConection.Open();
            wishlistDataAdapter.Fill(wishlistDataTable);
            imprumuturiDataAdapter.Fill(imprumuturiDataTable);
            bibliotecaDatabaseConection.Close();

            imprumutPage_numarCartiLabel.Text = "Mai aveti voie sa imprumutati " + (3 - Int32.Parse(imprumuturiDataTable.Rows[0].ItemArray[0].ToString())).ToString() + " carti!";

            imprumutPage_wishlistTreeView.Nodes.Clear();
            for (int i = 0; i < wishlistDataTable.Rows.Count; i++)
            {
                numeCarteDataTable = new DataTable();
                numeCarteCommand.Parameters["@idCarte"].Value = wishlistDataTable.Rows[i]["idCarte"];
                bibliotecaDatabaseConection.Open();
                numeCarteDataAdapter.Fill(numeCarteDataTable);
                bibliotecaDatabaseConection.Close();
                imprumutPage_wishlistTreeView.Nodes.Add(numeCarteDataTable.Rows[0]["Titlu"].ToString());
                imprumutPage_wishlistTreeView.Nodes[i].Name = wishlistDataTable.Rows[i]["idCarte"].ToString();

                autoriDataTable = new DataTable();

                autoriCommand.Parameters["@idCarte"].Value = wishlistDataTable.Rows[i]["idCarte"];

                bibliotecaDatabaseConection.Open();
                autoriDataAdapter.Fill(autoriDataTable);
                bibliotecaDatabaseConection.Close();
                imprumutPage_wishlistTreeView.Nodes[i].Nodes.Clear();
                foreach (DataRow row in autoriDataTable.Rows)
                    imprumutPage_wishlistTreeView.Nodes[i].Nodes.Add(row["Nume"].ToString());
            }

            bibliotecaDatabaseConection.Open();
            carteDataAdapter.Fill(carteDataTable);
            bibliotecaDatabaseConection.Close();

            imprumutPage_cartiTreeView.Nodes.Clear();
            for (int i = 0; i < carteDataTable.Rows.Count; i++)
            {
                imprumutPage_cartiTreeView.Nodes.Add(carteDataTable.Rows[i]["Titlu"].ToString());
                imprumutPage_cartiTreeView.Nodes[i].Name = carteDataTable.Rows[i]["idCarte"].ToString();

                autoriDataTable = new DataTable();

                autoriCommand.Parameters["@idCarte"].Value = carteDataTable.Rows[i]["idCarte"];

                bibliotecaDatabaseConection.Open();
                autoriDataAdapter.Fill(autoriDataTable);
                bibliotecaDatabaseConection.Close();
                imprumutPage_cartiTreeView.Nodes[i].Nodes.Clear();
                foreach (DataRow row in autoriDataTable.Rows)
                    imprumutPage_cartiTreeView.Nodes[i].Nodes.Add(row["Nume"].ToString());
            }

            tabControler.SelectedTab = imprumutPage;
        }

        private void vizualizareIstoricToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MySqlCommand cautareCommand = new MySqlCommand("select * from carte where idCarte in (select idCarte from istoricimprumuturi where idUtilizator = @userId);", bibliotecaDatabaseConection);
            MySqlDataAdapter cautareDataAdapter = new MySqlDataAdapter(cautareCommand);

            cautareCommand.Parameters.Add("@userId", MySqlDbType.VarChar, 45);

            cautareCommand.Parameters["@userId"].Value = userId;
            rezultatDataTable = new DataTable();

            bibliotecaDatabaseConection.Open();
            cautareDataAdapter.Fill(rezultatDataTable);
            bibliotecaDatabaseConection.Close();

            indexRexultat = 0;

            if (rezultatDataTable.Rows.Count == 0)
            {
                MessageBox.Show("Nici o carte imprumutata!");
                return;
            }

            incarcaCatea(indexRexultat);

            if (rezultatDataTable.Rows.Count == 1)
                rezultateleCautariiPage_urmatorButton.Visible = false;
            rezultateleCautariiPage_anteriorButton.Visible = false;

            tabControler.SelectedTab = rezultateleCautariiPage;
        }

        protected void incarcaCatea(int poz)
        {
            if(poz<rezultatDataTable.Rows.Count)
            {
                MySqlCommand autoriCommand = new MySqlCommand("select Nume from autor where idAutor in (select Autor_idAutor from carteautor where Carte_idCarte = @idCarte)",bibliotecaDatabaseConection);
                MySqlCommand genCommand = new MySqlCommand("select Nume from gen where idGen in (select Gen_idGen from cartegen where Carte_idCarte = @idCarte)", bibliotecaDatabaseConection);
                MySqlCommand edituriCommand = new MySqlCommand("select Nume from editura where idEditura in (select Editura_idEditura from carteeditura where Carte_idCarte = @idCarte)", bibliotecaDatabaseConection);
                MySqlCommand checkWishlistCommand = new MySqlCommand("select count(*) as NumarCarti from wishlist where idUtilizator = @userId and idCarte = @idCarte;", bibliotecaDatabaseConection);
                MySqlDataAdapter autoriDataAdapter = new MySqlDataAdapter(autoriCommand);
                MySqlDataAdapter genDataAdapter = new MySqlDataAdapter(genCommand);
                MySqlDataAdapter edituraDataAdapter = new MySqlDataAdapter(edituriCommand);
                MySqlDataAdapter checkWishlistDataAdapter = new MySqlDataAdapter(checkWishlistCommand);
                DataTable informatiiDataTable;

                autoriCommand.Parameters.Add("@idCarte", MySqlDbType.Int32);
                genCommand.Parameters.Add("@idCarte", MySqlDbType.Int32);
                edituriCommand.Parameters.Add("@idCarte", MySqlDbType.Int32);

                autoriCommand.Parameters["@idCarte"].Value = rezultatDataTable.Rows[poz]["idCarte"];
                genCommand.Parameters["@idCarte"].Value = rezultatDataTable.Rows[poz]["idCarte"];
                edituriCommand.Parameters["@idCarte"].Value = rezultatDataTable.Rows[poz]["idCarte"];

                checkWishlistCommand.Parameters.Add("@userId", MySqlDbType.Int32);
                checkWishlistCommand.Parameters.Add("@idCarte", MySqlDbType.Int32);

                checkWishlistCommand.Parameters["@userId"].Value = userId;
                checkWishlistCommand.Parameters["@idCarte"].Value = rezultatDataTable.Rows[poz]["idCarte"];

                rezultateleCautariiPage_titluTextBox.Text = rezultatDataTable.Rows[poz]["Titlu"].ToString();
                rezultateleCautariiPage_isbnTextBox.Text = rezultatDataTable.Rows[poz]["ISBN"].ToString();
                rezultateleCautariiPage_rezumatTextBox.Text = rezultatDataTable.Rows[poz]["Rezumat"].ToString();
                rezultateleCautariiPage_dataTextBox.Text = rezultatDataTable.Rows[poz]["DataAparitie"].ToString();
                rezultateleCautariiPage_nrPaginiTextBox.Text = rezultatDataTable.Rows[poz]["NrPagini"].ToString();
                rezultateleCautariiPage_notaTextBox.Text = rezultatDataTable.Rows[poz]["NotaCarte"].ToString();
                if (rezultatDataTable.Rows[poz]["ImagineCoperta"].ToString() != "")
                {
                    byte[] imageData = (byte[])rezultatDataTable.Rows[poz]["ImagineCoperta"];
                    MemoryStream stream = new MemoryStream(imageData);
                    rezultateleCautariiPage_imaginePanou.BackgroundImage = ResizeImage.ImageResize(Image.FromStream(stream), rezultateleCautariiPage_imaginePanou.Width, rezultateleCautariiPage_imaginePanou.Height);
                }
                else
                    rezultateleCautariiPage_imaginePanou.BackgroundImage = null;
                if (rezultatDataTable.Rows[poz]["idColectie"].ToString() != "")
                {
                    MySqlCommand colectieCommand = new MySqlCommand("select nume from Colectii where idColectii = @idColectie", bibliotecaDatabaseConection);
                    MySqlDataAdapter colectieDataAdapter = new MySqlDataAdapter(colectieCommand);
                    DataTable colectieDataTable = new DataTable();

                    colectieCommand.Parameters.Add("@idColectie", MySqlDbType.Int32);
                    colectieCommand.Parameters["@idColectie"].Value = rezultatDataTable.Rows[poz]["idColectie"];

                    bibliotecaDatabaseConection.Open();
                    colectieDataAdapter.Fill(colectieDataTable);
                    bibliotecaDatabaseConection.Close();

                    rezultateleCautariiPage_colectieTextBox.Text = colectieDataTable.Rows[0]["nume"].ToString();
                }
                else
                    rezultateleCautariiPage_colectieTextBox.Text = "";

                informatiiDataTable = new DataTable();
                bibliotecaDatabaseConection.Open();
                autoriDataAdapter.Fill(informatiiDataTable);
                bibliotecaDatabaseConection.Close();
                rezultateleCautariiPage_autoriListBox.Items.Clear();
                foreach (DataRow row in informatiiDataTable.Rows)
                    rezultateleCautariiPage_autoriListBox.Items.Add(row["Nume"].ToString());

                informatiiDataTable = new DataTable();
                bibliotecaDatabaseConection.Open();
                genDataAdapter.Fill(informatiiDataTable);
                bibliotecaDatabaseConection.Close();
                rezultateleCautariiPage_genuriListBox.Items.Clear();
                foreach (DataRow row in informatiiDataTable.Rows)
                    rezultateleCautariiPage_genuriListBox.Items.Add(row["Nume"].ToString());

                informatiiDataTable = new DataTable();
                bibliotecaDatabaseConection.Open();
                edituraDataAdapter.Fill(informatiiDataTable);
                bibliotecaDatabaseConection.Close();
                rezultateleCautariiPage_edituraListBox.Items.Clear();
                foreach (DataRow row in informatiiDataTable.Rows)
                    rezultateleCautariiPage_edituraListBox.Items.Add(row["Nume"].ToString());

                informatiiDataTable = new DataTable();
                bibliotecaDatabaseConection.Open();
                checkWishlistDataAdapter.Fill(informatiiDataTable);
                bibliotecaDatabaseConection.Close();
                if(informatiiDataTable.Rows.Count == 1 && Int32.Parse(informatiiDataTable.Rows[0]["NumarCarti"].ToString()) != 0)
                {
                    rezultateleCautariiPage_marcareButton.Text = "Cartea este marcata!";
                    rezultateleCautariiPage_marcareButton.Enabled = false;
                }
                else
                {
                    rezultateleCautariiPage_marcareButton.Text = "Marcare: Posibila carte de imprumutat";
                    rezultateleCautariiPage_marcareButton.Enabled = true;
                }
            }
        }
    }
}