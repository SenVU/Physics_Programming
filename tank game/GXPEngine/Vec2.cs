using GXPEngine;
using System;

public struct Vec2
{
    public float x;
    public float y;

    public Vec2(float pX = 0, float pY = 0)
    {
        this.x = pX;
        this.y = pY;
    }

    public Vec2(float pXY)
    {
        this.x = pXY;
        this.y = pXY;
    }

    // --------operators
    public static Vec2 operator *(Vec2 left, Vec2 right) => new Vec2(left.x * right.x, left.y * right.y);
    public static Vec2 operator *(Vec2 left, float right) => left * new Vec2(right);
    public static Vec2 operator *(float left, Vec2 right) => right*left;
    public static Vec2 operator /(Vec2 left, Vec2 right) => new Vec2(left.x / right.x, left.y / right.y);
    public static Vec2 operator /(Vec2 left, float right) => new Vec2(left.x / right, left.y / right);
    public static Vec2 operator -(Vec2 left, Vec2 right) => new Vec2(left.x - right.x, left.y - right.y);
    public static Vec2 operator +(Vec2 left, Vec2 right) => new Vec2(left.x + right.x, left.y + right.y);
    public static bool operator ==(Vec2 left, Vec2 right) => left.x==right.x && left.y==right.y;
    public static bool operator !=(Vec2 left, Vec2 right) => !(left==right);


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
    /// Liniar interpolation.
    /// </summary>
    /// <param name="otherVec">The other vector to lerp to.</param>
    /// <param name="factor">How much of the original vector gets replaced.</param>
    /// <returns>Interpolated vector.</returns>
    public Vec2 Lerped(Vec2 otherVec, float factor)
    {
        Vec2 toReturn = this;
        toReturn.Lerp(otherVec, factor);
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
    /// Gets the direction of a vector
    /// </summary>
    /// <returns>Angle in radians</returns>
    public float GetAngleRadians() { return Mathf.Atan2(y,x); }

    /// <summary>
    /// Gets the direction of a vector
    /// </summary>
    /// <returns>Angle in degrees</returns>
    public float GetAngleDegrees() { return Rad2Deg(GetAngleRadians()); }

    /// <summary>
    /// sets new X and Y.
    /// </summary>
    public void SetXY(float x, float y) { this.x = x; this.y = y; }

    /// <summary>
    /// Sets the length to the new value.
    /// </summary>
    /// <param name="length">New length.</param>
    public void SetLength(float length) {
        //if (this.Length() == 0) throw new ArgumentException("Cant set length on a Vec2 with a length of 0");
        this = this.Normalized() * length; 
    } 

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

    /// <summary>
    /// Liniar interpolation on the current vector.
    /// </summary>
    /// <param name="targetVec">The other vector to lerp to.</param>
    /// <param name="factor">How much of the original vector gets replaced.</param>
    public void Lerp(Vec2 targetVec, float factor)
    {
        this = this + (targetVec - this) * factor;
    }

    /// <summary>
    /// Rotates the vectors direction clockwise
    /// </summary>
    /// /// <param name="rad">The amount to rotate in radians.</param>
    public void RotateRadians(float rad)
    {
        this = new Vec2(Mathf.Cos(rad) * x - Mathf.Sin(rad) * y, Mathf.Cos(rad) * y + Mathf.Sin(rad) * x);

    }

    /// <summary>
    /// Rotates the vectors direction clockwise
    /// </summary>
    /// /// <param name="deg">The amount to rotate in degrees.</param>
    public void RotateDegrees(float deg)
    {
        RotateRadians(Deg2Rad(deg));
    }

    /// <summary>
    /// Rotates the vectors direction clockwise around another vector
    /// </summary>
    /// /// <param name="rad">The amount to rotate in radians.</param>
    public void RotateAroundRadians(Vec2 otherVec, float rad) 
    {
        this -= otherVec;
        this.RotateRadians(rad);
        this += otherVec;
    }

    /// <summary>
    /// Rotates the vectors direction clockwise around another vector
    /// </summary>
    /// /// <param name="deg">The amount to rotate in degrees.</param>
    public void RotateAroundDegrees(Vec2 otherVec, float deg) 
    {
        this -= otherVec;
        this.RotateDegrees(deg);
        this += otherVec;
    }

    /// <summary>
    /// Set vector angle to the given direction in degrees.
    /// </summary>
    public void SetAngleDegrees(float deg)
    {
        SetAngleRadians(Deg2Rad(deg));
    }

    /// <summary>
    /// Set vector angle to the given direction in radians.
    /// </summary>
    public void SetAngleRadians(float rad)
    {
        this = new Vec2(this.Length(),0);
        this.RotateRadians(rad);
    }
 

    // ------------Statics

    /// <summary>
    /// converts the given degrees to radians
    /// </summary>
    private static float Deg2Rad(float deg) { return deg * (Mathf.PI / 180); }

    /// <summary>
    /// converts the given radians to degrees
    /// </summary>
    private static float Rad2Deg(float rad) { return rad * (180 / Mathf.PI); }

    /// <summary>
    /// Returns a new unit vector pointing in a random direction
    /// </summary>
    public static Vec2 RandomUnitVector() {
        Random random = new Random();
        return GetUnitVectorDeg(random.Next(360)); 
    }

    /// <summary>
    /// returns a new vector pointing in the given direction in degrees.
    /// </summary>
    public static Vec2 GetUnitVectorDeg(float deg)
    {
        return GetUnitVectorRad(Deg2Rad(deg));
    }

    /// <summary>
    /// returns a new vector pointing in the given direction in radians.
    /// </summary>
    public static Vec2 GetUnitVectorRad(float rad)
    {
        Vec2 toReturn = new Vec2(1,0);
        toReturn.SetAngleRadians(rad); 
        toReturn.SetLength(1);
        return toReturn;
    }






    public static void UnitTest()
    {
        //Console.WriteLine("180 deg in rad:" + Vec2.Deg2Rad(180)); method is now private
        //Console.WriteLine("1 rad in deg:" + Vec2.Rad2Deg(1)); method is now private

        Vec2 testVec = Vec2.GetUnitVectorDeg(45);
        Vec2 testVec2 = Vec2.GetUnitVectorRad(2);
        Vec2 testVec3 = Vec2.RandomUnitVector();

        Console.WriteLine("The unit vector for deg angle 45 :" + testVec + "-length:" + testVec.Length() + "-deg:" + testVec.GetAngleDegrees());
        Console.WriteLine("The unit vector for rad angle 2 :" + testVec2 + "-length:" + testVec2.Length() + "-rad:" + testVec2.GetAngleRadians());
        Console.WriteLine("A random unit vector:" + testVec3 + "-length:" + testVec3.Length() + "-deg:" + testVec3.GetAngleDegrees());

        testVec = new Vec2(5, 5);
        testVec2 = testVec;
        testVec2.RotateDegrees(45);
        testVec3 = testVec;
        testVec3.RotateRadians(1);

        Console.WriteLine("the vector " + testVec + "-deg:" + testVec.GetAngleDegrees() + " rotated by 45 deg :" + testVec2 + "-deg:" + testVec2.GetAngleDegrees());
        Console.WriteLine("the vector " + testVec + "-rad:" + testVec.GetAngleRadians() + " rotated by 1 rad :" + testVec3 + "-deg:" + testVec3.GetAngleRadians());

        testVec2 = testVec;
        testVec2.SetAngleDegrees(70);
        testVec3 = testVec;
        testVec3.SetAngleRadians(2);

        Console.WriteLine("the vector " + testVec + " set to 70 deg :" + testVec2 + "-deg:" + testVec2.GetAngleDegrees());
        Console.WriteLine("the vector " + testVec + " set to 2 rad :" + testVec3 + "-rad:" + testVec3.GetAngleRadians());

        Vec2 rotatePoint = new Vec2(2, 6);
        testVec2 = testVec;
        testVec2.RotateAroundDegrees(rotatePoint, 90);
        testVec3 = testVec;
        testVec3.RotateAroundRadians(rotatePoint, 2);

        Console.WriteLine("the vector " + testVec + " rotated around :" + rotatePoint + " by 90 deg" + testVec2);
        Console.WriteLine("the vector " + testVec + " rotated around :" + rotatePoint + " by 2 rad" + testVec3);

    }
}

