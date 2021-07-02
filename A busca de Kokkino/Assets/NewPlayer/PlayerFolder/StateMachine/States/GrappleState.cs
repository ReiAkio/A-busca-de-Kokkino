using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

namespace PlayerFolder
{
    [Serializable]
    public class GrappleState : PlayerState
    {
        public GameObject attachedPoint;
        public SpringJoint2D joint;
        public float acceleration;
        public LineRenderer lineRenderer;

        private void UpdateLineRender()
        {
            var pointPosition = attachedPoint.transform.position;
            var bodyPosition =  rigidbody2D.transform.position;
            lineRenderer.SetPosition(0,pointPosition);
            lineRenderer.SetPosition(1, bodyPosition);
        }
        
        public override void UpdateState()
        {
            UpdateLineRender();
            var normalized = ((Vector2) attachedPoint.transform.position - rigidbody2D.position).normalized;
            rigidbody2D.transform.rotation = Quaternion.Euler(0,0,Mathf.Rad2Deg * Mathf.Atan2(normalized.y,normalized.x) - 90 );
            base.UpdateState();
            if (!inputHandler.grapplingRequest ) //|| (attachedPoint.transform.position - rigidbody2D.transform.position).magnitude > playerHandler.maxAttachDistance
            {
                stateMachine.ChangeState(stateMachine.fallingState);
            }   
        }

        public override void FixedUpdateState()
        {
            rigidbody2D.velocity +=
                Vector2.Perpendicular(((Vector2) attachedPoint.transform.position - rigidbody2D.position).normalized) *
                (acceleration * Time.deltaTime * inputHandler.inputDirection.x *-1f);
        }

        public override void EnterState()
        {
            lineRenderer.enabled = true;
            attachedPoint = playerHandler.nearestAttachablePoint;
            joint.connectedBody = attachedPoint.GetComponent<Rigidbody2D>();
            
            joint.enabled = true; 
            base.EnterState();
            //playerHandler.isAttached = true;
            UpdateAudio();
            UpdateLineRender();

        }

        public override void LeaveState()
        {
            lineRenderer.enabled = false;
            rigidbody2D.transform.rotation = quaternion.identity;
            joint.enabled = false;
            base.LeaveState();
            //playerHandler.isAttached = false;
        }
        
    }
}
