using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static float inputCutoff = 0.1f;

    public GameObject[] managers;
    public Image[] UI;
    public GameObject startMenuUI;
    public float secondsEnabled;
    public bool isTutorial = true;
    private float fixedTime;
    private CanvasRenderer[] canvasRenderers;


    private void Awake()
    {
        deactivateAll();
        UI = GameObject.Find("GameUI").GetComponentsInChildren<Image>();
        foreach(Image image in UI)
        {
            image.gameObject.SetActive(false);
        }
        fixedTime = Time.fixedDeltaTime;
        isTutorial = true;
        canvasRenderers = GetComponentsInChildren<CanvasRenderer>();
        foreach(CanvasRenderer UIElement in canvasRenderers)
        {
            UIElement.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (!isTutorial)
        {
            secondsEnabled += fixedTime;
            if (secondsEnabled >= 130)
            {
                secondsEnabled = -123123f;
                deactivateAll();
                startEnding();
            }
            else if (secondsEnabled >= 100f)
            {
                managers[3].GetComponent<DoubtMeterScript>().doubtingSpeed = 3.3f;
                managers[3].GetComponent<DoubtMeterScript>().totalDoubtProgress -= 0.2f;
                managers[1].GetComponent<EatingManager>().doubtingStandard = 0.85f;
                managers[1].GetComponent<EatingManager>().doubtingReducer = 2f;
                managers[2].GetComponent<DrinkingManager>().doubtingStandard = 0.85f;
                managers[2].GetComponent<DrinkingManager>().doubtingReducer = 2f;
            }
            else if (secondsEnabled >= 60)
            {
                managers[3].GetComponent<DoubtMeterScript>().doubtingSpeed = 2f; 
                managers[3].GetComponent<DoubtMeterScript>().totalDoubtProgress -= 0.2f;
            }
            else if (secondsEnabled >= 30)
            {
                managers[1].GetComponent<EatingManager>().doubtingStandard = 0.75f;
                managers[1].GetComponent<EatingManager>().doubtingReducer = 3f;
                managers[2].GetComponent<DrinkingManager>().doubtingStandard *= 0.75f;
                managers[2].GetComponent<DrinkingManager>().doubtingReducer = 3f;
                managers[3].GetComponent<DoubtMeterScript>().doubtingSpeed = 1.75f;
                managers[3].GetComponent<DoubtMeterScript>().totalDoubtProgress -= 0.2f;
                
            }
            
            
        }
        
    }

    public void startGame()
    {
        startMenuUI.SetActive(false);
        StartCoroutine(doTutorial());
    }

    public void startPhase2()
    {
        managers[1].SetActive(true);
        managers[2].SetActive(true);
        foreach(CanvasRenderer UIElement in canvasRenderers)
        {
            UIElement.gameObject.SetActive(true);
        }
        StartCoroutine(wait10seconds());
    }

    public void startPhase3()
    {
        foreach(CanvasRenderer UIElement in canvasRenderers)
        {
            UIElement.gameObject.SetActive(false);
        }
        managers[0].GetComponent<BreathingManager>().doubtLevel = 0f;
        managers[1].GetComponent<EatingManager>().doubtLevel = 0f;
        managers[2].GetComponent<DrinkingManager>().doubtLevel = 0f;
        managers[3].SetActive(true);
        isTutorial = false;
    }

    public void startEnding()
    {
        deactivateAll();
        secondsEnabled = -14123;
        UI[1].gameObject.SetActive(true);
        StartCoroutine(restartGameIEnumerator(15f));
    }

    private IEnumerator wait10seconds()
    {
        yield return new WaitForSeconds(10);
        startPhase3();
    }

    private IEnumerator doTutorial()
    {
        UI[2].gameObject.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        UI[2].gameObject.SetActive(false);
        UI[3].gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        UI[3].gameObject.SetActive(false);
        managers[0].SetActive(true);
    }
    
    public void loseGame()
    {
        deactivateAll();
        secondsEnabled = 0f;
        UI[0].gameObject.SetActive(true);
        StartCoroutine(restartGameIEnumerator(5f));
    }

    private IEnumerator restartGameIEnumerator(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void deactivateAll()
    {
        foreach (GameObject gameObject in managers)
        {
            gameObject.SetActive(false);
        }
    }

    public static bool getBreathInputPressed()
    {
        return Input.GetAxis("BreathInput") >= inputCutoff;
    }

    public static bool getEatInputPressed()
    {
        return Input.GetAxis("EatInput") >= inputCutoff;
    }

    public static bool getDrinkInputPressed()
    {
        return Input.GetAxis("DrinkInput") >= inputCutoff;
    }

    public static bool getAxisPressed(string axis)
    {
        return Input.GetAxis(axis) >= inputCutoff; 
    }

    
}
