using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace WOW
{
    public class BallisiticManager : MonoBehaviour
    {
        struct FiringTable
        {
            public int ID;
            public int Shell;
            public float Angle;
            public float X;
            public float Y;
        }

        public static string m_DatabaseFileName = "WOWS.db";
        public static string m_TableName = "FiringTable";
        private static DatabaseAccess m_DatabaseAccess;

        void Start()
        {
            //print(m_DatabaseFileName);
            string filePath = Path.Combine(Application.streamingAssetsPath, m_DatabaseFileName);
            Debug.Log(filePath);
            m_DatabaseAccess = new DatabaseAccess("data source = " + filePath);

            //print(GetAngle(2, 12000));
        }

        public static float GetAngle(int shell, int targetX)
        {
            FiringTable min = default;
            FiringTable max = default;
            
            string sql = string.Format("SELECT * FROM FiringTable WHERE Shell=={0} AND X<{1} ORDER BY X DESC LIMIT 1;", shell, targetX);
            var res = m_DatabaseAccess.ExecuteQuery(sql);

            if (res.Read())
            {
                max.ID = res.GetInt32(0);
                max.Shell = res.GetInt32(1);
                max.Angle = res.GetFloat(2);
                max.X = res.GetFloat(3);
                max.Y = res.GetFloat(4);
            }

            sql = string.Format("SELECT * FROM FiringTable WHERE Shell=={0} AND X>{1} ORDER BY X ASC LIMIT 1;", shell, targetX);
            res = m_DatabaseAccess.ExecuteQuery(sql);

            if (res.Read())
            {
                max.ID = res.GetInt32(0);
                max.Shell = res.GetInt32(1);
                max.Angle = res.GetFloat(2);
                max.X = res.GetFloat(3);
                max.Y = res.GetFloat(4);
            }

            if ((max.X - targetX) < (targetX - min.X))
            {
                return max.Angle;
            }
            else
            {
                return min.Angle;
            }
            // 추후 근사치 계산 추가?
        }

        private void OnDestroy()
        {
            m_DatabaseAccess.CloseSqlConnection();
        }
        /*
        bool isOpen = false;
        IEnumerator OpenMouse()
        {
            yield return new WaitForSeconds(1);

            if (랜덤 값이 3이하면)
            {
                ingBling = true;
                // 색변경
            }

            //입 열기 시작
            while (!isOpen)
            {
                // 입 열리는 동작
                if (입이 열렸다!)
                    break;
                yield return null;
            }

            yield return new WaitForSeconds(입열려있는시간);

            ingBling = false;
            //입 닫기 시작
            while (isOpen)
            {
                // 입이 닫히는 동작
                if (입이 닫혔다!)
                    break;
                yield return null;
            }
            
            yield break;
        }
        */
    }
    
}