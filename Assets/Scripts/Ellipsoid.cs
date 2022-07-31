using UnityEngine;

public class Ellipsoid
{
    public float xAxis;
    public float yAxis;
    public float zAxis;
    public float xOffset;
    public float yOffset;
    public float zOffset;

    public Ellipsoid(float xAxis, float yAxis, float zAxis, float xOffset = 0, float yOffset = 0, float zOffset = 0)
    {
        this.xAxis = xAxis;
        this.yAxis = yAxis;
        this.zAxis = zAxis;
        this.xOffset = xOffset;
        this.yOffset = yOffset;
        this.zOffset = zOffset;
    }

    public Vector3 Evaluate(float t, float u)
    {
        float angle = Mathf.Deg2Rad * 360f * t;
        float x = xAxis * (1 - (u / yAxis)) * Mathf.Sin(angle) + xOffset;
        float y = u + yOffset;
        float z = zAxis * (1 - (u / yAxis)) * Mathf.Cos(angle) + zOffset;
        return new Vector3(x, y, z);
    }
}