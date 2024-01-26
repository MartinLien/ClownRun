using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    private float _speed = 1f;
    public float Speed { set { _speed = value; } }

    private void Update()
    {
        transform.position -= Vector3.forward * _speed * Time.deltaTime;
    }
}
