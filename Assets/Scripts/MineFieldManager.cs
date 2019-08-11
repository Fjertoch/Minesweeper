using UnityEngine;
using TMPro;

public class MineFieldManager : MonoBehaviour{
    [SerializeField]
    private TextMeshProUGUI bombsRemainingText;
    [SerializeField]
    private int width = 20;
    [SerializeField]
    private int height = 20;
    [SerializeField]
    private float bombsMultiplier = 0.35f;

    public MineField field;

    void Start(){
        int numberOfBombs = Mathf.FloorToInt((width * height) * bombsMultiplier);
        Debug.Log("Number of bombs is: Floor(" + width + " * " + height + " * " + bombsMultiplier + ") = " + numberOfBombs);
        field = new MineField(width, height, numberOfBombs);
        field.RegisterTileChanged(OnTileChanged);
        UpdateRemainingBombsText();
    }

    private void UpdateRemainingBombsText() {
        int remainingBombs = field.GetRemainingBombsCount();
        bombsRemainingText.text = "Bombs remaining: " + remainingBombs;
    }

    public void OnTileChanged(Tile tileData) {
        UpdateRemainingBombsText();
    }

}
