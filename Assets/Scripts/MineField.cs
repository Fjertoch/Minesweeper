using System;
using UnityEngine;

public class MineField
{
    private Action<Tile> cbTileChanged;
    private System.Random rand = new System.Random();

    public Tile[,] tiles;

    public int width {
        get;
        protected set;
    }
    public int height {
        get;
        protected set;
    }

    public MineField(int width, int height, int numberOfBombs) {
        CreateMineField(width, height, numberOfBombs);
    }

    public void CreateMineField(int width, int height, int numberOfBombs) {
        this.width = width;
        this.height = height;
        tiles = new Tile[width, height];

        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                bool hasBomb = GetHasBombs(ref numberOfBombs);
                tiles[x, y] = new Tile(this, x, y, hasBomb);
                tiles[x, y].RegisterTileChangedCallback(OnTileChanged);
            }
        }
    }

    private bool GetHasBombs(ref int numberOfBombs) {
        if(numberOfBombs > 0) {
            bool hasBomb = rand.Next(2) == 0;
            numberOfBombs--;
            return hasBomb;
        }
        return false;
    }

    private void OnTileChanged(Tile t) {
        //Debug.Log("Tile " + t.x + ", " + t.y + " changed.");
        cbTileChanged(t);
    }

    public Tile GetTileAt(int x, int y) {
        Tile t;
        try {
            t = tiles[x, y];
        }
        catch(IndexOutOfRangeException e) {
            t = null;
        }
        return t;
    }

    public void RegisterTileChanged(Action<Tile> callbackfunc) {
        cbTileChanged += callbackfunc;
    }

    public void UnregisterTileChanged(Action<Tile> callbackfunc) {
        cbTileChanged -= callbackfunc;
    }

}
