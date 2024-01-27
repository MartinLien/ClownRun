using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float _speed = 1f;
    public float Speed { set { _speed = value; } }

    public System.Action OnTrigger;

    private void Update()
    {
        transform.position -= Vector3.forward * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            OnTrigger?.Invoke();
        }
    }
}
