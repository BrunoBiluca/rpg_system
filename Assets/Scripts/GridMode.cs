using UnityEditor.UI;
using UnityEngine;

public class GridMode {
    private int width;
    private int height;
    private int cellSize;

    private int[,] gridArray;

    public GridMode(int width, int height) {
        this.width = width;
        this.height = height;

        gridArray = new int[width, height];

        cellSize = 4;

        for(int x = 0; x < gridArray.GetLength(0); x++) {
            for(int y = 0; y < gridArray.GetLength(1); y++) {
                CreateWordText(gridArray[x, y].ToString(), GetWorldPosition(x, y));
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }

    private Vector3 GetWorldPosition(int x, int y) {
        return new Vector3(x, y) * cellSize;
    }

    public TextMesh CreateWordText(string text, Vector3 localPosition, Transform parent = null) {
        var gameObject = new GameObject("World_Text", typeof(TextMesh));

        var transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition + new Vector3(cellSize/2, cellSize/2);


        var textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.text = text;
        textMesh.color = Color.white;
        textMesh.fontSize = 16;
        textMesh.anchor = TextAnchor.MiddleCenter;

        return textMesh;
    }
}
