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
        // ����Ʈ Ű�� ������
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // �� true
            isScoped = !isScoped;
            // �� ī�޶� true
            otherCam.SetActive(true);
           

            // ���� FALSE �Ͻ�
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
        // �̹����� ����
        scopeOverlay.SetActive(false);
        maincross.SetActive(true);
        cross.SetActive(false);
        // �ٸ� ķ ����
        otherCam.SetActive(false);

        otherCamera.fieldOfView = normalFOV;
    }

    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(0.15f);
     
        // �̹���
        scopeOverlay.SetActive(true);
        maincross.SetActive(false);
        cross.SetActive(true);

        // �ٸ�ķ Ű��
        otherCam.SetActive(true);


        normalFOV = otherCamera.fieldOfView;
        otherCamera.fieldOfView = scopedFOV;

    }
}
