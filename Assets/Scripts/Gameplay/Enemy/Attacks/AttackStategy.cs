using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackStategy : ScriptableObject
{
    public abstract void Attack(Transform transform, MonoBehaviour monoBehaviour);
}
