using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    // Update is called once per frame
    void Update()
    {
        Vector3 vec = _target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(vec.normalized);
    }
}
