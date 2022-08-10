using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.Projectile;
using WOW.Data;
using WOW;

public class BallisticDemoFire : MonoBehaviour
{
    public GameObject bulletFactory;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<100; i++)
        {
            CreateBullet(GetBullet(Random.Range(1, 600)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateBullet(ShellData shellData)
    {
        GameObject bullet = Instantiate(bulletFactory);
        bullet.GetComponent<Shell>().shellData = shellData;
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
    }

    ShellData GetBullet(int ShellID)
    {
        return BallisiticManager.Instance.GetShellData(ShellID);
    }
}
