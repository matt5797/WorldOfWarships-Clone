using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance {get { Init(); return s_instance; } }
    SoundManager _sound = new SoundManager();
    ResourceManager _resource = new ResourceManager();


    public static SoundManager Sound { get { return Instance._sound; } }

    public static ResourceManager Resource { get { return Instance._resource; } }

    static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("Managers");
            if(go == null)
            {
                // managers »ý¼º
                go = new GameObject { name = "Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._sound.Init();
        }
    }

    void Start()
    {
        Init();
    }
}
