//headgestures.cs - Configure and recognize head gestures performed by the user to trigger appropriate actions.
//Team 52
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class headgestures : MonoBehaviour {

	//Declarations.
    private Vector3[] angles;
    private int index;
    private Vector3 origAngle;
    private Text responseText;
    private static bool selectObject;
    private int numItems = 0;

	//Called once at the beginning of the program. Initialize variables declared.
	void Start ()
    {
        selectObject = cartaddobject.selectObject;
        numItems = 0;
        ResetData();
        responseText = GameObject.Find("CartItems").GetComponent<Text>();
    }
	
	//Called once per frame. Initializes orientation coordinates, which is a vector representing values across X, Y and Z axes.
	void Update ()
    {
        angles[index] = GvrViewer.Instance.HeadPose.Orientation.eulerAngles;
        selectObject = cartaddobject.selectObject;
        index++;
        if (index == 30)
        {
            if (selectObject == true)
                DetectHeadGestures();
            ResetData();
        }
	}

	//Reinitialize orientation coordinates.
	void ResetData()
    {
        angles = new Vector3[30];
        index = 0;
        origAngle = GvrViewer.Instance.HeadPose.Orientation.eulerAngles;
    }

	//The core function of the program. If user moves head up and down, then yes. If user moves head left and right, then no. Add items to cart accordingly.s
    void DetectHeadGestures()
    {
        bool right = false, left = false, up = false, down = false;
        responseText.text = "Cart: " + numItems + " items(s) \n Nod to buy, No to cancel.";
        for (int i = 0; i < 30; i++)
        {
            if (angles[i].x < origAngle.x - 10.0f && !up)
            {
                up = true;
            }
            else if (angles[i].x > origAngle.x + 10.0f && !down)
            {
                down = true;
            }

            if (angles[i].y < origAngle.y - 20.0f && !left)
            {
                left = true;
            }
            else if (angles[i].y > origAngle.y + 20.0f && !right)
            {
                right = true;
            }

            if ((left && right))
            {
                cartaddobject.selectObject = false;
                Debug.Log("No");
                responseText.text = "Cart: " + numItems + " items(s)";
                break;
            }

            if ((up && down))
            {
                Debug.Log("Yes");
                numItems++;
                cartaddobject.selectObject = false;
                responseText.text = "Cart: " + numItems + " items(s)";
                break;
            }
        }
    }

}
