using System;
using TensorFlow;
using UnityEngine;

public class AIMgr
{
    private static AIMgr _instance;

    private TFGraph graph;
    private TFSession session;


    public static AIMgr instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AIMgr();
            }
            return _instance;
        }
    }

    public AIMgr()
    {
        TextAsset graphModel = Resources.Load("basket_ball.pb") as TextAsset;
        Debug.Log(graphModel.bytes.Length);
        graph = new TFGraph();
        graph.Import(graphModel.bytes);

        session = new TFSession(graph);
    }

    public float GetForceForDistance(float distance)
    {
        var runner = session.GetRunner();
        runner.AddInput(
            graph["batch_normalization_1_input"][0], new float[1, 1] { { distance } }
            );

        runner.Fetch(graph["dense_3/BiasAdd"][0]);
        float[,] recurrent_tensor = runner.Run()[0].GetValue() as float[,];

        var force = recurrent_tensor[0, 0];
        return force;
    }
}
