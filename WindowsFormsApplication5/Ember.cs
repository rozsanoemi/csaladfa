using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApplication5
{
    class Ember
    {
        public bool marbejarva = false;

        public Ember parja;
        public List<Ember> gyermekek = new List<Ember>();

        public String Nev;
        public Color Szin = Color.Black;
        

        public Ember(String Nev,Color Szin)
        {
            this.Nev = Nev;
            this.Szin = Szin;
        }

        public Ember(String Nev)
        {
            this.Nev = Nev;
        }

        public void kiir()
        {
            Console.WriteLine("Név:{0}", Nev);
        }
    }
}
