using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementOnPath : MonoBehaviour
{
    public string pathName = "MonzaPath";
    public float spacing = 0.25f;
    private float resolution = 1;
    public float speed = 5;

    private Path path;
    private Vector3[] pathPoints;
    private Vector3 destination;


    public void Start()
    {
        //Find the path and compute equally spaced points on it 
        path = GameObject.Find(pathName).GetComponent<PathCreator>().path;
        pathPoints = path.CalculateEvenlySpacedPoints(spacing, resolution);
    }


    private void FixedUpdate()
    {
        for (int i = 0; i <= pathPoints.Length;)
        {
            GetComponent<Rigidbody>().MovePosition(pathPoints[i]);
            i = i++ % pathPoints.Length;
        }
    }
}