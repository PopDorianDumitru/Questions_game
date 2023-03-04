using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Intrebari_Bac
{
    public partial class Form1 : Form
    {
        ///Memoram tabpage-urile pentru a le putea introduce sau scoate usor din tabcontrol
        TabPage imag = new TabPage(), aut = new TabPage() , log = new TabPage(), profil = new TabPage(), fundaluri = new TabPage(), strt = new TabPage();
        TabPage tpjc = new TabPage(), hlp = new TabPage(), iesire = new TabPage();
        ///Memoram date importante despre utilizator 
        ///precum nume, parola, id si pozitia lui in vectorul format din baza de date
        string utilizator_sesiune, parola_sesiune;
        int id_sesiune, index_sesiune;
        
        ///Scoate din tabcontrol tabpage-ul cu care se deschide aplicatia
        ///Insereaza tabpage-ul pentru logare in cont
        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage1);
    
            
            tabControl1.TabPages.Insert(tabControl1.TabPages.Count, log);
         
        }
        
        ///Se incarca aplicatia
        ///Creeaza si seteaza tabelurile pentru utilizator, imagini profil/fundal, cumparare imagini profil/fundal
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.Cumparari_Fundaluri' table. You can move, or remove it, as needed.
            this.cumparari_FundaluriTableAdapter.Fill(this.database1DataSet.Cumparari_Fundaluri);
            // TODO: This line of code loads data into the 'database1DataSet.Fundaluri' table. You can move, or remove it, as needed.
            this.fundaluriTableAdapter.Fill(this.database1DataSet.Fundaluri);
            // TODO: This line of code loads data into the 'database1DataSet.Cumparari_Imagini_Profil' table. You can move, or remove it, as needed.
            this.cumparari_Imagini_ProfilTableAdapter.Fill(this.database1DataSet.Cumparari_Imagini_Profil);
            // TODO: This line of code loads data into the 'database1DataSet.Imagini_profil' table. You can move, or remove it, as needed.
            this.imagini_profilTableAdapter.Fill(this.database1DataSet.Imagini_profil);
            this.utilizatoriTableAdapter.Fill(this.database1DataSet.Utilizatori);
        }
        
        ///Inserarea tabpage-ului care incarca imaginile de profil
        private void deschide_imagini_profil()
        {
            ///Daca nu exista tabpage-ul respectiv atunci nu face nimic
            if (tabControl1.TabPages.Contains(imag) == false) 
            {
                ///Parcurgem prin tabelul bazei de date ce contine imaginile de profil
                for(int i = 0; i < database1DataSet.Imagini_profil.Count; i++)
                {
                    ///Inseram fiecare imagine de profil gasita in lista
                    listBox1.Items.Add($"{database1DataSet.Imagini_profil[i].Nume_imagine}");
                }
                tabControl1.TabPages.Insert(tabControl1.TabPages.Count, imag);
            }
        }
        
        ///Cautam in tabelul de utilizatori cine este logat in aplicatie
        private void gaseste_cont(string util, string par)
        {
            int ok = 0;
            for(int i = 0; i < database1DataSet.Utilizatori.Count && ok == 0; i++)
            {
                ///Daca parola si numele de utilizatori sunt identice, inseamna ca am gasit contul
                if(util == database1DataSet.Utilizatori[i].Nume_Utilizator && par == database1DataSet.Utilizatori[i].Parola)
                {
                    ok = 1;
                    ///Retinem datele relevante pentru a nu mai avea nevoie sa le cautam
                    index_sesiune = i; ///Pozitia contului in vectorul format din tabelul de utilizatori
                    id_sesiune = database1DataSet.Utilizatori[i].Id; ///Id-ul contului conectat
                }
            }    
        }

        ///Deschidem tabpage-ul care contine profilul contului
        private void deschide_profil()
        {
            tabControl1.TabPages.Insert(tabControl1.TabPages.Count, profil);
            ///Introducem poza de profil selectata a contului
            pictureBox2.Image = Image.FromFile($"Imagini_profil\\{database1DataSet.Utilizatori[index_sesiune].Imag_prof}.jpg");
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            
            try
            {
                
                tabPage5.BackgroundImage = Image.FromFile($"Fundal\\{database1DataSet.Utilizatori[index_sesiune].Fundal_Selectat}.jpg");
                tabPage5.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch
            {

            }
            try
            {
                textBox9.Text = database1DataSet.Utilizatori[index_sesiune].Monede.ToString();
            
            }
            catch
            {
                textBox9.Text = "0";
            }
               
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string util = textBox1.Text, prl = textBox2.Text, prlc = textBox3.Text, loc = textBox4.Text, sco = textBox5.Text, adm = textBox6.Text;
            if (util == "" || prl == "" || adm == "")
                MessageBox.Show("Introduceti datele corespunzatoare!");
            else
            {
                if (prl == prlc)
                {
                    int ok = 1;
                    for (int i = 0; i < database1DataSet.Utilizatori.Count && ok == 1; i++)
                        if (util == database1DataSet.Utilizatori[i].Nume_Utilizator || adm == database1DataSet.Utilizatori[i].Adresa_Mail)
                        {
                            ok = 0;
                        }
                    if (ok == 1)
                    {
                        utilizatoriTableAdapter.Inserare_Utilizator_Nou(util, prl, loc, sco, adm, "def_avatar", 0, 0, 0);
                        utilizatoriTableAdapter.Fill(database1DataSet.Utilizatori);
                        tabControl1.TabPages.Remove(tabPage3);
                        utilizator_sesiune = util;
                        parola_sesiune = prl;
                        gaseste_cont(util, prl);
                        id_sesiune = database1DataSet.Utilizatori[index_sesiune].Id; 
                        deschide_profil();
                        
                    }
                    else
                        MessageBox.Show("Numele de utilizator sau adresa de email sunt folosite de un alt utilizator");
                    
                    
                }
                else
                    MessageBox.Show("Parolele nu sunt identice!");
                
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(tabControl1.TabPages.Contains(fundaluri) == false)
            {
                for(int i = 0; i < database1DataSet.Fundaluri.Count; i++)
                {
                    listBox2.Items.Add($"{database1DataSet.Fundaluri[i].Nume_Fundal}");
                }
                tabControl1.TabPages.Insert(tabControl1.TabPages.Count, fundaluri);
                
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox4.Image = Image.FromFile($"Fundal\\{listBox2.SelectedItem.ToString()}.jpg");
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            int index_imagine = -1;
            for (int i = 0; i < database1DataSet.Fundaluri.Count && index_imagine == -1; i++)
            {
                if (database1DataSet.Fundaluri[i].Nume_Fundal == listBox2.SelectedItem.ToString())
                    index_imagine = i;
            }
            int ok = 0;
            for (int i = 0; i < database1DataSet.Cumparari_Fundaluri.Count && ok == 0; i++)
            {
                if (database1DataSet.Cumparari_Fundaluri[i].Id_Utilizator == id_sesiune && database1DataSet.Cumparari_Fundaluri[i].Id_Fundal == database1DataSet.Fundaluri[index_imagine].Id_Fundal)
                    ok = 1;
            }
            if (ok == 0)
            {
                DialogResult set = MessageBox.Show($"Doriti sa cumparati fundalul pentru {database1DataSet.Fundaluri[index_imagine].Valoare} de monede?", "Cumparare", MessageBoxButtons.YesNo);
                if (set == DialogResult.Yes)
                {
                    if (database1DataSet.Utilizatori[index_sesiune].Monede < database1DataSet.Fundaluri[index_imagine].Valoare)
                        MessageBox.Show("Nu ai destule monede!");
                    else
                    {
                        int monede_sesiune = Convert.ToInt32(textBox9.Text) - database1DataSet.Fundaluri[index_imagine].Valoare;
                        textBox9.Text = monede_sesiune.ToString();
                        utilizatoriTableAdapter.UpdateMonedeUtilizator(monede_sesiune, id_sesiune);
                        utilizatoriTableAdapter.Fill(this.database1DataSet.Utilizatori);
                        cumparari_FundaluriTableAdapter.InsertQuery_Cumparare_Fundal(database1DataSet.Fundaluri[index_imagine].Id_Fundal, id_sesiune, database1DataSet.Fundaluri[index_imagine].Valoare);
                        cumparari_FundaluriTableAdapter.Fill(this.database1DataSet.Cumparari_Fundaluri);
                    }
                }

            }
            else
            {
                DialogResult set = MessageBox.Show("Selectezi drept fundal?", "Selectare", MessageBoxButtons.YesNo);
                if (set == DialogResult.Yes)
                {
                    tabPage5.BackgroundImage = Image.FromFile($"Fundal\\{database1DataSet.Fundaluri[index_imagine].Nume_Fundal}.jpg");
                    tabPage5.BackgroundImageLayout = ImageLayout.Stretch;
                    utilizatoriTableAdapter.UpdateQuery_Fundal_Utilizator(database1DataSet.Fundaluri[index_imagine].Nume_Fundal, id_sesiune);
                }
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DialogResult x = new DialogResult();
            x = MessageBox.Show("Esti sigur ca vrei sa stergi contul?", "Stergere cont", MessageBoxButtons.YesNo);
            if(utilizator_sesiune != "admin" && x == DialogResult.Yes)
            {
                cumparari_FundaluriTableAdapter.DeleteQueryCumparareFundal(id_sesiune);
                cumparari_Imagini_ProfilTableAdapter.DeleteQueryCumparareProfil(id_sesiune);
                utilizatoriTableAdapter.DeleteQueryUtilizator(id_sesiune);
                Application.Exit();
            }
        }

        private void InapoiLaStart(object sender, EventArgs e)
        {

            tabControl1.TabPages.Remove(log);
            tabControl1.TabPages.Insert(tabControl1.TabPages.Count, strt);
         
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if(!tabControl1.TabPages.Contains(tpjc))
             tabControl1.TabPages.Insert(tabControl1.TabPages.Count, tpjc);
            
            if (utilizator_sesiune == "admin")
            {
                panel1.Visible = true;
                utilizatoriDataGridView.Enabled = true;
            }
        }

        private void celeMaiMulteRaspunsuriCorecteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            utilizatoriDataGridView.Visible = true;
            utilizatoriTableAdapter.GetDataByRaspunsuriCorecte();
            utilizatoriTableAdapter.FillByRaspunsuriCorecte(database1DataSet.Utilizatori);
        }

        private void celeMaiMulteTestePerfecteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            utilizatoriDataGridView.Visible = true;
            utilizatoriTableAdapter.GetDataByTestePerfecte();
            utilizatoriTableAdapter.FillByTestePerfecte(database1DataSet.Utilizatori);
        }

        private void ceiMaiBogatiJucatoriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            utilizatoriDataGridView.Visible = true;
            utilizatoriTableAdapter.GetDataByBogatie();
            utilizatoriTableAdapter.FillByBogatie(database1DataSet.Utilizatori);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                int id_stergere = Convert.ToInt32(utilizatoriDataGridView.SelectedRows[0].Cells[0].Value);
                DialogResult x = new DialogResult();
                x = MessageBox.Show("Esti sigur ca vrei sa stergi contul?", "Stergere cont", MessageBoxButtons.YesNo);
                if (x == DialogResult.Yes)
                {
                    
                    cumparari_FundaluriTableAdapter.DeleteQueryCumparareFundal(id_stergere);
                    cumparari_Imagini_ProfilTableAdapter.DeleteQueryCumparareProfil(id_stergere);
                    utilizatoriTableAdapter.DeleteQueryUtilizator(id_stergere);
                    utilizatoriTableAdapter.Fill(this.database1DataSet.Utilizatori);

                }
            }
            catch
            {
                MessageBox.Show("Daca doresti sa stergi un cont, trebuie selectata o linie!");
            }
            
        }

        private void deschide_help(object sender, EventArgs e)
        {
            if(!tabControl1.TabPages.Contains(hlp))
            {

                tabControl1.TabPages.Insert(tabControl1.TabPages.Count, hlp);

            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            utilizatoriDataGridView.Visible = true;
            string s = textBox10.Text;
            utilizatoriTableAdapter.GetDataByCautareUtilizator(s);
            utilizatoriTableAdapter.FillByCautareUtilizator(database1DataSet.Utilizatori, s);
        }

        private void inchide_aplicatia(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(aut);
        


            tabControl1.TabPages.Insert(tabControl1.TabPages.Count, strt);
        
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 x = new Form2();
            x.ShowDialog();
            if (x.DialogResult == DialogResult.Cancel)
            {
                MessageBox.Show($"Ai castigat {x.punctaj} monede");
                int monede_nou = Convert.ToInt32(textBox9.Text) + x.punctaj;
                utilizatoriTableAdapter.UpdateMonedeUtilizator(monede_nou, id_sesiune);
                textBox9.Text = monede_nou.ToString();
                utilizatoriTableAdapter.Fill(this.database1DataSet.Utilizatori);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            deschide_imagini_profil();
           
        }

        

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index_imagine = -1;
            for(int i = 0; i < database1DataSet.Imagini_profil.Count && index_imagine == -1; i++)
            {
                if (database1DataSet.Imagini_profil[i].Nume_imagine == listBox1.SelectedItem.ToString())
                    index_imagine = i;
            }
            pictureBox3.Image = Image.FromFile($"Imagini_profil\\{listBox1.SelectedItem.ToString()}.jpg");
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            int ok = 0;
            for (int i = 0; i < database1DataSet.Cumparari_Imagini_Profil.Count && ok == 0; i++)
            {
                if (database1DataSet.Cumparari_Imagini_Profil[i].Id_Utilizator == id_sesiune && database1DataSet.Cumparari_Imagini_Profil[i].Id_Imagine_Profil == database1DataSet.Imagini_profil[index_imagine].Id_imagine)
                    ok = 1;
            }
            if(ok == 0)
            {
                DialogResult set = MessageBox.Show($"Doriti sa cumparati imaginea pentru {database1DataSet.Imagini_profil[index_imagine].Valoare} de monede?","Cumparare", MessageBoxButtons.YesNo);
                if(set == DialogResult.Yes)
                {
                    if (database1DataSet.Utilizatori[index_sesiune].Monede < database1DataSet.Imagini_profil[index_imagine].Valoare)
                        MessageBox.Show("Nu ai destule monede!");
                    else
                    {
                        int monede_sesiune = Convert.ToInt32(textBox9.Text) - database1DataSet.Imagini_profil[index_imagine].Valoare;
                        textBox9.Text = monede_sesiune.ToString();
                        utilizatoriTableAdapter.UpdateMonedeUtilizator(monede_sesiune, id_sesiune);
                        utilizatoriTableAdapter.Fill(this.database1DataSet.Utilizatori);
                        cumparari_Imagini_ProfilTableAdapter.InsertQuery_Cumparare_Noua(id_sesiune, database1DataSet.Imagini_profil[index_imagine].Id_imagine, database1DataSet.Imagini_profil[index_imagine].Valoare);
                        cumparari_Imagini_ProfilTableAdapter.Fill(this.database1DataSet.Cumparari_Imagini_Profil);
                    }
                }
                
            }
            else
            {
                DialogResult set = MessageBox.Show("Selectezi drept imagine de profil?", "Selectare", MessageBoxButtons.YesNo);
                if(set == DialogResult.Yes)
                {
                    pictureBox2.Image = Image.FromFile($"Imagini_profil\\{listBox1.SelectedItem.ToString()}.jpg");
                    utilizatoriTableAdapter.Update_Imagine_Profil_Utilizator(database1DataSet.Imagini_profil[index_imagine].Nume_imagine, id_sesiune);
                }
            }
        }

       
        private void button4_Click(object sender, EventArgs e)
        {
            string num = textBox7.Text, prl = textBox8.Text;
            int ok = 0;
            for (int i = 0; i < database1DataSet.Utilizatori.Count && ok == 0; i++)
                if (database1DataSet.Utilizatori[i].Nume_Utilizator == num && database1DataSet.Utilizatori[i].Parola == prl)
                    ok = 1;
            if (ok == 1)
            {
                utilizator_sesiune = num;
                parola_sesiune = prl;
                gaseste_cont(num, prl);
                tabControl1.TabPages.Remove(tabPage2);
                deschide_profil();
             
            }
            else
            {
                MessageBox.Show("Numele de utilizator sau parola sunt gresite!");
            }
        }

        public Form1()
        {
            InitializeComponent();
            tpjc = tabPage7;
            strt = tabPage1;
            imag = tabPage4;
            aut = tabPage2;
            log = tabPage3;
            profil = tabPage5;
            fundaluri = tabPage6;
            hlp = tabPage8;
          
            tabControl1.TabPages.Remove(tabPage8);
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Remove(tabPage3);
            tabControl1.TabPages.Remove(tabPage5);
            tabControl1.TabPages.Remove(tabPage4);
            tabControl1.TabPages.Remove(tabPage6);
            tabControl1.TabPages.Remove(tabPage7);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage1);
            
            tabControl1.TabPages.Insert(tabControl1.TabPages.Count ,aut);
          
            pictureBox1.Image = Image.FromFile("Imagini_profil\\def_avatar.jpg");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
