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
    
    private const float MODIFIER_Y = 0.75f;
    public HexGrid(int width, int height, float cellSize) {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];

        Debug.Log("Creating a grid: width " + width + " height " + height);
        bool intersec;
        for (int x = 0; x < gridArray.GetLength(0); x++) {
            for (int y = 0; y < gridArray.GetLength(1); y++ ) {
                intersec = (y % 2 == 0) ? true : false;
                debugTextArray[x, y] = writeText(gridArray[x, y].ToString(), x, y, 20, Color.white, TextAnchor.MiddleCenter, TextAlignment.Center, intersec);
                drawHex(x, y, intersec);
                Debug.DrawLine(getWorldPosition(x, y), getWorldPosition(x, y + 1), Color.green, 100f);
                Debug.DrawLine(getWorldPosition(x, y), getWorldPosition(x + 1, y), Color.green, 100f);

            }
        }
    }

    private Vector3 getWorldPosition(float x, float y) {
        return new Vector3(x, y) * cellSize;
    }

    private void drawHex(int originalX, int y, bool intersec) {
        float coordY = y * MODIFIER_Y;
        float x = (intersec)
            ? originalX - 0.5f
            : originalX;        
        Debug.DrawLine(getWorldPosition(x, coordY + 0.25f), getWorldPosition(x + 0.5f, coordY), Color.white, 100f);
        Debug.DrawLine(getWorldPosition(x + 0.5f, coordY), getWorldPosition(x + 1, coordY + 0.25f), Color.white, 100f);
        Debug.DrawLine(getWorldPosition(x + 1, coordY + 0.25f), getWorldPosition(x + 1, coordY + 0.75f), Color.white, 100f);
        Debug.DrawLine(getWorldPosition(x + 1, coordY + 0.75f), getWorldPosition(x + 0.5f, coordY + 1), Color.white, 100f);
        Debug.DrawLine(getWorldPosition(x + 0.5f, coordY + 1), getWorldPosition(x, coordY + 0.75f), Color.white, 100f);
        Debug.DrawLine(getWorldPosition(x, coordY + 0.75f), getWorldPosition(x, coordY + 0.25f), Color.white, 100f);
    }

    // TODO move to utils class
    public TextMesh writeText(string text, int x, int y, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, bool intersec) {
        float coordY = y * MODIFIER_Y;
        float coordX = (intersec)
            ? x - 0.5f
            : x;
        return writeText(null, gridArray[x, y].ToString(), getWorldPosition(coordX, coordY) + new Vector3(cellSize, cellSize) * .5f,
                    20, Color.white, TextAnchor.MiddleCenter, TextAlignment.Center);
    }

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

    public void oneMore(Vector3 worldPosition) {
        int x, y;
        getXY(worldPosition, out x, out y);
        int value = getValue(worldPosition);
        setValue(x, y, ++value);
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
        Debug.Log("World position " + worldPosition.x + " " + worldPosition.y + " / " + cellSize);
        x = Mathf.FloorToInt(worldPosition.x / cellSize);
        y = Mathf.FloorToInt(worldPosition.y / cellSize);        
    }
}
