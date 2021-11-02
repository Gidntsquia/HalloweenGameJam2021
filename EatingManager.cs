using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EatingManager : ConsumptionManager
{
    public Animator burger;
    
    // Start is called before the first frame update
    void Start()
    {
        axis = "EatInput";
        progressCircle.fillMethod = Image.FillMethod.Radial360;
        progressCircle.fillAmount = fillProgress;
        fillSpeed = Time.fixedDeltaTime / fillTime;
    }

    
    protected override bool axisPressed()
    {
        bool isAxisPressed = GameManager.getEatInputPressed() && !GameManager.getDrinkInputPressed() && !GameManager.getBreathInputPressed();
        burger.SetBool("isEating", isAxisPressed);
        return isAxisPressed;
    }
}
