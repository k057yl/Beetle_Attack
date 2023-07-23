using UnityEngine;

[CreateAssetMenu(fileName = "NewConfig",menuName = "Configs")]
public class CharacterConfig : ScriptableObject
{
    public float Speed;
    public float SuperSpeed;
    public float BulletSpeed;
}