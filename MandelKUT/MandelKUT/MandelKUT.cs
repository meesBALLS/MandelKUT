using System.Drawing;
using System.Windows.Forms;
using System;


internal class MandelKUT : Form
{
    public MandelKUT()
    {
        this.Text = "MandelKUT";
        this.BackColor = Color.LightYellow;
        this.ClientSize = new Size(200, 100);
        this.Paint += this.Teken;
    }


    public double divergerent (double ca, double cb, int max_iter = 50)
    {
        double za = 0;
        double zb = 0; 
        for (int i = 0; i < max_iter; i++)
        {
            za = Math.Pow(za, 2) + Math.Pow(zb, 2) + ca;
            zb = 2*za*zb + cb;
        }
    }

    public void Teken(object o, PaintEventArgs pea)
    {
        Graphics gr = pea.Graphics;
        gr.FillEllipse(Brushes.Blue, 20, 20, 50, 50);
    }

    private void InitializeComponent()
    {

    }

    public static void Main()
    {
        Application.Run(new MandelKUT());
    }
}
