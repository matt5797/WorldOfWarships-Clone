using UnityEngine;
using WOW.Data;

namespace WOW.Controller
{
    public class CameraController : MonoBehaviour
    {
        public State<CameraController> cameraState;
        public Transform m_Target;

        public CameraStateData stateData;

        public bool isChangingState = false;

        // Start is called before the first frame update
        void Start()
        {
            m_Target = GameObject.FindGameObjectWithTag("PlayerController")?.GetComponent<PlayerController>().ship.transform;
            cameraState = new OrbitState();
        }

        // Update is called once per frame
        void Update()
        {
            State<CameraController> nowState = cameraState.InputHandle(this);
            cameraState.action(this);

            if (!nowState.Equals(cameraState))
            {
                print(cameraState + " to " + nowState);
                cameraState.Exit(this);
                cameraState = nowState;
            }
        }

        private void FixedUpdate()
        {
            cameraState.FixedUpdate(this);
        }

        private void LateUpdate()
        {
            cameraState.LateUpdate(this);
        }

        private void OnDrawGizmos()
        {
            if (stateData!=null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(m_Target.position + stateData.targetOffset, 0.1f);
            }

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, 0.1f);
        }
    }
}