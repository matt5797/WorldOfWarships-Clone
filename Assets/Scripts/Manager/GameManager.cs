using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WOW.Controller;

namespace WOW.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public int currentModel = 5;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        public void StartGame()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            SceneManager.LoadScene(1);
            //PlayerController pc = GameObject.Find("PlayerController").GetComponent<PlayerController>();
            /*if (currentModel==5)
                pc.SetModel(0);*/
        }

        public void EndGame()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(0);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}