using UnityEngine;

public class Model
{
    public int Health { get; set; }
    public int Damage { get; set; }

    public Model(int health)
    {
        Health = health;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= Constants.ZERO)
        {
            Debug.Log("Game Over!!!");
        }
    }
}
