using UnityEngine;
public struct Line
{
    const float GradientLineVertical = 1e5f;
    float Gradient;
    float InterceptY;
    float GradientPerpendicular;
    Vector2 PointLineA;
    Vector2 PointLineB;
    bool ApproachSide;
    public Line(Vector2 pointLine, Vector2 pointLinePerpendicular)
    {
        float dx = pointLine.x - pointLinePerpendicular.x;
        float dy = pointLine.y - pointLinePerpendicular.y;
        if (dx == 0)
            GradientPerpendicular = GradientLineVertical;
        else
            GradientPerpendicular = dy / dx;
        if (GradientPerpendicular == 0)
            Gradient = GradientLineVertical;
        else
            Gradient = -1 / GradientPerpendicular;
        InterceptY = pointLine.y - Gradient * pointLine.x;
        PointLineA = pointLine;
        PointLineB = pointLine + new Vector2(1, Gradient);
        ApproachSide = false;
        ApproachSide = GetSide(pointLinePerpendicular);
    }
    bool GetSide(Vector2 p)
    {
        return (p.x - PointLineA.x) * (PointLineB.y - PointLineA.y) > (p.y - PointLineA.y) * (PointLineB.x - PointLineA.x);
    }
    public bool HasCrossed(Vector2 p)
    {
        return GetSide(p) != ApproachSide;
    }
    public float DistanceFromPoint(Vector2 p)
    {
        float interceptYPerpendicular = p.y - GradientPerpendicular * p.x;
        float intersectX = (interceptYPerpendicular - InterceptY) / (Gradient - GradientPerpendicular);
        float intersectY = Gradient * intersectX - InterceptY;
        return Vector2.Distance(p, new Vector2(intersectX, intersectY));
    }
    public void DrawWithGizmos(float length)
    {
        Vector3 lineDirection = new Vector3(1, Gradient, 0).normalized;
        Vector3 lineCenter = new Vector3(PointLineA.x, PointLineA.y, 0) + Vector3.back;
        Gizmos.DrawLine(lineCenter - lineDirection * length / 2f, lineCenter + lineDirection * length / 2f);
    }
}
