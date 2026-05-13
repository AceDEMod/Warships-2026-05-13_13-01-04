using UnityEngine;

public class Grid : MonoBehaviour
{
    public Cell[,] cells = new Cell[10, 10];

    void Awake () {
        Cell[] allCells = GetComponentsInChildren<Cell>();
        foreach (Cell cell in allCells) {
            cells[cell.row, cell.col] = cell;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
