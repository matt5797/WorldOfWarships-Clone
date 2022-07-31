using UnityEngine;

public class Ellipse
{
    public float xAxis;
    public float yAxis;

    public Ellipse(float xAxis, float yAxis)
    {
        this.xAxis = xAxis;
        this.yAxis = yAxis;
    }

    public Vector2 Evaluate(float t)
    {
        float angle = Mathf.Deg2Rad * 360f * t;
        float x = xAxis * Mathf.Sin(angle);
        float y = yAxis * Mathf.Cos(angle);
        return new Vector2(x, y);
    }
}