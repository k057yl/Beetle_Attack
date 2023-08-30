using UnityEngine;

public class Model
{
    private int _health;
    public int Health
    {
        get { return _health;}
        set { _health = value; }
    }
    
    private int _kill;
    public int Kill
    {
        get { return _kill;}
        set { _kill = value; }
    }

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

    public void TakeKills(int kill)
    {
        Kill += kill;
    }
}
