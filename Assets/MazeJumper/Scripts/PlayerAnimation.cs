using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

  // Animation
  private Animator animComp;
  private float animState;
  private const string animSpeedString = "Speed";

    // Start is called before the first frame update
    void Start()
    {
        animComp = this.GetComponent<Animator>();
    }

    public void SetAnimationState(int newState)
    {
      animState = newState;
      animComp.SetFloat(animSpeedString, animState);
    }
}
