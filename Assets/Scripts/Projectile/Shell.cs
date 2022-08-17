using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.Data;
using WOW.DamageSystem;

namespace WOW.Projectile
{
    public class Shell : ProjectileBase
    {
        // SHELL CONSTANTS //
        private double C = 0.5561613f; //PENETRATION
        private double a = 9.81f; // GRAVITY
        private double T_0 = 288; // TEMPERATURE AT SEA LEVEL
        private double L = 0.0065f; // TEMPERATURE LAPSE RATE 
        private double p_0 = 101325f; // PRESSURE AT SEA LEVEL 
        private double R = 8.31447f; // UNIV GAS CONSTANT
        private double M = 0.0289644f; // MOLAR MASS OF AIR

        private double cw_1; // QUADRATIC DRAG COEFFICIENT
        private double cw_2; // LINEAR DRAG COEFFICIENT
        private double k; // CONSTANTS TERMS OF DRAG

        private double shootAngle;
        private double dt;
        private double t;

        private double T;
        private double p;
        private double rho;
        private double v_x;
        private double v_y;
        private double x;
        private double y;

        [NonSerialized] public bool isShoot;    // 발사 여부
        public ShellData shellData; // 탄환 데이터
        public float speedMultiple = 1; // 시간 배속
        public float DestroyY = -0.01f; // 탄환 한계선
        Quaternion fireRotation;    // 발사 각도
        float penetration;  // 관통력

        Ray forwardRay; // 정면 레이
        RaycastHit raycastHit;  // 레이 정보 저장 변수
        public LayerMask rayLayerMask;  // 레이 대상이 되는 레이어들

        Vector3 lastPosition;   // 마지막 위치
        RaycastHit lastraycastHit;  // 마지막 레이 캐스팅이 맞은 부위의 정보

        //public ParticleSystem[] impactParticles;
        public ParticleSystem[] explosionParticles; // 폭발시 플레이할 파티클들

        public float worldScale = 100;  // 1유닛이 몇 미터인지

        private void Start()
        {
            if (shellData != null)
            {
                // 각 탄환의 정보에 맞추어 변수를 조정
                cw_1 = 1;
                cw_2 = 100 + 1000 / 3 * shellData.bulletDiametr;

                C = C * shellData.bulletKrupp / 2400; // KRUPP INCLUSION
                k = (0.5 * shellData.bulletAirDrag * Math.Pow((shellData.bulletDiametr / 2), 2) * Math.PI / shellData.bulletMass); // CONSTANTS TERMS OF DRAG

                // 만들어진 시점의 각도가 해수면 대비 몇 도인지 계산
                float angle = Vector3.SignedAngle(Vector3.up, transform.up, transform.right) * -1;
                // 탄환을 발사, 발사 각도를 전달
                OnShoot(angle);

                //forwardRay = new Ray(transform.position, transform.forward);
                // 처음에는 마지막 위치가 없기 때문에 일단 현재 위치를 저장
                lastPosition = transform.position;
            }
        }

        private void FixedUpdate()
        {
            // 시간 변화량을, 그동안의 지난 시간 * 가속 변수로 설정한다.
            double dt = Time.deltaTime * speedMultiple;
            if (isShoot && shellData != null)
            {
                // x, y 좌표는 dx, dy의 시간 변화량의 곱 만큼 더한 값이 된다.
                x += v_x * dt;
                y += v_y * dt;
                // 공식에 따라 시간 변화량만큼 이동한 시점의 X, Y 변화량을 구해준다.
                T = T_0 - L * y;
                p = p_0 * Math.Pow((1 - L * y / T_0), (a * M / (R * L)));
                rho = p * M / (R * T);
                v_x = (v_x - dt * k * rho * (cw_1 * Math.Pow(v_x, 2) + cw_2 * v_x));
                v_y = (v_y - dt * a - dt * k * rho * (cw_1 * Math.Pow(v_y, 2) + cw_2 * Math.Abs(v_y)) * Math.Sign(v_y));
                t += dt;

                // Y한계선 밑으로 떨어지면 자신을 파괴한다.
                if (transform.position.y <= DestroyY)
                {
                    Destroy(gameObject);
                }

                //Move
                //transform.position += fireRotation * new Vector3(0, (float)y / 100, (float)x / 100);
                //transform.position = new Vector3(0, (float)y / 100, (float)x / 100);
                // X, Y 변화량만큼의 벡터를 만들고, 발사 시점의 각도만큼 회전시켜서, 내 위치에 더해준다.
                transform.position += fireRotation * new Vector3(0, (float)v_y / 3000, (float)v_x / 3000);

                // 이전 좌표와 현재 좌표의 차이를 통해 이동 벡터를 구한다.
                Vector3 attackVector = transform.position - lastPosition;

                // 이동 벡터가 너무 작으면 임의의 값을 넣어 오류를 방지한다.
                if (attackVector.magnitude < 0.001f)
                {
                    attackVector = Vector3.forward * 0.0001f;
                }

                // 내 위치에서 이동 벡터를 향해 레이를 생성한다.
                forwardRay = new Ray(transform.position, attackVector.normalized);
                // 레이캐스팅을 통해 내가 맞은 부위의 정보를 구한다.
                if (Physics.Raycast(forwardRay, out raycastHit, attackVector.magnitude, rayLayerMask))
                {
                    //print(raycastHit.collider.name +" / "+ raycastHit.normal);
                    lastraycastHit = raycastHit;
                }
                // 다음에 쓰기 위해 현재 좌표를 기록한다.
                lastPosition = transform.position;
            }
        }

        // 탄환을 발사한다.
        public void OnShoot(double shootAngle)
        {
            // 발사 여부를 설정한다.
            isShoot = true;
            // 발사 시점의 각도를 가져온다.
            fireRotation = transform.rotation;
            //매개변수로 받은 발사 각도를 라디안으로 변환하여 클래스 변수에 저장한다.
            this.shootAngle = shootAngle * Mathf.Deg2Rad;
            print("OnShoot: " + shootAngle + ", " + this.shootAngle);
            // 초기 x,y 변화량을 계산한다.
            v_x = Math.Cos(this.shootAngle) * shellData.bulletSpeed;
            v_y = Math.Sin(this.shootAngle) * shellData.bulletSpeed * UnityEngine.Random.Range(1,10) / 10;
            // 초기 위치를 0, 0으로 설정한다.
            y = 0;
            x = 0;
        }

        // 현재 관통력을 계산하여 반환합니다.
        public double GetPenetration()
        {
            double v_total = Math.Pow((Math.Pow(v_y, 2) + Math.Pow(v_x, 2)), 0.5);
            return C * Math.Pow(v_total, 1.1) * Math.Pow(shellData.bulletMass, 0.55) / Math.Pow((shellData.bulletDiametr * 1000), 0.65);
        }

        // 입사각을 계산한다, 90도 벽에 맞을 경우의 각도라서 폐기
        public double GetImpactAngle()
        {
            return Math.Atan(Math.Abs(v_y) / Math.Abs(v_x));
        }

        protected override void OnImpact(Damageable damageable)
        {
            //print(GetInstanceID() + " OnImpact");
            penetration = (float)GetPenetration() * worldScale;
            print("init penetration: " + penetration);
            //print("관통력: " + penetration);
            // 피격 시 이벤트
            //print("Shell OnImpact");
            // 오버매치이면 과관통, 도탄 무시
            if (!damageable.CheckOvermatch(shellData.bulletDiametr))
            {
                float angle = Vector3.Angle(lastraycastHit.normal * -1, transform.forward);
                if (lastraycastHit.normal.magnitude == 0)
                {
                    // 레이가 초기값일 가능성이 높지만, 만약을 위해 60도로 판정
                    angle = 60;
                    //Debug.LogAssertion("레이가 제로 " + lastraycastHit.normal);
                }
                //print(lastraycastHit.normal + " / " + transform.forward);
                print("입사각: " + angle);
                // 도탄 확인
                if (damageable.CheckRicochet(angle, shellData.bulletRicochetAt, shellData.bulletAlwaysRicochetAt))
                {
                    print("도탄 발생");
                    DamageTextManager.Instance.CreateDamageText(transform, "도탄 발생", 20);
                    OnExplosion();
                    return;
                }
                // 관통 확인
                if (!damageable.CheckPenetrate(ref penetration))
                {
                    print("관통 실패: " + penetration);
                    DamageTextManager.Instance.CreateDamageText(transform, "관통 실패", 20);
                    OnExplosion();
                    return;
                }
            }
            else
            {
                print("오버매치 발생");
            }
            // 부가 효과 체크
            damageable.CheckEffect(shellData);
            damageable.CheckDamage(shellData.damage, GetInstanceID());
        }

        protected override void OnThrough(Damageable damageable)
        {
            //print(damageable.name + " 관통");
            if (!damageable.CheckOvermatch(shellData.bulletDiametr))
            {
                float angle = Vector3.Angle(lastraycastHit.normal * -1, transform.forward);
                if (lastraycastHit.normal.magnitude == 0)
                {
                    // 레이가 초기값일 가능성이 높지만, 만약을 위해 60도로 판정
                    angle = 60;
                    //Debug.LogAssertion("레이가 제로 " + lastraycastHit.normal);
                }
                //print(lastraycastHit.normal + " / " + transform.forward);
                //print("입사각: " + angle);
                // 도탄 확인
                if (damageable.CheckRicochet(angle, shellData.bulletRicochetAt, shellData.bulletAlwaysRicochetAt))
                {
                    //print("도탄 발생");
                    OnExplosion();
                    return;
                }
                // 관통 확인
                if (!damageable.CheckPenetrate(ref penetration))
                {
                    //print("관통 실패");
                    OnExplosion();
                    return;
                }
            }
            else
            {
                print("오버매치 발생");
            }
            damageable.OnThrough(GetInstanceID());
        }

        /// <summary>
        /// 탄환 폭발 시 호출되는 함수
        /// </summary>
        void OnExplosion()
        {
            // 추후 확산 판정 삽입?
            //print("OnExplosion");
            foreach (ParticleSystem particleFactory in explosionParticles)
            {
                ParticleSystem particle = Instantiate<ParticleSystem>(particleFactory);
                particle.transform.position = transform.position;
                particle.Play();
            }
            OnApplyDamage();
            Destroy(gameObject);
        }
    }
}