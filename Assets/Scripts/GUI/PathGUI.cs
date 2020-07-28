using System;
using UnityEngine;

public class PathGUI : GUIDestroyable
{
    private readonly GameObject lineObj;
    public PathGUI(Vector2 point1, Vector2 point2) : base()
	{
        lineObj = new GameObject("Line");
        LineRenderer line = lineObj.AddComponent<LineRenderer>();
        line.startWidth = .4f;
        line.endWidth = .4f;
        line.SetPosition(0, new Vector3(point1.x, point1.y, 0));
        line.SetPosition(1, new Vector3(point2.x, point2.y, 0));
        line.material = Constants.MATERIAL;
        lineObj.AddComponent<PathGUIController>();
        lineObj.transform.SetParent(Constants.GRID.transform);
    }

    public override void Destroy()
    {
        UnityEngine.Object.Destroy(lineObj);
    }
}
