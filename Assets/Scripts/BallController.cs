using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float force;

    public float distance;

    public Material MaterialBallScored;

    private bool hasTrigger1 = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DoDespawn(30));
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Trigger01")
        {
            hasTrigger1 = true;
        }

        if (other.name == "Trigger02")
        {
            if (hasTrigger1)
            {
                OnScored();
            }
        }

    }

    IEnumerator DoDespawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnScored()
    {
        GetComponent<Renderer>().material = MaterialBallScored;
        Debug.Log(distance + "," + force);
    }
}
