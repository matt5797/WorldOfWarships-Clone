using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        //SceneType = Define.Scene.Game;

        
        Managers.Sound.Play("WoW_OST_Game", Define.Sound.Bgm);
        

    }



    public override void Clear()
    {

    }

}
