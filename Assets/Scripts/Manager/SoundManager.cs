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

    //���� �ݺ� ��� ����
    public void Clear()
    {
        // ����� ���� ��� ���߱�, �뷡 ����
        foreach(AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        // dictionary ���� 
        _audioClips.Clear();
    }


    // ����� Ŭ��, ���� Ÿ�� = effect ����, ��� �ӵ� )
    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1f)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        // �������
        if (type == Define.Sound.Bgm)
        {
            // ��������� �ҷ���
            AudioClip audioClip = Managers.Resource.Load<AudioClip>(path);
            if (audioClip == null)
            {
                Debug.Log($"AudioClip Missing ! {path}");
                return;
            }

            // ��� �ӵ�
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
