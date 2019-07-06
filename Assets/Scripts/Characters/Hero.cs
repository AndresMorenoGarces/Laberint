using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    private float initialSpeed = 5;
    private float finalSpeed;
    GameObject heroBody;
    private float rotationInX;
    public GameObject thirdPersonCamera;
    public GameObject skyCamera;
    public GameObject lightSkyCamera;
    public GameObject lantern;
    [SerializeField]
    private float mapTimer = 5;
    public Text canvasMapSky;
    public Text canvasMapThirdPerson;

    public void Displace()
    {
        if (Input.GetKey(KeyCode.W))
        {
            heroBody.transform.position += heroBody.transform.forward * Accelerate() * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            heroBody.transform.position -= heroBody.transform.forward * Accelerate() * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            heroBody.transform.position -= heroBody.transform.right * Accelerate() * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            heroBody.transform.position += heroBody.transform.right * Accelerate() * Time.deltaTime;
        }
    }

    float Accelerate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            finalSpeed = 12;

            return finalSpeed;
        }
        else
        {
            return initialSpeed;
        }
    }

    public void Rotate()
    {
        rotationInX += Input.GetAxis("Mouse X");
        heroBody.transform.eulerAngles = new Vector3(0, rotationInX * 2, 0);
        thirdPersonCamera.transform.eulerAngles = new Vector3(0, rotationInX * 2, 0);

    }

    public void AlternateCamera()
    {
        if (Input.GetKey(KeyCode.Space) == true)
        {
            if (mapTimer >= 0)
            {
                mapTimer -= Time.deltaTime;
                canvasMapSky.text = "Map Time: " + (int)mapTimer;
                
            }

            if (mapTimer >= 0)
            {
                thirdPersonCamera.SetActive(false);
                skyCamera.SetActive(true);
                lightSkyCamera.SetActive(true);
            }
            else
            {
                thirdPersonCamera.SetActive(true);
                skyCamera.SetActive(false);
                lightSkyCamera.SetActive(false);
            }
        }
        else
        {
            thirdPersonCamera.SetActive(true);
            skyCamera.SetActive(false);
            if (mapTimer <= 5)
            {
                mapTimer += Time.deltaTime;
                canvasMapThirdPerson.text = "Map Time: " + (int)mapTimer;
            }
        }
    }

    bool OnLantern()
    {
        if (Input.GetKey(KeyCode.Mouse0) == true)
        {
            lantern.SetActive(true);
        }
        else
        {
            lantern.SetActive(false);
        }
        return false;
    }

    private void Awake()
    {
        heroBody = gameObject;
    }

    private void Update()
    {
        Displace();
        Rotate();
        AlternateCamera();
        OnLantern();
    }

}
