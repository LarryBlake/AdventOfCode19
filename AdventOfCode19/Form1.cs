using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdventOfCode19
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Show();

            textBox1.Text = Tyranny().ToString();
        }

        // Day 1
        int Tyranny()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\..\tyranny.txt");
            int ans = 0;
            foreach (string s in  lines)
            {
                int m = int.Parse(s);
                int f = Fuel(m);
                while (f > 0)
                {
                    ans += f;
                    f = Fuel(f);
                }
            }
            return ans;
        }
        int Fuel(int mass)
        {
            int f = mass / 3;
            f -= 2;
            if (f < 0) f = 0;

            return f;
        }
    }
}
