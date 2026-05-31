using UnityEngine;
using UnityEditor;

public class CellAutoAssign : EditorWindow
{
    //
    [MenuItem("Tools/Auto Assign Cell Rows and Cols")]
    static void Assign()
    {
        Cell[] allCells = FindObjectsOfType<Cell>();
        int assigned = 0;

        foreach (Cell cell in allCells)
        {
            Transform rowTransform = cell.transform.parent;
            Transform gridTransform = rowTransform.parent;

            if (rowTransform == null || gridTransform == null)
            {
                Debug.LogWarning("Cell " + cell.name + " does not have correct parent structure!");
                continue;
            }

            string rowName = rowTransform.name;
            string colName = cell.gameObject.name;

            int row = -1;
            int col = -1;

            if (rowName.StartsWith("Row"))
                int.TryParse(rowName.Substring(3), out row);

            if (colName.StartsWith("Cube"))
                int.TryParse(colName.Substring(4), out col);

            if (row == -1 || col == -1)
            {
                Debug.LogWarning("Could not parse row/col for: " + cell.name);
                continue;
            }

            cell.row = row - 1;
            cell.col = col - 1;

            EditorUtility.SetDirty(cell);
            assigned++;
        }

        foreach (Cell cell in allCells)
        {
            // Print every object that has Cell.cs
            Debug.Log("Cell found on: " + cell.gameObject.name + 
                    " | Parent: " + cell.transform.parent.name);
        }

        Debug.Log("Auto assigned " + assigned + " cells!");
    }


    // Scripts to make
    // Grid.cs = attach it to Plane1 and Plane2 so each grid has a proper 2D array reference to its 100 cells
    // GameManager.cs = manages turn order between player and bot
    // ShipPlacement.cs = lets player place ships before the game starts
    // BotController.cs = handles the bot's automatic attacks

}