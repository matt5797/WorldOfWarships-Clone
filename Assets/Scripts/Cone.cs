using UnityEngine;


public class Cone
{
    public float xAxis;
    public float yAxis;
    public float zAxis;
    public float yOffset;
    public float direction = -1;

    public Cone(float xAxis, float yAxis, float zAxis, float yOffset)
    {
        this.xAxis = xAxis;
        this.yAxis = yAxis;
        this.zAxis = zAxis;
        this.yOffset = yOffset;
    }

    public Vector3 Evaluate(float t, float u)
    {
        float angle = Mathf.Deg2Rad * 360f * t;
        float x = xAxis * ((u / yAxis) + yOffset) * Mathf.Sin(angle) * direction;
        float y = u;
        float z = zAxis * ((u / yAxis) + yOffset) * Mathf.Cos(angle) * direction;
        return new Vector3(x, y, z);
    }
}