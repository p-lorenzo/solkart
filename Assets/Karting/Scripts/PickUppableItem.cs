using UnityEngine;

public class PickUppableItem : MonoBehaviour
{
    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;

        if (!other.CompareTag("Player")) return;
        ItemPickup(other);

        // Distruggi l'oggetto "coin"
        Destroy(gameObject);

        collected = true;
    }

    protected virtual void ItemPickup(Collider other)
    {
        // Implementa la logica di raccolta dell'oggetto
    }

    private void Update()
    {
        transform.Rotate(0, 0, 1);
    }
}