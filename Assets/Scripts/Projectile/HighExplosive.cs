using UnityEngine;

namespace WOW.Projectile
{
    public class HighExplosive : MonoBehaviour
    {
        public GameObject HE_Effect;
        private void OnCollisionEnter(Collision collision)
        {
            GameObject effect = Instantiate(HE_Effect);
            effect.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}