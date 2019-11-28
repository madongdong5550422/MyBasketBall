using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float force;

    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DoDespawn(30));
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
}
