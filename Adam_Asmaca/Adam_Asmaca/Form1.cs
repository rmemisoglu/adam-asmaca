using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace Adam_Asmaca
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int temp;
        string encok;
        int qtsira;
        int index; //kelimeleriGetir() fonksiyonunda indexleme değişkeni.
        int sira;  //bulunan harfin kelimenin hangi sırasında olduğunu tutan değişken. 
        int sayac = 0; //tur sayacı.
        int yanlis = 0;
        int count = 0;
        string harfler = "abcçdefgğhıijklmnoöprsştuüvyz";  //aranacek harfler dizesi.
        string harfler2 = "abcçdefgğhıijklmnoöprsştuüvyz";
        string[,] harfTekrar = new string[29, 2];


        string[] kelime = new string[7423]; //kelimeler.txt dosyasından çekilen kelimeler dizisi.

        ArrayList diziList = new ArrayList(); //uygun kriterlerde kelimelerin tutulduğu dinamik boyutlu arraylist.
        string aranan;  //aranan kelime.
        int klength = 0;  //aranan kelime uzunluğu


        public void kelimeleriGetir()
        {

            StreamReader sr = new StreamReader("kelimeler.txt", Encoding.GetEncoding("windows-1254"));
            index = 0;
            while (!sr.EndOfStream)
            {
                string satir = sr.ReadLine();
                kelime[index] = satir;
                index++;

            }

            sr.Close();


        }

        public void temizle() //aranan kelimelerde olmayan harfleri silme
        {
            for (int sd = 0; sd < 29; sd++)
            {
                if (harfTekrar[sd, 1] == "0" && harfler.IndexOf(harfTekrar[sd, 0]) > -1)
                {
                    qtsira = harfler.IndexOf(harfTekrar[sd, 0]);
                    harfler = harfler.Remove(qtsira, 1);
                }
            }
        }
        public string encokbul(string[,] ss)
        {
            int max = Convert.ToInt16(ss[0, 1]);
            int max2 = 0;

            for (int i = 1; i < ss.GetLength(0); i++)
            {
                if (max < Convert.ToInt16(ss[i, 1]))
                {
                    max2 = i;
                }
            }
            string s = ss[max2, 0];

            return s;
        }
        public void harfUret()
        {
            Random rastgele = new Random();
            
            string uret = "";
            temizle();
            uret = harfler[rastgele.Next(harfler.Length)].ToString(); //üretilen random sayıya göre gelen harf.
            int q = harfler.IndexOf(uret); //random üretilen harfin sırası  
            harfler = harfler.Remove(q, 1);   //bir üretilen harfi birdaha üretmemek için o harfi silme işlemi.
            label4.Text = harfler;
            listBox4.Items.Add(harfler);

            label3.Text = uret;

            listBox2.Items.Add(label3.Text);

        }
        public string kelimeSec()
        {
            kelimeleriGetir();
            Random r = new Random();
            int rkelime = r.Next(kelime.Length);
            return kelime[rkelime]; //üretilen random sayıya göre kelime dizisinden kelime seçimi.
        }

        public string ayniSira(string a1, string b1, string c1)
        {
            string geridonus = "";
            bool gg = true;
            for (int i = 0; i < a1.Length; i++)
            {
                if (b1.IndexOf(c1, i) == (a1.IndexOf(c1, i)) && a1.Length == b1.Length)
                {
                    gg = true;
                }
                else if (b1.IndexOf(c1, i) != (a1.IndexOf(c1, i)) && a1.Length == b1.Length)
                {
                    gg = false;
                    break;
                }

            }
            if (gg == true)
            {
                geridonus = a1;
            }
            return geridonus;
        }
        public void doldur()
        {
            for (int i = 0; i < 29; i++)
            {
                harfTekrar[i, 0] = harfler2[i].ToString();
                harfTekrar[i, 1] = "0";
            }
        }
        public void tekrar(string t,string th)
        {
           
            int hsira = 0;
            
            string harf="";
            for (int sw = 0; sw < t.Length; sw++)
            {
                harf = t[sw].ToString();
                hsira = harfler2.IndexOf(harf);
                count = Convert.ToInt32(harfTekrar[hsira, 1]) + 1;
                harfTekrar[hsira, 1] = count.ToString();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            temp = 0;
            temizle();
            diziList.Clear();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            timer1.Interval = 500;   //timer aralığı(ms)
            timer1.Enabled = true;
            harfler = "abcçdefgğhıijklmnoöprsştuüvyz";
            sayac = 0;
            yanlis = 0;
            kelimeleriGetir();
            aranan = kelimeSec();   //kelime sec methodundan return olan değeri aranan değişkenine yükleme.
            //aranan = "çözdürülebilme";
            klength = aranan.Length;
            label2.Text = aranan;

            foreach(Control c in panel1.Controls)
            {
                if (c.GetType() == typeof(Label))
                {
                    c.BackColor = Color.Gold;
                }
            }

            for (int i = 0; i < kelime.Length; i++)
            {
                string z = kelime[i];
                if (z.Length == klength) //aranan kelimeyle aynı uzunluktaki kelimeleri diziListe yükleme.
                {
                    diziList.Add(z);
                }

            }

            for (int j = 0; j < diziList.Count; j++)
            {
                listBox1.Items.Add(diziList[j]);    //diziListi listbox'a yazdırma. 
                tekrar(listBox1.Items[j].ToString(),harfler);

            }
            label5.Text = "";
            for (int i = 0; i < 29; i++)
            {
                label5.Text += harfTekrar[i, 0] + " " + harfTekrar[i, 1] + ",";
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            //temizle();
            harfUret(); //harf üret methodundan random bir harf.(üretilen harf label3 te tutulmaktadır)
            doldur();


            if (aranan.Contains(label3.Text) == true) //random harf eğer aranan kelimesinde varsa. 
            {
                foreach (Control b in panel1.Controls)
                {
                    if (b.Text == label3.Text)
                    {
                        b.BackColor = Color.Green;
                    }
                }

                sira = aranan.IndexOf(label3.Text);   //harfin aranan kelimesinde hangi sırada olduğunu sira değişkenine yükledik.  

                listBox1.Items.Clear(); //listbox' burada temizlemenin amacı listbox'u diziList'e yüklenecek olan uygun 
                                        //kriterlerdeki kelimeler için depolama alanı olarak kullanmak.
                for (int i = 0; i < diziList.Count; i++)
                {
                    if (sira == diziList[i].ToString().IndexOf(label3.Text) && aranan.Length == diziList[i].ToString().Length)//dizilist'te aranan ile aynı uzunlukta
                                                                                                                              //olan ve bulunan harf ile aynı sırada harfleri barındıran kelimeleri
                    {
                        listBox1.Items.Add(diziList[i]);    //listboxa yükleme
                        
                    }
                   
                }
                foreach(string sd in listBox1.Items)
                {
                    tekrar(sd,harfler);

                }
                diziList.Clear();//dizilisti temizleme (buradaki amaç yukardaki depolama mantığını açıklıyor)
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    diziList.Add(listBox1.Items[i]);//listboxu diziliste atma(her kriterin aslında kendinden önceki kriterleride barındırması mantığı) 
                }
            }
            else if (!aranan.Contains(label3.Text)) //random harf eğer aranan kelimesinde yoksa.
            {
                foreach(Control b in panel1.Controls)
                {
                    if (b.Text == label3.Text)
                    {
                        b.BackColor = Color.Red;
                    }
                }
                yanlis++;
                listBox1.Items.Clear();
                for (int i = 0; i < diziList.Count; i++)
                {
                    if (diziList[i].ToString().Contains(label3.Text) == false && aranan.Length == diziList[i].ToString().Length) //dizilist'te içinde o harften bulunmayan
                                                                                                                                 //ve aranan kelime ile aynı uzunluktaki harfleri
                    {
                        listBox1.Items.Add(diziList[i]); //listbox'a atma
                       
                    }

                }
                foreach (string sd in listBox1.Items)
                {
                    tekrar(sd,harfler);
                }
                diziList.Clear();//dizilisti temizleme (buradaki amaç yukardaki depolama mantığını açıklıyor)
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    diziList.Add(listBox1.Items[i]);//listboxu diziliste atma(her kriterin aslında kendinden önceki kriterleride barındırması mantığı) 
                }


            }
            label5.Text = "";
            for (int i = 0; i < 29; i++)
            {
                label5.Text += harfTekrar[i, 0] + " " + harfTekrar[i, 1] + " , ";
                
            }
            listBox3.Items.Add(label5.Text);
            sayac++; //tur sayısı
            label1.Text = sayac.ToString();

            
            if (listBox1.Items.Count == 1)//bütün kriterlerin sonucunda listbox'ta bir tane kelime kaldıysa 
            {
                if (aranan.Equals(listBox1.Items[0]))
                { //bu kelime aranana eşitse
                   
                        timer1.Enabled = false;
                        MessageBox.Show(label2.Text + " kelimesini " + yanlis + " tanesi yanlış toplam " +sayac+ " hamlede buldu");
                    

                }

            }
            listBox1.Items.Clear(); //listboxu timer interval değerine göre

            for (int i = 0; i < diziList.Count; i++)
            {
                listBox1.Items.Add(diziList[i]);    //her adımda güncellemek.

            }



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            kelimeleriGetir();
            doldur();
            timer1.Enabled = false;
         

        }

    }
}
