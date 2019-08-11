using System;
using UnityEngine;

public class Tile{
    private Action<Tile> cbTileChanged;
    private bool _wasVisited = false;
    private bool _flagged = false;

    public bool hasBomb;
    public bool wasVisited {
        get {
            return _wasVisited;
        }
        set {
            bool oldVisited = _wasVisited;
            _wasVisited = value;
            VisitNeighboringSafeTiles();
            if(cbTileChanged != null && _wasVisited != oldVisited) {
                cbTileChanged(this);
            }
        }
    }

    public bool isFlagged {
        get {
            return _flagged;
        }
        set {
            bool oldFlagged = _flagged;
            _flagged = value;
            
            if(cbTileChanged != null && _flagged != oldFlagged) {
                if(_flagged) {
                    mineField.numberOfFlags++;
                }
                else {
                    mineField.numberOfFlags--;
                }
                cbTileChanged(this);
            }
        }
    }

    public int numberOfAdjacentBombs {
        get {
            return CountAdjacentBombs();
        }
    }

    // Self-aware tiles
    public MineField mineField;
    public int x {
        get;
        protected set;
    }
    public int y {
        get;
        protected set;
    }

    public Tile(MineField mf, int x, int y, bool hasBomb) {
        this.x = x;
        this.y = y;
        this.hasBomb = hasBomb;
        mineField = mf;
    }

    private void VisitNeighboringSafeTiles() {
        if(this.numberOfAdjacentBombs > 0 || this.hasBomb) {
            // There is at least one bomb close to this Tile, so we don't visit any other neighbor, as it could be dangerous! Or we just detonated a bomb!
            return;
        }
        Tile[] neighbours = GetNeighbours();
        foreach(Tile t in neighbours) {
            if(t != null && t.wasVisited == false && t.hasBomb == false && t.isFlagged == false) {
                t.wasVisited = true;
            }
        }
    }

    private int CountAdjacentBombs( bool debug = false) {
        Tile[] neighbours = GetNeighbours();
        int neighbouringBombCount = 0;
        foreach(Tile t in neighbours) {
            if(debug) {
                Debug.Log("Tile " + x + ", " + y + "hasBomb: " + t.hasBomb);
            }
            if(t != null && t.hasBomb) {
                neighbouringBombCount++;
            }
        }
        if(debug) {
            Debug.Log("Checked Tile " + x + ", " + y + " has " + neighbouringBombCount + " adjacent bombs");
        }
        return neighbouringBombCount;
    }

    private Tile[] GetNeighbours() {
        Tile[] ns = new Tile[8];
        Tile n;

        n = mineField.GetTileAt(x, y + 1);
        ns[0] = n;  // Could be null, but that's okay.
        n = mineField.GetTileAt(x + 1, y);
        ns[1] = n;  // Could be null, but that's okay.
        n = mineField.GetTileAt(x, y - 1);
        ns[2] = n;  // Could be null, but that's okay.
        n = mineField.GetTileAt(x - 1, y);
        ns[3] = n;  // Could be null, but that's okay.
        n = mineField.GetTileAt(x + 1, y + 1);
        ns[4] = n;  // Could be null, but that's okay.
        n = mineField.GetTileAt(x + 1, y - 1);
        ns[5] = n;  // Could be null, but that's okay.
        n = mineField.GetTileAt(x - 1, y - 1);
        ns[6] = n;  // Could be null, but that's okay.
        n = mineField.GetTileAt(x - 1, y + 1);
        ns[7] = n;  // Could be null, but that's okay.

        return ns;
    }

    public void RegisterTileChangedCallback(Action<Tile> callback) {
        cbTileChanged += callback;
    }

    public void UnregisterTileTypeChangedCallback(Action<Tile> callback) {
        cbTileChanged -= callback;
    }

    //-----------------------------------------------------------------------------
    // DEBUG
    //-----------------------------------------------------------------------------
    public void DebugCountBombs() {
        CountAdjacentBombs(true);
    }
}
