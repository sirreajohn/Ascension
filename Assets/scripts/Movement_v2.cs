using UnityEngine;
using UnityEngine.InputSystem;

public class Movement_v2 : MonoBehaviour
{
    Rigidbody phys_engine;
    AudioSource audio_player;  // audio is set to play on awake, it pauses and unpause on demand and is set to loop
    PlayerFuel fuel_systems;
    float thrush_trigger = 0f;  // thrust trigger 
    float torque_direction;
    bool infinite_fuel = false;
    
    float initial_thrust_speed;

    [SerializeField] float precise_factor;
    [SerializeField] float thrust_speed = 0f;
    [SerializeField] float torque_speed = 0f;
    [SerializeField] ParticleSystem thrust_particles;
    [SerializeField] ParticleSystem boost_particles;

    ParticleSystem current_exhaust;

    private void Start()
    { 
        initial_thrust_speed = thrust_speed;
        fuel_systems = FindObjectOfType<PlayerFuel>();
        phys_engine = GetComponent<Rigidbody>();
        current_exhaust = thrust_particles;

        // weird ass audio impl
        audio_player = GetComponent<AudioSource>();
        audio_player.Play();
        audio_player.Pause();
    }

    private void Update() 
    {   
        processThrust();
        bank_along_direction();
    }
    private void processThrust()
    {
        if (thrush_trigger == 1f)
        {
            start_thrusting();
        }
    }
    private void bank_along_direction()
    {
        Vector3 torque = new Vector3(0, 0, torque_direction * (torque_speed));
        phys_engine.AddRelativeTorque(torque * Time.deltaTime);
    }

    private void start_thrusting()
    {
        Vector3 thrust_direction = new Vector3(0, thrush_trigger * thrust_speed, 0);  // apply force only along Y-axis
        
        if(!current_exhaust.isPlaying)  // prevent repeated triggers
            current_exhaust.Play(); 
        
        audio_player.UnPause();
        if (!infinite_fuel)  // infinite fuel mode checker
            fuel_systems.reduce_fuel();

        phys_engine.AddRelativeForce(thrust_direction * Time.deltaTime);
    }

    public void set_thurst_speed(float new_thrust_multiplier)
    {
        toggle_infinite_fuel();  // no fuel usage while in boost
        thrust_speed = thrust_speed * new_thrust_multiplier;
        current_exhaust.Stop();
        current_exhaust = boost_particles;
    }

    public void reset_thrust_speed()
    {
        toggle_infinite_fuel();  // return to fuel usage  
        thrust_speed = initial_thrust_speed;
        current_exhaust.Stop();
        current_exhaust = thrust_particles;
    }

    public void thrust_upward(InputAction.CallbackContext context)
    {
        thrush_trigger = context.ReadValue<Vector2>().y;   
        if (context.action.phase == InputActionPhase.Canceled)
        {
            stop_thrusting();
        }
    }

    public void bank_left_right(InputAction.CallbackContext value)
    {
        torque_direction = value.ReadValue<Vector2>().x;
    }

    public void stop_thrusting()
    {
        audio_player.Pause();
        current_exhaust.Stop();
    }
    public void toggle_infinite_fuel()
    {
        infinite_fuel = !infinite_fuel;
    }

}
