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
        calculateOccupiedCells();
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
        occupiedCells.Clear();
        for (int i = 0; i < size; i++)
        {
            int row = isHorizontal ? startingRow : startingRow + i;
            int col = isHorizontal ? startingCol + i : startingCol;
            Cell cell = grid.cells[row, col];
            occupiedCells.Add(cell);
        }
        markOccupiedCells();
    }

    // Checks if a specific cell is occupied by this ship, used for hit detection
    public bool isCellOccupied(int row, int col) {
        foreach (Cell cell in occupiedCells) {
            if (cell.row == row && cell.col == col) {
                return true;
            }
        }
        return false;
    }

    //maybe combine isCellOccupied() with takeDamage() and calculateOccupiedCells() methods?????

}