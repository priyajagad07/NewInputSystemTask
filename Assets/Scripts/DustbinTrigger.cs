using UnityEngine;
using UnityEngine.InputSystem;

public class DustbinTrigger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        DraggableItem item = other.GetComponent<DraggableItem>();

        if(item != null && Mouse.current.leftButton.wasReleasedThisFrame)
        {
            GameManager gm = GameManager.Instance;
            gm.AddScore();

            item.Dispose();
            Debug.Log("Item disposed");

            ItemSpawner.instance.SpawnItem();
        }
    }
}