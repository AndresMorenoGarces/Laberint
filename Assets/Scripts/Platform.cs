using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour, IInteractuable
{
    Transform platform;
    public Transform hero;
    public GameObject cheatWall;
    int range = 7;

    public float movementSpeed;

    public void Interact()
    {
        Vector3 displacementHeroToPlatform = hero.transform.position - transform.position;
        float distanceToHero = displacementHeroToPlatform.magnitude;

        if (distanceToHero <= range)
        {
            Displace();
            cheatWall.SetActive(true);
        }
        else
            cheatWall.SetActive(false);
    }

    public void Displace()
    {
        hero.SetParent(platform);

        if(platform.position.x == -40 && platform.transform.position.y < 15)
        {
            platform.position += platform.up * 5 * Time.deltaTime;
        }
        else if(platform.position.x > -80 && platform.transform.position.y >= 15)
        {
            platform.position -= platform.right * 5 * Time.deltaTime;
        }
        else if(platform.position.x <= -80 && platform.transform.position.y <= 16 && platform.transform.position.y >= 5)
        {
            platform.position -= platform.up * 5 * Time.deltaTime;
        }
        else 
            range = 0;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void Awake()
    {
        platform = transform;
    }
    private void Update()
    {
        Interact();
    }
}
