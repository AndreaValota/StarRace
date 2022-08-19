using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartCountdown : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI Count;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        StartCoroutine(Countdown(3));
    }

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;

        while (count > 0)
        {

            // display something...
            Count.text = count.ToString();
            yield return new WaitForSecondsRealtime(1);
            count--;
        }

        Count.text = "";

        StartGame();
    }

    void StartGame()
    {
        Time.timeScale = 1;
    }

}
