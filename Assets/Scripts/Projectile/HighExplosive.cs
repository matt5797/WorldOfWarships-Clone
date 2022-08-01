using UnityEngine;

namespace WOW.Projectile
{

    public class HighExplosive : MonoBehaviour
    {
        Vector3[] HE_points = new Vector3[4];

        private float HE_timerMax = 0;
        private float HE_timerCurrent = 0;
        private float HE_speed;
        

        public void Init(Transform _startTr, Transform _endTr, float _speed, float _newPointDistanceFromStartTr, float _newPointDistanceFromEndTr)
        {
            HE_speed = _speed;

            // 끝에 도착할 시간을 랜덤
            HE_timerMax = Random.Range(0.8f, 1.0f);

            // 시작 지점.
            HE_points[0] = _startTr.position;

            // 시작 지점을 기준으로 랜덤 포인트
            HE_points[1] = _startTr.position +
                (_newPointDistanceFromStartTr * Random.Range(-1.0f, 1.0f) * _startTr.right) + // X (좌, 우 전체)
                (_newPointDistanceFromStartTr * Random.Range(-0.15f, 1.0f) * _startTr.up) + // Y (아래쪽 조금, 위쪽 전체)
                (_newPointDistanceFromStartTr * Random.Range(-1.0f, -0.8f) * _startTr.forward); // Z (뒤 쪽만)

            // 도착 지점을 기준으로 랜덤 포인트
            HE_points[2] = _endTr.forward +
                (_newPointDistanceFromEndTr * Random.Range(-1.0f, 1.0f) * _endTr.right) + // X (좌, 우 전체)
                (_newPointDistanceFromEndTr * Random.Range(-1.0f, 1.0f) * _endTr.up) + // Y (위, 아래 전체)
                (_newPointDistanceFromEndTr * Random.Range(0.8f, 1.0f) * _endTr.forward); // Z (앞 쪽만)

            // 도착 지점.
            HE_points[3] = _endTr.position;

            transform.position = _startTr.position;
        }

        void Update()
        {
            if (HE_timerCurrent > HE_timerMax)
            {
                return;
            }

            // 경과 시간 계산.
            HE_timerCurrent += Time.deltaTime * HE_speed;

            // 베지어 곡선으로 X,Y,Z 좌표 얻기.
            transform.position = new Vector3(
                CubicBezierCurve(HE_points[0].x, HE_points[1].x, HE_points[2].x, HE_points[3].x),
                CubicBezierCurve(HE_points[0].y, HE_points[1].y, HE_points[2].y, HE_points[3].y),
                CubicBezierCurve(HE_points[0].z, HE_points[1].z, HE_points[2].z, HE_points[3].z)
            );
        }

    
        private float CubicBezierCurve(float a, float b, float c, float d)
        {
            float t = HE_timerCurrent / HE_timerMax; // 현재 경과 시간 / 최대 시간

            float ab = Mathf.Lerp(a, b, t);
            float bc = Mathf.Lerp(b, c, t);
            float cd = Mathf.Lerp(c, d, t);

            float abbc = Mathf.Lerp(ab, bc, t);
            float bccd = Mathf.Lerp(bc, cd, t);

            return Mathf.Lerp(abbc, bccd, t);
        }

        void OnTriggerEnter(Collider collision)
        {
            Destroy(this.gameObject, 0.35f);
        }

        // GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * force);
        // Destroy(this.gameObject, 10.0f);

        // public GameObject HE_Effect;
        //  private void OnCollisionEnter(Collision collision)
        //  {
        //GameObject effect = Instantiate(HE_Effect);
        //effect.transform.position = transform.position;

        // GameObject obj = Instantiate(expEffect, transform.forward, Quaternion.identity);
        // Destroy(this.gameObject);
        // Destroy(gameObject, 2.0f);
        //   }
    }
}