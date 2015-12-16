using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biblioteca
{
    public partial class aplicatie : Form
    {
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ai puso!");
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControler.SelectedTab = loginPage;
        }

        private void signinToolStripMenuItem_Click(object sender, EventArgs e)
        {
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