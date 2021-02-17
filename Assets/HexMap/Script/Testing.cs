using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour {
    private HexGrid grid;
    private void Start() {
        grid = new HexGrid(5, 5, 5f);
    }

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) {
            grid.oneMore(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        if (Input.GetMouseButtonDown(1)) {
            Debug.Log(grid.getValue(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        }
                
    }
}
