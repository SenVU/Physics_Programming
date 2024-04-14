using System.Collections.Generic;

public static class Vec2ColliderManager
{
    // TODO create a collision order

    public static List<Vec2Collider> colliders = new List<Vec2Collider>();
    // settings

    static float bounciness = 0.4f;

    public static void HandleCollisions()
    {
        foreach (Vec2Collider c in colliders)
        {
            if (c.IsStationary() || !c.IsSolid()) { continue; }
            foreach (Vec2Collider c2 in colliders)
            {
                if (c2 == c) { continue; }
                if (c is Vec2BallCollider)
                {
                    // ball on :
                    if (c2 is Vec2BallCollider) { if (c2 == c) { continue; } HandleCollisionBallBall((Vec2BallCollider)c, (Vec2BallCollider)c2); } // ball
                    if (c2 is Vec2LineCollider) { HandleCollisionBallLine((Vec2BallCollider)c, (Vec2LineCollider)c2); } // line
                }
            }
        }
    }

    static bool HandleCollisionBallBall(Vec2BallCollider c, Vec2BallCollider c2)
    {
        bool hasCollided = false;

        Vec2 oldPos = c._position - c._velocity;
        float distance = c._position.distance(c2._position);
        float oldDistance = oldPos.distance(c2._position);
        Vec2 collisionNormal = (c2._position - c._position).Normalized();
        if (distance - (c.radius + c2.radius) < 0.000001f && oldDistance - (c.radius + c2.radius) < 0.000001f)
        {
            //TODO this code is not accurate
            //was already close to other ball
            if (c.IsSolid() && c2.IsSolid())
            {
                float overlap = c.radius + c2.radius - distance;
                c._position += collisionNormal * -overlap;
            }
            c.Trigger(c2);
            c2.Trigger(c);
            hasCollided = true;
        }
        else if (Vec2PhysicsCalculations.TimeOfImpactBall(c._position, oldPos, c2._position, c.radius, c2.radius) < 1)
        {
            //collision detected with other ball

            if (c.IsSolid() && c2.IsSolid())
            {
                Vec2 POI = Vec2PhysicsCalculations.PointOfImpactBall(c._position, oldPos, c2._position, c.radius, c2.radius);
                c._position = POI;
                Vec2 COMVelocity = c2.IsStationary() ? new Vec2(0, 0) : (c.Mass * c._velocity + c2.Mass * c2._velocity) / (c.Mass + c2.Mass);

                c._velocity = c._velocity - (1 + bounciness) * ((c._velocity - COMVelocity).Dot(collisionNormal)) * collisionNormal;
                if (!c2.IsStationary()) c2._velocity = c2._velocity - (1 + bounciness) * ((c2._velocity - COMVelocity).Dot(collisionNormal)) * collisionNormal;
            }
            c.Trigger(c2);
            c2.Trigger(c);
            hasCollided = true;
        }

        return hasCollided;
    }

    static bool HandleCollisionBallLine(Vec2BallCollider cB, Vec2LineCollider cL)
    {
        bool hasCollided = false;

        Vec2 oldPos = cB._position - cB._velocity; // the old position is technicaly not correct

        float distance = Vec2PhysicsCalculations.LineDistance(cB._position, cL.startPos, cL.endPos);
        float oldDistance = Vec2PhysicsCalculations.LineDistance(oldPos, cL.startPos, cL.endPos);

        Vec2 differanceVecStart = cB._position - cL.startPos;
        Vec2 differanceVecEnd = cB._position - cL.endPos;

        Vec2 lineVec = cL.endPos - cL.startPos;
        Vec2 lineNormal = lineVec.Normal();

        float scalarVecStart = differanceVecStart.Dot(lineVec.Normalized());
        float scalarVecEnd = differanceVecEnd.Dot(lineVec.Normalized() * -1);
        
        if ((distance <= cB.radius && distance >= -cB.radius) && 
            (oldDistance <= cB.radius && oldDistance >= -cB.radius) && 
            (scalarVecStart > 0 && scalarVecEnd > 0))
        { // check to see if it was already colliding
            //TODO this code does not work like it should
            if (cB.IsSolid() && cL.IsSolid())
            {
                cB._position += lineNormal * (cB.radius - distance);
            }
            cB.Trigger(cL);
            cL.Trigger(cB);
            hasCollided = true;
        }
        else if ((distance < cB.radius && distance > -cB.radius) && 
                (scalarVecStart > 0 && scalarVecEnd > 0))
        { // check for collision
            if (cB.IsSolid() && cL.IsSolid())
            {
                // calculate the point of impact
                Vec2 POI;
                if (distance >= 0) POI = Vec2PhysicsCalculations.PointOfImpactLine(cB._position, oldPos, cL.startPos, cL.endPos, cB.radius);
                else POI = Vec2PhysicsCalculations.PointOfImpactLine(cB._position, cB._position - cB._velocity, cL.endPos, cL.startPos, cB.radius);
                // return the ball to the point of impact
                cB._position = POI;
                // reflect the velocity on the line normal
                cB._velocity.Reflect(lineNormal, bounciness);
            }
            cB.Trigger(cL);
            cL.Trigger(cB);
            hasCollided = true;
        }
        else
        { // if the previous collision failes try colliding with the caps at either end
            if (HandleCollisionBallBall(cB, cL.beginCap) ||
                HandleCollisionBallBall(cB, cL.endCap))
            {
                cB.Trigger(cL);
                cL.Trigger(cB);
                hasCollided = true;
            }
        }

        return hasCollided;
    }
}

