using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    private Transform bar;
    private SpriteRenderer barSprite;

    private float barSize;

    private Color fullBarColor = new Color32(82, 200, 33, 255);
    private Color middleBarColor = new Color32(200, 111, 33, 255);
    private Color lowBarColor = new Color32(200, 33, 40, 255);

    private void Awake() {
        bar = transform.Find("Bar");
        barSprite = bar.Find("BarSprite").GetComponent<SpriteRenderer>();
        barSprite.color = fullBarColor;
        SetFull();
    }

    public float GetSize() {
        return barSize;
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

            if(barSize == 1)
                barSprite.color = fullBarColor;
            else if(barSize > .1f && barSize <= .4f)
                barSprite.color = middleBarColor;
            else if(barSize <= .1f)
                barSprite.color = lowBarColor;

            CancelInvoke();
            return;
        }

        bar.localScale = new Vector3(newBarSize, bar.localScale.y);
    }
}
