using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullCurtans : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<Curtains>().Exit();
    }
}
