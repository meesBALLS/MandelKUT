using System.Drawing;
using System.Windows.Forms;
using System;
using System.Diagnostics.Tracing;
using System.Runtime.ConstrainedExecution;
using System.Diagnostics;

internal class MandelKUT : Form
{

    int size = 1080;
    Bitmap plaatje;
    Button renderKnop;
    TextBox t, t2;
    int schaal; int iteraties;
    public MandelKUT()
    {
        t = new TextBox(); Controls.Add(t); t2 = new TextBox(); Controls.Add(t2); t2.Location = new Point(40, 40);
        renderKnop = new Button(); Controls.Add(renderKnop); renderKnop.Location = new Point(110, 110);
        plaatje = new Bitmap(size/2, size/2);
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
            if(za*za + zb*zb > 4)
            {
                return i;
            }
            double tempZa = za;
            za = za*za - zb*zb + ca;
            zb = 2 * tempZa * zb + cb;
        }
        return 0;
    }

    void renderKnop_Click(object o, EventArgs e)
    {
        schaal = int.Parse(t.Text);
        iteraties = int.Parse(t2.Text);
        Console.WriteLine(schaal.ToString());
        Invalidate();
    }

    public void Teken(object o, PaintEventArgs pea)
    {
        Stopwatch sw = Stopwatch.StartNew();
        Graphics gr = pea.Graphics;
        double factor = 20 * (1 + schaal);
        //double tussenstap = size / (4 * factor);//
        for (int i = 0; i<plaatje.Width; i++)
        {
            
            for (int j=0; j<plaatje.Height; j++)
            {
                double x = i;
                x = (x - size / 4) / factor;
                //x = x/factor-tussenstap//
                double y = j;               
                y = (y- size/4)/factor;

                int q = Divergerent(x + 0.5, y, iteraties);
                //y=y/factor-tussenstap//
                if (q%2==0)
                {
                    plaatje.SetPixel(i, j, Color.Pink);
                }
                else if (q%3==0)
                {
                    plaatje.SetPixel(i, j, Color.Aqua);
                }
                else if (q%5==0)
                {
                    plaatje.SetPixel(i, j, Color.Black);
                }

            }
        }
        gr.DrawImage(plaatje,size/8,size/8);
        sw.Stop();
        Console.WriteLine(sw.Elapsed.ToString());
        
    }

    private void InitializeComponent()
    {
    }
    
    

    
    public static void Main()
    {
        Application.Run(new MandelKUT());
        
    }
}
