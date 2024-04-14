
using GXPEngine;

public class Vec2BallCollider : Vec2Collider
{
    public int radius;
    public Vec2BallCollider(int radius, float density=1, bool stationary = false, bool solid = true, bool addToManager=true) : base(Mathf.PI*Mathf.Pow(radius,2)*density, stationary, solid, addToManager) 
    {
        this.radius = radius;
    }
}
