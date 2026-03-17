using UnityEngine;

public class DragObject : MonoBehaviour
{
    private InputManager inputManager;

    private DraggableItem currentItem;
    private Plane dragPlane;
    private Vector3 offset;

    public float dragSpeed = 5f;

    void Start()
    {
        inputManager = InputManager.Instance;

        inputManager.onMousePress.AddListener(PickItem);
        inputManager.onMouseRelease.AddListener(ReleaseItem);
    }

    void PickItem()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            currentItem = hit.collider.GetComponent<DraggableItem>();

            if (currentItem != null)
            {
                Debug.Log("Picked item");
                dragPlane = new Plane(Vector3.up, hit.point);
                offset = hit.collider.transform.position - hit.point;

                currentItem.StartDrag();
            }
        }
    }

    void ReleaseItem()
    {
        if (currentItem != null)
        {
            currentItem.EndDrag();
            currentItem = null;
        }
    }

    void Update()
    {
        if (currentItem != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (dragPlane.Raycast(ray, out float enter))
            {
                Vector3 point = ray.GetPoint(enter);

                Vector3 targetPos = point + offset;

                Vector3 smoothPos = Vector3.Lerp(
                    currentItem.transform.position,
                    targetPos,
                    dragSpeed * Time.deltaTime
                );

                currentItem.DragItem(smoothPos);
            }
        }
    }
}
