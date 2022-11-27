using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerFuel : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("fuel items")]
    [SerializeField] float fuel = 100f;
    [SerializeField] float rate_of_consumption = 1f;
    [SerializeField] TMP_Text fuel_bar;
    [SerializeField] Slider fuel_silder;

    [Header("boost items")]
    [SerializeField] float boost_time;
    [SerializeField] float boost_multiplier = 1f;  // basically same speed

    float max_fuel;
    Movement_v2 movement_script;
    private void Start() 
    {
        movement_script = FindObjectOfType<Movement_v2>();    
        max_fuel = fuel;
    }

    public float get_fuel()
    {
        return fuel;
    }

    public void reduce_fuel()
    {
        if (fuel > 0)
            fuel -= (rate_of_consumption * Time.deltaTime);
        else
        {
            fuel = 0;
            movement_script.stop_thrusting();
            movement_script.enabled = false;
        }
    }

    void update_fuel_bar()
    {
        fuel_bar.text = $"Fuel : {fuel.ToString("0.00")}";
        fuel_silder.value = fuel;
    }

    private void Update() 
    {
        update_fuel_bar();
    }
    IEnumerator trigger_boost_event()
    {
        movement_script.set_thurst_speed(boost_multiplier);
        yield return new WaitForSeconds(boost_time);
        movement_script.reset_thrust_speed();
    }
    
    // pickup behaviors
    private void OnTriggerEnter(Collider other) 
    {
        string collision_tag = other.gameObject.tag;
        Destroy(other.gameObject);
        if (collision_tag == "fuel")
        {
            fuel = max_fuel; 
        }
        else if (collision_tag == "boost")
        {
            StartCoroutine(trigger_boost_event());
        }
    }
}
