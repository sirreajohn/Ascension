using UnityEngine;
using UnityEngine.InputSystem;

public class Movement_v2 : MonoBehaviour
{
    Rigidbody phys_engine;
    AudioSource audio_player;  // audio is set to play on awake, it pauses and unpause on demand and is set to loop
    PlayerFuel fuel_systems;
    float thrush_trigger = 0f;
    float torque_direction;
    bool infinite_fuel = false;

    [SerializeField] float precise_factor;
    [SerializeField] float thrust_speed = 0f;
    [SerializeField] float torque_speed = 0f;
    [SerializeField] ParticleSystem thrust_particles;

    void Start()
    { 
        fuel_systems = FindObjectOfType<PlayerFuel>();
        phys_engine = GetComponent<Rigidbody>();
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
    void processThrust()
    {
        if (thrush_trigger == 1f)
        {
            start_thrusting();
        }
    }
    void bank_along_direction()
    {
        Vector3 torque = new Vector3(0, 0, torque_direction * (torque_speed));
        phys_engine.AddRelativeTorque(torque * Time.deltaTime);
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


    private void start_thrusting()
    {
        Vector3 thrust_direction = new Vector3(0, thrush_trigger * thrust_speed, 0);  // apply force only along Y-axis
        
        if(!thrust_particles.isPlaying)  // prevent repeated triggers
            thrust_particles.Play(); 
        
        audio_player.UnPause();
        if (!infinite_fuel)  // infinite fuel mode checker
            fuel_systems.reduce_fuel();

        phys_engine.AddRelativeForce(thrust_direction * Time.deltaTime);
    }

    public void stop_thrusting()
    {
        audio_player.Pause();
        thrust_particles.Stop();
    }
    public void toggle_infinite_fuel()
    {
        infinite_fuel = !infinite_fuel;
    }

}
