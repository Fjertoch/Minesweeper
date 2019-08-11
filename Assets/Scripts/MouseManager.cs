using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    private MineSweeperManager msm;
    private MineFieldManager mfm;

    void Start() {
        msm = FindObjectOfType<MineSweeperManager>();
        mfm = FindObjectOfType<MineFieldManager>();
    }

    void Update() {
        if(msm.gameOver == false) {
            if(Input.GetMouseButtonDown(0)) {
                Tile clickedTile = GetTileUnderMouse();
                clickedTile.wasVisited = true;
                if(clickedTile.hasBomb) {
                    msm.GameOver();
                }
            }
            if(Input.GetMouseButtonDown(1)) {
                Tile clickedTile = GetTileUnderMouse();
                if(clickedTile.wasVisited == false) {
                    clickedTile.isFlagged = !clickedTile.isFlagged;
                }
            }
            if(Input.GetMouseButtonDown(2)) {
                Tile clickedTile = GetTileUnderMouse();
                clickedTile.DebugCountBombs();
            }
            if(Input.GetKeyDown(KeyCode.D)) {
                mfm.field.MakeAllVisible();
            }
        }
        else {
            //Debug.Log("Game Over");
        }
    }

    private Tile GetTileUnderMouse() {
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int x = Mathf.FloorToInt(clickPosition.x);
        int y = Mathf.FloorToInt(clickPosition.y);
        Tile tileUnderMouse = mfm.field.GetTileAt(x, y);
        return tileUnderMouse;
    }
}
