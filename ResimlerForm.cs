﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ResimGallery.ORM.Facade;

namespace ResimGallery
{
    public partial class ResimlerForm : Form
    {
        public ResimlerForm()
        {
            InitializeComponent();
        }

        public int ProductID { get; set; }

        DataTable ResimDoldur()
        {
            DataTable dt = ProductsPicture.LisDataTable(ProductID);
            return dt;
        }

        private void ResimlerForm_Load(object sender, EventArgs e)
        {
            DataTable dt = ResimDoldur();

            foreach (DataRow item in dt.Rows)
            {
                ResimAta(item);
                return;
            }
        }

        private void ResimAta(DataRow item)
        {
            byte[] resim = (byte[]) item.ItemArray[2];
            MemoryStream ms = new MemoryStream(resim.Length);
            ms.Write(resim, 0, resim.Length);

            Image img = Image.FromStream(ms);
            pictureBox1.BackgroundImage = img;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;

            pictureBox1.Tag = item.ItemArray[0];
            return;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            int id = (int) pictureBox1.Tag;

            DataTable dt = ResimDoldur();
            id++;
            foreach (DataRow item in dt.Rows)
            {
                if ((int)item.ItemArray[0] == id)
                {
                    ResimAta(item);
                }

            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int id = (int) pictureBox1.Tag;

            DataTable dt = ResimDoldur();
            id--;
            foreach (DataRow item in dt.Rows)
            {
                if ((int)item.ItemArray[0] == id)
                {
                    ResimAta(item);
                }

            }
        }
    }
}
