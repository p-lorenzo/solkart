using UnityEngine;

public class TimeUpper : PickUppableItem
{
    TimeManager m_TimeManager;
    
    private void Start()
    {
        m_TimeManager = FindObjectOfType<TimeManager>();
        DebugUtility.HandleErrorIfNullFindObject<TimeManager, TimeUpper>(m_TimeManager, this);
    }
    protected override void ItemPickup(Collider other)
    {
        base.ItemPickup(other);
        m_TimeManager.AddTime(10);
    }
}