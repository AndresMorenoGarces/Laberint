using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    float lastCounter = 5;
    public Transform objetivePlace;
    public Transform heroPlace;
    public Text canvasWin;

    public void CompletedTest()
    {
        Vector3 displacementHeroToTreasure = heroPlace.position - objetivePlace.position;
        float distanceToTreasure = displacementHeroToTreasure.magnitude;

        if (distanceToTreasure < 10)
        {
            canvasWin.text = "¡You Win!";

            if (lastCounter < 0)
            {
                SceneManager.LoadScene(2);
            }

            lastCounter -= Time.deltaTime;
        }
        else
        {
            lastCounter = 5;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(objetivePlace.position, 10);
    }

    void Update()
    {
        CompletedTest();
    }
}
