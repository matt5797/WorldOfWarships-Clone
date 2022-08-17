using System;
using UnityEngine;
using TMPro;
using WOW.Controller;

namespace WOW.UI
{
    public class CrosshairUI : MonoBehaviour
    {

        public TextMeshProUGUI dist_text;
        public TextMeshProUGUI second_text;

        float distance;
        float speed = 10;
        float second;
        float time;

        void Update()
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {

                // distance(��������ġ, ��������) / ��ӹٲ�¼ӷ�
                //float dist = Vector3.Distance(transform.position, hitInfo.transform.position) / speed;
                //float dist = (GameObject.Find("Player Controller").transform.position - hitInfo.point).magnitude; // �Ÿ�
                //float arrival = dist / speed; //�ð�

                // ���� ���� ������ �Ÿ�
                distance = Vector3.Distance(hitInfo.point, GameObject.Find("Player Controller").transform.position);

                // �����ϴ� �ð� (�ð� (�Ÿ� / �ӷ�))
                time = distance / speed;

                // �ʷ� ����� ������ 
                second = time % 3600 % 60;

                //Debug.Log("���� �ϴ� �Ÿ� : " + distance + "Km");
                //Debug.Log("�� : " + second + "s");
                // �Ǽ��� �ð����� ����

            }

            dist_text.text = Mathf.Round(distance * 10) * 0.1F + "Km";
            second_text.text = Mathf.Round(second * 10) * 0.1F + "s";

        }

    }
}