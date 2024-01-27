using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOnTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle = null;
    [SerializeField] private string _layerToTrigger = "Player";
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(_layerToTrigger))
            _particle.Play();
    }
}
