using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameManager gameManager;
    public Grid botGrid;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Cell cell = hit.collider.GetComponent<Cell>();
                if (cell != null && botGrid.cells[cell.row, cell.col] == cell)
                {
                    if (gameManager.isPlayerTurn && !gameManager.gameOver)
                    {
                        gameManager.playerAttack(cell.row, cell.col);
                        Debug.Log("Cell clicked! ");
                    }
                }
            }
        }
    }
}
