using UnityEngine;

public class Oscillator : MonoBehaviour
{
    
    Vector3 starting_position;
    const float tau = Mathf.PI * 2; // 6.283
    [SerializeField] Vector3 movement_vector;
    [SerializeField] [Range(0, 1)] float movement_factor = 0f;
    [SerializeField] float period;
    void Start()
    {
        starting_position = transform.position;
    }
 
    void Update()
    {
        if (period <= Mathf.Epsilon) return;

        float cycles = Time.time / period;
        float raw_sine_wave = Mathf.Sin(cycles * tau);

        movement_factor = (raw_sine_wave + 1f) / 2f;
        Vector3 offset = movement_vector * movement_factor;
        transform.position = starting_position + offset;
    }
}
