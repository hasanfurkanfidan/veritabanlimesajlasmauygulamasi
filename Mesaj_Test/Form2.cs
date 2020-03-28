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
namespace Mesaj_Test
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
         public string numara;
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-3VB3SSC\MSSQLSERVERR;Initial Catalog=Mesajlasma;Integrated Security=True");
        private void Form2_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            lblnumara.Text = numara;
            SqlCommand komut = new SqlCommand("select ad,soyad from tblkisiler where numara = @p1",baglanti);
            komut.Parameters.AddWithValue("@p1", lblnumara.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                lbladsoyad.Text = dr[0].ToString() + " " + dr[1].ToString();
            }
            baglanti.Close();
            baglanti.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tblmesajlar where alici=" + lblnumara.Text, baglanti);

            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
            baglanti.Open();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select * from tblmesajlar where gonderen=" + lblnumara.Text, baglanti);

            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into tblmesajlar (gonderen,alici,baslik,icerik) values (@p1,@p2,@p3,@p4)", baglanti);
            komut.Parameters.AddWithValue("@p1", lblnumara.Text);
            komut.Parameters.AddWithValue("@p2", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p3", textBox1.Text);
            komut.Parameters.AddWithValue("@p4", richTextBox1.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Mesajınız iletildi...");
            
           
                
        }
    }
}
