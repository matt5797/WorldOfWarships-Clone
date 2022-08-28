using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
   // 나는 일정시간 마다 무전 소리를 듣고 싶어
    [System.Serializable]
    public struct BgmType
    {
        public string name;
        public AudioClip audio;
        
    }
    public BgmType[] BGMList;

    //private AudioSource BGM;
    private AudioSource audioSource;
    private string NowBGMname = "";

    private int playingCount;
    private int bgmNum;




    void Start()
    {
        if (BGMList.Length > 0) Playlist(BGMList[0].name);
      
        audioSource = GetComponent<AudioSource>();
       
    }


    

    void Playlist(string name)
    {
    
   
        //if (NowBGMname.Equals(name)) return;


        for (int i = 0; i < BGMList.Length; ++i)
        {
           
            if (BGMList[i].name.Equals(name))
            {

                audioSource.clip = BGMList[i].audio;
                audioSource.Play();
                NowBGMname = name;
                // audioSource.loop = false;

            }


        }
    }


}
