using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Float")]
public class FloatVariable : ScriptableObject
{
    [SerializeField] private float _variable = 1f;
    public float Variable { get { return _variable; } }
}
