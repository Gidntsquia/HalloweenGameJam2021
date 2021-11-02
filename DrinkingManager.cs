using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkingManager : ConsumptionManager
{
    public Animator drink;
    // Start is called before the first frame update
    void Start()
    {
        axis = "DrinkInput";
        progressCircle.fillMethod = Image.FillMethod.Radial360;
        progressCircle.fillAmount = fillProgress;
        fillSpeed = Time.fixedDeltaTime / fillTime;
    }

    
    protected override bool axisPressed()
    {
        bool isAxisPressed = GameManager.getDrinkInputPressed() && !GameManager.getEatInputPressed() && !GameManager.getBreathInputPressed();
        drink.SetBool("isDrinking", isAxisPressed);
        return isAxisPressed;
    }
}
