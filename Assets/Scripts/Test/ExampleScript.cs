using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    public Camera firstCam;
    public Camera thirdCam;
    public bool isfirstCam = false;
    public bool isClicked = false;
    public float clicktimeMax = 0.5f;
    public float curClickTime = 0.0f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !isClicked)
        {
            isClicked = true;
            firstCamControl();
        }
        if (isClicked)
        {
            ClickedTime();
        }
    }

    void firstCamControl()
    {
        isfirstCam = !isfirstCam;

        if (isfirstCam)
        {

            firstCam.enabled = false;
            thirdCam.enabled = true;


        }
        else
        {

            firstCam.enabled = true;
            thirdCam.enabled = false;

        }
    }

    void ClickedTime()
    {
        curClickTime += Time.deltaTime;
        if (curClickTime >= clicktimeMax)
        {
            isClicked = false;
            curClickTime = 0.0f;
        }
    }

}

