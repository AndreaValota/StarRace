
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class PositionCounter : MonoBehaviour
{

    public bool isFinishLine=false;
    public GameObject LapCountCollider;

    public float[] DistanceArrays;

    [Header("Cars Car01 = Player Car")]
    public GameObject Car01;
    public GameObject Car02;
    public GameObject Car03;
    public GameObject Car04;


    float First;
    float Second;
    float Third;
    float Fourth;

    [Header("UI")]
    public TextMeshProUGUI Car01Text;
    /*public TextMeshPro Car02Text;
    public TextMeshPro Car03Text;
    public TextMeshPro Car04Text;*/

    public GameObject NextCheckPoint;

    void Start()
    {
        NextCheckPoint.SetActive(false);
    }


    void Update()
    {

        DistanceArrays[0] = Vector3.Distance(transform.position, Car01.transform.position);
        DistanceArrays[1] = Vector3.Distance(transform.position, Car02.transform.position);
        DistanceArrays[2] = Vector3.Distance(transform.position, Car03.transform.position);
        DistanceArrays[3] = Vector3.Distance(transform.position, Car04.transform.position);

        Array.Sort(DistanceArrays);

        First = DistanceArrays[0];
        Second = DistanceArrays[1];
        Third = DistanceArrays[2];
        Fourth = DistanceArrays[3];

        float Car01Dist = Vector3.Distance(transform.position, Car01.transform.position);
        //float Car02Dist = Vector3.Distance(transform.position, Car02.transform.position);
        //float Car03Dist = Vector3.Distance(transform.position, Car03.transform.position);
        //float Car04Dist = Vector3.Distance(transform.position, Car04.transform.position);

        #region Car01UI
        int pos = 0;
        int i = 4;
        if (Car01Dist == First)
        {
            pos = 1;
            //Car01Text.text = "1/4";
        }
        if (Car01Dist == Second)
        {
            pos = 2;
            //Car01Text.text = "2/4";
        }
        if(Car01Dist == Third)
        {
            pos = 3;
            //Car01Text.text = "3/4";
        }
        if (Car01Dist == Fourth)
        {
            pos = 4;
            //Car01Text.text = "4/4";
        }

        
        if(GameObject.Find("FinishLineLapTrigger").GetComponent<LapCounter>().Lap > Car02.GetComponent<DelegatedSteering>().lap)
        {
            i--;
        }
        if (GameObject.Find("FinishLineLapTrigger").GetComponent<LapCounter>().Lap > Car03.GetComponent<DelegatedSteering>().lap)
        {
            i--;
        }
        if (GameObject.Find("FinishLineLapTrigger").GetComponent<LapCounter>().Lap > Car04.GetComponent<DelegatedSteering>().lap)
        {
            i--;
        }

        //Debug.Log("i = " + i);
        if (i < pos) pos = i;

        Car01Text.text = pos+"/4";
        #endregion

        //Debug.Log(GameObject.Find("FinishLineLapTrigger").GetComponent<LapCounter>().Lap);
        //Debug.Log(Car02.GetComponent<DelegatedSteering>().lap);
        //Debug.Log("--------------------");

        /*#region Car02UI
        if (Car02Dist == First)
        {
            Car02Text.text = "1st";
        }
        if (Car02Dist == Second)
        {
            Car02Text.text = "2nd";
        }
        if (Car02Dist == Third)
        {
            Car02Text.text = "3rd";
        }
        if (Car02Dist == Fourth)
        {
            Car02Text.text = "4th";
        }
        #endregion

        #region Car03UI
        if (Car03Dist == First)
        {
            Car03Text.text = "1st";
        }
        if (Car03Dist == Second)
        {
            Car03Text.text = "2nd";
        }
        if (Car03Dist == Third)
        {
            Car03Text.text = "3rd";
        }
        if (Car03Dist == Fourth)
        {
            Car03Text.text = "4th";
        }
        #endregion

        #region Car04UI
        if (Car04Dist == First)
        {
            Car04Text.text = "1st";
        }
        if (Car04Dist == Second)
        {
            Car04Text.text = "2nd";
        }
        if (Car04Dist == Third)
        {
            Car04Text.text = "3rd";
        }
        if (Car04Dist == Fourth)
        {
            Car04Text.text = "4th";
        }
        #endregion*/

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "AICar" )
        {
            NextCheckPoint.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (isFinishLine)
        {
            //LapCountCollider.SetActive(true);
            LapCountCollider.GetComponent<LapCounter>().allCheckDone = true;
        }
    }
}