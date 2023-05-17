using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityProjeUygulama
{
    public partial class FrmUrun : Form
    {
        public FrmUrun()
        {
            InitializeComponent();
        }

        DbEntityUrunEntities db = new DbEntityUrunEntities();
        private void BtnListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = (from x in db.TBLURUN
                                        select new 
                                        { x.URUNID,
                                          x.URUNAD,
                                          x.MARKA, 
                                          x.STOK, 
                                          x.FIYAT,
                                          x.TBLKATEGORI.AD,
                                          x.DURUM
                                        }).ToList();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            TBLURUN t = new TBLURUN();
            t.URUNAD = TxtUrunAdı.Text;
            t.MARKA = TxtMarka.Text;
            t.STOK = short.Parse(TxtStok.Text);
            t.FIYAT = decimal.Parse(TxtFiyat.Text);
            t.DURUM = true;
            t.KATEGORI = int.Parse(CmbKategori.Text);
            db.TBLURUN.Add(t);
            db.SaveChanges();
            MessageBox.Show("Ürün Eklendi");
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(TxtID.Text);
            var urunid = db.TBLURUN.Find(x);
            db.TBLURUN.Remove(urunid);
            db.SaveChanges();
            MessageBox.Show("Ürün Silindi");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtUrunAdı.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtMarka.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            TxtStok.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtFiyat.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            TxtDurum.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            CmbKategori.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(TxtID.Text);
            var urunid = db.TBLURUN.Find(x);
            urunid.URUNAD = TxtUrunAdı.Text;
            urunid.MARKA = TxtMarka.Text;
            urunid.STOK = short.Parse(TxtStok.Text);
            urunid.FIYAT = decimal.Parse(TxtFiyat.Text);
            MessageBox.Show("Ürün Güncellendi");
        }

        private void FrmUrun_Load(object sender, EventArgs e)
        {
            var kategoriler = (from x in db.TBLKATEGORI
                               select new
                               {
                                   x.ID,
                                   x.AD
                               }
                               ).ToList();
            CmbKategori.ValueMember = "ID";
            CmbKategori.DisplayMember = "AD";
            CmbKategori.DataSource = kategoriler;
        }
    }
}
