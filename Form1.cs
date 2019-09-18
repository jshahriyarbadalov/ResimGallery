using ResimGallery.ORM.Entity;
using ResimGallery.ORM.Facade;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResimGallery
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Products.LisTable();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void resimEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            int id = (int)dataGridView1.CurrentRow.Cells["ProductId"].Value;

            openFileDialog1.Title = "Resim Sec";
            openFileDialog1.Filter = "Jpg |*.jpg| Png|*.png|";
            DialogResult result=openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                FileStream fs=new FileStream(openFileDialog1.FileName, FileMode.Open,FileAccess.Read);

                BinaryReader br=new BinaryReader(fs);
                byte[] resim = br.ReadBytes((int)fs.Length);
                br.Close();
                fs.Close();

                ProductPicture pp=new ProductPicture();
                pp.ProductID = id;
                pp.Picture = resim;
                if (ProductsPicture.Add(pp))
                {
                    dataGridView1.DataSource = Products.LisTable();
                    MessageBox.Show("Product added photo successful!");
                }
                else
                {
                    MessageBox.Show("Product  added photo not successful!");
                }
            }
        }

        private void resimGosterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            int id = (int)dataGridView1.CurrentRow.Cells["ProductId"].Value;

            ResimlerForm rf=new ResimlerForm();
            rf.ProductID = id;
            rf.ShowDialog();
        }
    }
}
