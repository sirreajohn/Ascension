using UnityEngine;


// detach parts when explode.
public class Movement : MonoBehaviour
{
    Rigidbody phys_engine;
    AudioSource audio_player;  // audio is set to play on awake, it pauses and unpause on demand and is set to loop
    PlayerFuel fuel_systems;
    bool infinite_fuel = false;

    [SerializeField] float precise_factor;
    [SerializeField] float thrust_speed = 0f;
    [SerializeField] float torque_speed = 0f;
    [SerializeField] ParticleSystem thrust_particles;

    void Start()
    { 
        fuel_systems = FindObjectOfType<PlayerFuel>();
        phys_engine = GetComponent<Rigidbody>();
        audio_player = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        processThrust();
        processRotation();
    }

    void processThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
           start_thrusting();
        }
        else
        {
            stop_thrusting();
        }
    }

    void processRotation()
    {

        if (Input.GetKey(KeyCode.A))
        {
            bank_left();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            bank_right();
        }
    }

    public void stop_thrusting()
    {
        audio_player.Pause();
        thrust_particles.Stop();
    }

    private void start_thrusting()
    {
        Vector3 thrust_direction = new Vector3(0, 1 * thrust_speed, 0);  // apply force only along Y-axis
        thrust_particles.Play();
        audio_player.UnPause();
        if (!infinite_fuel)
            fuel_systems.reduce_fuel();
        phys_engine.AddRelativeForce(thrust_direction * Time.deltaTime);
    }

    private void bank_right()
    {
        if (!Input.GetKey(KeyCode.LeftShift)) // make precise movements 
            precise_factor = 1f;
        bank_along_direction(torque_direction: -1f, precise_factor: precise_factor); // bank right
    }

    private void bank_left()
    {
        if (!Input.GetKey(KeyCode.LeftShift)) // make precise movements 
            precise_factor = 1f;
        bank_along_direction(torque_direction: 1f, precise_factor: precise_factor); // bank left
    }

    void bank_along_direction(float torque_direction, float precise_factor)
    {
        Vector3 torque = new Vector3(0, 0, 1 * torque_direction * (torque_speed * precise_factor));
        phys_engine.AddRelativeTorque(torque * Time.deltaTime);
    }

    public void toggle_infinite_fuel()
    {
        infinite_fuel = !infinite_fuel;
    }

}
