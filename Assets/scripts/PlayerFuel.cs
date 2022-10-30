using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerFuel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float fuel = 100f;
    [SerializeField] float rate_of_consumption = 1f;
    [SerializeField] TMP_Text fuel_bar;
    [SerializeField] Slider fuel_silder;
    float max_fuel;
    Movement movement_script;
    private void Start() 
    {
        movement_script = FindObjectOfType<Movement>();    
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

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "fuel")
        {
            fuel = max_fuel;
            Destroy(other.gameObject);
        }
    }
}
