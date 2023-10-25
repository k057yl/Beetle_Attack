using UnityEngine;

[CreateAssetMenu(fileName = "NewConfig",menuName = "Configs")]
public class Config : ScriptableObject
{
    public float Speed;
    public float SuperSpeed;
    public float Energy;
    public Rigidbody2D Rigidbody2D { get; set; }
}