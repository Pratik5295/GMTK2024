using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemiesListSO", menuName = "ScriptableObjects/Enemy/EnemiesListSO")]
public class EnemiesListSO : ScriptableObject
{
    public List<EnemySO> _enemiesList;
}
