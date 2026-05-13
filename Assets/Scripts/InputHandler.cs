using UnityEngine;

public class InputHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Cell cell = hit.collider.GetComponent<Cell>();
                if(cell != null){
                    bool wasHit = cell.onClick();
                    Debug.Log("Cell clicked! Has ship: " + wasHit);
                }
            }
        }
    }
}
