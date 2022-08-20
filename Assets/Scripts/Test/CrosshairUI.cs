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
        public float speed = 10;
        float second;
        float time;

        void Update()
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {

                // distance(대포알위치, 도착지점) / 계속바뀌는속력
                //float dist = Vector3.Distance(transform.position, hitInfo.transform.position) / speed;
                //float dist = (GameObject.Find("Player Controller").transform.position - hitInfo.point).magnitude; // 거리
                //float arrival = dist / speed; //시간

                // 나와 닿은 지점의 거리
                distance = Vector3.Distance(hitInfo.point, GameObject.FindGameObjectWithTag("PlayerController").transform.position) * 0.1f;

                // 도달하는 시간 (시간 (거리 / 속력))
                time = distance / speed;

                // 초로 만들어 버리기 
                second = time % 3600 % 60;

                //Debug.Log("도달 하는 거리 : " + distance + "Km");
                //Debug.Log("초 : " + second + "s");
                // 실수를 시간으로 변경

            }

            string dis = string.Format("{0:F2}", Mathf.Round(distance * 100) * 0.01F);
            string sec = string.Format("{0:F2}", Mathf.Round(second * 100) * 0.01F);

            dist_text.text = dis + "Km";
            second_text.text = sec + "s";

        }

    }
}
