using System.Drawing;
using System.Windows.Forms;
using System;
using System.Diagnostics.Tracing;
using System.Runtime.ConstrainedExecution;

internal class MandelKUT : Form
{
    public MandelKUT()
    {
        this.Text = "MandelKUT";
        this.BackColor = Color.White;
        this.ClientSize = new Size(400, 400);
        this.Paint += this.Teken;

    }
    int counter = 0;
    Form scherm = new Form();

    Bitmap plaatje = new Bitmap(400, 400);
    Label afbeelding = new Label();


    public int Divergerent(double ca, double cb, int max_iter = 50)
    {
        double za = 0;
        double zb = 0; 
        for (int i = 0; i < max_iter; i++)
        { 
            ///als het kleiner is dan 4 stopt het anders gaat het door met calculeren
            if(Math.Pow(ca, 2) + Math.Pow(cb, 2) > 4)
            {
                counter = i;
                return counter;
            }
            za = Math.Pow(za, 2) - Math.Pow(zb, 2) + ca;
            zb = 2 * za * zb + cb;
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
                if(Divergerent(i,j,50)%2==0)
                {
                    plaatje.SetPixel(i, j, Color.White);
                }
                if (Divergerent(i, j, 50) % 2 != 0)
                {
                    plaatje.SetPixel(i, j, Color.Black);
                }
            }
        }

    }

    private void InitializeComponent()
    {

    }

    public static void Main()
    {
        Application.Run(new MandelKUT());
    }
}
