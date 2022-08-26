using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Start;

        // BGM ���
        Managers.Sound.Play("WoW_OST start", Define.Sound.Bgm);

    }

    public override void Clear()
    {

    }

}
