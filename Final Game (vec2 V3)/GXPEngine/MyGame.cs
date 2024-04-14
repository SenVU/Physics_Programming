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

    HoverCraft player;
    HoverCraft playerTwo;
    PlayerCamera camera;
    PlayerCamera cameraTwo;
    Sprite raceTrack;

    public List<HoverCraft> hoverCrafts = new List<HoverCraft>();
    public List<WallSegment> walls = new List<WallSegment>();
    public List<CheckPoint> checkPoints = new List<CheckPoint>();

    // settings
    int AiCraft = 0;
    bool twoPlayerMode = true;
    bool renderBackground = false;

    public MyGame() : base(1920, 1080, false)
    {
        Vec2.UnitTest();

        raceTrack = new Sprite("data/Textures/RaceTrack.jpg", false, false);
        if (renderBackground) AddChild(raceTrack);

        // create the player and camera
        player = new HoverCraft(30);
        player._collider._position = new Vec2(3400, 2650); // player starting position
        player.vecRotation.RotateDegrees(90);
        camera = new PlayerCamera(0,0, twoPlayerMode?width/2:width, height, player);
        
        AddChild(camera);
        hoverCrafts.Add(player);

        // create the second player and camera
        if (twoPlayerMode)
        {
            playerTwo = new HoverCraft(30);
            playerTwo._collider._position = new Vec2(3400, 2800); // player two starting position
            playerTwo.vecRotation.RotateDegrees(90);
            cameraTwo = new PlayerCamera(width / 2, 0, width/2, height, playerTwo);

            AddChild(cameraTwo);
            hoverCrafts.Add(playerTwo);
        }

        // create AI hoverCrafts
        for (int i = 0; i < AiCraft; i++)
        {
            HoverCraft craft = new HoverCraft(30);
            craft._collider._position = new Vec2(3400, 2650 + (i+1) * 65);
            craft.vecRotation.RotateDegrees(90);
            hoverCrafts.Add(craft);
        }

        // add all hoverCrafts to the engine
        foreach (HoverCraft craft in hoverCrafts)
        {
            AddChild(craft);
        }

        // create all checkPoints
        // For the future an editor might be nice
        checkPoints.Add(new CheckPoint(new Vec2(3400, 2615), new Vec2(3400, 2875)));
        checkPoints.Add(new CheckPoint(new Vec2(3515, 2615), new Vec2(3515, 2875), 255, 0, 0, 255, 5));
        checkPoints.Add(new CheckPoint(new Vec2(4410, 2615), new Vec2(4410, 2875)));
        checkPoints.Add(new CheckPoint(new Vec2(4710, 2465), new Vec2(4930, 2620)));
        checkPoints.Add(new CheckPoint(new Vec2(4860, 2210), new Vec2(5115, 2210)));
        checkPoints.Add(new CheckPoint(new Vec2(4745, 1880), new Vec2(4940, 1690)));
        checkPoints.Add(new CheckPoint(new Vec2(4620, 1825), new Vec2(4620, 1565)));
        checkPoints.Add(new CheckPoint(new Vec2(4500, 1880), new Vec2(4340, 1665)));
        checkPoints.Add(new CheckPoint(new Vec2(4455, 1940), new Vec2(4190, 1940)));
        checkPoints.Add(new CheckPoint(new Vec2(4455, 2125), new Vec2(4190, 2125)));
        checkPoints.Add(new CheckPoint(new Vec2(4390, 2320), new Vec2(4150, 2160)));
        checkPoints.Add(new CheckPoint(new Vec2(4100, 2450), new Vec2(4100, 2175)));
        checkPoints.Add(new CheckPoint(new Vec2(3330, 2460), new Vec2(3330, 2185)));
        checkPoints.Add(new CheckPoint(new Vec2(2910, 2215), new Vec2(3120, 2030)));
        checkPoints.Add(new CheckPoint(new Vec2(2750, 1755), new Vec2(3025, 1755)));
        checkPoints.Add(new CheckPoint(new Vec2(2930, 1420), new Vec2(3100, 1650)));
        checkPoints.Add(new CheckPoint(new Vec2(3230, 1340), new Vec2(3230, 1600)));
        checkPoints.Add(new CheckPoint(new Vec2(4310, 1340), new Vec2(4310, 1600)));
        checkPoints.Add(new CheckPoint(new Vec2(4510, 1300), new Vec2(4615, 1535)));
        checkPoints.Add(new CheckPoint(new Vec2(4690, 1215), new Vec2(4880, 1380)));
        checkPoints.Add(new CheckPoint(new Vec2(4740, 1140), new Vec2(5000, 1200)));
        checkPoints.Add(new CheckPoint(new Vec2(4760, 950), new Vec2(4990, 835)));
        checkPoints.Add(new CheckPoint(new Vec2(4670, 830), new Vec2(4850, 635)));
        checkPoints.Add(new CheckPoint(new Vec2(4560, 785), new Vec2(4560, 530)));
        checkPoints.Add(new CheckPoint(new Vec2(3070, 785), new Vec2(3070, 530)));
        checkPoints.Add(new CheckPoint(new Vec2(2840, 820), new Vec2(2770, 575)));
        checkPoints.Add(new CheckPoint(new Vec2(2530, 945), new Vec2(2400, 725)));
        checkPoints.Add(new CheckPoint(new Vec2(2370, 1075), new Vec2(2145, 940)));
        checkPoints.Add(new CheckPoint(new Vec2(2290, 1200), new Vec2(2040, 1120)));
        checkPoints.Add(new CheckPoint(new Vec2(2235, 1425), new Vec2(1980, 1420)));
        checkPoints.Add(new CheckPoint(new Vec2(2215, 1680), new Vec2(1950, 1635)));
        checkPoints.Add(new CheckPoint(new Vec2(2030, 1970), new Vec2(1865, 1750)));
        checkPoints.Add(new CheckPoint(new Vec2(1570, 2010), new Vec2(1690, 1760)));
        checkPoints.Add(new CheckPoint(new Vec2(1410, 1880), new Vec2(1600, 1680)));
        checkPoints.Add(new CheckPoint(new Vec2(980, 1310), new Vec2(1160, 1120)));
        checkPoints.Add(new CheckPoint(new Vec2(940, 1290), new Vec2(970, 1020)));
        checkPoints.Add(new CheckPoint(new Vec2(815, 1320), new Vec2(690, 1080)));
        checkPoints.Add(new CheckPoint(new Vec2(740, 1400), new Vec2(520, 1260)));
        checkPoints.Add(new CheckPoint(new Vec2(675, 1600), new Vec2(415, 1600)));
        checkPoints.Add(new CheckPoint(new Vec2(675, 2045), new Vec2(415, 2045)));


        checkPoints.Add(new CheckPoint(new Vec2(785, 2295), new Vec2(600, 2490)));
        checkPoints.Add(new CheckPoint(new Vec2(1010, 2480), new Vec2(900, 2730)));
        checkPoints.Add(new CheckPoint(new Vec2(1275, 2570), new Vec2(1255, 2840)));
        checkPoints.Add(new CheckPoint(new Vec2(1535, 2615), new Vec2(1535, 2870)));

        // add all checkPoints to the engine
        foreach (CheckPoint point in checkPoints)
        {
            AddChild(point);
        }

        //using the checkPoints generate walls
        for (int i = 1; i < checkPoints.Count; i++) 
        { 
            CheckPoint point = checkPoints[i];
            CheckPoint previousPoint = checkPoints[i - 1];

            walls.Add(new WallSegment(point.startPos, previousPoint.startPos, 255, 255, 255, 255, 3));
            walls.Add(new WallSegment(point.endPos, previousPoint.endPos, 255, 255, 255, 255, 3));
        }

        // one more set of walls from the last to the first checkPoint
        CheckPoint startPoint = checkPoints[0];
        CheckPoint endPoint = checkPoints[checkPoints.Count - 1];
        walls.Add(new WallSegment(endPoint.startPos, startPoint.startPos, 255, 255, 255, 255, 3));
        walls.Add(new WallSegment(endPoint.endPos, startPoint.endPos, 255, 255, 255, 255, 3));

        // add all walls to the engine
        foreach (WallSegment wall in walls)
        {
            AddChild(wall);
        }
    }

    void Update()
    {
        // player controled craft inputs
        float WSControl = (Input.GetKey(Key.W) ? 1 : 0) + (Input.GetKey(Key.S) ? -1 : 0);
        float ADControl = (Input.GetKey(Key.D) ? 1 : 0) + (Input.GetKey(Key.A) ? -1 : 0);

        float UDControl = (Input.GetKey(Key.UP) ? 1 : 0) + (Input.GetKey(Key.DOWN) ? -1 : 0);
        float LRControl = (Input.GetKey(Key.RIGHT) ? 1 : 0) + (Input.GetKey(Key.LEFT) ? -1 : 0);

        player.Input(WSControl, ADControl);
        if (twoPlayerMode) { playerTwo.Input(UDControl, LRControl); }
        
        foreach (HoverCraft craft in hoverCrafts)
        {
            craft.Step(); // moves all craft
            if (craft != player && craft != playerTwo)
            {
                craft.Input(checkPoints); // if not a player, use AI movament
            }
        }

        // requests the collision manager to resolve all collisions
        Vec2ColliderManager.HandleCollisions();

        foreach (HoverCraft craft in hoverCrafts)
        {
            craft.UpdateScreenPosition();
        }

        // move the camera
        camera.Step(true);
        if (twoPlayerMode) cameraTwo.Step(true);
    }
}