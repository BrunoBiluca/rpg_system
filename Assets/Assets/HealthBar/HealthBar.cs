using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    private Transform bar;

    private float barSize;

    private Color fullBar = new Color(82, 200, 33);
    private Color HalfBar = new Color(82, 200, 33);
    private Color LowBar = new Color(82, 200, 33);

    private void Awake() {
        bar = transform.Find("Bar");
    }

    public void SetFull() {
        SetSize(1f);
    }

    public void Subtract(float value) {
        SetSize(barSize - value);
    }

    public void SetSize(float size) {
        barSize = size;

        if(barSize < 0) barSize = 0;
        else if(barSize > 1f) barSize = 1f;

        InvokeRepeating("SlowBarReduction", 0f, 0.05f);

        // TODO: implementar as animações
    }

    public void SlowBarReduction() {
        var newBarSize = bar.localScale.x - 0.01f;
        if(newBarSize <= barSize) {
            bar.localScale = new Vector3(barSize, bar.localScale.y);
            CancelInvoke();
            return;
        }

        bar.localScale = new Vector3(newBarSize, bar.localScale.y);
    }

    public void SetColor(Color color) {
        bar.GetComponent<SpriteRenderer>();

    }

    private void Update() {
    }
}
