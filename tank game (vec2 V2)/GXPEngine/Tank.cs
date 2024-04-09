using GXPEngine;

public class Tank : EasyDraw
{
    // settings

    float speed = 5;
    float rotationSpeed = 1;
    bool rotateInPlace = false;



    float acceleration;


    public Vec2 position
    {
        get
        {
            return _position;
        }
    }
    public Turret turret;

    Vec2 velocity;
    Vec2 _position;
    Vec2 angle = Vec2.GetUnitVectorDeg(0);

    

    public Tank(int pWidth, int pHeight, Vec2 pPosition) : base(pWidth, pHeight)
    {
        _position = pPosition;

        UpdateScreenPosition();
        SetOrigin(pWidth/2, pHeight / 2);
        Draw(0, 0, 255);

        turret = new Turret(pWidth - 20, pHeight - 20);
        AddChild(turret);
    }

    void Draw(byte red, byte green, byte blue)
    {
        Fill(red, green, blue);
        Stroke(red, green, blue);
        Rect(width/2, height/2, width, height);
    }

    void KeyControls()
    {
        velocity = new Vec2();

        if (Input.GetKey(Key.UP))
        {
            acceleration = Mathf.Min(acceleration+0.01f, 1);

        }
        else if (Input.GetKey(Key.DOWN))
        {
            acceleration = Mathf.Max(acceleration - 0.01f, -1);
        } else if (acceleration>0)
        {
            acceleration = Mathf.Max(acceleration - 0.01f, 0);
        }
        else if (acceleration < 0)
        {
            acceleration = Mathf.Min(acceleration + 0.01f, 0);
        }

        velocity = new Vec2(0, -speed*acceleration);
        velocity.RotateDegrees(angle.GetAngleDegrees());

        if (velocity != new Vec2() || rotateInPlace)
        {
            float rotationTarget = rotationSpeed * acceleration;
            if (Input.GetKey(Key.RIGHT))
            {
                angle.RotateDegrees(rotationTarget);
            }
            else if (Input.GetKey(Key.LEFT))
            {
                angle.RotateDegrees(-rotationTarget);
            }
        }

    }

    void UpdateScreenPosition()
    {
        x = position.x;
        y = position.y;
    }

    public void Step()
    {
        KeyControls();
        velocity.MaxLength(speed*Mathf.Abs(acceleration));
        _position += velocity;

        UpdateScreenPosition();

        rotation = angle.GetAngleDegrees();
        //turret.SetPosition(_position);
        turret.Step();
    }
}
