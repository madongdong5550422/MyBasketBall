using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TensorFlow;

public class NewBehaviourScript : MonoBehaviour {

    private TFGraph graph;
    private TFSession session;

	// Use this for initialization
	void Start () {
        TextAsset graphModel = Resources.Load("basket_ball.pb") as TextAsset;
        Debug.Log(graphModel.bytes.Length);
        ////graphModel.text

        //TextAsset testText = Resources.Load("test") as TextAsset;
        //Debug.Log(testText);

        graph = new TFGraph();
        graph.Import(graphModel.bytes);

        session = new TFSession(graph);


    }
	
	// Update is called once per frame
	void Update () {

        var distance = Random.Range(4f, 10f);
        distance = 6.121656f;
        var runner = session.GetRunner();

        runner.AddInput(
            graph["batch_normalization_1_input"][0], new float[1, 1] { {distance} }
            );

        runner.Fetch(graph["dense_3/BiasAdd"][0]);
        float[,] recurrent_tensor = runner.Run()[0].GetValue() as float[,];

        var force = recurrent_tensor[0, 0];

        Debug.Log(distance + "," + force);
	}
}
