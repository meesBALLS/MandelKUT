using System.Drawing;
using System.Windows.Forms;
using System;
using System.Diagnostics;

internal class MandelKUT : Form
{

    int size = 1080;
    Bitmap plaatje;
    Button renderKnop;
    TextBox tX, tY, tSchaal, tIter;
    Label lblX, lblY, lblSchaal, lblIter;
    int schaal, iteraties, centerx, centery;
    public MandelKUT()
    {
        lblX = new Label(); lblY = new Label();
        lblX.Location = new Point(170, 10); lblY.Location = new Point(170, 35);
        lblX.Size = new Size(15, 20); lblY.Size = new Size(15, 20);
        lblX.Text = "X:"; lblY.Text = "Y:";
        Controls.Add(lblX); Controls.Add(lblY);

        tX = new TextBox(); tY = new TextBox();
        tX.Location = new Point(190, 10); tY.Location = new Point(190, 35);
        Controls.Add(tX); Controls.Add(tY);


        tSchaal = new TextBox(); Controls.Add(tSchaal); tIter = new TextBox(); Controls.Add(tIter); tIter.Location = new Point(60, 45); tSchaal.Location = new Point(60, 10);
        renderKnop = new Button(); Controls.Add(renderKnop); renderKnop.Location = new Point(60, 110); renderKnop.Text = "bereken";
        lblSchaal = new Label(); Controls.Add(lblSchaal); lblIter = new Label(); Controls.Add(lblIter);
        lblSchaal.Location = new Point(20, 10); lblSchaal.Text = "schaal:";
        lblIter.Location = new Point(20, 35); lblIter.Text = "max \naantal:";
        lblIter.Size = new Size(50, 40);

        plaatje = new Bitmap(size / 2, size / 2);
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



    void renderKnop_Click(object o, EventArgs e)
    {
        if (int.TryParse(tSchaal.Text, out schaal) &&
            int.TryParse(tIter.Text, out iteraties) &&
            int.TryParse(tX.Text, out centerx) && // Discard the result
            int.TryParse(tY.Text, out centery))  // Discard the result
        {
            // Continue with your code
            Invalidate();
        }
        else
        {
            MessageBox.Show("Please enter valid integers.");
        }
    }


    public void Teken(object o, PaintEventArgs pea)
    {
        Stopwatch sw = Stopwatch.StartNew();
        Graphics gr = pea.Graphics;
        double factor = 20 * (1 + schaal);
        //double tussenstap = size / (4 * factor);//
        double centerX = (double)centerx;
        double centerY = (double)centery;
        centerX /= 100.0;
        centerY /= 100.0;
        for (int i = 0; i < plaatje.Width; i++)
        {

            for (int j = 0; j < plaatje.Height; j++)
            {

                double x = i;
                x = (x - size / 4) / factor + centerX;

                double y = j;
                y = (y - size / 4) / factor + centerY;

                int q = Divergerent(x + centerx, y+centery, iteraties);
                //y=y/factor-tussenstap//
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
        gr.DrawImage(plaatje, size / 8, size / 8);
        sw.Stop();
        Console.WriteLine(sw.Elapsed.ToString());
    }



    public static void Main()
    {
        Application.Run(new MandelKUT());
    }
}