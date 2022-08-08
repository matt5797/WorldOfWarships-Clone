using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

using WOW.Data;

public class ShellDataParser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //var jtc2 = JsonToOject<ShellData>(jsonData); 
        //jtc2.Print();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    string ObjectToJson(object obj) 
    { 
        return JsonUtility.ToJson(obj); 
    }
    
    T JsonToOject<T>(string jsonData) 
    { 
        return JsonUtility.FromJson<T>(jsonData); 
    }

    T LoadJsonFile<T>(string loadPath, string fileName) 
    { 
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open); 
        byte[] data = new byte[fileStream.Length]; 
        fileStream.Read(data, 0, data.Length); 
        fileStream.Close(); 
        string jsonData = Encoding.UTF8.GetString(data); 
        return JsonUtility.FromJson<T>(jsonData); 
    }
}
