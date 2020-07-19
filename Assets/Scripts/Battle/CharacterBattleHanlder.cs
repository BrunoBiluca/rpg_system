using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BattleHandler;

public class CharacterBattleHanlder : MonoBehaviour {

    private HealthBar healthBar;
    private Animator animator;

    public CharacterState state;

    private Vector3 slideTargetPosition;
    private Action onAttackComplete;
    private Action onSlideComplete;

    private void Start() {
        animator = GetComponent<Animator>();

        healthBar = Instantiate(
                GameAssetsManager.i.healthBar, 
                transform.position + new Vector3(0, 0.25f, 0), 
                Quaternion.identity,
                transform
            ).GetComponent<HealthBar>();
    }

    private float attackTime;
    private void Update() {
        switch(state) {
            case CharacterState.Idle:
                break;
            case CharacterState.Busy:
                if(attackTime == .5f)
                    animator.SetTrigger("attack");

                attackTime -= Time.deltaTime;
                if(attackTime <= 0)
                    onAttackComplete();
                break;
            case CharacterState.Sliding:
                animator.SetBool("sliding", true);
                var slidingSpeed = 2.5f;
                transform.position += (slideTargetPosition - transform.position) * slidingSpeed * Time.deltaTime;

                var distance = Vector3.Distance(transform.position, slideTargetPosition);
                if(distance <= 0.1) {
                    animator.SetBool("sliding", false);
                    onSlideComplete();
                }
                break;
        }
    }
 
    public void Attack(CharacterBattleHanlder target, Action onAttackComplete) {
        state = CharacterState.Busy;
        attackTime = .5f;
        this.onAttackComplete = onAttackComplete;
    }

    public void SlideToPosition(Vector3 targetPosition, Action onSlideComplete) {
        state = CharacterState.Sliding;
        slideTargetPosition = targetPosition;
        this.onSlideComplete = onSlideComplete;
    }

    internal void Damage(int amoutDamage) {
        animator.SetTrigger("damage");
        if(amoutDamage > 30)
            DamagePopup.CreateCritical($"{amoutDamage}", transform.position);
        else
            DamagePopup.Create($"{amoutDamage}", transform.position);

        healthBar.Subtract(amoutDamage / 100.0f);
    }

    internal bool IsDead() {
        return healthBar.GetSize() <= 0;
    }
}
