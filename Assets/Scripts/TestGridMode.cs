using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGridMode : MonoBehaviour {

    GridMode grid;

    List<Vector3> mouseInputs;

    void Start() {
        grid = new GridMode(5, 5);

        mouseInputs = new List<Vector3>();
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            var mouse = Input.mousePosition;
            var worldPosition = Camera.main.ScreenToWorldPoint(mouse);
            grid.SetNodeValue(worldPosition, "32");

            mouseInputs.Add(worldPosition);
            if(mouseInputs.Count >= 2) {
                var canDraw = grid.DrawLines(mouseInputs.ToArray());

                if(!canDraw) {
                    mouseInputs.Clear();
                }
            }
                
            foreach(var input in mouseInputs) {
                Debug.Log(input);
            }
        }
    }
}
