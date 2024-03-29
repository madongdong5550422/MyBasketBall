﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public enum Mode
    {
        None = 0,
        Generate = 1,
        Test = 2,
    }

    public float Speed = 5;

    public Mode mode = Mode.Generate;

    public float interval = 1f;

    public GameObject ballPrefab;

    float intervalTime = 1f;

    private Rigidbody Rigidbody;
    // Use this for initialization
    void Start ()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void FixedUpdate () {
        Vector3 movement = new Vector3(
            Input.GetAxis("Horizontal") * Speed * Time.deltaTime,
            0f,
            Input.GetAxis("Vertical") * Speed * Time.deltaTime
        );

        Rigidbody.MovePosition(Vector3.MoveTowards(
            transform.position,
            transform.position + movement,
            Speed
        ));

        intervalTime += Time.fixedDeltaTime;
        if (intervalTime > interval)
        {
            intervalTime = 0;
            Movement();
        }

    }

    void Movement()
    {
        switch (mode)
        {
            case Mode.None:
                break;
            case Mode.Generate:
                float force = Random.Range(0.1f, 1.0f);
                Shoot(force);
                break;
            case Mode.Test:
                break;
            default:
                break;
        }
    }

    void Shoot(float force)
    {
        var ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        var bc = ball.GetComponent<BallController>();

        bc.force = force;

        var f = (force + 0.3f) * 300;

        bc.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        ball.GetComponent<Rigidbody>().AddForce(new Vector3(-f, f, 0));
    }
}
