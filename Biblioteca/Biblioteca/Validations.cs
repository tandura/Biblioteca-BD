using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Biblioteca
{
    public partial class aplicatie : Form
    {
        private void signin_ocupatieComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((String)signin_ocupatieComboBox.SelectedItem == "Alta ocupatie")
            {
                signin_ocupatieTextBox.Text = "Introduceti ocupatia";
                signin_ocupatieTextBox.Visible = true;
            }
            else
                signin_ocupatieTextBox.Visible = false;
        }

        private void signin_confirmPassTextBox_Leave(object sender, EventArgs e)
        {
            if (signin_passwordTextBox.Text != signin_confirmPassTextBox.Text)
            {
                signin_comfirmPassErrorLabel.Text = "Password don't mach!";
                signin_passwordErrorLabel.Text = "Password don't mach!";

                signin_comfirmPassErrorLabel.Visible = true;
                signin_passwordErrorLabel.Visible = true;
            }
            else
            {
                signin_comfirmPassErrorLabel.Visible = false;
                signin_passwordErrorLabel.Visible = false;
            }
        }

        private void signin_passwordTextBox_Leave(object sender, EventArgs e)
        {
            if(signin_confirmPassTextBox.Text != "")
            {
                if (signin_confirmPassTextBox.Text != signin_passwordTextBox.Text)
                {
                    signin_comfirmPassErrorLabel.Text = "Password don't mach!";
                    signin_passwordErrorLabel.Text = "Password don't mach!";

                    signin_comfirmPassErrorLabel.Visible = true;
                    signin_passwordErrorLabel.Visible = true;
                }
                else
                {
                    signin_comfirmPassErrorLabel.Visible = false;
                    signin_passwordErrorLabel.Visible = false;
                }
            }
        }

        private void signin_orasTextBox_Leave(object sender, EventArgs e)
        {
            if (signin_orasTextBox.Text != "")
            {
                if (signin_orasErrorLabel.Visible)
                    signin_orasErrorLabel.Visible = false;

                if (signin_stradaTextBox.Text == "")
                {
                    signin_stradaErrorLabel.Text = "Daca ati introdus \"orasul\", \"strada\" devine obligatorie!";
                    signin_stradaErrorLabel.Visible = true;
                }

                if (signin_codPostalTextBox.Text == "")
                {
                    signin_codPostalErrorLabel.Text = "Daca ati introdus \"orasul\", \"codul postal\" devine obligatoriu!";
                    signin_codPostalErrorLabel.Visible = true;
                }
            }
            else
                if((signin_orasTextBox.Text=="" && signin_stradaTextBox.Text=="" && signin_codPostalTextBox.Text=="")||(signin_orasTextBox.Text=="" && signin_stradaTextBox.Text=="" && signin_codPostalTextBox.Text==""))
                {
                    signin_stradaErrorLabel.Visible = false;
                    signin_orasErrorLabel.Visible = false;
                    signin_codPostalErrorLabel.Visible = false;
                }
                else
                    if (signin_codPostalTextBox.Text != "")
                    {
                        signin_orasErrorLabel.Text = "Daca ati introdus \"codul postal\", \"orasul\" devine obligatoriu!";
                        signin_orasErrorLabel.Visible = true;
                    }
                    else
                        if (signin_stradaTextBox.Text != "")
                        {
                            signin_orasErrorLabel.Text = "Daca ati introdus \"strada\", \"orasul\" devine obligatoriu!";
                            signin_orasErrorLabel.Visible = true;
                        }
        }

        private void signin_codPostalTextBox_Leave(object sender, EventArgs e)
        {
            if (signin_codPostalTextBox.Text != "")
            {
                if (signin_codPostalErrorLabel.Visible)
                    signin_codPostalErrorLabel.Visible = false;

                if (signin_stradaTextBox.Text == "")
                {
                    signin_stradaErrorLabel.Text = "Daca ati introdus \"codul postal\", \"strada\" devine obligatorie!";
                    signin_stradaErrorLabel.Visible = true;
                }

                if (signin_orasTextBox.Text == "")
                {
                    signin_orasErrorLabel.Text = "Daca ati introdus \"codul postal\", \"orasul\" devine obligatoriu!";
                    signin_orasErrorLabel.Visible = true;
                }
            }
            else
                if ((signin_orasTextBox.Text == "" && signin_stradaTextBox.Text == "" && signin_codPostalTextBox.Text == "") || (signin_orasTextBox.Text == "" && signin_stradaTextBox.Text == "" && signin_codPostalTextBox.Text == ""))
                {
                    signin_stradaErrorLabel.Visible = false;
                    signin_orasErrorLabel.Visible = false;
                    signin_codPostalErrorLabel.Visible = false;
                }
                else
                    if (signin_orasTextBox.Text != "")
                    {
                        signin_codPostalErrorLabel.Text = "Daca ati introdus \"orasul\", \"codul postal\" devine obligatoriu!";
                        signin_codPostalErrorLabel.Visible = true;
                    }
                    else
                        if (signin_stradaTextBox.Text != "")
                        {
                            signin_codPostalErrorLabel.Text = "Daca ati introdus \"strada\", \"codul postal\" devine obligatoriu!";
                            signin_codPostalErrorLabel.Visible = true;
                        }
        }

        private void signin_stradaTextBox_Leave(object sender, EventArgs e)
        {
            if (signin_stradaTextBox.Text != "")
            {
                if (signin_stradaErrorLabel.Visible)
                    signin_stradaErrorLabel.Visible = false;

                if (signin_codPostalTextBox.Text == "")
                {
                    signin_codPostalErrorLabel.Text = "Daca ati introdus \"strada\", \"codul postal\" devine obligatoriu!";
                    signin_codPostalErrorLabel.Visible = true;
                }

                if (signin_orasTextBox.Text == "")
                {
                    signin_orasErrorLabel.Text = "Daca ati introdus \"strada\", \"orasul\" devine obligatoriu!";
                    signin_orasErrorLabel.Visible = true;
                }
            }
            else
                if ((signin_orasTextBox.Text == "" && signin_stradaTextBox.Text == "" && signin_codPostalTextBox.Text == "") || (signin_orasTextBox.Text == "" && signin_stradaTextBox.Text == "" && signin_codPostalTextBox.Text == ""))
                {
                    signin_stradaErrorLabel.Visible = false;
                    signin_orasErrorLabel.Visible = false;
                    signin_codPostalErrorLabel.Visible = false;
                }
                else
                    if (signin_orasTextBox.Text != "")
                    {
                        signin_stradaErrorLabel.Text = "Daca ati introdus \"orasul\", \"strada\" devine obligatorie!";
                        signin_stradaErrorLabel.Visible = true;
                    }
                    else
                        if (signin_codPostalTextBox.Text != "")
                        {
                            signin_stradaErrorLabel.Text = "Daca ati introdus \"codul postal\", \"strada\" devine obligatorie!";
                            signin_stradaErrorLabel.Visible = true;
                        }
        }

        private void signin_serverEmailTextBox_Leave(object sender, EventArgs e)
        {
            if (signin_serverEmailTextBox.Text == "")
            {
                if (signin_corpEmailTextBox.Text != "")
                {
                    signin_emailErrorLabel.Text = "Email invalid!";
                    signin_emailErrorLabel.Visible = true;
                }
                else
                    signin_emailErrorLabel.Visible = false;
            }
            else
            {
                if (signin_corpEmailTextBox.Text != "")
                {
                    EmailChecker emailChecker = new EmailChecker();
                    if (emailChecker.IsValidEmail(signin_corpEmailTextBox.Text.Trim() + "@" + signin_serverEmailTextBox.Text.Trim()))
                    {
                        signin_emailErrorLabel.Visible = false;
                    }
                    else
                    {
                        signin_emailErrorLabel.Text = "Email invalid!";
                        signin_emailErrorLabel.Visible = true;
                    }
                }
                else
                {
                    signin_emailErrorLabel.Text = "Email invalid!";
                    signin_emailErrorLabel.Visible = true;
                }
            }
        }

        private void signin_corpEmailTextBox_Leave(object sender, EventArgs e)
        {
            if (signin_corpEmailTextBox.Text != "" && signin_serverEmailTextBox.Text != "")
            {
                EmailChecker emailChecker = new EmailChecker();
                if (emailChecker.IsValidEmail(signin_corpEmailTextBox.Text.Trim() + "@" + signin_serverEmailTextBox.Text.Trim()))
                    signin_emailErrorLabel.Visible = false;
                else
                {
                    signin_emailErrorLabel.Text = "Email invalid!";
                    signin_emailErrorLabel.Visible = true;
                }
            }
            else
                if (signin_corpEmailTextBox.Text == "" && signin_serverEmailTextBox.Text == "")
                    signin_emailErrorLabel.Visible = false;
        }

        private void acordareFunctiePage_usernameTextBox_Leave(object sender, EventArgs e)
        {
            if (acordareFunctiePage_usernameTextBox.Text != "")
                acordareFunctiePage_usernameErrorLabel.Visible = false;
            else
            {
                acordareFunctiePage_usernameErrorLabel.Text = "Introduceti un username!";
                acordareFunctiePage_usernameErrorLabel.Visible = true;
                return;
            }
        }

        private void acordareFunctiePage_functieComboBox_Leave(object sender, EventArgs e)
        {
            if (acordareFunctiePage_functieComboBox.SelectedIndex == 0)
            {
                acordareFunctiePage_functieErrorLabel.Text = "Selectati Functia!";
                acordareFunctiePage_functieErrorLabel.Visible = true;
                return;
            }
            else
                acordareFunctiePage_functieErrorLabel.Visible = false;
        }

        private void inserareCartePage_numarPaginiTextBox_Leave(object sender, EventArgs e)
        {
            int numarPagini;
            if (inserareCartePage_numarPaginiTextBox.Text == "" || !Int32.TryParse(inserareCartePage_numarPaginiTextBox.Text, out numarPagini))
            {
                inserareCartePage_nrPaginiErrorLabel.Text = "Necesar!";
                inserareCartePage_nrPaginiErrorLabel.Visible = true;
                return;
            }
            else
                inserareCartePage_nrPaginiErrorLabel.Visible = false;
        }

        private void inserareCartePage_notaCarteTextBox_Leave(object sender, EventArgs e)
        {
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
        }

        private void inserareCartePage_numarExemplareTextBox_Leave(object sender, EventArgs e)
        {
            if (inserareCartePage_numarExemplareTextBox.Text == "")
            {
                inserareCartePage_exemplareErrorLabel.Text = "Necesar!";
                inserareCartePage_exemplareErrorLabel.Visible = true;
            }
            else
                inserareCartePage_exemplareErrorLabel.Visible = false;
        }

        private void inserareCartePage_edituriCheckList_Leave(object sender, EventArgs e)
        {
            if (inserareCartePage_edituriCheckList.CheckedItems.Count == 0)
            {
                inserareCartePage_edituraErrorLabel.Text = "Selectati cel putin o editura!";
                inserareCartePage_edituraErrorLabel.Visible = true;
                return;
            }
            else
                inserareCartePage_edituraErrorLabel.Visible = false;
        }

        private void inserareCartePage_autoriCheckList_Leave(object sender, EventArgs e)
        {
            if (inserareCartePage_autoriCheckList.CheckedItems.Count == 0)
            {
                inserareCartePage_autorErrorLabel.Text = "Selectati cel putin un autor!";
                inserareCartePage_autorErrorLabel.Visible = true;
                return;
            }
            else
                inserareCartePage_autorErrorLabel.Visible = false;
        }

        private void inserareCartePage_genuriCheckList_Leave(object sender, EventArgs e)
        {
            if (inserareCartePage_genuriCheckList.CheckedItems.Count == 0)
            {
                inserareCartePage_genErrorLabel.Text = "Selectati cel putin un gen!";
                inserareCartePage_genErrorLabel.Visible = true;
                return;
            }
            else
                inserareCartePage_genErrorLabel.Visible = false;
        }

        private void inserareCartePage_titluTextBox_Leave(object sender, EventArgs e)
        {
            if (inserareCartePage_titluTextBox.Text == "")
            {
                inserareCartePage_titluErrorLabel.Text = "Introduceti un titlu!";
                inserareCartePage_titluErrorLabel.Visible = true;
                return;
            }
            else
                inserareCartePage_titluErrorLabel.Visible = false;
        }

        private void inserareCartePage_isbnTextBox_Leave(object sender, EventArgs e)
        {
            if (IsbnValidation.TryValidate(inserareCartePage_isbnTextBox.Text) == false)
            {
                inserareCartePage_isbnErrorLabel.Text = "Isbn-ul nu este valid";
                inserareCartePage_isbnErrorLabel.Visible = true;
                return;
            }
            else
                inserareCartePage_isbnErrorLabel.Visible = false;
        }
    }
}