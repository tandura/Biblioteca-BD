﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Runtime.InteropServices;

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

            this.imprumutPage_wishlistTreeView.DrawMode = TreeViewDrawMode.OwnerDrawText;
            this.imprumutPage_cartiTreeView.DrawMode = TreeViewDrawMode.OwnerDrawText;
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
            EmailChecker emailChecher = new EmailChecker();
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
                    signin_emailErrorLabel.Text = "Email invalid!";
                    signin_emailErrorLabel.Visible = true;
                    return;
                }
                else
                    email = null;
            }
            else
                if (signin_serverEmailTextBox.Text == "")
                {
                    signin_emailErrorLabel.Text = "Email invalid!";
                    signin_emailErrorLabel.Visible = true;
                    return;
                }
                else
                    if (emailChecher.IsValidEmail(signin_corpEmailTextBox.Text + "@" + signin_serverEmailTextBox.Text))
                        email = signin_corpEmailTextBox.Text + "@" + signin_serverEmailTextBox.Text;
                    else
                    {
                        signin_emailErrorLabel.Text = "Email invalid!";
                        signin_emailErrorLabel.Visible = true;
                        return;
                    }

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

            logareUser();
        }

        private void aplicatie_FormClosing(object sender, FormClosingEventArgs e)
        {
            MySqlCommand curatareWishlist = new MySqlCommand("delete from wishlist where idUtilizator = @idUtilizator;", bibliotecaDatabaseConection);

            curatareWishlist.Parameters.Add("@idUtilizator", MySqlDbType.Int32);
            curatareWishlist.Parameters["@idUtilizator"].Value = userId;

            bibliotecaDatabaseConection.Open();
            curatareWishlist.ExecuteNonQuery();
            bibliotecaDatabaseConection.Close();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MySqlCommand curatareWishlist = new MySqlCommand("delete from wishlist where idUtilizator = @idUtilizator;", bibliotecaDatabaseConection);

            curatareWishlist.Parameters.Add("@idUtilizator", MySqlDbType.Int32);
            curatareWishlist.Parameters["@idUtilizator"].Value = userId;

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

            bibliotecaDatabaseConection.Open();
            curatareWishlist.ExecuteNonQuery();
            bibliotecaDatabaseConection.Close();

            userId = -1;
            tabControler.SelectedTab = home;
        }

        private void creareGenPage_creareButton_Click(object sender, EventArgs e)
        {
            MySqlCommand numarGen = new MySqlCommand("select count(*) as numarGenuri from Gen where Nume = @nume;",bibliotecaDatabaseConection);
            MySqlCommand insertGen = new MySqlCommand("insert into Gen(Nume) value (@nume);",bibliotecaDatabaseConection);
            MySqlDataAdapter numarGenuriAdapter = new MySqlDataAdapter(numarGen);
            DataTable numarGenuriDataTable = new DataTable();

            if(creareGenPage_numeTextBox.Text == "")
            {
                creareGenPage_numeErrorLabel.Text = "Introduceti un nume pentru gen!";
                creareGenPage_numeErrorLabel.Visible = true;
                return;
            }

            numarGen.Parameters.Add("@nume", MySqlDbType.VarChar, 45);
            insertGen.Parameters.Add("@nume", MySqlDbType.VarChar, 45);

            numarGen.Parameters["@nume"].Value = creareGenPage_numeTextBox.Text;

            bibliotecaDatabaseConection.Open();
            numarGenuriAdapter.Fill(numarGenuriDataTable);
            bibliotecaDatabaseConection.Close();

            if(numarGenuriDataTable.Rows.Count > 0)
            {
                if(Int32.Parse(numarGenuriDataTable.Rows[0].ItemArray[0].ToString()) > 0)
                {
                    creareGenPage_numeErrorLabel.Text = "Genul deja exista!";
                    creareGenPage_numeErrorLabel.Visible = true;
                    return;
                }
                else
                {
                    insertGen.Parameters["@nume"].Value = creareGenPage_numeTextBox.Text;
                    bibliotecaDatabaseConection.Open();
                    insertGen.ExecuteNonQuery();
                    bibliotecaDatabaseConection.Close();

                    creareGenPage_numeErrorLabel.Visible = false;

                    MessageBox.Show("Genul " + creareGenPage_numeTextBox.Text + " a fost creat!");
                }
            }
        }

        private void creareColectiePage_creareButton_Click(object sender, EventArgs e)
        {
            MySqlCommand numarColectie = new MySqlCommand("select count(*) as numarColectii from colectii where Nume = @nume;", bibliotecaDatabaseConection);
            MySqlCommand insertColectie = new MySqlCommand("adauga_colectie", bibliotecaDatabaseConection);
            MySqlDataAdapter numarColectiiAdapter = new MySqlDataAdapter(numarColectie);
            DataTable numarColectiiDataTable = new DataTable();

            if (creareColectiePage_numeTextBox.Text == "")
            {
                creareColectiePage_numeErrorLabel.Text = "Introduceti un nume pentru colectie!";
                creareColectiePage_numeErrorLabel.Visible = true;
                return;
            }

            numarColectie.Parameters.Add("@nume", MySqlDbType.VarChar, 45);

            insertColectie.CommandType = System.Data.CommandType.StoredProcedure;
            insertColectie.Parameters.Add("_nume", MySqlDbType.VarChar, 45);

            numarColectie.Parameters["@nume"].Value = creareColectiePage_numeTextBox.Text;

            bibliotecaDatabaseConection.Open();
            numarColectiiAdapter.Fill(numarColectiiDataTable);
            bibliotecaDatabaseConection.Close();

            if (numarColectiiDataTable.Rows.Count > 0)
            {
                if (Int32.Parse(numarColectiiDataTable.Rows[0].ItemArray[0].ToString()) > 0)
                {
                    creareColectiePage_numeErrorLabel.Text = "Colectia deja exista!";
                    creareColectiePage_numeErrorLabel.Visible = true;
                    return;
                }
                else
                {
                    insertColectie.Parameters["_nume"].Value = creareColectiePage_numeTextBox.Text;
                    bibliotecaDatabaseConection.Open();
                    insertColectie.ExecuteNonQuery();
                    bibliotecaDatabaseConection.Close();

                    creareColectiePage_numeErrorLabel.Visible = false;

                    MessageBox.Show("Colectia " + creareColectiePage_numeTextBox.Text + " a fost creata!");
                }
            }
        }

        private void inserareAutorPage_inserareButton_Click(object sender, EventArgs e)
        {
            MySqlCommand numarAutori = new MySqlCommand("select count(*) as numarColectii from autor where Nume = @nume;", bibliotecaDatabaseConection);
            MySqlCommand insertAutor = new MySqlCommand("adauga_autor", bibliotecaDatabaseConection);
            MySqlDataAdapter numarAutoriAdapter = new MySqlDataAdapter(numarAutori);
            DataTable numarAutoriDataTable = new DataTable();

            if (inserareAutorPage_numeTextBox.Text == "")
            {
                inserareAutorPage_numeErrorLabel.Text = "Introduceti un nume pentru autor!";
                inserareAutorPage_numeErrorLabel.Visible = true;
                return;
            }

            numarAutori.Parameters.Add("@nume", MySqlDbType.VarChar, 45);

            insertAutor.CommandType = System.Data.CommandType.StoredProcedure;
            insertAutor.Parameters.Add("_nume", MySqlDbType.VarChar, 45);

            numarAutori.Parameters["@nume"].Value = inserareAutorPage_numeTextBox.Text;

            bibliotecaDatabaseConection.Open();
            numarAutoriAdapter.Fill(numarAutoriDataTable);
            bibliotecaDatabaseConection.Close();

            if (numarAutoriDataTable.Rows.Count > 0)
            {
                if (Int32.Parse(numarAutoriDataTable.Rows[0].ItemArray[0].ToString()) > 0)
                {
                    inserareAutorPage_numeErrorLabel.Text = "Autorul deja exista!";
                    inserareAutorPage_numeErrorLabel.Visible = true;
                    return;
                }
                else
                {
                    insertAutor.Parameters["_nume"].Value = inserareAutorPage_numeTextBox.Text;
                    bibliotecaDatabaseConection.Open();
                    insertAutor.ExecuteNonQuery();
                    bibliotecaDatabaseConection.Close();

                    inserareAutorPage_numeErrorLabel.Visible = false;

                    MessageBox.Show("Autorul " + inserareAutorPage_numeTextBox.Text + " a fost inserat!");
                }
            }
        }

        private void creareEdituraPage_creareButton_Click(object sender, EventArgs e)
        {
            MySqlCommand numarEditura = new MySqlCommand("select count(*) as numarEditura from editura where Nume = @nume;", bibliotecaDatabaseConection);
            MySqlCommand insertEditura = new MySqlCommand("adauga_editura", bibliotecaDatabaseConection);
            MySqlDataAdapter numarEdituraAdapter = new MySqlDataAdapter(numarEditura);
            DataTable numarEdituraDataTable = new DataTable();

            if (creareEdituraPage_numeTextBox.Text == "")
            {
                creareEdituraPage_numeErrorLabel.Text = "Introduceti un nume pentru editura!";
                creareEdituraPage_numeErrorLabel.Visible = true;
                return;
            }

            numarEditura.Parameters.Add("@nume", MySqlDbType.VarChar, 45);

            insertEditura.CommandType = System.Data.CommandType.StoredProcedure;
            insertEditura.Parameters.Add("_nume", MySqlDbType.VarChar, 45);

            numarEditura.Parameters["@nume"].Value = creareEdituraPage_numeTextBox.Text;

            bibliotecaDatabaseConection.Open();
            numarEdituraAdapter.Fill(numarEdituraDataTable);
            bibliotecaDatabaseConection.Close();

            if (numarEdituraDataTable.Rows.Count > 0)
            {
                if (Int32.Parse(numarEdituraDataTable.Rows[0].ItemArray[0].ToString()) > 0)
                {
                    creareEdituraPage_numeErrorLabel.Text = "Editura deja exista!";
                    creareEdituraPage_numeErrorLabel.Visible = true;
                    return;
                }
                else
                {
                    insertEditura.Parameters["_nume"].Value = creareEdituraPage_numeTextBox.Text;
                    bibliotecaDatabaseConection.Open();
                    insertEditura.ExecuteNonQuery();
                    bibliotecaDatabaseConection.Close();

                    creareEdituraPage_numeErrorLabel.Visible = false;

                    MessageBox.Show("Editura " + creareEdituraPage_numeTextBox.Text + " a fost creata!");
                }
            }
        }

        private void inserareCartePage_inserarePozaButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog incarcaPozaDialog = new OpenFileDialog();
            incarcaPozaDialog.Filter = "Choose Image(*.jpg; *.png; *.gif;)|*.jpg; *.png; *.gif;";
            if(incarcaPozaDialog.ShowDialog() == DialogResult.OK)
            {
                inserareCartePage_imaginePanou.BackgroundImage = ResizeImage.ImageResize(Image.FromFile(incarcaPozaDialog.FileName), inserareCartePage_imaginePanou.Width, inserareCartePage_imaginePanou.Height);
                caleImagine = incarcaPozaDialog.FileName;
            }
        }

        private void inserareCartePage_inserareButton_Click(object sender, EventArgs e)
        {
            MySqlCommand inserareCommand = new MySqlCommand("insert into carte(Titlu,ISBN,Rezumat,DataAparitie,ImagineCoperta,NrPagini,idColectie,NotaCarte,NrCarti) values(@titlu,@isbn,@rezumat,@data,@imagine,@nrPagini,@idColectie,@nota,@nrCarti)", bibliotecaDatabaseConection);
            MySqlCommand carteAutorCommand = new MySqlCommand("insert into carteautor(Carte_idCarte,Autor_idAutor) values (@idCarte,@idAutor);",bibliotecaDatabaseConection);
            MySqlCommand carteEdituraCommand = new MySqlCommand("insert into carteeditura(Carte_idCarte,Editura_idEditura) values (@idCarte,@idEdit);",bibliotecaDatabaseConection);
            MySqlCommand carteGenCommand = new MySqlCommand("insert into cartegen(Carte_idCarte,Gen_idGen) values (@idCarte,@idGen);",bibliotecaDatabaseConection);
            MySqlCommand colectieCommand = new MySqlCommand("select idColectii from colectii where Nume = @numeColectie;", bibliotecaDatabaseConection);
            MySqlCommand autoriCommand = new MySqlCommand("SELECT idAutor FROM autor WHERE Nume=@nume;", bibliotecaDatabaseConection);
            MySqlCommand genCommand = new MySqlCommand("SELECT idGen FROM gen WHERE Nume=@nume;", bibliotecaDatabaseConection);
            MySqlCommand edituraCommand = new MySqlCommand("SELECT idEditura FROM editura WHERE Nume=@nume;", bibliotecaDatabaseConection);
            MySqlDataAdapter colectieDataAdaptor = new MySqlDataAdapter(colectieCommand);
            MySqlDataAdapter autorDataAdaptor = new MySqlDataAdapter(autoriCommand);
            MySqlDataAdapter genDataAdapter = new MySqlDataAdapter(genCommand);
            MySqlDataAdapter edituriDataAdapter = new MySqlDataAdapter(edituraCommand);
            DataTable colectieDataTable = new DataTable();
            DataTable autorDataTable = null;
            DataTable genDataTable = null;
            DataTable edituraDataTable = null;
            bool colectie;
            int numarPagini;
            long idCarte;
            byte[] ImageData;
            FileStream fs;
            BinaryReader br;

            if(inserareCartePage_imaginePanou.BackgroundImage != null)
            {
                fs = new FileStream(caleImagine, FileMode.Open, FileAccess.Read);
                br = new BinaryReader(fs);
                ImageData = br.ReadBytes((int)fs.Length);
                br.Close();
                fs.Close();
            }
            else
                ImageData = null;

            if(inserareCartePage_titluTextBox.Text == "")
            {
                inserareCartePage_titluErrorLabel.Text = "Introduceti un titlu!";
                inserareCartePage_titluErrorLabel.Visible = true;
                return;
            }
            else
                inserareCartePage_titluErrorLabel.Visible = false;

            if (IsbnValidation.TryValidate(inserareCartePage_isbnTextBox.Text) == false)
            {
                inserareCartePage_isbnErrorLabel.Text = "Isbn-ul nu este valid";
                inserareCartePage_isbnErrorLabel.Visible = true;
                return;
            }
            else
                inserareCartePage_isbnErrorLabel.Visible = false;

            if (inserareCartePage_autoriCheckList.CheckedItems.Count == 0)
            {
                inserareCartePage_autorErrorLabel.Text = "Selectati cel putin un autor!";
                inserareCartePage_autorErrorLabel.Visible = true;
                return;
            }
            else
                inserareCartePage_autorErrorLabel.Visible = false;

            if(inserareCartePage_edituriCheckList.CheckedItems.Count == 0)
            {
                inserareCartePage_edituraErrorLabel.Text = "Selectati cel putin o editura!";
                inserareCartePage_edituraErrorLabel.Visible = true;
                return;
            }
            else
                inserareCartePage_edituraErrorLabel.Visible = false;

            if (inserareCartePage_genuriCheckList.CheckedItems.Count == 0)
            {
                inserareCartePage_genErrorLabel.Text = "Selectati cel putin un gen!";
                inserareCartePage_genErrorLabel.Visible = true;
                return;
            }
            else
                inserareCartePage_genErrorLabel.Visible = false;

            if (inserareCartePage_notaCarteTextBox.Text == "")
            {
                inserareCartePage_notaErrorLabel.Text = "Introduceti nota!";
                inserareCartePage_notaErrorLabel.Visible = true;
                return;
            }
            else
                if (Int32.Parse(inserareCartePage_notaCarteTextBox.Text) > 5)
                {
                    inserareCartePage_notaErrorLabel.Text = "Nota trebuie sa fie <= 5!";
                    inserareCartePage_notaErrorLabel.Visible = true;
                    return;
                }
                else
                    inserareCartePage_notaErrorLabel.Visible = false;

            if (inserareCartePage_numarExemplareTextBox.Text == "")
            {
                inserareCartePage_exemplareErrorLabel.Text = "Necesar!";
                inserareCartePage_exemplareErrorLabel.Visible = true;
            }
            else
                inserareCartePage_exemplareErrorLabel.Visible = false;

            if (inserareCartePage_numarPaginiTextBox.Text == "" || !Int32.TryParse(inserareCartePage_numarPaginiTextBox.Text, out numarPagini))
            {
                inserareCartePage_nrPaginiErrorLabel.Text = "Necesar!";
                inserareCartePage_nrPaginiErrorLabel.Visible = true;
                return;
            }
            else
                inserareCartePage_nrPaginiErrorLabel.Visible = false;

            if (inserareCartePage_colectieComboBox.SelectedIndex == 0)
                colectie = false;
            else
            {
                colectieCommand.Parameters.Add("@numeColectie", MySqlDbType.VarChar, 45);
                colectieCommand.Parameters["@numeColectie"].Value = inserareCartePage_colectieComboBox.SelectedItem;

                bibliotecaDatabaseConection.Open();
                colectieDataAdaptor.Fill(colectieDataTable);
                bibliotecaDatabaseConection.Close();
                colectie = true;
            }

            inserareCommand.Parameters.Add("@titlu",MySqlDbType.VarChar,45);
            inserareCommand.Parameters.Add("@isbn",MySqlDbType.VarChar,45);
            inserareCommand.Parameters.Add("@rezumat",MySqlDbType.MediumText);
            inserareCommand.Parameters.Add("@data",MySqlDbType.Date);
            inserareCommand.Parameters.Add("@imagine",MySqlDbType.MediumBlob);
            inserareCommand.Parameters.Add("@nrPagini",MySqlDbType.Int32);
            inserareCommand.Parameters.Add("@nota",MySqlDbType.Int16);
            inserareCommand.Parameters.Add("@nrCarti",MySqlDbType.Int32);
            inserareCommand.Parameters.Add("@idColectie",MySqlDbType.Int32);

            autoriCommand.Parameters.Add("@nume", MySqlDbType.VarChar, 45);

            genCommand.Parameters.Add("@nume", MySqlDbType.VarChar, 45);

            edituraCommand.Parameters.Add("@nume", MySqlDbType.VarChar, 45);

            carteAutorCommand.Parameters.Add("@idCarte", MySqlDbType.Int32);
            carteAutorCommand.Parameters.Add("@idAutor", MySqlDbType.Int32);

            carteGenCommand.Parameters.Add("@idCarte", MySqlDbType.Int32);
            carteGenCommand.Parameters.Add("@idGen", MySqlDbType.Int32);

            carteEdituraCommand.Parameters.Add("@idCarte", MySqlDbType.Int32);
            carteEdituraCommand.Parameters.Add("@idEdit", MySqlDbType.Int32);

            inserareCommand.Parameters["@titlu"].Value = inserareCartePage_titluTextBox.Text;
            inserareCommand.Parameters["@isbn"].Value = inserareCartePage_isbnTextBox.Text;
            inserareCommand.Parameters["@rezumat"].Value = inserareCartePage_rezumatTextBox.Text;
            inserareCommand.Parameters["@data"].Value = inserareCartePage_dataAparitiei.Value.Date;
            inserareCommand.Parameters["@imagine"].Value = ImageData;
            inserareCommand.Parameters["@nrPagini"].Value = Int32.Parse(inserareCartePage_numarPaginiTextBox.Text);
            inserareCommand.Parameters["@nota"].Value = Int16.Parse(inserareCartePage_notaCarteTextBox.Text);
            inserareCommand.Parameters["@nrCarti"].Value = Int32.Parse(inserareCartePage_numarExemplareTextBox.Text);
            if(!colectie)
                inserareCommand.Parameters["@idColectie"].Value = null;
            else
                inserareCommand.Parameters["@idColectie"].Value = colectieDataTable.Rows[0].ItemArray[0];

            bibliotecaDatabaseConection.Open();
            inserareCommand.ExecuteNonQuery();
            idCarte = inserareCommand.LastInsertedId;
            bibliotecaDatabaseConection.Close();

            for(int i=0;i<inserareCartePage_autoriCheckList.Items.Count;i++)
            {
                if(inserareCartePage_autoriCheckList.GetItemChecked(i))
                {
                    autorDataTable = new DataTable();

                    autoriCommand.Parameters["@nume"].Value = inserareCartePage_autoriCheckList.Items[i].ToString();

                    bibliotecaDatabaseConection.Open();
                    autorDataAdaptor.Fill(autorDataTable);
                    bibliotecaDatabaseConection.Close();

                    if(autorDataTable.Rows.Count==1)
                    {
                        carteAutorCommand.Parameters["@idCarte"].Value = idCarte;
                        carteAutorCommand.Parameters["@idAutor"].Value = autorDataTable.Rows[0].ItemArray[0];

                        bibliotecaDatabaseConection.Open();
                        carteAutorCommand.ExecuteNonQuery();
                        bibliotecaDatabaseConection.Close();
                    }
                }
            }

            for (int i = 0; i < inserareCartePage_genuriCheckList.Items.Count; i++)
                if(inserareCartePage_genuriCheckList.GetItemChecked(i))
                {
                    genDataTable = new DataTable();

                    genCommand.Parameters["@nume"].Value = inserareCartePage_genuriCheckList.Items[i].ToString();

                    bibliotecaDatabaseConection.Open();
                    genDataAdapter.Fill(genDataTable);
                    bibliotecaDatabaseConection.Close();

                    if(genDataTable.Rows.Count == 1)
                    {
                        carteGenCommand.Parameters["@idCarte"].Value = idCarte;
                        carteGenCommand.Parameters["@idGen"].Value = genDataTable.Rows[0].ItemArray[0];

                        bibliotecaDatabaseConection.Open();
                        carteGenCommand.ExecuteNonQuery();
                        bibliotecaDatabaseConection.Close();
                    }
                }

            for (int i = 0; i < inserareCartePage_edituriCheckList.Items.Count; i++)
                if(inserareCartePage_edituriCheckList.GetItemChecked(i))
                {
                    edituraDataTable = new DataTable();

                    edituraCommand.Parameters["@nume"].Value = inserareCartePage_edituriCheckList.Items[i].ToString();

                    bibliotecaDatabaseConection.Open();
                    edituriDataAdapter.Fill(edituraDataTable);
                    bibliotecaDatabaseConection.Close();

                    if(edituraDataTable.Rows.Count == 1)
                    {
                        carteEdituraCommand.Parameters["@idCarte"].Value = idCarte;
                        carteEdituraCommand.Parameters["@idEdit"].Value = edituraDataTable.Rows[0].ItemArray[0];

                        bibliotecaDatabaseConection.Open();
                        carteEdituraCommand.ExecuteNonQuery();
                        bibliotecaDatabaseConection.Close();
                    }
                }


                MessageBox.Show("Cartea " + inserareCartePage_titluTextBox.Text + " a fost adaugata!");

        }

        private void acordareFunctiePage_acordaButton_Click(object sender, EventArgs e)
        {
            MySqlCommand acordaCommand = new MySqlCommand("update utilizator set idPrivilegiu = @idPriv WHERE Username=@username;", bibliotecaDatabaseConection);
            MySqlCommand privilegiuCommand = new MySqlCommand("select idPrivilegii from privilegii where Nume = @nume;", bibliotecaDatabaseConection);
            MySqlDataAdapter privilegiuDataAdapter = new MySqlDataAdapter(privilegiuCommand);
            DataTable privilegiuDataTable = new DataTable();

            privilegiuCommand.Parameters.Add("@nume",MySqlDbType.VarChar,45);

            acordaCommand.Parameters.Add("@username",MySqlDbType.VarChar,45);
            acordaCommand.Parameters.Add("@idPriv", MySqlDbType.Int32);

            if (acordareFunctiePage_usernameTextBox.Text == "")
            {
                acordareFunctiePage_usernameErrorLabel.Text = "Introduceti un username!";
                acordareFunctiePage_usernameErrorLabel.Visible = true;
                return;
            }
            else
                acordareFunctiePage_usernameErrorLabel.Visible = false;

            if (acordareFunctiePage_functieComboBox.SelectedIndex == 0)
            {
                acordareFunctiePage_functieErrorLabel.Text = "Selectati Functia!";
                acordareFunctiePage_functieErrorLabel.Visible = true;
                return;
            }
            else
                acordareFunctiePage_functieErrorLabel.Visible = false;

            privilegiuCommand.Parameters["@nume"].Value = acordareFunctiePage_functieComboBox.SelectedItem;

            bibliotecaDatabaseConection.Open();
            privilegiuDataAdapter.Fill(privilegiuDataTable);
            bibliotecaDatabaseConection.Close();

            acordaCommand.Parameters["@username"].Value = acordareFunctiePage_usernameTextBox.Text;
            acordaCommand.Parameters["@idPriv"].Value = privilegiuDataTable.Rows[0].ItemArray[0];

            bibliotecaDatabaseConection.Open();
            acordaCommand.ExecuteNonQuery();
            bibliotecaDatabaseConection.Close();

            MessageBox.Show("Utilizatorul " + acordareFunctiePage_usernameTextBox.Text + " a primit functia de " + acordareFunctiePage_functieComboBox.SelectedItem + ".");
        }

        private void rezultateleCautariiPage_anteriorButton_Click(object sender, EventArgs e)
        {
            incarcaCatea(--indexRexultat);
            if (indexRexultat == 0)
                rezultateleCautariiPage_anteriorButton.Visible = false;
            if (indexRexultat < rezultatDataTable.Rows.Count - 1)
                rezultateleCautariiPage_urmatorButton.Visible = true;
        }

        private void rezultateleCautariiPage_urmatorButton_Click(object sender, EventArgs e)
        {
            incarcaCatea(++indexRexultat);
            if (indexRexultat == rezultatDataTable.Rows.Count - 1)
                rezultateleCautariiPage_urmatorButton.Visible = false;
            if (indexRexultat > 0)
                rezultateleCautariiPage_anteriorButton.Visible = true;
        }

        private void rezultateleCautariiPage_marcareButton_Click(object sender, EventArgs e)
        {
            MySqlCommand inserareCommand = new MySqlCommand("insert into wishlist(idUtilizator,idCarte) values(@idUtilizator,@idCarte);", bibliotecaDatabaseConection);

            inserareCommand.Parameters.Add("@idUtilizator",MySqlDbType.Int32);
            inserareCommand.Parameters.Add("@idCarte", MySqlDbType.Int32);

            inserareCommand.Parameters["@idUtilizator"].Value = userId;
            inserareCommand.Parameters["@idCarte"].Value = rezultatDataTable.Rows[indexRexultat]["idCarte"];

            bibliotecaDatabaseConection.Open();
            inserareCommand.ExecuteNonQuery();
            bibliotecaDatabaseConection.Close();

            rezultateleCautariiPage_marcareButton.Text = "Cartea este marcata!";
            rezultateleCautariiPage_marcareButton.Enabled = false;
        }

        /// <summary>
        /// Hides the checkbox for the specified node on a TreeView control.
        /// </summary>
        private void HideCheckBox(TreeView tvw, TreeNode node)
        {
            TVITEM tvi = new TVITEM();
            tvi.hItem = node.Handle;
            tvi.mask = TVIF_STATE;
            tvi.stateMask = TVIS_STATEIMAGEMASK;
            tvi.state = 0;
            SendMessage(tvw.Handle, TVM_SETITEM, IntPtr.Zero, ref tvi);
        }

        private void imprumutPage_wishlistTreeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Node.Level >= 1) HideCheckBox(imprumutPage_wishlistTreeView,e.Node);
            e.DrawDefault = true;
        }

        private void imprumutPage_cartiTreeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Node.Level >= 1) HideCheckBox(imprumutPage_cartiTreeView, e.Node);
            e.DrawDefault = true;
        }

        private void imprumutPage_imprumutaButton_Click(object sender, EventArgs e)
        {
            MySqlCommand imprumuturiCommand = new MySqlCommand("select count(*) from imprumuturi where idUtilizator = @userId;", bibliotecaDatabaseConection);
            MySqlCommand insertCommand = new MySqlCommand("insert into imprumuturi(idUtilizator,idCarte,dataImprumut,dataRestituire) values (@idUtilizator,@idCarte,@dataImprumut,@dataRestituire);",bibliotecaDatabaseConection);
            MySqlDataAdapter imprumuturiDataAdapter = new MySqlDataAdapter(imprumuturiCommand);
            MySqlDataAdapter carteDataAdapter = new MySqlDataAdapter(imprumuturiCommand);
            DataTable imprumuturiDataTable = new DataTable();

            imprumuturiCommand.Parameters.Add("@userId", MySqlDbType.Int32);
            insertCommand.Parameters.Add("@idUtilizator", MySqlDbType.Int32);
            insertCommand.Parameters.Add("@idCarte", MySqlDbType.Int32);
            insertCommand.Parameters.Add("@dataImprumut", MySqlDbType.Date);
            insertCommand.Parameters.Add("@dataRestituire", MySqlDbType.Date);

            imprumuturiCommand.Parameters["@userId"].Value = userId;

            bibliotecaDatabaseConection.Open();
            imprumuturiDataAdapter.Fill(imprumuturiDataTable);
            bibliotecaDatabaseConection.Close();

            if(Int32.Parse(imprumuturiDataTable.Rows[0].ItemArray[0].ToString())<3)
            {
                int numarCarti = 0;
                for(int i=0; i < imprumutPage_wishlistTreeView.Nodes.Count; i++)
                    if (imprumutPage_wishlistTreeView.Nodes[i].Checked)
                        numarCarti++;
                for (int i = 0; i < imprumutPage_cartiTreeView.Nodes.Count; i++)
                    if (imprumutPage_cartiTreeView.Nodes[i].Checked)
                        numarCarti++;

                if (numarCarti <= 3 - Int32.Parse(imprumuturiDataTable.Rows[0].ItemArray[0].ToString()))
                {
                    for (int i = 0; i < imprumutPage_wishlistTreeView.Nodes.Count; i++)
                        if (imprumutPage_wishlistTreeView.Nodes[i].Checked)
                        {
                            insertCommand.Parameters["@idUtilizator"].Value = userId;
                            insertCommand.Parameters["@idCarte"].Value = imprumutPage_wishlistTreeView.Nodes[i].Name;
                            insertCommand.Parameters["@dataImprumut"].Value = DateTime.Now.Date;
                            insertCommand.Parameters["@dataRestituire"].Value = DateTime.Now.Date.AddDays(21);

                            bibliotecaDatabaseConection.Open();
                            insertCommand.ExecuteNonQuery();
                            bibliotecaDatabaseConection.Close();
                        }
                    for (int i = 0; i < imprumutPage_cartiTreeView.Nodes.Count; i++)
                        if (imprumutPage_cartiTreeView.Nodes[i].Checked)
                        {
                            insertCommand.Parameters["@idUtilizator"].Value = userId;
                            insertCommand.Parameters["@idCarte"].Value = imprumutPage_cartiTreeView.Nodes[i].Name;
                            insertCommand.Parameters["@dataImprumut"].Value = DateTime.Now.Date;
                            insertCommand.Parameters["@dataRestituire"].Value = DateTime.Now.Date.AddDays(21);

                            bibliotecaDatabaseConection.Open();
                            insertCommand.ExecuteNonQuery();
                            bibliotecaDatabaseConection.Close();
                        }
                    tabControler.SelectedTab = home;
                    MessageBox.Show("Cartile selectate au fost imprumutate!");
                }
                else
                    MessageBox.Show("Mai aveti voie sa imprumutati " + (3 - Int32.Parse(imprumuturiDataTable.Rows[0].ItemArray[0].ToString())).ToString() + " carti!\nDeselectati cateva carti!");
            }
        }

     
    }
}
