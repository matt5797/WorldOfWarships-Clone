using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOW.Data
{
    [CreateAssetMenu(fileName = "TorpedoData", menuName = "Scriptable Object/Torpedo Data", order = 0)]
    public class TorpedoData : ProjectileData
    {
        [Header("Shell Data")]
        public int speed;   // Speed of torpedo
        public int range;   // Range of torpedo
    }
}