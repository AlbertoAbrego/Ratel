using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour {
    private HexGrid grid;
    private void Start() {
        grid = new HexGrid(5, 3, 5f);
    }

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) {
            grid.setValue(Camera.main.ScreenToWorldPoint(Input.mousePosition), 3);
        }

        if (Input.GetMouseButtonDown(1)) {
            Debug.Log(grid.getValue(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        }
                
    }
}
