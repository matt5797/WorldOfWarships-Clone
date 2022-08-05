using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace WOW
{
    public class DamageTextManager : MonoBehaviour
    {
        public static DamageTextManager Instance = null;

        void Awake()
        {
            if (null == Instance)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }


        public GameObject damageTextFactory;
        public Canvas canvas;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CreateDamageText(Transform target, string message, float fontSize=default)
        {
            GameObject textObject = Instantiate(damageTextFactory, Camera.main.WorldToScreenPoint(target.position), Quaternion.identity, canvas.transform);
            TextMeshProUGUI text = textObject.GetComponent<TextMeshProUGUI>();
            if (text!=null)
                text.SetText(message);
            if (fontSize != default)
                text.fontSize = fontSize;
        }
    }
}