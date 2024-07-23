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

    // Track the direction of the last movement
    private bool wasMovingForward;
    private bool wasMovingBackward;

    // Update is called once per frame
    void Update()
    {
        // Handle forward movement
        if (Input.GetKeyDown(KeyCode.W))
        {
            StopAllCoroutines(); // Stop any running coroutine
            SetMovementParameters(true, false, false, false);
            wasMovingForward = true;
            wasMovingBackward = false;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            StartCoroutine(ResetMovementParameters());
        }

        // Handle backward movement
        if (Input.GetKeyDown(KeyCode.S))
        {
            StopAllCoroutines(); // Stop any running coroutine
            SetMovementParameters(false, true, false, false);
            wasMovingForward = false;
            wasMovingBackward = true;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            StartCoroutine(ResetMovementParameters());
        }
    }

    IEnumerator ResetMovementParameters()
    {
        yield return new WaitForSeconds(0.1f); // Small delay to allow smooth transition

        // Handle stopping movement
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            if (wasMovingForward)
            {
                SetMovementParameters(false, false, true, false);
            }
            else if (wasMovingBackward)
            {
                SetMovementParameters(false, false, false, true);
            }
        }
    }

    void SetMovementParameters(bool isMovingForward, bool isMovingBackward, bool isStopping, bool isStoppingBackward)
    {
        // Hydraulic animations
        hydraulicRightAnim.SetBool("Hydraulic-Close-right", isMovingForward || isMovingBackward);
        hydraulicRightAnim.SetBool("Hydraulic-Open-right", !(isMovingForward || isMovingBackward));

        hydraulicLeftAnim.SetBool("Hydraulic-Close-left", isMovingForward || isMovingBackward);
        hydraulicLeftAnim.SetBool("Hydraulic-Open-left", !(isMovingForward || isMovingBackward));

        // Wheel spin animations
        wheelSpinnAnim.SetBool("Spinn-Forward", isMovingForward);
        wheelSpinnAnim.SetBool("Spinn-Backward", isMovingBackward);
        wheelSpinnAnim.SetBool("Spinn-Stop", isStopping);
        wheelSpinnAnim.SetBool("Spinn-Stop-back", isStoppingBackward);

        // Move lean animations
        moveLeanAnim.SetBool("Lean-Forward", isMovingForward);
        moveLeanAnim.SetBool("Lean-Stop", isStopping || isStoppingBackward);
    }
}
