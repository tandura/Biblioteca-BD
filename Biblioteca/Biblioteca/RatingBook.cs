using MySql.Data.MySqlClient;
using MySql.Data;
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
            MySqlCommand updateCommand = new MySqlCommand("update carte set NotaCarte = (NotaCarte + @notaCarte)/2 where idCarte = @idCarte;", bibliotecaDatabaseConection);

            updateCommand.Parameters.Add("@idCarte", MySqlDbType.Int32);
            updateCommand.Parameters.Add("@notaCarte", MySqlDbType.Int32);

            updateCommand.Parameters["@notaCarte"].Value = ((FiveStarRating)sender).SelectedStar;
            updateCommand.Parameters["@idCarte"].Value = rezultatDataTable.Rows[indexRexultat]["idCarte"];

            bibliotecaDatabaseConection.Open();
            updateCommand.ExecuteNonQuery();
            bibliotecaDatabaseConection.Close();

            rezultatDataTable.Rows[indexRexultat]["NotaCarte"] = (Int32.Parse(rezultatDataTable.Rows[indexRexultat]["NotaCarte"].ToString()) + ((FiveStarRating)sender).SelectedStar) / 2;

            incarcaCatea(indexRexultat);

            MessageBox.Show("Ratingul dumneavoastra a fost luat in considerare.");
        }

    }
}