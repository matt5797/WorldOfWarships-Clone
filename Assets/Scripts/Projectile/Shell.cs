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
        private double L = 0.0065f; // TEMPERATURE LAPSE RATE �µ� �����?
        private double p_0 = 101325f; // PRESSURE AT SEA LEVEL �ٴ� �з�?
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

        [NonSerialized] public bool isShoot;
        public ShellData shellData;
        public float speedMultiple = 1;
        public float DestroyY = -10;
        Quaternion fireRotation;
        float penetration;

        Ray forwardRay;
        RaycastHit raycastHit;
        public LayerMask rayLayerMask;

        Vector3 lastPosition;
        RaycastHit lastraycastHit;

        //public ParticleSystem[] impactParticles;
        public ParticleSystem[] explosionParticles;

        private void Start()
        {
            if (shellData != null)
            {
                cw_1 = 1;
                cw_2 = 100 + 1000 / 3 * shellData.bulletDiametr;

                C = C * shellData.bulletKrupp / 2400; // KRUPP INCLUSION
                k = (0.5 * shellData.bulletAirDrag * Math.Pow((shellData.bulletDiametr / 2), 2) * Math.PI / shellData.bulletMass); // CONSTANTS TERMS OF DRAG

                float angle = Vector3.SignedAngle(Vector3.up, transform.up, transform.right) * -1;
                OnShoot(angle);

                //forwardRay = new Ray(transform.position, transform.forward);
                lastPosition = transform.position;
            }
        }

        private void Update()
        {
            
        }

        private void FixedUpdate()
        {
            double dt = Time.deltaTime * speedMultiple;
            if (isShoot && shellData != null)
            {
                x += v_x * dt;
                y += v_y * dt;
                T = T_0 - L * y;
                p = p_0 * Math.Pow((1 - L * y / T_0), (a * M / (R * L)));
                rho = p * M / (R * T);
                v_x = (v_x - dt * k * rho * (cw_1 * Math.Pow(v_x, 2) + cw_2 * v_x));
                v_y = (v_y - dt * a - dt * k * rho * (cw_1 * Math.Pow(v_y, 2) + cw_2 * Math.Abs(v_y)) * Math.Sign(v_y));
                t += dt;

                if (transform.position.y <= DestroyY)
                {
                    Destroy(gameObject);
                }

                //Move
                //transform.position += fireRotation * new Vector3(0, (float)y / 100, (float)x / 100);
                //transform.position = new Vector3(0, (float)y / 100, (float)x / 100);
                transform.position += fireRotation * new Vector3(0, (float)v_y / 250, (float)v_x / 250);

                Vector3 attackVector = transform.position - lastPosition;

                if (attackVector.magnitude < 0.001f)
                {
                    // A zero vector for the sphere cast don't yield any result, even if a collider overlap the "sphere" created by radius. 
                    // so we set a very tiny microscopic forward cast to be sure it will catch anything overlaping that "stationary" sphere cast
                    attackVector = Vector3.forward * 0.0001f;
                }

                forwardRay = new Ray(transform.position, attackVector.normalized);
                if (Physics.Raycast(forwardRay, out raycastHit, attackVector.magnitude, rayLayerMask))
                {
                    //print(raycastHit.collider.name +" / "+ raycastHit.normal);
                    lastraycastHit = raycastHit;
                }
                Debug.DrawRay(transform.position, transform.forward);

                lastPosition = transform.position;
            }
        }

        public void OnShoot(double shootAngle)
        {
            isShoot = true;
            fireRotation = transform.rotation;
            this.shootAngle = shootAngle * Mathf.Deg2Rad;
            print("OnShoot: " + shootAngle + ", " + this.shootAngle);
            v_x = Math.Cos(this.shootAngle) * shellData.bulletSpeed;
            v_y = Math.Sin(this.shootAngle) * shellData.bulletSpeed;
            y = 0;
            x = 0;
        }

        public double GetPenetration()
        {
            double v_total = Math.Pow((Math.Pow(v_y, 2) + Math.Pow(v_x, 2)), 0.5);
            return C * Math.Pow(v_total, 1.1) * Math.Pow(shellData.bulletMass, 0.55) / Math.Pow((shellData.bulletDiametr * 1000), 0.65);
        }

        public double GetImpactAngle()
        {
            return Math.Atan(Math.Abs(v_y) / Math.Abs(v_x));
        }

        protected override void OnImpact(Damageable damageable)
        {
            print(GetInstanceID() + " OnImpact");
            penetration = (float)GetPenetration();
            print("�����: " + penetration);
            // �ǰ� �� �̺�Ʈ
            //print("Shell OnImpact");
            // ������ġ�̸� ������, ��ź ����
            if (!damageable.CheckOvermatch(shellData.bulletDiametr))
            {
                float angle = Vector3.Angle(lastraycastHit.normal * -1, transform.forward);
                if (lastraycastHit.normal.magnitude == 0)
                {
                    // ���̰� �ʱⰪ�� ���ɼ��� ������, ������ ���� 60���� ����
                    angle = 60;
                    Debug.LogAssertion("���̰� ���� " + lastraycastHit.normal);
                }
                //print(lastraycastHit.normal + " / " + transform.forward);
                print("�Ի簢: " + angle);
                // ��ź Ȯ��
                if (damageable.CheckRicochet(angle, shellData.bulletRicochetAt, shellData.bulletAlwaysRicochetAt))
                {
                    print("��ź �߻�");
                    OnExplosion();
                    return;
                }
                // ���� Ȯ��
                if (!damageable.CheckPenetrate(ref penetration))
                {
                    print("���� ����");
                    OnExplosion();
                    return;
                }
            }
            else
            {
                print("������ġ �߻�");
            }
            // �ΰ� ȿ�� üũ
            damageable.CheckEffect(shellData);
            damageable.CheckDamage(shellData.damage, GetInstanceID());
        }

        protected override void OnThrough(Damageable damageable)
        {
            //print(damageable.name + " ����");
            if (!damageable.CheckOvermatch(shellData.bulletDiametr))
            {
                float angle = Vector3.Angle(lastraycastHit.normal * -1, transform.forward);
                if (lastraycastHit.normal.magnitude == 0)
                {
                    // ���̰� �ʱⰪ�� ���ɼ��� ������, ������ ���� 60���� ����
                    angle = 60;
                    Debug.LogAssertion("���̰� ���� " + lastraycastHit.normal);
                }
                //print(lastraycastHit.normal + " / " + transform.forward);
                print("�Ի簢: " + angle);
                // ��ź Ȯ��
                if (damageable.CheckRicochet(angle, shellData.bulletRicochetAt, shellData.bulletAlwaysRicochetAt))
                {
                    print("��ź �߻�");
                    OnExplosion();
                    return;
                }
                // ���� Ȯ��
                if (!damageable.CheckPenetrate(ref penetration))
                {
                    print("���� ����");
                    OnExplosion();
                    return;
                }
            }
            else
            {
                print("������ġ �߻�");
            }
            damageable.OnThrough(GetInstanceID());
        }

        void OnExplosion()
        {
            // ���� Ȯ�� ���� ����?
            print("OnExplosion");
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