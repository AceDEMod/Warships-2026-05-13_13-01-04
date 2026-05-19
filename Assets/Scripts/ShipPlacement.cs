using UnityEngine;

public class ShipPlacement
{
    int[] shipSizes = new int[] { 5, 4, 3, 3, 2 };

    public void placeShipsRandom(Grid grid, Fleet fleet)
    {
        foreach (int size in shipSizes)
        {
            bool placed = false;
            while (!placed)
            {
                int randRow = Random.Range(0, 10);
                int randCol = Random.Range(0, 10);
                bool isHorizontal = Random.value > 0.5f;
                if (canPlaceShip(grid, fleet, randRow, randCol, size, isHorizontal))
                {
                    Ship newShip = new Ship(size, randRow, randCol, isHorizontal, grid);
                    newShip.calculateOccupiedCells();
                    fleet.addShip(newShip);
                    placed = true;
                }
            }
        }
    }

    public bool canPlaceShip(Grid grid, Fleet fleet, int row, int col, int size, bool isHorizontal)
    {
        if (isHorizontal)  {
            if (col + size > 10) {return false;}
        }
        else {
            if (row + size > 10) { return false;}
        }

            for (int i = 0; i < size; i++) {
                int checkRow = isHorizontal ? row : row + i;
                int checkCol = isHorizontal ? col + i : col;
                if (checkRow >= 10 || checkCol >= 10) { return false;}
                if (fleet.getShip(checkRow, checkCol) != null) { return false;}
        }
        return true;
    }
}
