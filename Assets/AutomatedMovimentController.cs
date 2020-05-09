using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomatedMovimentController : MonoBehaviour {

    private Rigidbody2D rigidbody2d;

    private CharacterAnimatorController animatorController;

    private Vector3 moviment;

    private List<string> moves;

    private void Awake() {
        animatorController = GetComponent<CharacterAnimatorController>();
        rigidbody2d = GetComponent<Rigidbody2D>();

        InvokeRepeating("ChooseMoviment", 0f, 2f);
        moves = new List<string>();
    }

    void Update() {
        var mov = new Vector3(0, 0, 0);

        if(moves.Contains("up")) mov.y = 1;

        if(moves.Contains("down")) mov.y = -1;

        if(moves.Contains("left")) mov.x = -1;

        if(moves.Contains("right")) mov.x = 1;

        moviment = mov.normalized;
        animatorController.PlayWalkAnimation(moviment);
    }

    private void FixedUpdate() {
        rigidbody2d.velocity = moviment;
    }

    void ChooseMoviment() {
        var verticalMoves = new List<string>() { "up", "down", "empty" };
        var horizontalMoves = new List<string>() { "left", "right", "emtpy" };

        moves.Clear();
        moves.Add(verticalMoves[UnityEngine.Random.Range(0, verticalMoves.Count)]);
        moves.Add(horizontalMoves[UnityEngine.Random.Range(0, horizontalMoves.Count)]);
    }
}
