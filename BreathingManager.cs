using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BreathingManager : MonoBehaviour
{

    // Hold space for 3 seconds - bar fill = 1.0
    // Release space for 3 seconds - bar fill = 0.0

    // Abnormal Score: starts increasing after 3 seconds

    public GameManager gameManager;
    public Slider bar;
    public float barFill = 0.0f;
    public float breathTime = 3.0f;   // Same for breath in and out
    public bool breathingIn = true;
    public float secondsEnabled = 0.0f;
    public float doubtLevel = 0.0f;
    public bool isTutorial = true;
    private float fillSpeed;
    private float fixedDeltaTime;
    private bool firstTime = true;

    

    // Start is called before the first frame update
    void Start()
    {
        secondsEnabled = 0f;    
        bar.value = 0f;
        fixedDeltaTime = Time.fixedDeltaTime;
        fillSpeed =  fixedDeltaTime / breathTime;
        firstTime = true;
    }

    private void FixedUpdate()
    {
        barFill = 1.181f/(1 + Mathf.Pow(2.71828f, -(secondsEnabled / breathTime - 0.5f) * 5f)) - 0.091f; // Sigmoid function 
        if (Input.GetAxis("BreathInput") >= 0.1f)
        {
            breathingIn = true;
            secondsEnabled += fixedDeltaTime;
            if (barFill >= 1f)
            {
                doubtLevel += 0.0005f;
                if (isTutorial)
                {
                    firstTime = false;
                }
                
            }
            else
            {
                doubtLevel -= 0.0007f;
            }
            
        }
        else
        {
            breathingIn = false;
            secondsEnabled -= fixedDeltaTime;
            if (barFill <= 0f)
            {
                doubtLevel += 0.0005f;
                if (isTutorial && !firstTime)
                {
                    GameObject.Find("TutorialMessage").gameObject.SetActive(false);
                    gameManager.startPhase2();
                    isTutorial = false;
                }
            }
            else
            {
                //doubtLevel -= 0.0007f;
            }

        }
        doubtLevel = Mathf.Clamp(doubtLevel, 0f, 1f);
        secondsEnabled = Mathf.Clamp(secondsEnabled, 0f, breathTime);
        barFill = Mathf.Clamp(barFill, 0f, 1.0f);
        bar.value = barFill;
        
    }
}
