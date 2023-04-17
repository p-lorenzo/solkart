using System;
using UnityEngine;
using TMPro;

public class TimerHUDManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI elapsedTimeText;
    TimeManager m_TimeManager;
    
    private void Start()
    {
        m_TimeManager = FindObjectOfType<TimeManager>();
        DebugUtility.HandleErrorIfNullFindObject<TimeManager, ObjectiveReachTargets>(m_TimeManager, this);


        if (m_TimeManager.IsFinite)
        {
            timerText.text = "";
        }
    }
    
    void Update()
    {
        if (m_TimeManager.IsFinite)
        {   
            elapsedTimeText.gameObject.SetActive(true);
            timerText.gameObject.SetActive(true);
            int elapsedTime = (int) Math.Ceiling(m_TimeManager.TotalElapsedTime);
            int timeRemaining = (int) Math.Ceiling(m_TimeManager.TimeRemaining);
            elapsedTimeText.text = string.Format("{0}:{1:00}", elapsedTime / 60, elapsedTime % 60);
            timerText.text = string.Format("{0}:{1:00}", timeRemaining / 60, timeRemaining % 60);
        }
        else
        {
            timerText.gameObject.SetActive(false);
        }
    }
}
