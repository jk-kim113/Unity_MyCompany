using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField]
    float _speed, _tilt;

    Rigidbody _myRB;

    private void Start()
    {
        _myRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(horizontal, vertical, 0);


        _myRB.velocity = dir.normalized * _speed;

        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, mXMin, mXMax),
        //                                 transform.position.y,
        //                                 Mathf.Clamp(transform.position.z, mZMin, mZMax));

        _myRB.rotation = Quaternion.Euler(90, 0, 180 +_tilt * horizontal);
    }
}
