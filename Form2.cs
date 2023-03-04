using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intrebari_Bac
{
    public partial class Form2 : Form
    {
        int x, y, z, w, q;
        public int punctaj = 0;
        Random intrebare = new Random();
        private void Seteaza_Raspunsuri(int x, int y, int z, int w, int q)
        {
            radioButton1.Text = database1DataSet.Intrebari[x].Raspuns_1;
            radioButton2.Text = database1DataSet.Intrebari[x].Raspuns_2;
            radioButton3.Text = database1DataSet.Intrebari[x].Raspuns_3;
            radioButton4.Text = database1DataSet.Intrebari[x].Raspuns_4;

            radioButton5.Text = database1DataSet.Intrebari[y].Raspuns_1;
            radioButton6.Text = database1DataSet.Intrebari[y].Raspuns_2;
            radioButton7.Text = database1DataSet.Intrebari[y].Raspuns_3;
            radioButton8.Text = database1DataSet.Intrebari[y].Raspuns_4;

            radioButton9.Text = database1DataSet.Intrebari[z].Raspuns_1;
            radioButton10.Text = database1DataSet.Intrebari[z].Raspuns_2;
            radioButton11.Text = database1DataSet.Intrebari[z].Raspuns_3;
            radioButton12.Text = database1DataSet.Intrebari[z].Raspuns_4;

            radioButton13.Text = database1DataSet.Intrebari[w].Raspuns_1;
            radioButton14.Text = database1DataSet.Intrebari[w].Raspuns_2;
            radioButton15.Text = database1DataSet.Intrebari[w].Raspuns_3;
            radioButton16.Text = database1DataSet.Intrebari[w].Raspuns_4;

            radioButton17.Text = database1DataSet.Intrebari[q].Raspuns_1;
            radioButton18.Text = database1DataSet.Intrebari[q].Raspuns_2;
            radioButton19.Text = database1DataSet.Intrebari[q].Raspuns_3;
            radioButton20.Text = database1DataSet.Intrebari[q].Raspuns_4;
        }
        private void Creeaza_intrebari(int x, int y, int z, int w, int q)
        {
            richTextBox1.Text = database1DataSet.Intrebari[x].Enunt.ToString();
            richTextBox2.Text = database1DataSet.Intrebari[y].Enunt.ToString();
            richTextBox3.Text = database1DataSet.Intrebari[z].Enunt.ToString();
            richTextBox4.Text = database1DataSet.Intrebari[w].Enunt.ToString();
            richTextBox5.Text = database1DataSet.Intrebari[q].Enunt.ToString();
        }
        private void Seteaza_Imagini(int x, int y, int z, int w, int q)
        {
            try { pictureBox1.Image = Image.FromFile($"Imagini_intrebare\\{database1DataSet.Intrebari[x].Imagine}");
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;}
            catch { }
            try {  pictureBox2.Image = Image.FromFile($"Imagini_intrebare\\{database1DataSet.Intrebari[y].Imagine}");
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;}
            catch { }
            try { pictureBox3.Image = Image.FromFile($"Imagini_intrebare\\{database1DataSet.Intrebari[z].Imagine}");
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;}
            catch { }
            try { pictureBox4.Image = Image.FromFile($"Imagini_intrebare\\{database1DataSet.Intrebari[w].Imagine}");
                pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;}
            catch { }
            try { pictureBox5.Image = Image.FromFile($"Imagini_intrebare\\{database1DataSet.Intrebari[q].Imagine}");
                pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage; }
            catch { }
        }
        private void Corecteaza_Intrebare(GroupBox t, int nr, RadioButton ans)
        {
            
                ans.BackColor = System.Drawing.Color.Red;
            
            foreach(RadioButton rdb in t.Controls.OfType<RadioButton>())
            {
                if(rdb.Text == database1DataSet.Intrebari[nr][$"Raspuns_{database1DataSet.Intrebari[nr].Raspuns_Corect}"].ToString())
                {
                    rdb.BackColor = System.Drawing.Color.LightGreen;
                    break;
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void intrebariBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.intrebariBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.database1DataSet);

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.Intrebari' table. You can move, or remove it, as needed.
      
            // TODO: This line of code loads data into the 'database1DataSet.Intrebari' table. You can move, or remove it, as needed.
            this.intrebariTableAdapter.Fillintrebare(this.database1DataSet.Intrebari);
            x = intrebare.Next(0, database1DataSet.Intrebari.Count - 1);
            y = intrebare.Next(0, database1DataSet.Intrebari.Count - 1);
            while (y == x) y = intrebare.Next(0, database1DataSet.Intrebari.Count - 1);
            z = intrebare.Next(0, database1DataSet.Intrebari.Count - 1);
            while (z == y || z == x) z = intrebare.Next(0, database1DataSet.Intrebari.Count - 1);
            w = intrebare.Next(0, database1DataSet.Intrebari.Count - 1);
            while (w == x || w == y || w == z) w = intrebare.Next(0, database1DataSet.Intrebari.Count - 1);
            q = intrebare.Next(0, database1DataSet.Intrebari.Count - 1);
            while (q == x || q == y || q == z || q == w) q = intrebare.Next(0, database1DataSet.Intrebari.Count - 1);
            Creeaza_intrebari(x, y, z, w, q);
            Seteaza_Raspunsuri(x, y, z, w, q);
            Seteaza_Imagini(x, y, z, w, q);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Visible = true;
            string rasp1 = "", rasp2 = "", rasp3 = "", rasp4 = "", rasp5 = "";
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
            groupBox5.Enabled = false;
            RadioButton ans1 = new RadioButton(), ans2= new RadioButton(), ans3 = new RadioButton(), ans4 = new RadioButton(), ans5= new RadioButton();
            foreach(RadioButton rdb in groupBox1.Controls.OfType<RadioButton>())
            {
                if(rdb.Checked == true)
                {
                    rasp1 = rdb.Text;
                    ans1 = rdb;
                }
            }
            foreach (RadioButton rdb in groupBox2.Controls.OfType<RadioButton>())
            {
                if (rdb.Checked == true)
                {
                    rasp2 = rdb.Text;
                    ans2 = rdb;
                }
            }
            foreach (RadioButton rdb in groupBox3.Controls.OfType<RadioButton>())
            {
                if (rdb.Checked == true)
                {
                    rasp3 = rdb.Text;
                    ans3 = rdb;
                }
            }
            foreach (RadioButton rdb in groupBox4.Controls.OfType<RadioButton>())
            {
                if (rdb.Checked == true)
                {
                    rasp4 = rdb.Text;
                    ans4 = rdb;
                }
            }
            foreach (RadioButton rdb in groupBox5.Controls.OfType<RadioButton>())
            {
                if (rdb.Checked == true)
                {
                    rasp5 = rdb.Text;
                    ans5 = rdb;
                }
            }
            int rasp_cor = 0;
            if (rasp1 == database1DataSet.Intrebari[x][$"Raspuns_{database1DataSet.Intrebari[x].Raspuns_Corect}"].ToString())
            {
                rasp_cor++;
                ans1.BackColor = System.Drawing.Color.LightGreen;
            }
            else
                Corecteaza_Intrebare(groupBox1, x, ans1);
            if (rasp2 == database1DataSet.Intrebari[y][$"Raspuns_{database1DataSet.Intrebari[y].Raspuns_Corect}"].ToString())
            {
                rasp_cor++;
                ans2.BackColor = System.Drawing.Color.LightGreen;
            }
            else
                Corecteaza_Intrebare(groupBox2, y, ans2);
            if (rasp3 == database1DataSet.Intrebari[z][$"Raspuns_{database1DataSet.Intrebari[z].Raspuns_Corect}"].ToString())
            {
                rasp_cor++;
                ans3.BackColor = System.Drawing.Color.LightGreen;
            }
            else
                Corecteaza_Intrebare(groupBox3, z, ans3);
            if (rasp4 == database1DataSet.Intrebari[w][$"Raspuns_{database1DataSet.Intrebari[w].Raspuns_Corect}"].ToString())
            {
                rasp_cor++;
                ans4.BackColor = System.Drawing.Color.LightGreen;
            }
            else
                Corecteaza_Intrebare(groupBox4, w, ans4);
            if (rasp5 == database1DataSet.Intrebari[q][$"Raspuns_{database1DataSet.Intrebari[q].Raspuns_Corect}"].ToString())
            {
                rasp_cor++;
                ans5.BackColor = System.Drawing.Color.LightGreen;
            }
            else
                Corecteaza_Intrebare(groupBox5, q, ans5);
            MessageBox.Show($"Raspunsuri corecte: {rasp_cor}/5");
            punctaj = rasp_cor * 20;
        }
    }
}
