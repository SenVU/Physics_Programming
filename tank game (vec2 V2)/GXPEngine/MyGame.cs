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

    Tank tank;


    public MyGame() : base(800, 600, false)
    {
        tank = new Tank(70, 100, new Vec2(height / 2, width / 2));
        AddChild(tank);
        //AddChild(tank.turret);
        Vec2.UnitTest();
    }

    void Update()
    {
        tank.Step();
    }
}