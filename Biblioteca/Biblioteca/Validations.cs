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
    }
}