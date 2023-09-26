using System.Drawing;
using System.Windows.Forms;
using System;
using System.Diagnostics;
using System.Drawing.Drawing2D;


internal class MandelKUT : Form
{
    int size = 1080;
    Point Muispunt = new Point(0, 0);
    Bitmap plaatje;
    Button renderKnop;
    TextBox tX, tY, tSchaal, tIter;
    Label lbX, lbY, lbSchaal, lbIter;
    int schaal = 1, iteraties = 50 ;
    double centerx, centery, factor;


    public MandelKUT()
    {
        lbX = new Label(); lbY = new Label();
        lbX.Location = new Point(170, 10); lbY.Location = new Point(170, 35);
        lbX.Size = new Size(15, 20); lbY.Size = new Size(15, 20);
        lbX.Text = "X:"; lbY.Text = "Y:";
        Controls.Add(lbX); Controls.Add(lbY);

        tX = new TextBox(); tY = new TextBox();
        tX.Location = new Point(190, 10); tY.Location = new Point(190, 35);
        Controls.Add(tX); Controls.Add(tY);


        tSchaal = new TextBox(); Controls.Add(tSchaal); tIter = new TextBox(); Controls.Add(tIter); tIter.Location = new Point(60, 45); tSchaal.Location = new Point(60, 10);
        renderKnop = new Button(); Controls.Add(renderKnop); renderKnop.Location = new Point(60, 110); renderKnop.Text = "bereken";
        lbSchaal = new Label(); Controls.Add(lbSchaal); lbIter = new Label(); Controls.Add(lbIter);
        lbSchaal.Location = new Point(20, 10); lbSchaal.Text = "schaal:";
        lbIter.Location = new Point(20, 35); lbIter.Text = "max \naantal:";
        lbIter.Size = new Size(50, 40);

        

        plaatje = new Bitmap(size / 2, size / 2);
        plaatje.SetResolution(96.0F, 96.0F);
        this.Text = "MandelKUT";
        this.BackColor = Color.Aqua;
        this.ClientSize = new Size(size, size);
        this.Paint += this.Teken;

        renderKnop.Click += renderKnop_Click;
        this.MouseClick += muisklik;
    }
    void muisklik(object sender, MouseEventArgs mea)
    {
        Muispunt = mea.Location;
        Console.WriteLine(Muispunt.ToString());
        double Muisx = (Muispunt.X-size) / factor;
        double Muisy = (Muispunt.Y-size) / factor;
        tX.Text = Muisx.ToString(); tY.Text = Muisy.ToString();
        /*centerx = Muisx;
        centery = Muisy;
        Invalidate();*/
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



    void renderKnop_Click(object o, EventArgs e)
    {
        if (int.TryParse(tSchaal.Text, out schaal) &&
            int.TryParse(tIter.Text, out iteraties) &&
            double.TryParse(tX.Text, out centerx) &&
            double.TryParse(tY.Text, out centery))
        {

            Invalidate();
        }
        else
        {
            MessageBox.Show("Please enter integers in all the boxes.");
        }
    }


    public void Teken(object o, PaintEventArgs pea)
    {
        Stopwatch sw = Stopwatch.StartNew();
        Graphics gr = pea.Graphics;
        gr.SmoothingMode = SmoothingMode.AntiAlias;

        factor = 20 * (1 + schaal);
        //double tussenstap = size / (4 * factor);//

        /*Color redColor = Color.FromArgb(255, 0, 0);*/
        for (int i = 0; i < plaatje.Width; i++)
        {

            for (int j = 0; j < plaatje.Height; j++)
            {
                
                double x = i;
                x = (x - size / 4) / factor + centerx;
                
                double y = j;
                y = (y - size / 4) / factor + centery;
                Color redColor = Color.FromArgb(255, 0, 0);
                int q = Divergerent(x + centerx, y + centery, iteraties);
                //y=y/factor-tussenstap//



                /*Color color = Color.FromArgb(255, collor, collor, collor);

                plaatje.SetPixel(i, j, color);*/
                if (q % 2 == 0)
                {
                    plaatje.SetPixel(i, j, Color.Pink);
                }
                else if (q % 3 == 0)
                {
                    plaatje.SetPixel(i, j, Color.Aqua);
                }
                else if (q % 5 == 0)
                {
                    plaatje.SetPixel(i, j, Color.Black);
                }

            }
        }
        gr.DrawImage(plaatje, size / 16, size / 16);
        sw.Stop();
        Console.WriteLine(sw.Elapsed.ToString());
        
    }



    public static void Main()
    {
        Application.Run(new MandelKUT());
    }
}