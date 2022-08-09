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

        public Dictionary<int, Dictionary<int, float>> m_FiringTable = new Dictionary<int, Dictionary<int, float>>();

        public static BallisiticManager Instance = null;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            string filePath = Path.Combine(Application.streamingAssetsPath, m_DatabaseFileName);
            Debug.Log(filePath);
            m_DatabaseAccess = new DatabaseAccess("data source = " + filePath);
        }
        
        void Start()
        {
            //print(m_DatabaseFileName);
            /*string filePath = Path.Combine(Application.streamingAssetsPath, m_DatabaseFileName);
            Debug.Log(filePath);
            m_DatabaseAccess = new DatabaseAccess("data source = " + filePath);*/

            //print(GetAngle(2, 12000));
        }

        void InsertFiringTable(int shell)
        {
            string sql = string.Format("SELECT X, Angle FROM FiringTable WHERE Shell=={0} AND (Angle*100)%100==0", shell);
            var res = m_DatabaseAccess.ExecuteQuery(sql);
            Dictionary<int, float> insertData = new Dictionary<int, float>();
            while (res.Read())
            {
                insertData.Add((int)res.GetFloat(0), res.GetFloat(1));
            }
            m_FiringTable.Add(shell, insertData);
        }

        public float GetAngle(int shell, int targetX)
        {
            if (!m_FiringTable.ContainsKey(shell))
            {
                InsertFiringTable(shell);
            }

            float nearestAngle = 0;
            int nearest = int.MaxValue;
            foreach (var item in m_FiringTable[shell])
            {
                if (Mathf.Abs(item.Key - targetX) < nearest)
                {
                    nearestAngle = item.Value;
                    nearest = Mathf.Abs(item.Key - targetX);
                }
            }

            return nearestAngle;
        }
        
        public int GetShellID(string ShellName)
        {
            string sql = string.Format("SELECT ID from Shells where name==\"{0}\";", ShellName);
            var res = m_DatabaseAccess.ExecuteQuery(sql);
            if (res.Read())
            {
                return res.GetInt32(0);
            }
            return 1;
        }


        /*public float GetAngle(int shell, int targetX)
        {
            FiringTable min = default;
            FiringTable max = default;

            string sql = string.Format("SELECT * FROM FiringTable WHERE Shell=={0} AND X<{1} ORDER BY X DESC, Angle LIMIT 1;", shell, targetX);
            var res = m_DatabaseAccess.ExecuteQuery(sql);

            if (res.Read())
            {
                max.ID = res.GetInt32(0);
                max.Shell = res.GetInt32(1);
                max.Angle = res.GetFloat(2);
                max.X = res.GetFloat(3);
                max.Y = res.GetFloat(4);
            }

            sql = string.Format("SELECT * FROM FiringTable WHERE Shell=={0} AND X>{1} ORDER BY X ASC, Angle LIMIT 1;", shell, targetX);
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
        }*/

        private void OnDestroy()
        {
            m_DatabaseAccess.CloseSqlConnection();
        }
    }
    
}