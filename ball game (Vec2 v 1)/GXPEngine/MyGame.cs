using GXPEngine;
using System.Drawing;

public class MyGame : Game
{
    public static MyGame myGame;
    public static MyGame GetGame() { return myGame;}
    static void Main()
    {
        myGame = new MyGame();
        myGame.Start();
    }

    Ball _ball;

    EasyDraw _text;

    public bool gameOver = false;

    public MyGame() : base(800, 600, false)
    {
        _ball = new Ball(30, new Vec2(width / 2, height / 2));
        AddChild(_ball);

        _text = new EasyDraw(150, 40);
        _text.TextAlign(CenterMode.Center, CenterMode.Center);
        AddChild(_text);

        Vec2.UnitTest();
    }

    void Update()
    {
        if (!gameOver)
        {
            _ball.Step();
        } else
        {
            _text.SetXY(width / 2 - _text.width / 2, height / 2 - _text.height / 2);
        }

        _text.Clear(Color.Transparent);
        _text.Text("Points: " + (int)_ball.GetPoints(), _text.width/2, _text.height/2);
    }
}