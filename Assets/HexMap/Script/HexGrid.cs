using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HexGrid {
    private int width;
    private int height;
    private float cellSize;
    private int [,] gridArray;
    private TextMesh[,] debugTextArray;

    public HexGrid(int width, int height, float cellSize) {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];

        Debug.Log("Creating a grid: width " + width + " height " + height);

        for (int x = 0; x < gridArray.GetLength(0); x++) {
            for (int y = 0; y < gridArray.GetLength(1); y++ ) {                
                debugTextArray[x,y] = writeText(null, gridArray[x, y].ToString(), getWorldPosition  (x, y) + new Vector3(cellSize, cellSize) * .5f, 
                    20, Color.white, TextAnchor.MiddleCenter, TextAlignment.Center);
                Debug.DrawLine(getWorldPosition(x, y), getWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(getWorldPosition(x, y), getWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(getWorldPosition(0, height), getWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(getWorldPosition(width, 0), getWorldPosition(width, height), Color.white, 100f);
    }

    private Vector3 getWorldPosition(int x, int y) {
        return new Vector3(x, y) * cellSize;
    }

    // TODO move to utils class
    public TextMesh writeText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, 
        TextAlignment textAlignment) {
        GameObject gameObject = new GameObject("World_text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;

        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        
        return textMesh;
    }

    private void setValue(int x, int y, int value) {
        if (x >= 0 && y >= 0 && x < width && y < height) {
            Debug.Log("You clicked in x: " + x + " y: " + y);
            gridArray[x, y] = value;
            debugTextArray[x, y].text = gridArray[x, y].ToString();
        }        
    }

    public void setValue(Vector3 worldPosition, int value) {
        int x, y;
        getXY(worldPosition, out x, out y);
        setValue(x, y, value);
    }

    public int getValue(Vector3 worldPosition) {
        int x, y;
        getXY(worldPosition, out x, out y);        
        return getValue(x, y);
    }

    private int getValue(int x, int y) {
        if (x >= 0 && y >= 0 && x < width && y < height) {
            Debug.Log("You clicked in x: " + x + " y: " + y);
            return gridArray[x, y];
        }
        else {
            Debug.Log("You clicked out of the grid");
            return 0;
        }
    }

    private Vector3 GetWorldPosition(int x, int y) {
        return new Vector3(x, y) * cellSize;
    }

    private void getXY(Vector3 worldPosition, out int x, out int y) {
        x = Mathf.FloorToInt(worldPosition.x / cellSize);
        y = Mathf.FloorToInt(worldPosition.y / cellSize);
    }
}
