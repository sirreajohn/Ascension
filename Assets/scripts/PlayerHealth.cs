
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] float player_health = 100f;


    public bool reduceHealth(float reduction_amount)
    {
        float new_health = player_health - reduction_amount;
        if (new_health <= 0)
        {
            return true;
        }
        else
        {
            player_health = new_health;
            return false;
        }
    }
}
