using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chainV2 : MonoBehaviour
{
    public BezierSpline spline;
    public List<Transform> points;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spline.Reset();

        for (int i = 0; i < points.Count; i++)
        {
            spline.SetControlPoint(i+1, points[i].position);
        }
    }
}
