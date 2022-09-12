using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(4);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
