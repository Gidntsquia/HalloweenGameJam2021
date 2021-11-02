using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubtMeterScript : MonoBehaviour
{
    public GameManager gameManager;
    public Slider doubtBar;
    public BreathingManager breathingManager;
    public ConsumptionManager eatingManger;
    public ConsumptionManager drinkingManager;
    public float totalDoubtProgress = 0.0f;
    public float doubtingSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        doubtBar.value = 0f;
    }
    
    private void FixedUpdate()
    {
        totalDoubtProgress = doubtingSpeed * (breathingManager.doubtLevel + eatingManger.doubtLevel + drinkingManager.doubtLevel); 
        doubtBar.value = Mathf.Clamp(totalDoubtProgress, 0f, 1f);
        if (doubtBar.value >= 1f)
        {
            gameManager.loseGame();
            gameObject.SetActive(false);
        }
    }
}
