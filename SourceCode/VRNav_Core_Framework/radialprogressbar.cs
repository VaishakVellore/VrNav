//radialprogressbar.cs - Manages the animation of the radial progress bar as seen in our VR apps.
//Team 52
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RPB : MonoBehaviour {

	//Declarations.
	public Transform LoadingBar;
	public Transform TextIndicator;
	public Transform TextLoading;
	[SerializeField] private float currentAmount;
	[SerializeField] private float speed;
    public Image RadialProgressBarImage;
    public Image LoadingBarImage;
    public Image CentreImage;
    public static bool progressBarOn;
    public static bool timedGazeClick;
    public static bool selectedObject;

    //Called one when the program begins. Initialize variables just declared.
	void Start()
    {
        progressBarOn = cartaddobject.progressBarOn;
        RadialProgressBarImage = GameObject.Find("RadialProgressBar").GetComponent<Image>();
        LoadingBarImage = GameObject.Find("LoadingBar").GetComponent<Image>();
        CentreImage = GameObject.Find("Centre").GetComponent<Image>();
        timedGazeClick = cartaddobject.timedGazeClick;
        selectedObject = cartaddobject.selectObject;
    }

    //Called once a frame. Controls the animation of the progress bar, and triggers an action if 100% is completed.
    void Update () 
    {
        RadialProgressBarImage.enabled = true;
        LoadingBarImage.enabled = true;
        CentreImage.enabled = true;
        progressBarOn = cartaddobject.progressBarOn;
        timedGazeClick = cartaddobject.timedGazeClick;
        selectedObject = cartaddobject.selectObject;
        
        //If the user focuses on an object, display radial progress bar, and start it.
        if (progressBarOn)
        {
            
            if (currentAmount < 100)
            {
                currentAmount += speed * Time.deltaTime;
                TextIndicator.GetComponent<Text>().text = ((int)currentAmount).ToString() + "%";
                TextLoading.gameObject.SetActive(true);
            }
            
            //If 100% of the radial progress bar is completed, then trigger an action.
            else
            {
                TextLoading.gameObject.SetActive(false);
                cartaddobject.timedGazeClick = true;
                RadialProgressBarImage.enabled = false;
                LoadingBarImage.enabled = false;
                CentreImage.enabled = false;
            }
            LoadingBar.GetComponent<Image>().fillAmount = currentAmount / 100;
        }
       
        //Reset the radial progress bar and hide it, because the user isn't looking at the object anymore.
        else
        {
            currentAmount = 0;
            RadialProgressBarImage.enabled = false;
            LoadingBarImage.enabled = false;
            CentreImage.enabled = false;
        }
	}
}
