using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Turret : EasyDraw
{
    Vec2 _position = new Vec2();
    Vec2 angle = Vec2.GetUnitVectorDeg(0);

    public Turret(int pWidth, int pHeight) : base(pWidth, pHeight) {
        Draw(255, 0, 0);
        SetOrigin(pWidth/2, pHeight/2);
    }

    void Draw(byte red, byte green, byte blue)
    {
        Fill(red, green, blue);
        Stroke(red, green, blue);
        Triangle(0,height,width,height,width/2,0);
    }

    public void Step()
    {
        Vec2 delta = new Vec2(parent.x , parent.y) - new Vec2(Input.mouseX, Input.mouseY);
        rotation = delta.GetAngleDegrees()-90-parent.rotation;
        SetXY(_position.x, _position.y);
    }
}
