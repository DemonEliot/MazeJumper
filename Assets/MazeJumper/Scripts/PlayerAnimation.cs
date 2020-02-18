using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

  // Animation
  private Animator animComp;
  private float animState;

    // Start is called before the first frame update
    void Start()
    {
        animComp = this.GetComponent<Animator>();
    }

    public void SetAnimationState(int newState)
    {
      animState = newState;
      animComp.SetFloat(Tags.SPEED, animState);
    }
}
