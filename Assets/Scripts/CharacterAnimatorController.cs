using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorController : MonoBehaviour {
    Animator animator;

    bool isVertical;

    private void Awake() {
        animator = GetComponent<Animator>();

        isVertical = true;
    }

    public void PlayWalkAnimation(Vector3 moviment) {

        if(Math.Abs(moviment.x) == 1 && Math.Abs(moviment.y) == 0) isVertical = false;
        else if(Math.Abs(moviment.y) == 1 && Math.Abs(moviment.x) == 0) isVertical = true;

        animator.SetBool("isVertical", isVertical);
        animator.SetInteger("dx", (int) Math.Round(moviment.x));
        animator.SetInteger("dy", (int) Math.Round(moviment.y));
    }
}
