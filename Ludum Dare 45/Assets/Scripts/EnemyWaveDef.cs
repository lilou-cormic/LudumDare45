using UnityEngine;

[CreateAssetMenu(fileName = "Wave0-1-1", menuName = "EnemyWaveDef")]
public class EnemyWaveDef : ScriptableObject
{
    [SerializeField]  EnemyGroupDef[] _Sequence = null;
    public EnemyGroupDef[] Sequence => _Sequence;
}
