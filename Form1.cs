using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mylibrary;
using System.Threading;

namespace WindowsFormsApp_liczby_pierwsze
{

    public partial class Form1 : Form

    {
       

        delegate void listboxdel(int a);
        delegate void progressbardel(int a);
        public Form1()
        {
            InitializeComponent();
            cancelTokenSource = new CancellationTokenSource();
            token = cancelTokenSource.Token;
            
        }
        CancellationTokenSource cancelTokenSource;
        CancellationToken token;
        Task t, t2;

        private void Button_enter_Click(object sender, EventArgs e)
        {
            t.Start();
            //t.Wait();

            //foreach (var item in numbers)
            //{
            //    listBox1.Items.Add(item);
            //}
        }
        static List<int> numbers = new List<int>();
        private void Form1_Load(object sender, EventArgs e)
        {
            t = new Task(() =>
            {
                int count = 0;
                for (int i = textBox_zak1.Text.i(); i < textBox_zak2.Text.i(); i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        MessageBox.Show("Cancelled");
                        return;
                    }
                    count = 0;
                    for (int h = 2; h < i; h++)
                    {
                        if (i%h==0)
                        {
                            count++;
                        }

                    }
                    if (count==0)
                    {
                        
                        numbers.Add(i);
                    }
                    Progressbaradd((i*100)/textBox_zak2.Text.i());
                }
               
            });

            t2 = t.ContinueWith((Task t) =>
            {
                foreach (var item in numbers)
                {
                    LIstboxadd(item);
                }
            });
            

            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            cancelTokenSource.Cancel();
        }

        private void ProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void LIstboxadd(int a)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new listboxdel(LIstboxadd), new object[] { a });
                return;
            }
            else
            {
                listBox1.Items.Add(a);
            }
        }


        private void Progressbaradd(int a)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new progressbardel(Progressbaradd), new object[] { a });
                return;
            }
            else
            {
                progressBar1.Value = a;
            }
        }
    }
    
}
