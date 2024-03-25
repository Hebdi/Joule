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

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            hydraulicRightAnim.SetTrigger("Hydraulic-Close-right");
            hydraulicLeftAnim.SetTrigger("Hydraulic-Close-left");
            wheelSpinnAnim.SetTrigger("Spinn-Forward");
            moveLeanAnim.SetTrigger("Lean-Forward");
        }


        if (Input.GetKeyUp(KeyCode.W))
        {
            hydraulicRightAnim.SetTrigger("Hydraulic-Open-right");
            hydraulicLeftAnim.SetTrigger("Hydraulic-Open-left");
            wheelSpinnAnim.SetTrigger("Spinn-Stop");
            moveLeanAnim.SetTrigger("Lean-Stop");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            wheelSpinnAnim.SetTrigger("Spinn-Backward");
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            wheelSpinnAnim.SetTrigger("Spinn-Stop-back");
        }
    }
}
