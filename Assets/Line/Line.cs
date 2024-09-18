using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Line : MonoBehaviour
{
    private LineRenderer Line1;

    List<Vector2> points;

    private void Awake()
    {
        Line1 = GetComponent<LineRenderer>();
    }

    public void UpdateLine(Vector2 position)
    {
        if (points == null)
        {
            points = new List<Vector2>();

            SetPoint(position);
            return;
        }

        if (Vector2.Distance(points.Last(), position) > .1f)
        {
            SetPoint(position);
        }
    }


    void SetPoint(Vector2 point)
    {
        points.Add(point);

        Line1.positionCount = points.Count;
        Line1.SetPosition(points.Count -1, point);
         
    }
}
