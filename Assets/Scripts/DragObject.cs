using UnityEngine;
public class DragObject : MonoBehaviour
{
    private DraggableItem currentItem;
    private Plane dragPlane;
    private Vector3 offset;
    public float dragSpeed = 5f;

    void Update()
    {
        //pick item
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                currentItem = hit.collider.GetComponent<DraggableItem>();

                if (currentItem != null)
                {
                    dragPlane = new Plane(Vector3.up, hit.point);
                    offset = hit.collider.transform.position - hit.point;

                    currentItem.StartDrag();
                }
            }
        }

        //release item
        if (Input.GetMouseButtonUp(0) && currentItem != null)
        {
            currentItem.EndDrag();
            currentItem = null;
        }
    }

    void FixedUpdate()
    {
        if (currentItem != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (dragPlane.Raycast(ray, out float enter))
            {
                Vector3 point = ray.GetPoint(enter);

                Vector3 targetPos = point + offset;
                Vector3 smoothPos = Vector3.Lerp(currentItem.transform.position, targetPos, dragSpeed * Time.deltaTime);

                currentItem.DragItem(smoothPos);
            }
        }
    }
}