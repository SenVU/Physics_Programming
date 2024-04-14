using GXPEngine;
using System;
using System.Runtime.Remoting.Messaging;

public class Vec2Collider : GameObject // a master Class, not for use in the collision manager
{
    bool solid;
    bool stationary;

    public Vec2 _position = new Vec2(0, 0);
    public Vec2 _velocity = new Vec2(0, 0);

    public float Mass;

    public Vec2Collider(float Mass, bool stationary = false, bool solid = true, bool addToManager = true) : base()
    {
        this.solid = solid;
        this.Mass = Mass;
        this.stationary = stationary;

        if (addToManager) Vec2ColliderManager.colliders.Add(this);
    }
    public bool IsSolid() { return solid; }
    public bool IsStationary() {  return stationary; }


    public virtual void Trigger(Vec2Collider otherCollider)
    {
        //override this method
    }
}

