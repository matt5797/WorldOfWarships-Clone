using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.BattleShip;

namespace WOW.Influence
{
    public class InfluenceArmament : MonoBehaviour
    {
        BattleShipBase m_BattleShipBase;
        public Transform rootPosition;
        public float radius;
        public int influenceScore;
        public float dot;

        // Start is called before the first frame update
        void Start()
        {
            m_BattleShipBase = GetComponentInParent<BattleShipBase>();
            //StartCoroutine(CheckInfluence());
        }

        IEnumerator CheckInfluence()
        {
            while (true)
            {
                InfluenceMap.Instance.CheckInfluence(rootPosition, m_BattleShipBase.camp, GetInstanceID(), influenceScore, radius, dot);
                yield return new WaitForSeconds(5);
            }
        }

    }
}