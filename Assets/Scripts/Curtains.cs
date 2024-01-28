using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtains : MonoBehaviour
{
    [SerializeField] private Animator _anim = null;

    public void Enter()
    {
        _anim.SetTrigger("Enter");
    }

    public void Exit()
    {
        _anim.SetTrigger("Exit");
    }
}
