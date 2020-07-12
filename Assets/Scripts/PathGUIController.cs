using System;
using UnityEngine;

public class PathGUIController : MonoBehaviour
{
    void Update()
    {
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        if (scrollDelta != 0)
        {
            LineRenderer lr = GetComponent<LineRenderer>();
            lr.startWidth = scrollDelta > 0 ? lr.startWidth / 1.2f : lr.startWidth * 1.2f;
            lr.endWidth = scrollDelta > 0 ? lr.endWidth / 1.2f : lr.endWidth * 1.2f;
        }
    }
}
