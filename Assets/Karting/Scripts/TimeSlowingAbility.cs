using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlowingAbility : MonoBehaviour
{
    [SerializeField]
    private float slowMotionScale = 0.1f;

    [SerializeField] 
    private float transitionTime = 3f;
    
    [SerializeField] 
    private Slider slider;
    
    private float remainingTimeJuice = 1f;

    [SerializeField] 
    private float timeJuiceDepletionRate = 0.2f;
    
    private bool isSlowing = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = remainingTimeJuice;
        if (remainingTimeJuice <= 0)
        {
            isSlowing = false;
        }
        if (isSlowing)
        {
            remainingTimeJuice -= Time.unscaledDeltaTime * timeJuiceDepletionRate;
        }
        if (Input.GetKeyDown(KeyCode.Space) && remainingTimeJuice > 0)
        {
            isSlowing = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isSlowing = false;
        }

        float targetTimeScale = isSlowing ? slowMotionScale : 1f;
        Time.timeScale = Mathf.Lerp(Time.timeScale, targetTimeScale, transitionTime * Time.unscaledDeltaTime);
    }
}
