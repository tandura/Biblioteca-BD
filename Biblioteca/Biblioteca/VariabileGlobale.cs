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
        FiveStarRating rating = new FiveStarRating();
        protected string conectionString = "server=localhost;port=3100;database=biblioteca;username=root;password=root";
        protected MySqlConnection bibliotecaDatabaseConection;
    }
}