using UnityEngine;

public class DustbinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        DraggableItem item = other.GetComponent<DraggableItem>();

        if (item != null)
        {
            item.Dispose();
            Debug.Log("Item disposed");
        }
    }
}