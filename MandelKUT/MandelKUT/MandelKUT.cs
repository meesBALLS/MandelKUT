using System.Drawing;
using System.Windows.Forms;
using System;
using System.Diagnostics.Tracing;
using System.Runtime.ConstrainedExecution;

internal class MandelKUT : Form
{
    int counter = 0;
    Form scherm = new Form();
    int size = 2160;
    Label afbeelding = new Label();
    Bitmap plaatje;
    public MandelKUT()
    {
        plaatje = new Bitmap(size, size);
        this.Text = "MandelKUT";
        this.BackColor = Color.LightBlue;
        this.ClientSize = new Size(size, size);
        this.Paint += this.Teken;

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

    public void Teken(object o, PaintEventArgs pea)
    {
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
                if(Divergerent(x, y, 500)%2==1)
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
    }

    private void InitializeComponent()
    {

    }

    public static void Main()
    {
        Application.Run(new MandelKUT());
    }
}
