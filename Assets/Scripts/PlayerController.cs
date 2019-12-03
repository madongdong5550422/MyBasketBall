using System.Collections;
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

    public float interval = 0.1f;

    public GameObject ballPrefab;

    float intervalTime = 0f;

    public GameObject target;

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
        float distance = (target.transform.position - transform.position).magnitude;
        float force = 0f;
        switch (mode)
        {
            case Mode.None:
                break;
            case Mode.Generate:
                force = Random.Range(0.1f, 1.0f);
                Shoot(force, distance);
                break;
            case Mode.Test:
                force = AIMgr.instance.GetForceForDistance(distance);
                Shoot(force, distance);
                break;
            default:
                break;
        }
    }

    void Shoot(float force, float distance)
    {
        var ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        var bc = ball.GetComponent<BallController>();

        bc.force = force;
        bc.xPos = transform.position.x;
        bc.zPos = transform.position.z;

        var f = (force + 0.3f) * 300;

        bc.transform.position = transform.position + new Vector3(0, 0.5f, 0);

        Vector3 direct = target.transform.position - transform.position;
        direct.y = Mathf.Sqrt(direct.x * direct.x + direct.z + direct.z);

        Vector3 forceDir = Mathf.Sqrt(f * f + f * f) * direct.normalized;

        //ball.GetComponent<Rigidbody>().AddForce(new Vector3(-f, f, 0));
        ball.GetComponent<Rigidbody>().AddForce(forceDir);

        StaticMgr.instance.ShootAddOne();

        if (mode == Mode.Generate)
        {
            var posX = Random.Range(-5f, 6f);
            var posZ = Random.Range(-5f, 5f);
            var pos = transform.position;
            pos.x = posX;
            pos.z = posZ;
            transform.position = pos;
        }
    }
}
