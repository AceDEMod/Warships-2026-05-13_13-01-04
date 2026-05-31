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
    public List<Cell> occupiedCells = new List<Cell>();

    public Ship(int shipSize, int row, int col, bool horizontal, Grid paramGrid) {
        size = shipSize;
        startingRow = row;
        startingCol = col;
        isHorizontal = horizontal;
        grid = paramGrid;
    }

    //returns true if the ship is sunk, false otherwise
    public bool isShipSunk() { return isSunk; }
    // returns the number of hits before ship sunk
    public int getHealth() { return size - hits; }

    // Takes one hit and checks if ship sunk
    public void takeDamage() {
            hits++;
            if (hits >= size)
            {
                isSunk = true;
                Debug.Log("Ship sunk!");
            }
        }
    //visual update for ship placement
    public void markOccupiedCells() {
        foreach (Cell cell in occupiedCells) {  
            cell.PlaceShip();
        }
    }

    // Calculates occupied cells based on starting position, orientation, and size of the ship in constructor
    public void calculateOccupiedCells(){
        Debug.Log("Calculating occupied cells for ship size " + size);
        occupiedCells.Clear();
        for (int i = 0; i < size; i++)
        {
            int row = isHorizontal ? startingRow : startingRow + i;
            int col = isHorizontal ? startingCol + i : startingCol;
            //debugs

            if (grid == null)
            {
                Debug.Log("ERROR: Grid is null!");
                return;
            }
            if (grid.cells == null)
            {
                Debug.Log("ERROR: Grid.cells is null!");
                return;
            }

            Cell cell = grid.cells[row, col];
            if (cell == null)
            {
                Debug.Log("ERROR: Cell at (" + row + ", " + col + ") is null!");
                return;
            }

            //debugs

            occupiedCells.Add(cell);
        }
        Debug.Log("Ship size " + size + " has " + occupiedCells.Count + " occupied cells");
    }

    // Checks if a specific cell is occupied by this ship, used for hit detection
    public bool isCellOccupied(int row, int col) {
        Debug.Log("Checking if cell (" + row + ", " + col + "), occupied cells count: " + occupiedCells.Count);
        foreach (Cell cell in occupiedCells) {
            if (cell.row == row && cell.col == col) {
                return true;
            }
        }
        return false;
    }

    //maybe combine isCellOccupied() with takeDamage() and calculateOccupiedCells() methods?????

}