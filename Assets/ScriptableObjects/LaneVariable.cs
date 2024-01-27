using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Lane")]
public class LaneVariable : ScriptableObject
{
    [SerializeField] private CharacterController.Lane _variable;
    public CharacterController.Lane Variable { get { return _variable; } set { _variable = value; } }
}
