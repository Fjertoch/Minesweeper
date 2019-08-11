using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileSpriteManager : MonoBehaviour
{
    [SerializeField]
    private GameObject tilePrefab;

    private Dictionary<Tile, GameObject> tileGameObjectMap;
    
    void Start(){
        CreateFieldGameObjects();
    }

    void CreateFieldGameObjects() {
        Tile[,] tiles = MineFieldManager.Instance.field.tiles;
        tileGameObjectMap = new Dictionary<Tile, GameObject>();
        for(int x = 0; x < MineFieldManager.Instance.field.width; x++) {
            for(int y = 0; y < MineFieldManager.Instance.field.height; y++) {
                Tile tileData = tiles[x, y];
                GameObject tileGO = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity, this.transform);
                tileGO.name = "Tile_" + x + "_" + y;
                tileGameObjectMap[tileData] = tileGO;
                //tileGO.layer = LayerMask.NameToLayer("Tile");
                OnTileChanged(tileData);
            }
        }
        MineFieldManager.Instance.field.RegisterTileChanged(OnTileChanged);
    }

    void OnTileChanged(Tile tileData) {
        if(tileGameObjectMap.ContainsKey(tileData) == false) {
            Debug.LogError("tileGameObjectMap doesn't contain the tileData, did you forget to add the tile to the dictionary? Or maybe forget to unregister a callback?");
            return;
        }

        GameObject tileGO = tileGameObjectMap[tileData];

        if(tileGO == null) {
            Debug.LogError("tileGameObjectMap's returned GameObject is null, did you forget to add the tile to the dictionary? Or maybe forget to unregister a callback?");
            return;
        }
        TextMeshPro tmp = tileGO.transform.GetComponentInChildren<TextMeshPro>(true);
        GameObject bomb = tileGO.transform.Find("Bomb").gameObject;
        GameObject flag = tileGO.transform.Find("Flag").gameObject;
        if(tileData.wasVisited) {
            if(tileData.hasBomb) {
                // Tile shouldn't change its content but to be sure we disable the other gfx
                tmp.gameObject.SetActive(false);
                bomb.SetActive(true);
                flag.SetActive(false);
            }
            else {
                tmp.text = tileData.numberOfAdjacentBombs.ToString();
                // Tile shouldn't change its content but to be sure we disable the other gfx
                bomb.SetActive(false);
                tmp.gameObject.SetActive(true);
                flag.SetActive(false);
            }
        }
        else if(tileData.isFlagged) {
            tmp.gameObject.SetActive(false);
            bomb.SetActive(false);
            flag.SetActive(true);
        }
        else {
            tmp.gameObject.SetActive(false);
            bomb.SetActive(false);
            flag.SetActive(false);
        }
    }

}
