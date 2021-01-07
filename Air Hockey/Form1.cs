using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;

namespace Air_Hockey
{
    public partial class Form1 : Form
    {
        int paddle1X = 120;
        int paddle1Y = 35;
        int player1Score = 0;

        int paddle2X = 120;
        int paddle2Y = 450;
        int player2Score = 0;

        int goal1X = 90;
        int goal1Y = 0;
        
        int goal2X = 90;
        int goal2Y = 495;

        int goalWidth = 120;
        int goalHeight = 5;

        int paddleWidth = 60;
        int paddleHeight = 10;
        int paddleSpeed = 4;

        int ballX = 140;
        int ballY = 235;
        int ballXSpeed = 5;
        int ballYSpeed = -5;
        int ballWidth = 20;
        int ballHeight = 20;

        bool wDown = false;
        bool sDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool aDown = false;
        bool dDown = false;
        bool leftArrowDown = false;
        bool rightArrowDown = false;
        

        int centerlineX = 0;
        int centerlineY = 245;
        int centerlineWidth = 300;
        int centerlineHeight = 5;

        int centerX = 75;
        int centerY = 175;
        int centerWidth = 150;
        int centerHeight = 150;

        int sideline1X = 0;
        int sideline2X = 295;
        int sideline1Y = 0;
        int sideline2Y = 0;
        int sidelineHeight = 500;
        int sidelineWidth = 5;

        int backlineHeight = 5;
        int backlineWidth = 90;
        int backline1X = 0;
        int backline2X = 210;
        int backline3X = 0;
        int backline4X = 210;
        int backline1Y = 0;
        int backline2Y = 0;
        int backline3Y = 495;
        int backline4Y = 495;
        SoundPlayer hit = new SoundPlayer(Properties.Resources._440559__charliewd100__futuristic_smg_sound_effect);
        SoundPlayer goal = new SoundPlayer(Properties.Resources._391133__michalwa2003__horn_of_doom);


        SolidBrush blueBrush = new SolidBrush(Color.Blue);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        Font screenFont = new Font("Consolas", 12);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush yellowBrush = new SolidBrush(Color.DeepPink);
        Pen yellowPen = new Pen(Color.DeepPink, 5);
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
               

            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                
            }
        }



        private void gameTimer_Tick(object sender, EventArgs e)
        {
            ////move ball
            ballX += ballXSpeed;
            ballY += ballYSpeed;

            //move player 1
            if (wDown == true && paddle1Y > 0)
            {
                paddle1Y -= paddleSpeed;
            }
            if (sDown == true && paddle1Y < this.Height - paddleHeight)
            {
                paddle1Y += paddleSpeed;
            }
            if (aDown == true && paddle1X > 0)
            {
                paddle1X -= paddleSpeed;
            }
            if (dDown == true && paddle1X < this.Width - paddleWidth)
            {
                paddle1X += paddleSpeed;
            }
            //move player 2
            if (upArrowDown == true && paddle2Y > 0)
            {
                paddle2Y -= paddleSpeed;
            }
            if (downArrowDown == true && paddle2Y < this.Height - paddleHeight)
            {
                paddle2Y += paddleSpeed;
            }
            if (leftArrowDown == true && paddle2X > 0)
            {
                paddle2X -= paddleSpeed;
            }
            if (rightArrowDown == true && paddle2X < this.Width - paddleWidth)
            {
                paddle2X += paddleSpeed;
            }
            ////left wall collision
            if (ballX < 0)
            {
                ballXSpeed *= -1;
            }
            //right wall collision
            if (ballX > 300)
            {
                ballXSpeed *= -1;
            }
           
            //top and bottom wall collision
            if (ballY < 0 || ballY > this.Height - ballHeight)
            {
                ballYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed;
            }
            //create objects on screen to be used for collision detection
            Rectangle player1Rec = new Rectangle(paddle1X, paddle1Y, paddleWidth, paddleHeight);
            Rectangle player2Rec = new Rectangle(paddle2X, paddle2Y, paddleWidth, paddleHeight);
            Rectangle ballRec = new Rectangle(ballX, ballY, ballWidth, ballHeight);
            Rectangle goal1Rec = new Rectangle(goal1X, goal1Y, goalWidth, goalHeight);
            Rectangle goal2Rec = new Rectangle(goal2X, goal2Y, goalWidth, goalHeight);

            //check if ball hits either paddle. If it does change the direction
            //and place the ball in front of the paddle hit
            if (player1Rec.IntersectsWith(ballRec))
            {
                ballYSpeed *= -1;
                ballY = paddle1Y + paddleHeight + 1;
                hit.Play();
            }
            if (player2Rec.IntersectsWith(ballRec))
            {
                ballYSpeed *= -1;
                ballY = paddle2Y - ballHeight - 1;
                hit.Play();
            }
            //point calculation and restart positions
            if (goal1Rec.IntersectsWith(ballRec))
            {
                player2Score++;
                ballX = 140;
                ballY = 235;
                goal.Play();
                paddle1Y = 35;
                paddle2Y = 450;
                paddle1X = 120;
                paddle2X = 120;
                ballXSpeed = 3;
                ballYSpeed = -3;
                Thread.Sleep(1500);
                ballXSpeed = 5;
                ballYSpeed = -5;
            }
            else if (goal2Rec.IntersectsWith(ballRec))
            {
                player1Score++;
                ballX = 140;
                ballY = 235;
                goal.Play();
                paddle1Y = 35;
                paddle2Y = 450;
                paddle1X = 120;
                paddle2X = 120;
                ballXSpeed = 3;
                ballYSpeed = -3;
                Thread.Sleep(1500);
                ballXSpeed = 5;
                ballYSpeed = -5;
            }

            if (player1Score == 3)
            {
                gameTimer.Enabled = false;
                winnerLabel.Visible = true;
                winnerLabel.Text = "Player One Wins!";
            }
            else if (player2Score == 3)
            {
                gameTimer.Enabled = false;
                winnerLabel.Visible = true;
                winnerLabel.Text = "Player Two WIns!";          
            }
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //playing field

            e.Graphics.DrawEllipse(yellowPen, centerX, centerY, centerWidth, centerHeight);
            e.Graphics.DrawArc(yellowPen, 90, -60, 125, 120, 0, 180);
            e.Graphics.DrawArc(yellowPen, 90, 440, 125, 120, 180, 180);
            e.Graphics.FillRectangle(yellowBrush, backline1X, backline1Y, backlineWidth, backlineHeight);
            e.Graphics.FillRectangle(yellowBrush, backline2X, backline2Y, backlineWidth, backlineHeight);
            e.Graphics.FillRectangle(yellowBrush, backline3X, backline3Y, backlineWidth, backlineHeight);
            e.Graphics.FillRectangle(yellowBrush, backline4X, backline4Y, backlineWidth, backlineHeight);
            e.Graphics.FillRectangle(yellowBrush, sideline1X, sideline1Y, sidelineWidth, sidelineHeight);
            e.Graphics.FillRectangle(yellowBrush, sideline2X, sideline2Y, sidelineWidth, sidelineHeight);
            e.Graphics.FillRectangle(yellowBrush, centerlineX, centerlineY, centerlineWidth, centerlineHeight);
            
            //objects
            
            e.Graphics.FillRectangle(blueBrush, paddle1X, paddle1Y, paddleWidth, paddleHeight);
            e.Graphics.FillRectangle(redBrush, paddle2X, paddle2Y, paddleWidth, paddleHeight);
            e.Graphics.FillRectangle(blueBrush, goal1X, goal1Y, goalWidth, goalHeight);
            e.Graphics.FillRectangle(redBrush, goal2X, goal2Y, goalWidth, goalHeight);
            e.Graphics.FillRectangle(whiteBrush, ballX, ballY, ballWidth, ballHeight);

            e.Graphics.DrawString($"{player1Score}", screenFont, whiteBrush, 145, 185);
            e.Graphics.DrawString($"{player2Score}", screenFont, whiteBrush, 145, 300);
        }
    }
}
    
    

    

