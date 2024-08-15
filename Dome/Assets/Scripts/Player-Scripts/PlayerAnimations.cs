using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    // Animation Hydraulics
    public Animator hydraulicRightAnim;
    public Animator hydraulicLeftAnim;
    public Animator wheelSpinnAnim;
    public Animator moveLeanAnim;

    private bool isMovingForward = false;
    private bool isMovingBackward = false;

    void Update()
    {
        // Handle forward movement
        if (Input.GetKeyDown(KeyCode.W))
        {
            isMovingForward = true;
            isMovingBackward = false;
            SetMovementParameters();
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            isMovingForward = false;
            SetMovementParameters();
        }

        // Handle backward movement
        if (Input.GetKeyDown(KeyCode.S))
        {
            isMovingBackward = true;
            isMovingForward = false;
            SetMovementParameters();
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            isMovingBackward = false;
            SetMovementParameters();
        }
    }

    void SetMovementParameters()
    {
        bool isMoving = isMovingForward || isMovingBackward;

        // Hydraulic animations
        hydraulicRightAnim.SetBool("Hydraulic-Close-right", isMoving);
        hydraulicRightAnim.SetBool("Hydraulic-Open-right", !isMoving);

        hydraulicLeftAnim.SetBool("Hydraulic-Close-left", isMoving);
        hydraulicLeftAnim.SetBool("Hydraulic-Open-left", !isMoving);

        // Wheel spin animations
        wheelSpinnAnim.SetBool("Spinn-Forward", isMovingForward);
        wheelSpinnAnim.SetBool("Spinn-Backward", isMovingBackward);

        if (!isMoving)
        {
            wheelSpinnAnim.SetBool("Spinn-Forward", false);
            wheelSpinnAnim.SetBool("Spinn-Backward", false);
            wheelSpinnAnim.SetBool("Spinn-Stop", true);
            wheelSpinnAnim.SetBool("Spinn-Stop-back", true);
        }
        else
        {
            wheelSpinnAnim.SetBool("Spinn-Stop", false);
            wheelSpinnAnim.SetBool("Spinn-Stop-back", false);
        }

        // Move lean animations
        moveLeanAnim.SetBool("Lean-Forward", isMovingForward);

        if (!isMoving)
        {
            moveLeanAnim.SetBool("Lean-Forward", false);
            moveLeanAnim.SetBool("Lean-Stop", true);
        }
        else
        {
            moveLeanAnim.SetBool("Lean-Stop", false);
        }
    }
}
