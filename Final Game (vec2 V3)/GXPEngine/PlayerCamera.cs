using GXPEngine;

public class PlayerCamera : Camera
{
    HoverCraft followCraft;

    public Vec2 _position;
    public Vec2 vecRotation;
    public PlayerCamera(int windowX, int windowY, int windowWidth, int windowHeight, HoverCraft pFollowCraft) : base (windowX, windowY, windowWidth, windowHeight, true)
    {
        followCraft = pFollowCraft;
    }

    void UpdateScreenPosition()
    {
        x = _position.x; y = _position.y;
        rotation = vecRotation.GetAngleDegrees();
    }

    public void Step()
    {
        _position.Lerp(followCraft._position+(followCraft._velocity*40), 0.05f);
        vecRotation.Lerp(followCraft.vecRotation, 0.03f);

        UpdateScreenPosition();
    }
}
