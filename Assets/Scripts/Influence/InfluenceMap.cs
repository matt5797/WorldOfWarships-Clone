using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using WOW.BattleShip;

namespace WOW.Influence
{
    [System.Serializable]
    public struct InfluencePoint
    {
        public Vector3 position;
        public int[] influence;

        public InfluencePoint(Vector3 position, int[] influence)
        {
            this.position = position;
            this.influence = influence;
        }
    }
    
    public class InfluenceMap : MonoBehaviour
    {
        public static InfluenceMap Instance;
        public InfluencePoint[] influencePoints;
        public Dictionary<int, int>[][] influenceCache; // [포인트인덱스][진영]

        StreamWriter writer;

        void Awake()
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

            influencePoints = new InfluencePoint[62500];
            int index = 0;
            for (int x = -120; x < 130; x++)
            {
                for (int y = -120; y < 130; y++)
                {
                    influencePoints[index++] = new InfluencePoint(new Vector3(x, 0, y), new int[] { 0, 0 });
                }
            }

            influenceCache = new Dictionary<int, int>[influencePoints.Length][];
            for (int i = 0; i < influencePoints.Length; i++)
            {
                influenceCache[i] = new Dictionary<int, int>[2];
                influenceCache[i][0] = new Dictionary<int, int>();
                influenceCache[i][1] = new Dictionary<int, int>();
            }
        }
        
        // Start is called before the first frame update
        void Start()
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, "influence.csv");

            //FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);

            //writer = new StreamWriter(fileStream, System.Text.Encoding.Unicode);
            //writer = System.IO.File.CreateText(filePath);

            //StartCoroutine(UpdateInfluence());
        }

        public void CheckInfluence(Transform from, Camp camp, int instance, int value, float range, float dot)
        {
            int campIndex = (int)camp;
            
            for (int i=0; i<influencePoints.Length; i++)
            {
                if (!influenceCache[i][campIndex].ContainsKey(instance))
                {
                    if (Vector3.Distance(from.position, influencePoints[i].position) > range)
                        continue;
                    if (Vector3.Dot(from.forward, (influencePoints[i].position - from.position).normalized) < dot)
                        continue;

                    influenceCache[i][campIndex].Add(instance, value);
                }
            }
        }

        IEnumerator UpdateInfluence()
        {
            int cnt = 0;
            while (true)
            {
                yield return new WaitForSeconds(10);
                //foreach (InfluencePoint influencePoint in influencePoints)
                for (int i = 0; i < influencePoints.Length; i++)
                {
                    influencePoints[i].influence[0] = 0;
                    foreach (KeyValuePair<int, int> pair in influenceCache[i][0])
                    {
                        influencePoints[i].influence[0] += pair.Value;
                    }
                    influenceCache[i][0].Clear();
                    influencePoints[i].influence[1] = 0;
                    foreach (KeyValuePair<int, int> pair in influenceCache[i][1])
                    {
                        influencePoints[i].influence[1] += pair.Value;
                    }
                    influenceCache[i][1].Clear();
                }
                //MapToString(cnt++);
            }
        }

        void MapToJson()
        {
            string json = "[";
            for (int i = 0; i < influencePoints.Length; i++)
            {
                json += JsonUtility.ToJson(influencePoints[i]) + ", ";
            }
            json = json.Substring(0, json.Length - 2);
            json += "]";
            Debug.Log(json);
        }

        
        void MapToString(int index)
        {
            string str = "";
            for (int i = 0; i < influencePoints.Length; i++)
            {
                str += index + "\t" + influencePoints[i].position.x + "\t" + influencePoints[i].position.z + "\t" + influencePoints[i].influence[0] + "\t" + influencePoints[i].influence[1] + "\n";
            }
            writer.WriteLine(str);
        }

        private void OnDestroy()
        {
            writer.Close();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            foreach (InfluencePoint influencePoint in influencePoints)
            {
                Gizmos.DrawSphere(influencePoint.position, 0.1f);
            }
        }
    }
}