using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{

  // Animation
  private Animator animator;
  private float animationState;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    public void SetAnimationState(int newState)
    {
      animationState = newState;
      animator.SetFloat(Tags.SPEED, animationState);
    }
}
