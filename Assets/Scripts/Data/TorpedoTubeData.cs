using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOW.Data
{
    [CreateAssetMenu(fileName = "TorpedoTubeData", menuName = "Scriptable Object/Torpedo Tube Data", order = 0)]
    public class TorpedoTubeData : ArmamentData
    {
        [Header("Torpedo Tube Data")]
        public int range = 5; // 최대 사거리
    }
}