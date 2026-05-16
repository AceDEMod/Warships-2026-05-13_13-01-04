using UnityEngine;
using System.Collections.Generic;

public class Ship
{
    public int size;
    public int startingRow;
    public int startingCol;
    public bool isHorizontal; //horizontal=true, flase=vertical
    public int hits = 0;
    public bool isSunk = false;
    private Grid grid;
    private List<Cell> occupiedCells = new List<Cell>();

    public Ship(int shipSize, int row, int col, bool horizontal, Grid paramGrid) {
        size = shipSize;
        startingRow = row;
        startingCol = col;
        isHorizontal = horizontal;
        grid = paramGrid;
    }

}
