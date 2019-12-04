using System;
using TensorFlow;
using UnityEngine;

public class AIMgr
{
    private static AIMgr _instance;

    private TFGraph graph;
    private TFSession session;

    private TFGraph graph2d;
    private TFSession session2d;


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

        TextAsset graphModel2d = Resources.Load("basket_ball_2d.pb") as TextAsset;
        Debug.Log(graphModel2d.bytes.Length);
        graph2d = new TFGraph();
        graph2d.Import(graphModel2d.bytes);

        session2d = new TFSession(graph2d);
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

    public float GetForceForPos(float posX, float posZ)
    {
        var runner = session2d.GetRunner();
        runner.AddInput(
            graph2d["batch_normalization_1_input"][0], new float[1, 2] { { posX, posZ } }
            );

        runner.Fetch(graph2d["dense_5/BiasAdd"][0]);
        float[,] recurrent_tensor = runner.Run()[0].GetValue() as float[,];

        var force = recurrent_tensor[0, 0];
        return force;
    }
}
