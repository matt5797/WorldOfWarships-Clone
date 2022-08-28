using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playBGM : MonoBehaviour
{
    public AudioClip[] Music = new AudioClip[12]; // »ç¿ëÇÒ BGM


    public AudioSource AS;



    void Awake()
    {
        AS = this.GetComponent<AudioSource>();
    }

    void Start()
    {

    }

    void Update()
    {
        
        if (!AS.isPlaying)
        {
            Play();
        }
    }

  

    float currentT = 0;
    public float playT = 15f;
    void Play()
    {

         currentT += Time.deltaTime;
       
        if (currentT > playT)
         {
             currentT = 0;

             AS.clip = Music[Random.Range(0, Music.Length)];

             AS.Play();
         }


        /*for (int i = 0; i < Music.Length; i++)
        {

          //  AS.clip = Music[i];


            currentT += Time.deltaTime;

            if (currentT > playT)
            {
                currentT = 0;
                AS.clip = Music[i];
                AS.Play();
      

            }


        }*/









    }
}

