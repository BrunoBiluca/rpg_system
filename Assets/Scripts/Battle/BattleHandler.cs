using System;
using UnityEngine;

public class BattleHandler : MonoBehaviour {

    [SerializeField] public GameObject playerPrefab;
    [SerializeField] public GameObject pfSelectedCharacterCircle;

    private CharacterBattleHanlder player;
    private CharacterBattleHanlder enemy;

    private CharacterBattleHanlder selectedCharacter;
    private GameObject selectedCircle;

    public enum CharacterState {
        Idle,
        Busy,
        Sliding
    }

    void Start() {
        player = SpawnCharacter(true);
        enemy = SpawnCharacter(false);

        SetSelectedCharacter(player);
    }

    private void SetSelectedCharacter(CharacterBattleHanlder character) {
        Destroy(selectedCircle);
        if(character == player) {
            selectedCharacter = player;
            selectedCircle = Instantiate(pfSelectedCharacterCircle, player.transform.position, Quaternion.identity);
        }
        else if(character == enemy) {
            selectedCharacter = enemy;
            selectedCircle = Instantiate(pfSelectedCharacterCircle, enemy.transform.position, Quaternion.identity);

            var distance = Vector3.Distance(enemy.transform.position, player.transform.position);
            var slideTargetPosition = new Vector3(enemy.transform.position.x - distance, enemy.transform.position.y);
            enemy.SlideToPosition(slideTargetPosition, () => {
                enemy.Attack(enemy, () => {
                    var damage = UnityEngine.Random.Range(10, 40);
                    player.Damage(damage);
                    if(enemy.IsDead())
                        FightOver("Enemy");

                    enemy.SlideToPosition(new Vector3(1.5f, 0, 0), () => {
                        enemy.state = CharacterState.Idle;
                        SetSelectedCharacter(player);
                    });
                });
            });
        }
    }

    private CharacterBattleHanlder SpawnCharacter(bool isPlayerTeam) {
        if(isPlayerTeam) {
            return Instantiate(playerPrefab, new Vector3(-1.5f, 0, 0), Quaternion.identity)
                .GetComponent<CharacterBattleHanlder>();
        } else {
            var obj = Instantiate(playerPrefab, new Vector3(1.5f, 0, 0), Quaternion.identity);
            var spriteRenderer = obj.GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = true;

            return obj.GetComponent<CharacterBattleHanlder>();
        }
    }



    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            if(selectedCharacter != player) return;
            if(player.state != CharacterState.Idle) return;
            
            player.state = CharacterState.Sliding;

            var distance = Vector3.Distance(player.transform.position, enemy.transform.position);
            var slideTargetPosition = new Vector3(player.transform.position.x + distance, player.transform.position.y);
            player.SlideToPosition(slideTargetPosition, () => {
                player.Attack(enemy, () => {
                    var damage = UnityEngine.Random.Range(10, 40);
                    enemy.Damage(damage);

                    if(enemy.IsDead())
                        FightOver("Player");

                    player.SlideToPosition(new Vector3(-1.6f, 0, 0), () => {
                        player.state = CharacterState.Idle;
                        SetSelectedCharacter(enemy);
                    });
                });
            });
        }
    }

    private void FightOver(string winner) {
        Debug.Log(winner);
    }
}
