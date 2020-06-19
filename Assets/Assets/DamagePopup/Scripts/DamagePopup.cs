using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour {

    public static DamagePopup Create(string text, Vector3 position) {
        var go = Instantiate(GameAssetsManager.i.damagePopup, new Vector3(position.x, position.y, -1), Quaternion.identity);
        var damagePopup = go.GetComponent<DamagePopup>();
        damagePopup.Setup(text, new Color32(255, 255, 255, 255));
        Debug.Log("Create");
        return damagePopup;
    }

    public static DamagePopup CreateCritical(string text, Vector3 position) {
        var go = Instantiate(GameAssetsManager.i.damagePopup, new Vector3(position.x, position.y, -1), Quaternion.identity);
        var damagePopup = go.GetComponent<DamagePopup>();
        damagePopup.Setup(text, new Color32(171, 11, 11, 255));
        Debug.Log("Create Critical");
        return damagePopup;
    }

    protected TextMeshPro textMesh;
    protected float fadeSpeed;
    protected Vector2 movimentSpeed;

    private void Awake() {
        textMesh = GetComponent<TextMeshPro>();
    }

    private void Setup(string text, Color color) {
        textMesh.text = text;
        textMesh.faceColor = color;
        fadeSpeed = 1f;
        movimentSpeed = new Vector2(1, .5f);
    }

    void Update() {
        var position = transform.position;
        position.x += movimentSpeed.x * Time.deltaTime;
        position.y += movimentSpeed.y * Time.deltaTime;
        transform.position = position;

        var color = textMesh.color;
        color.a -= fadeSpeed * Time.deltaTime;
        textMesh.color = color;

        if(textMesh.color.a <= 0) {
            Destroy(gameObject);
        }
    }
}
