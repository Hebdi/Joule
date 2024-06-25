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

    // Animation of Player
    private Animator mAnimator;

    // Update is called once per frame
    void Update()
    {
        // Handle forward movement
        if (Input.GetKeyDown(KeyCode.W))
        {
            SetMovementParameters(true, false);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            SetMovementParameters(false, false);
        }

        // Handle backward movement
        if (Input.GetKeyDown(KeyCode.S))
        {
            SetMovementParameters(false, true);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            SetMovementParameters(false, false);
        }
    }

    void SetMovementParameters(bool isMovingForward, bool isMovingBackward)
    {
        // Hydraulic animations
        hydraulicRightAnim.SetBool("Hydraulic-Close-right", isMovingForward || isMovingBackward);
        hydraulicRightAnim.SetBool("Hydraulic-Open-right", !(isMovingForward || isMovingBackward));

        hydraulicLeftAnim.SetBool("Hydraulic-Close-left", isMovingForward || isMovingBackward);
        hydraulicLeftAnim.SetBool("Hydraulic-Open-left", !(isMovingForward || isMovingBackward));

        // Wheel spin animations
        wheelSpinnAnim.SetBool("Spinn-Forward", isMovingForward);
        wheelSpinnAnim.SetBool("Spinn-Backward", isMovingBackward);
        wheelSpinnAnim.SetBool("Spinn-Stop", !(isMovingForward || isMovingBackward));

        // Move lean animations
        moveLeanAnim.SetBool("Lean-Forward", isMovingForward);
        moveLeanAnim.SetBool("Lean-Stop", !(isMovingForward || isMovingBackward));
    }
}
