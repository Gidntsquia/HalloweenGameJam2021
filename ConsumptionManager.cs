using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ConsumptionManager : MonoBehaviour
{
    public Image progressCircle;
    protected string axis;
    public float fillTime = 3.0f;
    public float fillProgress = 0.0f;
    public float doubtingStandard = 0.5f;
    public float doubtingReducer = 4f;
    public float doubtLevel = 0.0f;
    
    protected float fillSpeed;
    

    // Start is called before the first frame update
    void Start()
    {
        progressCircle.fillMethod = Image.FillMethod.Radial360;
        progressCircle.fillAmount = fillProgress;
        fillSpeed = Time.fixedDeltaTime / fillTime;
        doubtingReducer = 4f;
    }

    // Update is called once per frame
    
    private void FixedUpdate()
    {
        if (axisPressed())
        {
            fillProgress += fillSpeed;
        }
        else
        {
            fillProgress -= fillSpeed / doubtingReducer;
        }
        doubtLevel = Mathf.Clamp(doubtLevel + (doubtingStandard - fillProgress) / 1000, 0f, 1f);
        fillProgress = Mathf.Clamp(fillProgress, 0f, 1f);
        progressCircle.fillAmount = fillProgress;
    }

    protected abstract bool axisPressed();
}
