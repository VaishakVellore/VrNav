//displayUI.cs - Display UI associated with objects in the VR world. Used to display product information in the E-Commerce application.
//Team 52
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class displayUI : MonoBehaviour {

	//Declarations.
	public string myString, myPrice, myId;
	public Text myText;
	public float fadeTime;
	public bool displayInfo = false;
    public static string prodInfo;

	//Called once when the program starts. Display no text.
	void Start() 
	{
        myText.text = myString + "\n" + myPrice + "\n" + myId;
        myText.enabled = false;
	}

    //The user is looking at an object, so display UI associted with the object.
    public void PointerEnter()
    {
        displayInfo = true;
        prodInfo = myId + "    " + myString + "    " + myPrice;
    }

    //User is no longer looking at the object, so stop displaying associated UI. 
    public void PointerExit()
    {
        displayInfo = false;
    }

    //Called once per frame.
    void Update() 
    {
		FadeText();
	}

	//Displays/Hides text associated with the object.
	void FadeText()
	{
		if (displayInfo) 
		{
            myText.enabled = true;
            Debug.Log("On");
		} 
		else 
		{
            myText.enabled = false;
            Debug.Log("Off");
		}
	}
}
