using UnityEngine;

public class PickUppableItem : MonoBehaviour
{
    private bool collected = false;

    private async void OnTriggerEnter(Collider other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            //check if the player has TimeSlowingAbility then if it has, call RechargeTimeJuice()
            var timeSlowingAbility = other.GetComponentInParent<TimeSlowingAbility>();
            if (timeSlowingAbility != null)
            {
                Debug.Log("RechargeTimeJuice");
                timeSlowingAbility.RechargeTimeJuice();
            }
            
            // Distruggi l'oggetto "coin"
            Destroy(gameObject);

            collected = true;
        }
    }

    private void Update()
    {
        transform.Rotate(0, 0, 1);
    }
}