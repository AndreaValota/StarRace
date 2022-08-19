using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndRace : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI EndPosition;

    static private string pos = "";

    // Start is called before the first frame update
    void Start()
    {
        EndPosition.text = pos;
    }

    static public void SetPos(string lastPos)
    {
        pos = lastPos;
    }

}
