using GXPEngine;

public class CheckPoint : Vec2LineCollider
{
    EasyDraw easyDraw;
    int R;
    int G;
    int B;
    int A;
    float strokeWeight;

    public CheckPoint(Vec2 startPos, Vec2 endPos, int R = 0, int G = 255, int B =0, int A = 255, float strokeWeight = 1) : base(startPos, endPos, false)
    {
        
        this.R = R;
        this.G = G;
        this.B = B;
        this.A = A;
        this.strokeWeight = strokeWeight;

        UpdateScreenPos();
    }

    public void UpdateScreenPos()
    {
        float xSmall = Mathf.Min(startPos.x, endPos.x);
        float ySmall = Mathf.Min(startPos.y, endPos.y);
        float xLarge = Mathf.Max(startPos.x, endPos.x);
        float yLarge = Mathf.Max(startPos.y, endPos.y);

        easyDraw = new EasyDraw(Mathf.Ceiling(xLarge - xSmall) + 10, Mathf.Ceiling(yLarge - ySmall) + 10, false);
        easyDraw.SetXY(xSmall - 5, ySmall - 5);
        easyDraw.Stroke(R, G, B, A);
        easyDraw.StrokeWeight(strokeWeight);
        easyDraw.Line((startPos.x - xSmall) + 5, (startPos.y - ySmall) + 5, (endPos.x - xSmall) + 5, (endPos.y - ySmall) + 5);

        //RemoveChild(easyDraw);
        AddChild(easyDraw);
    }

    public override void Trigger(Vec2Collider otherColider)
    {
        if (otherColider.parent is HoverCraft craft)
        {
            craft.lastCheckPoint = this;
        }
    }
}

