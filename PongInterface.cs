//Name: Carlos Augusto Potter Neto
//Name: Lucas Siqueira
//Course ID: CPSC223N
//Assignment 6

using System;
using System.Drawing;
using System.Windows.Forms;

public class PongInterface : Form
{
    private Button newgameb = new Button();
    private Button gobutton = new Button();
    private Button pausebutton = new Button();
    private Button quitbutton = new Button();

    private Label title = new Label();
    private Label speedlabel = new Label();
    private Label playerr = new Label();
    private Label playerl = new Label();
    private Label winner = new Label();

    private TextBox speedinput = new TextBox();
    private TextBox directioninput = new TextBox();

    public Timer balltimer;

    public System.ComponentModel.IContainer chorand = null;

    public PongInterface()
    {
        chorand = new System.ComponentModel.Container();
        Text = "Assignment 6";
        title.Text = "Pong";
        speedlabel.Text = "Speed";
        gobutton.Text = "Go";
        quitbutton.Text = "Quit";
        newgameb.Text = "New";
        pausebutton.Text = "Pause";
        playerr.Text = "Player 2: ";
        playerl.Text = "Player 1: ";


        Size = new Size(650, 600);
        title.Size = new Size(250, 15);
        speedlabel.Size = new Size(50, 15);
        speedinput.Size = new Size(90, 15);
        gobutton.Size = new Size(90, 30);
        pausebutton.Size = new Size(90, 30);
        quitbutton.Size = new Size(90, 30);
        newgameb.Size = new Size(90, 30);
        playerr.Size = new Size(90, 30);
        playerl.Size = new Size(90, 30);
        winner.Size = new Size(100, 40);

        title.Location = new Point(200, 30);
        speedlabel.Location = new Point(190, 360);
        speedinput.Location = new Point(240, 355);
        gobutton.Location = new Point(400, 350);
        pausebutton.Location = new Point(500, 350);
        quitbutton.Location = new Point(500, 500);
        newgameb.Location = new Point(50, 350);
        playerr.Location = new Point(450, 410);
        playerl.Location = new Point(50, 410);
        winner.Location = new Point(200, 430);

        Controls.Add(title);
        Controls.Add(speedlabel);
        Controls.Add(speedinput);
        Controls.Add(newgameb);
        Controls.Add(gobutton);
        Controls.Add(pausebutton);
        Controls.Add(quitbutton);
        Controls.Add(playerl);
        Controls.Add(playerr);
        Controls.Add(winner);

        balltimer = new System.Windows.Forms.Timer(chorand);

        newgameb.Click += new EventHandler(create);
        gobutton.Click += new EventHandler(startball);
        pausebutton.Click += new EventHandler(pauseball);
        quitbutton.Click += new EventHandler(stoprun);

        gobutton.Enabled = false;
        pausebutton.Enabled = false;
        speedinput.Enabled = false;
        directioninput.Enabled = false;

    }
    bool paused;
    int x = 400;
    int y = 99999;
    int ypr = 160;
    int ypl = 160;
    int hitbound = 0;
    int speed = 0;
    int pointsR;
    int pointsL;
    int angle;
    int deltay;
    int hitpaddle;
    int randang;

    Random rnd = new Random();


    protected override void OnPaint(PaintEventArgs pnt)
    {
        Graphics table = pnt.Graphics;
        table.FillRectangle(Brushes.Green, 19, 50, 591, 280);
        System.Drawing.SolidBrush bolinha = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        table.FillEllipse(bolinha, x, y, 15, 15);
        System.Drawing.SolidBrush paddler = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        System.Drawing.SolidBrush paddlel = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        table.FillRectangle(paddlel, 19, ypl, 10, 60);
        table.FillRectangle(paddler, 600, ypr, 10, 60);
        base.OnPaint(pnt);
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (paused == false)
        {
            if (keyData.Equals(Keys.Down))
            {
                ypr += 15;
                if (ypr > 270)
                {
                    ypr = 270;
                }
            }
            else if (keyData.Equals(Keys.Up))
            {
                ypr -= 15;
                if (ypr < 50)
                {
                    ypr = 50;
                }
            }
            else if (keyData.Equals(Keys.W))
            {
                ypl -= 15;
                if (ypl < 50)
                {
                    ypl = 50;
                }
            }
            else if (keyData.Equals(Keys.S))
            {
                ypl += 15;
                if (ypl > 270)
                {
                    ypl = 270;
                }
            }
        }
        else if (paused == true)
        {
            if (keyData.Equals(Keys.Down))
            {
                
            }
            else if (keyData.Equals(Keys.Up))
            {               
            
            }
            else if (keyData.Equals(Keys.W))
            {
            
            }

            else if (keyData.Equals(Keys.S))
            {

            }
            
        }


        Invalidate();
        return base.ProcessCmdKey(ref msg, keyData);
    }


    public void create(Object sender, EventArgs events)
    {
        //balltimer = new System.Windows.Forms.Timer(chorand);
        speed = 0;
        hitbound = 0;
        paused = false;
        balltimer.Stop();
        x = 310;
        y = 180;
        playerl.Text = "Player 1: ";
        playerr.Text = "Player 2: ";
        pointsL = 0;
        pointsR = 0;

        gobutton.Enabled = true;
        pausebutton.Enabled = true;
        speedinput.Enabled = true;
        directioninput.Enabled = true;

        Invalidate();
    }

    public void startball(Object sender, EventArgs events)
    {
        //balltimer = new System.Windows.Forms.Timer(chorand);
        gobutton.Enabled = false;
        speed = int.Parse(speedinput.Text);
        if (speed == 0)
        {
            speed = 1;
        }
        balltimer.Interval = speed;
        balltimer.Start();
        balltimer.Tick += new EventHandler(ballmove);

        angle = rnd.Next(359);
        while(angle >= 80 & angle <= 100 | angle >= 260 & angle <= 280)
        {
            angle = rnd.Next(359);
        }        

        deltay = PongLogic.verticaldelta(angle);
    }

    public void ballmove(Object sender, EventArgs events)
    {

        if (hitpaddle == 0)
        {
            if (hitbound == 0)
            {
                if ((angle >= 0 && angle < 90) || (angle > 270 && angle <= 360))
                {
                    x = x + 5;
                    y = y - deltay;
                    if (y < 48)
                    {
                        y = 48;
                        hitbound = 1;
                    }
                    if (y > 315)
                    {
                        y = 315;
                        hitbound = 1;
                    }
                    //----------
                    if (x < 29 & (y >= ypl & y <= ypl + 60))
                    {
                        x = 29;
                        hitpaddle = 1;
                    }
                    if (x > 585 & (y >= ypr & y <= ypr + 60))
                    {
                        x = 585;
                        hitpaddle = 1;
                    }
                    //-------------
                }

                if ((angle > 90 && angle <= 180) || (angle >= 180 && angle < 270))
                {
                    x = x - 5;
                    y = y + deltay;
                    if (y < 48)
                    {
                        y = 48;
                        hitbound = 1;
                    }
                    if (y > 315)
                    {
                        y = 315;
                        hitbound = 1;
                    }
                    //----------
                    if (x < 29 & (y >= ypl & y <= ypl + 60))
                    {
                        x = 29;
                        hitpaddle = 1;
                    }
                    if (x > 585 & (y >= ypr & y <= ypr + 60))
                    {
                        x = 585;
                        hitpaddle = 1;
                    }
                    //-------------
                }
            }
            else if (hitbound == 1)
            {
                if ((angle >= 0 && angle < 90) || (angle > 270 && angle <= 360))
                {
                    x = x + 5;
                    y = y + deltay;
                    if (y < 48)
                    {
                        y = 48;
                        hitbound = 0;
                    }
                    if (y > 315)
                    {
                        y = 315;
                        hitbound = 0;
                    }
                    //----------
                    if (x < 29 & (y >= ypl & y <= ypl + 60))
                    {
                        x = 29;
                        hitpaddle = 1;
                    }
                    if (x > 585 & (y >= ypr & y <= ypr + 60))
                    {
                        x = 585;
                        hitpaddle = 1;
                    }
                    //-------------
                }

                if ((angle > 90 && angle <= 180) || (angle >= 180 && angle < 270))
                {
                    x = x - 5;
                    y = y - deltay;
                    if (y < 48)
                    {
                        y = 48;
                        hitbound = 0;
                    }
                    if (y > 315)
                    {
                        y = 315;
                        hitbound = 0;
                    }
                    if(x < 29 & (y >= ypl & y <= ypl+60))
                    {
                        x = 29;
                        hitpaddle = 1;
                    }
                    if (x > 585 & (y >= ypr & y <= ypr + 60)) //& y <= ypr + 60
                    {
                        x = 585;
                        hitpaddle = 1;
                    }
                 }                
            }
        }

        if (hitpaddle == 1)
        {
            if (hitbound == 0)
            {
                if ((angle >= 0 && angle < 90) || (angle > 270 && angle <= 360))
                {
                    x = x - 5;
                    y = y - deltay;
                    if (y < 48)
                    {
                        y = 48;
                        hitbound = 1;
                    }
                    if (y > 315)
                    {
                        y = 315;
                        hitbound = 1;
                    }
                    //----------
                    if (x < 29 & (y >= ypl & y <= ypl + 60))
                    {
                        x = 29;
                        hitpaddle = 0;
                    }
                    if (x > 585 & (y >= ypr & y <= ypr + 60))
                    {
                        x = 585;
                        hitpaddle = 0;
                    }
                    //-------------
                }

                if ((angle > 90 && angle <= 180) || (angle >= 180 && angle < 270))
                {
                    x = x + 5;
                    y = y + deltay;
                    if (y < 48)
                    {
                        y = 48;
                        hitbound = 1;
                    }
                    if (y > 315)
                    {
                        y = 315;
                        hitbound = 1;
                    }
                    //----------
                    if (x < 29 & (y >= ypl & y <= ypl + 60))
                    {
                        x = 29;
                        hitpaddle = 0;
                    }
                    if (x > 585 & (y >= ypr & y <= ypr + 60))
                    {
                        x = 585;
                        hitpaddle = 0;
                    }
                    //-------------
                }
            }
            else if (hitbound == 1)
            {
                if ((angle >= 0 && angle < 90) || (angle > 270 && angle <= 360))
                {
                    x = x - 5;
                    y = y + deltay;
                    if (y < 48)
                    {
                        y = 48;
                        hitbound = 0;
                    }
                    if (y > 315)
                    {
                        y = 315;
                        hitbound = 0;
                    }
                    //----------
                    if (x < 29 & (y >= ypl & y <= ypl + 60))
                    {
                        x = 29;
                        hitpaddle = 0;
                    }
                    if (x > 585 & (y >= ypr & y <= ypr + 60))
                    {
                        x = 585;
                        hitpaddle = 0;
                    }
                    //-------------
                }

                if ((angle > 90 && angle <= 180) || (angle >= 180 && angle < 270))
                {
                    x = x + 5;
                    y = y - deltay;
                    if (y < 48)
                    {
                        y = 48;
                        hitbound = 0;
                    }
                    if (y > 315)
                    {
                        y = 315;
                        hitbound = 0;
                    }
                    //----------
                    if (x < 29 & (y >= ypl & y <= ypl + 60))
                    {
                        x = 29;
                        hitpaddle = 0;
                    }
                    if (x > 585 & (y >= ypr & y <= ypr + 60))
                    {
                        x = 585;
                        hitpaddle = 0;
                    }
                    //-------------
                }

        }
            
        }


        if (x <= 19)
        {
            pointsR++;
            playerl.Text = "Player 1: " + pointsL;
            playerr.Text = "Player 2: " + pointsR;
            if (pointsR == 5)
            {
                x = 400;
                y = 9999;
                winner.Text = "PLAYER 2 WINS";
                paused = true;
                balltimer.Stop();
            }
            else
            {
                x = 310;
                y = 180;
                angle = rnd.Next(359);
                while (angle >= 80 & angle <= 100 | angle >= 260 & angle <= 280)
                {
                    angle = rnd.Next(359);
                }

                deltay = PongLogic.verticaldelta(angle);
            }
        }

        if (x > 610)
        {
            pointsL++;
            playerl.Text = "Player 1: " + pointsL;
            playerr.Text = "Player 2: " + pointsR;
            if (pointsL == 5)
            {
                x = 400;
                y = 9999;
                winner.Text = "PLAYER 1 WINS";
                paused = true;
                balltimer.Stop();
            }
            else
            {
                x = 310;
                y = 180;
                angle = rnd.Next(359);
                while (angle >= 80 & angle <= 100 | angle >= 260 & angle <= 280)
                {
                    angle = rnd.Next(359);
                }

                deltay = PongLogic.verticaldelta(angle);
            }
        }

        Invalidate();
        Console.WriteLine("x = " + x + " y = " + y + "angle =  " + angle);
        //Console.WriteLine(angle);
    }

    public void pauseball(Object sender, EventArgs events)
    {
        if (paused == false)
        {
            balltimer.Stop();
            paused = true;
        }
        else
        {
            balltimer.Start();
            paused = false;
        }
    }

    public void stoprun(Object sender, EventArgs events)
    {
        Close();
    }

}
