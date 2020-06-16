using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour {

    [SerializeField] public GameObject playerPrefab;
        
    void Start() {
        SpawnCharacter(true);
        SpawnCharacter(false);
    }

    private void SpawnCharacter(bool isPlayerTeam) {
        if(isPlayerTeam) {
            Instantiate(playerPrefab, new Vector3(-1.5f, 0, 0), Quaternion.identity);
        } else {
            var gameObject = Instantiate(playerPrefab, new Vector3(1.5f, 0, 0), Quaternion.identity);
            var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = true;
        }
    }

    void Update() {

    }
}
