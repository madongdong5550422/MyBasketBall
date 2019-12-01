using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PercentController : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshPro textMesh;

    void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = StaticMgr.instance.Percent();
    }
}
