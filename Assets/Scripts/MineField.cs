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
    public int numberOfBombs {
        get;
        protected set;
    }

    public int numberOfFlags = 0;

    public MineField(int width, int height, int numberOfBombs) {
        CreateMineField(width, height, numberOfBombs);
    }

    public void CreateMineField(int width, int height, int numberOfBombs) {
        this.width = width;
        this.height = height;
        this.numberOfBombs = numberOfBombs;
        tiles = new Tile[width, height];

        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                tiles[x, y] = new Tile(this, x, y, false);
                tiles[x, y].RegisterTileChangedCallback(OnTileChanged);
            }
        }
        PlaceBombs(numberOfBombs);
    }

    public void PlaceBombs(int numberOfBombs) {
        int bombCounter = numberOfBombs;
        System.Random rand = new System.Random();
        while(bombCounter > 0) {
            int randomNumber = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1.0000001f) * (width * height - 1));
            int x = randomNumber % width;
            int y = Mathf.FloorToInt(randomNumber / width);
            PlaceBombAt(x, y);
            bombCounter--;
        }
    }

    private void PlaceBombAt(Tile t) {
        t.hasBomb = true;
    }

    private void PlaceBombAt(int x, int y) {
        Tile t = tiles[x, y];
        PlaceBombAt(t);
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

    public int GetRemainingBombsCount() {
        return numberOfBombs - numberOfFlags;
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

    public void MakeAllVisible() {
        foreach(Tile t in tiles) {
            t.wasVisited = true;
        }
    }

    public void RegisterTileChanged(Action<Tile> callbackfunc) {
        cbTileChanged += callbackfunc;
    }

    public void UnregisterTileChanged(Action<Tile> callbackfunc) {
        cbTileChanged -= callbackfunc;
    }

}
