using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Path path;
    public string pathName = "TrackPath";
    private Vector3 resPoint = Vector3.zero;
    private Vector3[] points;
    private Vector3 lastPos = Vector3.zero;

    public void Start()
    {
        //Find the path and compute equally spaced points on it 
        path = GameObject.Find(pathName).GetComponent<PathCreator>().path;
        points = path.CalculateEvenlySpacedPoints(1, 1);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            lastPos = other.gameObject.GetComponent<MoveMe>().lastPos;
        }
        else if (other.tag == "AICar")
        {
            lastPos = other.gameObject.GetComponent<DelegatedSteering>().lastPos;
        }

        float dist;
        float minDist = 100;
        int minIndex = 0;
        Vector3 p = Vector3.zero;

        for (int i = 0; i < points.Length; i++)
        {
            p = points[i];
            dist = Vector3.Distance(lastPos, p);

            if (dist < minDist)
            {
                minDist = dist;
                resPoint = p;
                minIndex = i;
            }
        }


        if (other.tag == "Player")
        {
            //other.gameObject.GetComponent<MoveMe>().PlayerBody.MovePosition(new Vector3(resPoint.x, resPoint.y + 1, resPoint.z));
            other.gameObject.GetComponent<MoveMe>().PlayerBody.velocity = Vector3.zero;
            other.gameObject.GetComponent<MoveMe>().PlayerBody.position = new Vector3(resPoint.x, resPoint.y + 1, resPoint.z);
            
            other.gameObject.GetComponent<MoveMe>().PlayerBody.MoveRotation(Quaternion.LookRotation((points[(minIndex + 1) % points.Length] - resPoint).normalized, other.gameObject.GetComponent<MoveMe>().PlayerBody.transform.up));
        }
        else if (other.tag == "AICar")
        {
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().MovePosition(new Vector3(resPoint.x, resPoint.y + 1, resPoint.z));
            other.gameObject.GetComponent<Rigidbody>().MoveRotation(Quaternion.LookRotation((points[(minIndex + 1) % points.Length] - resPoint).normalized, other.gameObject.GetComponent<Rigidbody>().transform.up));
        }

    }
}
