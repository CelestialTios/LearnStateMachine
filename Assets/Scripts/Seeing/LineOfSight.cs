using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sight{
    public class LineOfSight : MonoBehaviour
    {
        public Transform eye;
        public float angleOfView = 45f;
        public float distOfView = 20f;
        public Light LightTorch;
        public bool viewGizmo = false;

        private Transform player; 
        private float playerDist;
        private float playerAngle;
        private Vector3 playerDir;
        private RaycastHit playerHit;

        private bool viewPlayer = false;

        private Color baseColor = Color.yellow;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            LightTorch.range = distOfView + 2;
            LightTorch.spotAngle = angleOfView * 2;
            LightTorch.color = baseColor;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 pos = player.position;
            pos.y = eye.position.y;
            playerDist = Vector3.Distance(eye.position, pos);
            if (playerDist < distOfView)
            {
                viewPlayer = OnLineView();
            }
            else
            {
                viewPlayer = false;
            }
        
            baseColor = viewPlayer ? Color.red : Color.yellow;

            LightTorch.color = baseColor;

            //DebugFov(eye, angleOfView, distOfView, baseColor);
        }


        private bool OnLineView()
        {
            playerDir = player.position - eye.position;
            Vector3 angleDir = playerDir;
            angleDir.y = eye.position.y;
            playerAngle = Vector3.Angle(eye.forward, playerDir);
            if(playerAngle < angleOfView)
            {
                Physics.Raycast(eye.position, playerDir, out playerHit, distOfView);
                return playerHit.transform == player;
            }
            return false;
        }

        private void DebugFov(Transform eyeTransform, float angle, float dist, Color color)
        {
            Vector3 extentLeft = Quaternion.AngleAxis(angle, Vector3.up) * eyeTransform.forward;
            Vector3 extentRight = Vector3.Reflect(extentLeft, eyeTransform.right);
            Debug.DrawRay(eyeTransform.position, extentLeft * dist, color);
            Debug.DrawRay(eyeTransform.position, extentRight * dist, color);
            Debug.DrawRay(eyeTransform.position, eyeTransform.forward * dist, color);
        }

        public bool GetViewPlayer() { return viewPlayer; }
    
        
        private void OnDrawGizmosSelected()
        {
            if (viewGizmo)
            {
                Gizmos.color = Color.green;
                Vector3 extentLeft = Quaternion.AngleAxis(angleOfView, Vector3.up) * eye.forward;
                Vector3 extentRight = Vector3.Reflect(extentLeft, eye.right);
                Gizmos.DrawRay(eye.position, extentLeft * distOfView);
                Gizmos.DrawRay(eye.position, extentRight * distOfView);
                Gizmos.DrawRay(eye.position, eye.forward * distOfView);
            }
        }
    }
}

