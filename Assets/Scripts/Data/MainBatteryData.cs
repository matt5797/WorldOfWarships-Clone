using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOW.Data
{
    [CreateAssetMenu(fileName = "MainBatteryData", menuName = "Scriptable Object/Main Battery Data", order = 0)]
    public class MainBatteryData : ArmamentData
    {
        [Header("Main Battery Data")]
        public int rotationAngleMin = 30; // �ּ� ȸ����
        public int rotationAngleMax = 120; // �ִ� ȸ����
    }
}