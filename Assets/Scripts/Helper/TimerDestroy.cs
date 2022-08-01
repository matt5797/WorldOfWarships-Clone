using UnityEngine;

namespace WOW.Helper
{
    public class TimerDestroy : MonoBehaviour
    {
        public float destoryTime = 1.5f;

        private void Start()
        {
            Destroy(this, destoryTime);
        }
    }
}