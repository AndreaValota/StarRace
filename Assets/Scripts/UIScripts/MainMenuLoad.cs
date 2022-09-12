using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLoad : MonoBehaviour
{
    public void PlayTrack(int index)
    {
        SceneManager.LoadScene(index);

        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (index == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

}
