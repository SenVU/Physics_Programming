using GXPEngine;
using System.Collections.Generic;

public class HoverCraft : EasyDraw
{
    public Vec2BallCollider _collider;
    public Vec2 vecRotation = Vec2.GetUnitVectorRad(0);

    public CheckPoint lastCheckPoint;

    float maxSpeed = 18;
    float StationaryRotationSpeed = 0.7f;
    float rotationSpeed = 2f;

    float inputGas;
    float inputSteering;

    public int radius;

    public HoverCraft(int pRadius, bool addCollider = true) : base(pRadius*2, pRadius*2, addCollider)
    {
        radius = pRadius;
        SetOrigin(radius, radius);

        _collider = new Vec2BallCollider(radius);

        AddChild(_collider);
    }

    void Draw()
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

    public void Input(List<CheckPoint> checkPoints)
    {
        // AI code
        int nextIndex = checkPoints.IndexOf(lastCheckPoint) + 1;
        if (nextIndex >= checkPoints.Count) { nextIndex = 0; }
        CheckPoint nextCheckpoint = checkPoints[nextIndex];

        Vec2 VecTargetAngle = (nextCheckpoint._position - this._collider._position).Normalized();

        VecTargetAngle.RotateDegrees(90);

        float targetAngle = VecTargetAngle.GetAngleDegrees();
        float angle = vecRotation.GetAngleDegrees();

        float angleDifference = targetAngle - angle;

        if (angleDifference > 180)
        {
            angleDifference -= 360;
        }
        else if (angleDifference < -180)
        {
            angleDifference += 360;
        }

        inputSteering = angleDifference / 5;

        inputSteering = Mathf.Clamp(inputSteering, -1, 1);

        inputGas = 1;
    }

    void VelocityControl()
    {
        Vec2 targetVelocity = vecRotation;
        targetVelocity.RotateDegrees(-90);
        targetVelocity.SetLength(inputGas * maxSpeed);

        _collider._velocity.Lerp(targetVelocity, 0.01f);
    }

    public void UpdateScreenPosition() 
    { 
        x=_collider._position.x; y= _collider._position.y;
        rotation= vecRotation.GetAngleDegrees();
        Draw();
    }

    public void Step()
    {
        // rotation speed is calculated for both stationary and moving at the same time
        vecRotation.RotateDegrees((inputSteering * StationaryRotationSpeed /* * (inputGas<0?-1:1))*/ + (inputSteering * (rotationSpeed-StationaryRotationSpeed) * (_collider._velocity.Length()/maxSpeed) /* * (inputGas<0?-1:1)*/))); 
        VelocityControl();
        _collider._position += _collider._velocity;
    }
}

