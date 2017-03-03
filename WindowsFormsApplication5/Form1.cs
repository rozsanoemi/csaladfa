using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GraphVizWrapper;
using GraphVizWrapper.Commands;
using GraphVizWrapper.Queries;
using System.IO;

namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        String gyujto(Ember peldany, int szint)
        {
            if (peldany.marbejarva)
            return "";


            peldany.marbejarva = true;
            StringBuilder sb = new StringBuilder();

            if (peldany.parja != null)
            {
                sb.Append(peldany.Nev);
                sb.Append("->");
                sb.Append(peldany.parja.Nev);
                sb.Append(";");

                sb.Append(gyujto(peldany.parja, szint));
            }
            foreach (Ember e in peldany.gyermekek)
            {
                sb.Append(peldany.Nev);
                sb.Append("->");
                sb.Append(e.Nev);
                sb.Append("[pendwith="+szint+", weight="+szint*2+", color=\"" + ColorTranslator.ToHtml (peldany.Szin) + "\"];");
                if (!e.marbejarva)
                    sb.Append(gyujto(e,szint+1));
                
            }

            return sb.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ember Apa = new Ember("Apa");
            Ember Anya = new Ember("Anya",Color.Red);
            Ember gy1 = new Ember("gy1");
            Ember gy2 = new Ember("gy2");
            Ember u1 = new Ember("u1");
            Ember u2 = new Ember("u2");
            Ember gy1p = new Ember("gy1p");
            Ember gy2p = new Ember("gy2p");
            Apa.parja = Anya;
            Anya.parja = Apa;


            Apa.gyermekek.Add(gy1);
            Apa.gyermekek.Add(gy2);
            Anya.gyermekek.Add(gy1);
            Anya.gyermekek.Add(gy2);

            gy1.parja = gy1p;
            gy1p.parja = gy1;

            gy2.parja = gy2p;
            gy2p.parja = gy2;

            gy1.gyermekek.Add(u1);
            gy2.gyermekek.Add(u2);
            gy1p.gyermekek.Add(u1);
            gy2p.gyermekek.Add(u2);

            Ember u1p = new Ember("u1p");
            Ember uu1 = new Ember("uu1");
            Ember uu1p = new Ember("uu1p");
            Ember uuu1 = new Ember("uuu1");
            Ember uuu2 = new Ember("uuu2");

            u1.parja = u1p;
            u1p.parja = u1;
            u1.gyermekek.Add(uu1);

            uu1.parja = uu1p;
            uu1p.parja = uu1;

            uu1.gyermekek.Add(uuu1);
            uu1.gyermekek.Add(uuu2);
            uu1p.gyermekek.Add(uuu1);
            uu1p.gyermekek.Add(uuu2);



            string s = gyujto(Apa, szint = 1) + gyujto(Anya);

            var getStartProcessQuery = new GetStartProcessQuery();
            var getProcessStartInfoQuery = new GetProcessStartInfoQuery();
            var registerLayoutPluginCommand = new RegisterLayoutPluginCommand(getProcessStartInfoQuery, getStartProcessQuery);


            var wrapper = new GraphGeneration(getStartProcessQuery,
                                              getProcessStartInfoQuery,
                                              registerLayoutPluginCommand);

            byte[] output = wrapper.GenerateGraph("digraph{" + s + "}", Enums.GraphReturnType.Png);

            using (MemoryStream ms = new MemoryStream(output))
            {
                Image i = Image.FromStream(ms);
                pictureBox1.Image = i;
            }
        }
    }
}
