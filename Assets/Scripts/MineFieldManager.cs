using UnityEngine;

public class MineFieldManager : MonoBehaviour{
    [SerializeField]
    private int width = 20;
    [SerializeField]
    private int height = 20;
    [SerializeField]
    private float bombsMultiplier = 0.35f;

    private int numberOfBombs;

    public static MineFieldManager Instance {
        get;
        protected set;
    }
    public MineField field;

    void Start()
    {
        if(Instance != null) {
            Debug.LogError("There is already one MineFieldManager!");
        }
        Instance = this;
        numberOfBombs = Mathf.FloorToInt((20 * 20) * bombsMultiplier);
        field = new MineField(width, height, numberOfBombs);
    }
}
