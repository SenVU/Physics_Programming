public static class Physics_engine
{
    /// <summary>
    /// Calculates the the time of impact with a line
    /// </summary>
    /// <param name="pNewPos">The position at the end of the frame</param>
    /// <param name="pOldPos">The position at the start of the frame</param>
    /// <param name="pLineSegmentStart">The start of the line colliding with</param>
    /// <param name="pLineSegmentEnd">The end of the line colliding with</param>
    /// <param name="radius">For a circle hitbox</param>
    /// <returns>0 = begining of frame, 1 = end of frame</returns>
    public static float TimeOfImpactLine(Vec2 pNewPos, Vec2 pOldPos, Vec2 pLineSegmentStart, Vec2 pLineSegmentEnd, float radius = 0)
    {
        Vec2 lineVec = pLineSegmentEnd - pLineSegmentStart;
        Vec2 LineNormal = lineVec.Normal();
        Vec2 oldDifferenceVec = pOldPos - pLineSegmentStart;
        Vec2 newDifferenceVec = pNewPos - pLineSegmentStart;

        float oldDist = oldDifferenceVec.Dot(LineNormal) - radius;
        float newDist = newDifferenceVec.Dot(LineNormal);

        return oldDist / newDist;
    }

    /// <summary>
    /// Calculates the the point of impact with a line
    /// </summary>
    /// <param name="pNewPos">The position at the end of the frame</param>
    /// <param name="pOldPos">The position at the start of the frame</param>
    /// <param name="pLineSegmentStart">The start of the line colliding with</param>
    /// <param name="pLineSegmentEnd">The end of the line colliding with</param>
    /// <param name="radius">For a circle hitbox</param>
    /// <returns>Vector position of the point of impact</returns>
    public static Vec2 PointOfImpactLine(Vec2 pNewPos, Vec2 pOldPos, Vec2 pLineSegmentStart, Vec2 pLineSegmentEnd, float radius = 0)
    {
        return pOldPos + TimeOfImpactLine(pNewPos, pOldPos, pLineSegmentStart, pLineSegmentEnd, radius) * (pNewPos - pOldPos);
    }
}

