using System.Drawing;
using System.Windows.Forms;
using System;
using System.Diagnostics;

internal class MandelKUT : Form
{

    int size = 1080;
    Bitmap plaatje;
    public MandelKUT()
    {
        plaatje = new Bitmap(size, size);
        this.Text = "MandelKUT";
        this.BackColor = Color.Aqua;
        this.ClientSize = new Size(size, size);
        this.Paint += this.Teken;

        renderKnop.Click += renderKnop_Click;
    }



    public int Divergerent(double ca, double cb, int max_iter = 50)
    {
        double za = 0;
        double zb = 0;
        for (int i = 0; i < max_iter; i++)
        {
            ///als het kleiner is dan 4 stopt het anders gaat het door met calculeren
            if (za * za + zb * zb > 4)
            {
                return i;
            }
            double tempZa = za;
            za = za * za - zb * zb + ca;
            zb = 2 * tempZa * zb + cb;
        }
        return 0;
    }

    public void Teken(object o, PaintEventArgs pea)
    {
        Stopwatch sw = Stopwatch.StartNew();
        Graphics gr = pea.Graphics;
        for(int i = 0; i<plaatje.Width; i++)
        {
            for(int j=0; j<plaatje.Height; j++)
            {
                
                double x = i;
                double y = j;
                int factor = 200;

                x = (x- size/2)/factor;
                y = (y- size/2)/factor;
                if(Divergerent(x, y, 5000)%2==1)
                {
                    plaatje.SetPixel(i, j, Color.Aqua);
                }
                else
                {
                    plaatje.SetPixel(i, j, Color.Black);
                }

            }
        }
        gr.DrawImage(plaatje,0,0);
        sw.Stop();
        Console.WriteLine(sw.Elapsed.ToString());
    }

    private void InitializeComponent()

    public static void Main()
    {
    }
}