using UnityEngine;

public class CharacterMovimentController : MonoBehaviour {

    private CharacterAnimatorController animatorController;

    private Rigidbody2D rigidbody2d;
    private Vector3 moviment;
    private bool dashMoviment;
    [SerializeField] public GameObject dashPrefab;

    [SerializeField]
    public LayerMask dashLayerMask;

    private void Awake() {
        animatorController = GetComponent<CharacterAnimatorController>();
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
        animatorController.PlayWalkAnimation(moviment);

        if(Input.GetKeyDown(KeyCode.RightControl))
            dashMoviment = true;
    }

    private void FixedUpdate() {
        rigidbody2d.velocity = moviment;

        if(dashMoviment) {
            var dashAmount = .3f;
            var dashPosition = transform.position + moviment * dashAmount;

            RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, moviment, dashAmount, dashLayerMask);
            if(raycastHit.collider != null)
                dashPosition = raycastHit.point;

            var dashTransform = Instantiate(dashPrefab, transform.position, Quaternion.identity);

            var targetDir = dashPosition - transform.position;
            var angle = Vector3.Angle(targetDir, transform.right);

            var direction = targetDir.y < 0 ? -1 : 1;

            dashTransform.transform.localEulerAngles = new Vector3(0, 0, direction * angle);
            Destroy(dashTransform, .4f);

            rigidbody2d.MovePosition(dashPosition);
            dashMoviment = false;
        }
    }

}
