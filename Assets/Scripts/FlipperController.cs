using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperController : MonoBehaviour
{
    public float motorSpeed = 1000f;  // The speed at which the flipper moves
    public float motorMaxForce = 1000f;  // The maximum force the motor can exert
    private HingeJoint2D hinge;

    private void Start()
    {
        // Get the HingeJoint2D component
        hinge = GetComponent<HingeJoint2D>();
    }

    public void FlipUp()
    {
        SetMotorSpeed(motorSpeed);
    }

    public void FlipDown()
    {
        SetMotorSpeed(-motorSpeed);
    }

    private void SetMotorSpeed(float speed)
    {
        // Create a new motor with the specified speed and maximum force, and assign it to the hinge joint
        JointMotor2D motor = new JointMotor2D { motorSpeed = speed, maxMotorTorque = motorMaxForce };
        hinge.motor = motor;
        hinge.useMotor = true;
    }
}
