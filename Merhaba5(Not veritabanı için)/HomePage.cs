using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Merhaba5_Not_veritabanı_için_
{

    /************* NOT *********/
    //Proje adına sağ tıklayıp ekle diyip veri sekmesi altında ado.net entity adlı ögeyi ekliyoruz
    //Gerekli veritabanı bağlantılarını yapıp veritabanını seçiyoruz
    //Veritabanı aşaması sırasında entity ismi soracaktır.Entity ismini kendimizin belirlemesi çalışma için avantaj sağlayacaktır.
    //Daha sonra kod kısmından bağlantıları yapıp hallediyoruz.
    public partial class HomePage : Form
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_ders_listesi_Click(object sender, EventArgs e)
        {
            /*SqlConnection baglanti = new SqlConnection(@"Data Source=HALBIM0004081;Persist Security Info=True;User ID=sa;Password=Halic1998");
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from dersler",baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();*/

            notlarEntities2 db = new notlarEntities2();
            var query = from item in db.dersler
                        select new { item.DERSID, item.DERSAD };
            dataGridView1.DataSource = query.ToList();

        }


        void ogrenci_listele()
        {
            notlarEntities2 db = new notlarEntities2();
            dataGridView1.DataSource = db.ogrenci.ToList();//Veritabanındaki öğrenciler tablosundan verileri alır.Ama entity üstünden alır.
            dataGridView1.Columns[3].Visible = true;
            dataGridView1.Columns[4].Visible = false;
        }

        private void btn_ogr_listele_Click(object sender, EventArgs e)
        {
            ogrenci_listele();
        }


        private void btn_not_listesi_Click(object sender, EventArgs e)
        {
            notlarEntities2 db = new notlarEntities2();
            var query = from item in db.ogrenci_not
                        select new { item.NOTID, item.OGR, item.DERS, item.SINAV1 };//Link to SQL Sorgusu
            dataGridView1.DataSource = query.ToList();
        }

        private void btn_kaydet_Click(object sender, EventArgs e)
        {
            notlarEntities2 ent = new notlarEntities2();

            ogrenci yeniogrenci = new ogrenci();
            yeniogrenci.AD = txt_box_ograd.Text.Trim();
            yeniogrenci.SOYAD = txt_box_ogrsoyad.Text.Trim();
            yeniogrenci.FOTOGRAF = txt_box_ogrfotograf.Text.Trim();
            ent.ogrenci.Add(yeniogrenci);
            ent.SaveChanges();
            MessageBox.Show("Öğrenci başarıyla kaydedildi");
            txt_box_ograd.ResetText();
            txt_box_ogrsoyad.ResetText();
            txt_box_ogrfotograf.ResetText();

        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
            notlarEntities2 db = new notlarEntities2();

            int id = Convert.ToInt32(txt_box_ogrid.Text);
            var x = db.ogrenci.Find(id);
            var y = db.ogrenci_not.Find(id);
            db.ogrenci_not.Remove(y);
            db.SaveChanges();
            db.ogrenci.Remove(x);
            MessageBox.Show("Öğrenci başarıyla silindi");

        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            notlarEntities2 db = new notlarEntities2();
            int id = Convert.ToInt32(txt_box_ogrid.Text);
            var x = db.ogrenci.Find(id);
            x.AD = txt_box_ograd.Text;
            x.SOYAD = txt_box_ogrsoyad.Text;
            x.FOTOGRAF = txt_box_ogrfotograf.Text;
            db.SaveChanges();
            MessageBox.Show("Güncelleme işlemi başarıyla tamamlandı");
        }

        private void btn_porcedure_Click(object sender, EventArgs e)
        {
            notlarEntities2 db = new notlarEntities2();
            dataGridView1.DataSource = db.notlistesi();
            if(checkBox1.Checked == true)
            {
                dataGridView1.DataSource = db.notlistesi().OrderBy(y => y.Ad_Soyad).ToList();
            }

            if (radio_z_a.Checked == true)
            {
                dataGridView1.DataSource = db.notlistesi().OrderByDescending(y => y.Ad_Soyad).ToList();
            }
        }

        private void btn_bul_Click(object sender, EventArgs e)
        {
            notlarEntities2 db = new notlarEntities2();
            dataGridView1.DataSource = db.ogrenci.Where(x => x.AD == txt_box_ograd.Text).ToList();
        }

        private void btn_bul_procedure_Click(object sender, EventArgs e)//Sınavda çıkabilir
        {
            notlarEntities2 db = new notlarEntities2();
            dataGridView1.DataSource = db.notlistesi().Where(x => x.Ad_Soyad == txt_box_ograd.Text).ToList();
        }

        private void btn_bul_prosedür_ders_Click(object sender, EventArgs e)
        {
            notlarEntities2 db = new notlarEntities2();
            dataGridView1.DataSource = db.notlistesi().Where(x => x.DERSAD == txt_box_dersad.Text).ToList();
        }

        private void txt_box_ograd_TextChanged(object sender, EventArgs e)
        {
            notlarEntities2 db = new notlarEntities2();
            string aranan = txt_box_ograd.Text;
            var degerler = from item in db.ogrenci
                           where item.AD.Contains(aranan)
                           select item;
            dataGridView1.DataSource = degerler.ToList();
        }

        private void radio_a_z_CheckedChanged(object sender, EventArgs e)
        {
            notlarEntities2 db = new notlarEntities2();
            dataGridView1.DataSource = db.notlistesi().OrderBy(y => y.Ad_Soyad).ToList();
        }

        private void radio_z_a_CheckedChanged(object sender, EventArgs e)
        {
            notlarEntities2 db = new notlarEntities2();
            dataGridView1.DataSource = db.notlistesi().OrderByDescending(y => y.Ad_Soyad).ToList();
        }

        private void btn_link_Click(object sender, EventArgs e)
        {
            notlarEntities2 db = new notlarEntities2();
            if (checkBox1.Checked == true && radio_a_z.Checked == true)
            {
                dataGridView1.DataSource = db.notlistesi().OrderBy(y => y.Ad_Soyad).Take(3).ToList();
            }

            if (checkBox1.Checked == true && radio_z_a.Checked == true)
            {
                dataGridView1.DataSource = db.notlistesi().OrderByDescending(y => y.Ad_Soyad).Take(3).ToList();
            }

        }
    }
}
