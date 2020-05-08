using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovimentController : MonoBehaviour {
    private Rigidbody2D rigidbody2d;
    private Vector3 moviment;
    private bool dashMoviment;

    [SerializeField]
    private LayerMask dashLayerMask;

    private void Awake() {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update() {
        // TODO: implementar movimentação aleatória, será necessário implementar sistema de comandos
        var mov = new Vector3(0, 0, 0);

        if(Input.GetKey(KeyCode.UpArrow))
            mov.y = 1;

        if(Input.GetKey(KeyCode.DownArrow))
            mov.y = -1;

        if(Input.GetKey(KeyCode.LeftArrow))
            mov.x = -1;

        if(Input.GetKey(KeyCode.RightArrow))
            mov.x = 1;

        moviment = mov.normalized;
        // TODO: implementar a animação de movimento do character

        if(Input.GetKeyDown(KeyCode.Space))
            dashMoviment = true;
    }

    private void FixedUpdate() {
        rigidbody2d.velocity = moviment;

        if(dashMoviment) {
            var dashAmount = 1.5f;
            var dashPosition = transform.position + moviment * dashAmount;

            RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, moviment, dashAmount, dashLayerMask);
            if(raycastHit.collider != null)
                dashPosition = raycastHit.point;

            rigidbody2d.MovePosition(dashPosition);
            dashMoviment = false;
        }
    }
}
