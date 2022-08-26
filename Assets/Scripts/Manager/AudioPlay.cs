using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    // ���� �����ð� ���� ���� �Ҹ��� ��� �;�
    [System.Serializable]
    public struct BgmType
    {
        public string name;
        public AudioClip audio;
    }
    public BgmType[] BGMList;

    public BgmType other;



    // Update is called once per frame
    void Update()
    {
       
    }

    public static void playSound(AudioClip clip, AudioSource audioPlayer)
    {
 
            audioPlayer.Stop();
            audioPlayer.clip = clip;
            audioPlayer.loop = false;
            audioPlayer.time = 0;
            audioPlayer.Play();
        
    }
}
