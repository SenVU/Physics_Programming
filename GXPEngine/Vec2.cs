using GXPEngine;
using System;
using System.Drawing.Printing;

public struct Vec2
{
    public float x;
    public float y;

    public Vec2(float pX = 0, float pY = 0)
    {
        this.x = pX;
        this.y = pY;
    }

    public Vec2(float pF)
    {
        this.x = pF;
        this.y = pF;
    }

    // operators
    public static Vec2 operator *(Vec2 left, Vec2 right) => new Vec2(left.x * right.x, left.y * right.y);
    public static Vec2 operator *(Vec2 left, float right) => left * new Vec2(right,right);
    public static Vec2 operator *(float left, Vec2 right) => right*left;
    public static Vec2 operator /(Vec2 left, Vec2 right) => new Vec2(left.x / right.x, left.y / right.y);
    public static Vec2 operator /(Vec2 left, float right) => new Vec2(left.x / right, left.y / right);
    public static Vec2 operator -(Vec2 left, Vec2 right) => new Vec2(left.x - right.x, left.y - right.y);
    public static Vec2 operator +(Vec2 left, Vec2 right) => new Vec2(left.x + right.x, left.y + right.y);

    /// <summary>
    /// Returns a string version of the vector.
    /// </summary>
    /// <returns>Format "(x,y)".</returns>
    public override string ToString() { return String.Format("({0},{1})", x, y); }

    /// <summary>
    /// Returns the current length.
    ///  </summary>
    public float Length() { return Mathf.Sqrt(x * x + y * y); }

    /// <summary>
    /// Returns a normalized version of this vector whithout changing the original.
    /// </summary>
    public Vec2 Normalized()
    {
        Vec2 toReturn = this;
        toReturn.Normalize();
        return toReturn;
    }

    /// <summary>
    /// Checks if a point is neatby the curent vector.
    /// </summary>
    /// <param name="otherVec">The other point to check.</param>
    /// <param name="range">The radius to check.</param>
    /// <returns>Returns true if the distance between this and otherVec is less than or equal to range.</returns>
    public bool InRange(Vec2 otherVec, float range)
    {
        Vec2 Delta = otherVec - this;
        return Delta.Length() <= range;
    }

    /// <summary>
    /// sets new X and Y.
    /// </summary>
    public void SetXY(float x, float y) { this.x = x; this.y = y; }

    /// <summary>
    /// Sets the length to the new value.
    /// </summary>
    /// <param name="length">New length.</param>
    public void SetLength(float length) { this = this.Normalized() * length; }

    /// <summary>
    /// sets the length to 1.
    /// </summary>
    public void Normalize()
    {
        if (this.Length() == 0) return;
        this = this / this.Length(); 
    }

    /// <summary>
    /// Sets the length to a maximum value if it is over.
    /// (only gets run once)
    /// </summary>
    /// <param name="maxLength">The maximum</param>
    public void MaxLength(float maxLength)
    {
        if (maxLength < 0) { throw new ArgumentException("maxLengt should not be negative"); }
        this.SetLength(Mathf.Min(maxLength, this.Length()));
    }
}

