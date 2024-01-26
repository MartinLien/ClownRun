using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    [SerializeField] private float _lifetime = 3f;

    private float _timer = 0;

    void Update()
    {
        if (_timer >= _lifetime)
            Destroy(this.gameObject);

        _timer += Time.deltaTime;
    }
}
