using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biblioteca
{
    public partial class aplicatie : Form
    {
        private void inserareRating()
        {
            rating = new FiveStarRating();
            rating.Top = 227;
            rating.Left = 150; 
            rating.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            rating.OutlineColor = Color.Black;
            rating.StarSelected += new StarSelectedEventHandler(SelectieRating);
            this.rezultateleCautariiPage.Controls.Add(this.rating);
        }

        private void tabControler_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControler.SelectedTab == tabControler.TabPages["rezultateleCautariiPage"])
                inserareRating();
            else
                if (this.rezultateleCautariiPage.Controls.Contains(rating) )
                    this.rezultateleCautariiPage.Controls.Remove(rating);
        }

        private void SelectieRating(object sender, EventArgs e)
        {
            MessageBox.Show("Ratingul este: " + ((FiveStarRating)sender).SelectedStar.ToString());
        }

    }
}