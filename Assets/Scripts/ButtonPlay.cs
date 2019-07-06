using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPlay : MonoBehaviour
{
    public void NewGame(bool click)
    {
        if (click == true)
        {
            SceneManager.LoadScene(1);
        }
    }
}
