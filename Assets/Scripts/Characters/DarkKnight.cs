using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine;

//[RequireComponent(requiredComponent:typeof(Waypoints))]
public class DarkKnight : MonoBehaviour 
{
    float counterRestarTest = 0.5f;
    float sightingCounter = 1f;
    public float rotateTime = 4;
    public float staticTime = 2;
    float viewRange = 0;
    float prodViewRange = 0;
    bool sightingPrecaution = false;
    GameObject precautionObject;
    GameObject precautionObject2;
    GameObject warningObject;
    GameObject warningObject2;
    public float displacementSpeed;
    public List<Transform> displacementPointTransforms;
    private int actualDisplacementPoint = 0;
    NavMeshAgent navMeshDisplacement;
    Transform darkKnightTransform;
    public Vector3 initialHeroTransform;
    Transform heroTransform;
    public Text canvasReset;
  
    bool CloseUpEvent()
    {
        Vector3 displacementToHero = heroTransform.position - darkKnightTransform.position;
        float distanceToHero = displacementToHero.magnitude;
        sightingPrecaution = false;

        if (distanceToHero < Suspect())
        {
            float prodDot = Vector3.Dot(darkKnightTransform.forward, displacementToHero.normalized);

            if (prodDot > ToConcentrate())
            {
                RaycastHit sighting;
                if (Physics.Raycast(darkKnightTransform.position + displacementToHero.normalized * 1.01f, displacementToHero.normalized, out sighting, Mathf.Infinity))
                {
                    Debug.DrawRay(darkKnightTransform.position + displacementToHero.normalized * 1.01f, displacementToHero.normalized * sighting.distance, Color.red);

                    if (sighting.collider.gameObject.name == "Hero")
                    {
                        precautionObject.transform.position = new Vector3(darkKnightTransform.position.x, darkKnightTransform.position.y + 6.5f, darkKnightTransform.position.z);
                        precautionObject2.transform.position = new Vector3(darkKnightTransform.position.x, darkKnightTransform.position.y + 5, darkKnightTransform.position.z);

                        if (sightingCounter < 0)
                        {
                            warningObject.transform.position = new Vector3(darkKnightTransform.position.x, darkKnightTransform.position.y + 6.5f, darkKnightTransform.position.z);
                            warningObject2.transform.position = new Vector3(darkKnightTransform.position.x, darkKnightTransform.position.y + 5, darkKnightTransform.position.z);
                            ApproachTheHero();
                            return true;
                        }
                        else
                        {
                            sightingPrecaution = true;
                        }
                        sightingCounter -= Time.deltaTime;
                    }
                }
            }
            else
            {
                Rotate();
            }
        }
        else
        {
            Navigation();          
            sightingCounter = 2;
        }
        return false;      
    }

    void RestartTest()
    {
        canvasReset.text = "Time to Reset: "+ counterRestarTest.ToString("F1");

        if (CloseUpEvent() == true)
        {
            if (counterRestarTest < 0)
            {
                heroTransform.position = initialHeroTransform;
                sightingCounter = 2;
            }
            counterRestarTest -= Time.deltaTime;
        }
        else
        counterRestarTest = 0.5f;
    }

    void Navigation()
    {
        Vector3 distanceNavMesh = navMeshDisplacement.destination;
        distanceNavMesh = displacementPointTransforms[actualDisplacementPoint].transform.position - darkKnightTransform.position;

        if (distanceNavMesh.magnitude > 1)
        {
            Vector3 directionVector = distanceNavMesh.normalized;
            darkKnightTransform.LookAt(directionVector + darkKnightTransform.position);
            darkKnightTransform.position += directionVector * displacementSpeed * Time.deltaTime;
        }
        else
        {
            actualDisplacementPoint++;
            if (actualDisplacementPoint >= displacementPointTransforms.Count)
            {
                actualDisplacementPoint = 0;
            }
        }
    }

    void ApproachTheHero()
    {
        Vector3 distanceNavMesh = navMeshDisplacement.destination;
        distanceNavMesh = heroTransform.position - darkKnightTransform.position;

        Vector3 directionVector = distanceNavMesh.normalized;
        transform.LookAt(directionVector + darkKnightTransform.position);

        if (distanceNavMesh.magnitude > 1f)
        {
            transform.position += directionVector * displacementSpeed * Time.deltaTime;
        }
    }

    float ToConcentrate()
    {
        if (Input.GetKey(KeyCode.LeftShift) == true)
        {
            prodViewRange = 0;
        }
        else
        {
            prodViewRange = 0.5f;
        }

        return prodViewRange;
    }

    float Suspect()
    {
        if (Input.GetKey(KeyCode.LeftShift) == true)
        {
            viewRange = 15;
        }
        else
        {
            viewRange = 8;
        }

        return viewRange;
    }

    void Rotate()
    {
        transform.Rotate(new Vector3(0, 100f, 0) * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Suspect());
    }

    private void Awake()
    {
        darkKnightTransform = transform;
        heroTransform = GameObject.Find("Hero").transform;

        navMeshDisplacement = GetComponent<NavMeshAgent>();

        precautionObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        precautionObject.transform.localScale = new Vector3(0.3f, 0.8f, 0.3f);
        precautionObject.GetComponent<Renderer>().material.color = Color.yellow;
        precautionObject.name = "Precaucion";
        precautionObject.transform.SetParent(transform);

        precautionObject2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        precautionObject2.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        precautionObject2.GetComponent<Renderer>().material.color = Color.yellow;
        precautionObject2.name = "Precaucion2";
        precautionObject2.transform.SetParent(transform);

        warningObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        warningObject.transform.localScale = new Vector3(0.3f, 0.8f, 0.3f);
        warningObject.GetComponent<Renderer>().material.color = Color.red;
        warningObject.name = "Advertencia";
        warningObject.transform.SetParent(transform);

        warningObject2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        warningObject2.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        warningObject2.GetComponent<Renderer>().material.color = Color.red;
        warningObject2.name = "Advertencia2";
        warningObject2.transform.SetParent(transform);
    }

    void Update()
    {
        CloseUpEvent();
        warningObject.SetActive(CloseUpEvent());
        warningObject2.SetActive(CloseUpEvent());
        precautionObject.SetActive(sightingPrecaution);
        precautionObject2.SetActive(sightingPrecaution);
        RestartTest();
    }
}