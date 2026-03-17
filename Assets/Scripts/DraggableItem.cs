using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    //prepares objects for dragging
    public void StartDrag()
    {
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    //moves the object
    public void DragItem(Vector3 position)
    {
        rb.MovePosition(position);
    }

    //enable physics again
    public void EndDrag()
    {
        rb.useGravity = true;
    }

    //remove item in dustbin
    public void Dispose()
    {
        Destroy(gameObject, 0.1f);
    }
}
