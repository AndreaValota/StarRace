using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class LapCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI LapCount;
    [SerializeField] Animator fadeOut;
    [SerializeField] Animator audioFadeOut;

    private int _lap=1;
    public bool allCheckDone = false;

    public int Lap
    {
        get { return _lap; }
        set { 
            _lap = value;
            LapCount.text = _lap.ToString()+"/3";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && allCheckDone)
        {
            Lap++;
            //gameObject.SetActive(false);
            allCheckDone = false;
        }

        if (other.CompareTag("AICar"))
        {
            other.GetComponent<DelegatedSteering>().lap++;
        }

        if (Lap == 4)
        {

            //EndRace endScript = GameObject.Find("SetTextScript").GetComponent<EndRace>();
            EndRace.SetPos(GameObject.Find("PositionCount").GetComponent<TextMeshProUGUI>().text);

            fadeOut.SetTrigger("FadeOut");
            audioFadeOut.SetTrigger("FadeOut");

            /*Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;*/
        }
            
    }

}
