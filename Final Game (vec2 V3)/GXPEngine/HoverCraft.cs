using GXPEngine;

public class HoverCraft : EasyDraw
{
    public Vec2 _position;
    public Vec2 _velocity;
    public Vec2 vecRotation = Vec2.GetUnitVectorRad(0);

    Vec2 oldPos; // the position at the start of the step used for collision handeling

    float maxSpeed = 5;
    float StationaryRotationSpeed = 0.2f;
    float rotationSpeed = 0.8f;

    float inputGas;
    float inputSteering;

    public int radius;

    public HoverCraft(int pRadius, bool addCollider = true) : base(pRadius*2, pRadius*2, addCollider)
    {
        radius = pRadius;
        SetOrigin(radius, radius);
    }

    void draw()
    {
        Fill(255, 255, 255);
        StrokeWeight(1);
        Stroke(255, 0, 0);
        Ellipse(radius, radius , width, height);
        Line(radius, radius, radius, 0);
    }

    public void Input(float gas, float steering)
    {
        inputGas = gas;
        inputSteering = steering;
    }

    void VelocityControl()
    {
        Vec2 targetVelocity = vecRotation;
        targetVelocity.RotateDegrees(-90);
        targetVelocity.SetLength(inputGas * maxSpeed);

        _velocity.Lerp(targetVelocity, 0.01f);
    }

    void UpdateScreenPosition() 
    { 
        x=_position.x; y=_position.y;
        rotation= vecRotation.GetAngleDegrees();
    }

    void checkCollisions()
    {

    }

    public void Step()
    {
        // rotation speed is calculated for both stationary and moving at the same time
        vecRotation.RotateDegrees((inputSteering * StationaryRotationSpeed * (inputGas<0?-1:1)) + (inputSteering * (rotationSpeed-StationaryRotationSpeed) * inputGas)); 
        VelocityControl();
        oldPos = _position;
        _position += _velocity;

        checkCollisions();

        UpdateScreenPosition();
        draw();
    }
}

