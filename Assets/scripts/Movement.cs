using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody phys_engine;
    [SerializeField] float precise_factor;
    [SerializeField] float thrust_speed = 0f;
    [SerializeField] float torque_speed = 0f;
    void Start()
    { 
        phys_engine = GetComponent<Rigidbody>();
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
            Vector3 thrust_direction = new Vector3(0, 1 * thrust_speed, 0);  // apply force only along Y-axis
            phys_engine.AddRelativeForce(thrust_direction * Time.deltaTime);
        }
    }

    void processRotation()
    {

        if (Input.GetKey(KeyCode.A))
        {  
            if(!Input.GetKey(KeyCode.LeftShift))
                precise_factor = 1f;
            bank_along_direction(torque_direction: 1f, precise_factor: precise_factor); // bank left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if(!Input.GetKey(KeyCode.LeftShift))
                precise_factor = 1f;
            bank_along_direction(torque_direction: -1f, precise_factor: precise_factor); // bank right
        }
    }

    void bank_along_direction(float torque_direction, float precise_factor)
    {
        Vector3 torque = new Vector3(0, 0, 1 * torque_direction * (torque_speed * precise_factor));
        // phys_engine.freezeRotation = true;
        phys_engine.AddRelativeTorque(torque * Time.deltaTime);
        // phys_engine.freezeRotation = false;
    }

}
