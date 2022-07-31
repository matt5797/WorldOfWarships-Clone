using UnityEngine;

namespace WOW.Helper
{
    public class TimerDestroy : MonoBehaviour
    {
        public float destoryTime = 1.5f;
        float currentTime = 0;

        private void Start()
        {
            Destroy(this, destoryTime);
        }
    }
}