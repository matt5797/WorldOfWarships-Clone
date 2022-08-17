using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using WOW.Data;


public class Ballistic: MonoBehaviour
{
    // SHELL CONSTANTS //
    private static double C = 0.5561613f; //PENETRATION
    private static double a = 9.81f; // GRAVITY
    private static double T_0 = 288; // TEMPERATURE AT SEA LEVEL
    private static double L = 0.0065f; // TEMPERATURE LAPSE RATE 온도 경과률?
    private static double p_0 = 101325f; // PRESSURE AT SEA LEVEL 바다 압력?
    private static double R = 8.31447f; // UNIV GAS CONSTANT
    private static double M = 0.0289644f; // MOLAR MASS OF AIR

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
    public float speedMultiple = 20;
    public float DestroyY = -10;
    Quaternion fireRotation;

    private void Start()
    {
        if (shellData != null)
        {
            cw_1 = 1; 
            cw_2 = 100 + 1000 / 3 * shellData.bulletDiametr; 

            C = C * shellData.bulletKrupp / 2400; // KRUPP INCLUSION
            k = (0.5 * shellData.bulletAirDrag * Math.Pow((shellData.bulletDiametr / 2), 2) * Math.PI / shellData.bulletMass); // CONSTANTS TERMS OF DRAG
        }
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

            if (transform.position.y<= DestroyY)
            {
                Destroy(gameObject);
            }

            //Move
            transform.position = fireRotation * new Vector3(0, (float)y/100, (float)x/100);
            //transform.position = new Vector3(0, (float)y / 100, (float)x / 100);
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
}