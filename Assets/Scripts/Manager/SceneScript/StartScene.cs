using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Start;

        // BGM Àç»ý
        Managers.Sound.Play("WoW_OST_Start", Define.Sound.Bgm);

    }

    public override void Clear()
    {

    }

}
