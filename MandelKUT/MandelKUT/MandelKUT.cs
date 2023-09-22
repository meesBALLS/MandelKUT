using System.Drawing;
using System.Windows.Forms;
using System;
using System.Diagnostics.Tracing;
using System.Runtime.ConstrainedExecution;
using System.Diagnostics;

internal class MandelKUT : Form
{

    int counter = 0;
    int size = 1080;
    Bitmap plaatje;
    Button schaalKnop;
    TextBox t;
    int schaal;
    public MandelKUT()
    {
        t = new TextBox(); Controls.Add(t);
        schaalKnop = new Button(); Controls.Add(schaalKnop); schaalKnop.Location = new Point(110, 110);
        plaatje = new Bitmap(size, size);
        this.Text = "MandelKUT";
        this.BackColor = Color.LightBlue;
        this.ClientSize = new Size(size, size);
        this.Paint += this.Teken;

        schaalKnop.Click += schaalKnop_Click;
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
                counter = i;
                return counter;
            }
            double tempZa = za;
            za = za*za - zb*zb + ca;
            zb = 2 * tempZa * zb + cb;
        }
        return 0;
    }

    void schaalKnop_Click(object o, EventArgs e)
    {
        schaal = int.Parse(t.Text);
        Console.WriteLine(schaal.ToString());
        Invalidate();
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
                int factor = 20*(1+schaal);

                x = (x- size/2)/factor;
                y = (y- size/2)/factor;
                if(Divergerent(x, y, 5000)%2==1)
                {
                    plaatje.SetPixel(i, j, Color.Aqua);
                }
                else
                {
                    plaatje.SetPixel(i, j, Color.Pink);
                }
            }
        }
        gr.DrawImage(plaatje,0,0);
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
