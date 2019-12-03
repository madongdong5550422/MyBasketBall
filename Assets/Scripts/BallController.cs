using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class BallController : MonoBehaviour
{
    public float force;

    public float distance;

    public float xPos;
    public float zPos;

    public Material MaterialBallScored;

    private bool hasTrigger1 = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DoDespawn(5));
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
        StaticMgr.instance.ScoreAddOne();
        GetComponent<Renderer>().material = MaterialBallScored;
        string info = xPos + "," + zPos + "," + force;
        Debug.Log("score");

        WriteInfo(info);
    }

    void WriteInfo(string info)
    {
        File.AppendAllText("successful_shots.csv", info += "\n");
    }

}
