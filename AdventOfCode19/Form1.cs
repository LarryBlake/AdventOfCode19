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

            textBox1.Text = Secure().ToString();
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

        // Day 2
        long IntCode()
        {
            string dat = "1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,10,1,19,1,5,19,23,1,23,5,27,2,27,10,31,1,5,31,35,2,35,6,39,1,6,39,43,2,13,43,47,2,9,47,51,1,6,51,55,1,55,9,59,2,6,59,63,1,5,63,67,2,67,13,71,1,9,71,75,1,75,9,79,2,79,10,83,1,6,83,87,1,5,87,91,1,6,91,95,1,95,13,99,1,10,99,103,2,6,103,107,1,107,5,111,1,111,13,115,1,115,13,119,1,13,119,123,2,123,13,127,1,127,6,131,1,131,9,135,1,5,135,139,2,139,6,143,2,6,143,147,1,5,147,151,1,151,2,155,1,9,155,0,99,2,14,0,0";
            string[] ar0 = dat.Split(',');
            long ln = ar0.Length;
            long[] ar1 = new long[ln];
            //for (int i = 0; i < ln; i++) ar1[i] = long.Parse(ar0[i]);
            //ar1[1] = 12; // from "1202"
            //ar1[2] = 2;  // message
            //return IntCode2(ar1);

            for (int n = 0; n < 100; n++)
            {
                for (int v = 0; v < 100; v++)
                {
                    for (int i = 0; i < ln; i++) ar1[i] = long.Parse(ar0[i]);
                    ar1[1] = n;
                    ar1[2] = v;
                    long ans = IntCode2(ar1);
                    if (ans == 19690720) return (100 * n) + v;
                }
            }
            return -1;
        }
        long IntCode2(long[] ar1)
        { 
            int w = 0;
            while (ar1[w] != 99)
            {
                long opcode = ar1[w];
                long f1 = ar1[w + 1];
                long f2 = ar1[w + 2];
                long dest = ar1[w + 3];

                if (opcode == 1) ar1[dest] = ar1[f1] + ar1[f2];
                else if (opcode == 2) ar1[dest] = ar1[f1] * ar1[f2];

                w += 4;
            }
            return ar1[0];
        }

        // Day 3
        int Wires()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\..\wires.txt");
            Dictionary<string, int> wire1 = GetPoints(lines[0]);
            Dictionary<string, int> wire2 = GetPoints(lines[1]);

            int closest = int.MaxValue;
            foreach (string s in wire1.Keys)
            {
                if (wire2.ContainsKey(s))
                {
                    int steps = wire1[s] + wire2[s];
                    if (steps < closest) closest = steps;
                    //string[] wk = s.Split(',');
                    //int manh = Math.Abs(int.Parse(wk[0])) + Math.Abs(int.Parse(wk[1]));
                    //if (manh < closest) closest = manh;
                }
            }
            return closest;
        }
        Dictionary<string, int> GetPoints(string s)
        {
            Dictionary<string, int> pts = new Dictionary<string, int>();
            string[] dirs = s.Split(',');
            int x = 0;
            int y = 0;
            int steps = 0;
            foreach (string w in dirs)
            {
                string d = w.Substring(0, 1);
                int ln = int.Parse(w.Substring(1));
                for (int i = 0; i < ln; i++)
                {
                    if (d == "U") y--;
                    else if (d == "R") x++;
                    else if (d == "D") y++;
                    else if (d == "L") x--;

                    steps++;

                    string nw = x.ToString() + "," + y.ToString();
                    if (!pts.ContainsKey(nw)) pts.Add(nw, steps);
                }
            }
            return pts;
        }

        // Day 4
        int Secure()
        {
            int c = 0;
            int lo = 278384;
            int hi = 824795;
            for (int i = lo; i <= hi; i++)
            {
                int[] digits = new int[6];
                int w = i;
                for (int j = 5; j >= 0; j--)
                {
                    digits[j] = (w % 10);
                    w /= 10;
                }

                bool increases = true;
                Dictionary<int, int> dcount = new Dictionary<int, int>();

                for (int j = 0; j < 5; j++)
                {
                    if (digits[j] > digits[j + 1])
                    {
                        increases = false;
                        break;
                    }
                    if (dcount.ContainsKey(digits[j])) dcount[digits[j]]++;
                    else dcount.Add(digits[j], 1);
                }
                if (dcount.ContainsKey(digits[5])) dcount[digits[5]]++;
                else dcount.Add(digits[5], 1);

                if (increases)
                {
                    foreach (int v in dcount.Values)
                    {
                        if (v == 2)
                        {
                            c++;
                            break;
                        }
                    }
                }
            }
            return c;
        }

    }
}
