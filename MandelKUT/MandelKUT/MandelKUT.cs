using System.Drawing;
using System.Windows.Forms;
using System;
using System.Globalization;
using System.Diagnostics;
using System.Drawing.Drawing2D;



internal class MandelKUT : Form
{
    int size = 1080;
    Point Muispunt = new Point(0, 0);
    Bitmap plaatje;
    PictureBox plaatjeDoos = new PictureBox();
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

        
       //plaatje.SetResolution(4*96.0F, 4*96.0F);
        this.Text = "MandelKUT";
        this.BackColor = Color.Aqua;
        this.ClientSize = new Size(size/2, (int)(size / 1.5));
        this.MinimumSize = new Size(size / 2, (int)(size / 1.5));
        renderKnop.Click += renderKnop_Click;
        this.MouseClick += muisklik;
        this.MouseWheel += muiswiel;
        this.Paint += this.Teken;

        
    }

    private void muiswiel(object sender, MouseEventArgs e)
    {
        if (e.Delta != 0)
        { Console.Out.WriteLine(e.Delta);
        iteraties += 50*(e.Delta / 120);
        tIter.Text = iteraties.ToString();
        Invalidate();
        }
            
    }

    void muisklik(object sender, MouseEventArgs mea)
    {
        if (mea.Button == MouseButtons.Left)
        {
            Muispunt = mea.Location;
            Console.WriteLine(Muispunt.ToString());
            double Muisx = MuisNaarMand(Muispunt.X) + centerx;
            double Muisy = MuisNaarMand(Muispunt.Y-180) + centery;
           // muispunt.y -180 want het plaatje is 180 pixels naar beneden geschoven
            tX.Text = (Muisx).ToString(); tY.Text = (-Muisy).ToString();
            UpdateCenter();
            schaal += 2*schaal;
            tSchaal.Text = schaal.ToString();
            
            Invalidate();
        }

        if(mea.Button == MouseButtons.Right)
        {
            schaal -= 2*schaal;
            tSchaal.Text = schaal.ToString();
            Invalidate();
        }

    }

    double MuisNaarMand(double i)
    {
        return ((i - (double)(size / 4)) / (double)(size / 8)) / schaal;
    }

    public void UpdateCenter()
    {
        centerx = GetDouble(tX.Text, 0);
        centery = -GetDouble(tY.Text, 0);
        
    }

    public static double GetDouble(string value, double defaultValue)
    {
        double result;

        // Try parsing in the current culture
        if (!double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out result) &&
            // Then try in US english
            !double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
            // Then in neutral language
            !double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out result))
        {
            result = defaultValue;
        }
        return result;
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
        centerx = GetDouble(tX.Text, 0);
        centery = -GetDouble(tY.Text, 0);
        if (int.TryParse(tSchaal.Text, out schaal) &&
            int.TryParse(tIter.Text, out iteraties) 
            )
        {
            Console.WriteLine(centerx.ToString());
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

        factor = 20 * (1 + schaal);
        //double tussenstap = size / (4 * factor);//

        /*Color redColor = Color.FromArgb(255, 0, 0);*/
        for (int i = 0; i < plaatje.Width; i++)
        {

            for (int j = 0; j < plaatje.Height; j++)
            {
                
                double x = i;
                x = MuisNaarMand(x);

                double y = j;
                y = MuisNaarMand(y);

                
                int q = Divergerent(x+centerx, y+centery, iteraties);
                //y=y/factor-tussenstap//

                int hue = (int)(255 * q / iteraties);
                                            


                /*Color color = Color.FromArgb(255, collor, collor, collor);

                plaatje.SetPixel(i, j, color);*/
                if (q % 2 == 0)
                {
                    plaatje.SetPixel(i, j, Color.FromArgb(0, hue, 0));
                    //plaatje.SetPixel(i, j, Color.Black);
                }
                else
                {
                    plaatje.SetPixel(i, j, Color.FromArgb(0, hue+1, 0));
                }


            }
        }
        plaatje.SetPixel(plaatje.Width / 2, plaatje.Height / 2, Color.White);
        gr.DrawImage(plaatje, 0, (int)((size/2)/3));
        
        sw.Stop();
        Console.WriteLine(sw.Elapsed.ToString());
        
    }



    public static void Main()
    {
        Application.Run(new MandelKUT());
    }
}

//fix my code pls   