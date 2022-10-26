
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] float player_health = 100f;
    [SerializeField] TMP_Text health_text;
    [SerializeField] Slider health_bar;
    float max_health;
    private void Start() 
    {
        max_health = player_health;
    }

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

    public float getHealth()
    {
        return player_health;
    }
    public void kill_player()
    {
        player_health = 0f;
    }
    private void Update()
    {
        update_health_bar();
    }
    private void update_health_bar()
    {
        health_text.text = $"Health: {player_health.ToString("0.00")}";
        health_bar.value = player_health;
    }
}
