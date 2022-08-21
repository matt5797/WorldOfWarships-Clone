using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    public GameObject scopeOverlay;
    public GameObject maincross;
    public GameObject cross;
    public GameObject otherCam;


    public Camera otherCamera;
    

    public float scopedFOV = 15f;
    private float normalFOV;
    bool isScoped = false;

    private void Start()
    {
        scopeOverlay.SetActive(false);
        otherCam.SetActive(false);
        maincross.SetActive(true);
        cross.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        // 쉬프트 키를 누르면
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // 줌 true
            isScoped = !isScoped;
            // 줌 카메라를 true
            otherCam.SetActive(true);
           

            // 줌이 FALSE 일시
            if (isScoped)
            {
                StartCoroutine(OnScoped());
                OnScoped(); 
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    OnUnscoped();
                }
            }


        }

    }
    void OnUnscoped()
    {
        // 이미지를 끈다
        scopeOverlay.SetActive(false);
        maincross.SetActive(true);
        cross.SetActive(false);
        // 다른 캠 끄고
        otherCam.SetActive(false);

        otherCamera.fieldOfView = normalFOV;
    }

    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(0.15f);
     
        // 이미지
        scopeOverlay.SetActive(true);
        maincross.SetActive(false);
        cross.SetActive(true);

        // 다른캠 키자
        otherCam.SetActive(true);


        normalFOV = otherCamera.fieldOfView;
        otherCamera.fieldOfView = scopedFOV;

    }
}
