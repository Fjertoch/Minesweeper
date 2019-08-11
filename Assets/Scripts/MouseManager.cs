using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = Mathf.FloorToInt(clickPosition.x);
            int y = Mathf.FloorToInt(clickPosition.y);
            Tile clickedTile = MineFieldManager.Instance.field.GetTileAt(x, y);
            clickedTile.wasVisited = true;
        }
        if(Input.GetMouseButtonDown(1)) {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = Mathf.FloorToInt(clickPosition.x);
            int y = Mathf.FloorToInt(clickPosition.y);
            Tile clickedTile = MineFieldManager.Instance.field.GetTileAt(x, y);
            if(clickedTile.wasVisited == false) {
                clickedTile.isFlagged = !clickedTile.isFlagged;
            }
            else {
                Debug.Log("Clicked visited Tile");
            }
        }
    }
}
