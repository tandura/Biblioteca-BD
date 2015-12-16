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
        protected void logareAdmin()
        {
            foreach (ToolStripMenuItem item in menuStrip.Items)
                    item.Visible = true;
        }

        protected void logareBibliotecar()
        {
            foreach (ToolStripMenuItem item in menuStrip.Items)
            {
                if (item.Name != "administrareToolStripMenuItem")
                    item.Visible = true;
                else
                {
                    item.Visible = true;
                    foreach (ToolStripMenuItem subItem in item.DropDown.Items)
                        if (subItem.Name == "acordaFunctieToolStripMenuItem")
                            subItem.Visible = false;
                }
            }
        }

        protected void logareUser()
        {
            foreach (ToolStripMenuItem item in menuStrip.Items)
                if (item.Name != "administrareToolStripMenuItem")
                    item.Visible = true;
        }

    }
}