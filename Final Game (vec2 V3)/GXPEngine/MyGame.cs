using GXPEngine;
using System.Collections.Generic;
using System.Drawing;
using System.Net.WebSockets;

public class MyGame : Game
{
    public static MyGame myGame;
    public static MyGame GetGame() { return myGame;}
    static void Main()
    {
        myGame = new MyGame();
        myGame.Start();
    }

    HoverCraft Player;
    PlayerCamera camera;

    public List<HoverCraft> hoverCrafts = new List<HoverCraft>();

    public MyGame() : base(800, 600, false)
    {
        //Vec2.UnitTest();
        Player = new HoverCraft(80);
        camera = new PlayerCamera(0,0,width, height, Player);
        //AddChild(camera);

        hoverCrafts.Add(Player);

        foreach (HoverCraft craft in hoverCrafts)
        {
            AddChild(craft);
        }
    }

    void Update()
    {
        float WSControl = (Input.GetKey(Key.W) ? 1 : 0) + (Input.GetKey(Key.S) ? -1 : 0);
        float ADControl = (Input.GetKey(Key.D) ? 1 : 0) + (Input.GetKey(Key.A) ? -1 : 0);
        Player.Input(WSControl, ADControl);
        
        foreach (HoverCraft craft in hoverCrafts)
        {
            craft.Step();
        }
        
        camera.Step();
    }
}