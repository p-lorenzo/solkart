using UnityEngine;

public class TimeJuice : PickUppableItem
{
    protected override void ItemPickup(Collider other)
    {
        base.ItemPickup(other);
        //check if the player has TimeSlowingAbility then if it has, call RechargeTimeJuice()
        var timeSlowingAbility = other.GetComponentInParent<TimeSlowingAbility>();
        if (timeSlowingAbility == null) return;
        Debug.Log("RechargeTimeJuice");
        timeSlowingAbility.RechargeTimeJuice();
    }
}