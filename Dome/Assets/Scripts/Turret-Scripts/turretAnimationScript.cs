using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretAnimationScript : MonoBehaviour
{
    public Animator turretBuildAnim;

    private Animator mAnimator;

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            turretBuildAnim.SetTrigger("TurretBuildAnim");
        }
    }
}
