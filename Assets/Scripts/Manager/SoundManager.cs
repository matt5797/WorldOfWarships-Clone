using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager
{ 
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    public void Init()
    {
        GameObject root = GameObject.Find("Sound");
        if (root == null)
        {
            root = new GameObject { name = "Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }

    //무한 반복 재생 위함
    public void Clear()
    {
        // 재생기 전부 재생 멈추기, 노래 빼기
        foreach(AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        // dictionary 비우기 
        _audioClips.Clear();
    }


    // 오디오 클립, 사운드 타입 = effect 관련, 재생 속도 )
    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1f)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        // 배경음악
        if (type == Define.Sound.Bgm)
        {
            // 배경음악을 불러와
            AudioClip audioClip = Managers.Resource.Load<AudioClip>(path);
            if (audioClip == null)
            {
                Debug.Log($"AudioClip Missing ! {path}");
                return;
            }

            // 재생 속도
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];

            if (audioSource.isPlaying == true)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        else // effect
        {
            AudioClip audioClip = GetOrAddAudioClip(path);
            if(audioClip == null)
            {
                Debug.Log($"AudioClip Missing ! {path}");
                return;
            }

            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            if (audioSource!=null)
            {
                audioSource.pitch = pitch;
                audioSource.PlayOneShot(audioClip);
            }
        }

    }

    AudioClip GetOrAddAudioClip(string path)
    {
      
        AudioClip audioClip = null;

        if(_audioClips.TryGetValue(path, out audioClip) == false)
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);
            _audioClips.Add(path, audioClip);

        }
        return audioClip;
     
    }
   
}
