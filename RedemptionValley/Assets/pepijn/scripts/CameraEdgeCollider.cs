using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEdgeCollider : MonoBehaviour
{
    EdgeCollider2D coll;
    Camera cam;
    float height;
    float width;
    Vector2[] edgePoints = new Vector2[5];                                                                                                                                                                                                                      

    private void Start()
    {
        coll = GetComponent<EdgeCollider2D>();
        cam = Camera.main;
    }

    private void LateUpdate()
    {
        GetEdgePoints();
        coll.points = edgePoints;
    }



    public void GetEdgePoints()
    {
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
        //3rd quadrant
        edgePoints[0].x = -0.5f * width;
        edgePoints[0].y = -0.5f * height;
        //2nd quadrant
        edgePoints[1].x = -0.5f * width;
        edgePoints[1].y = 0.5f * height;
        //1st quadrant
        edgePoints[2].x = 0.5f * width;
        edgePoints[2].y = 0.5f * height;
        //4th quadrant
        edgePoints[3].x = 0.5f * width;
        edgePoints[3].y = -0.5f * height;
        //3rd quadrant
        edgePoints[4].x = -0.49f * width;
        edgePoints[4].y = -0.5f * height;
    }
}
