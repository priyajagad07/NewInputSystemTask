using UnityEngine;
public class DragObject : MonoBehaviour
{
    private Rigidbody draggedRB;
    private Plane dragPlane;
    private Vector3 offset; 
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Dragger"))
                {
                    draggedRB = hit.collider.GetComponent<Rigidbody>();
                    if (draggedRB != null)
                    {
                        dragPlane = new Plane(Vector3.up, hit.point);
                   
                        offset = draggedRB.position - hit.point;

                        draggedRB.useGravity = false;
                        draggedRB.linearVelocity = Vector3.zero; 
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && draggedRB != null)
        {
            draggedRB.useGravity = true;
            draggedRB = null;
        }
    }

    void FixedUpdate()
    {
        if (draggedRB != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (dragPlane.Raycast(ray, out float enter))
            {
                Vector3 point = ray.GetPoint(enter);
                draggedRB.MovePosition(point + offset);
            }
        }
    }
}
