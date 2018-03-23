using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CLASE1
{

    public class TrajectoryCtrl
    {
        private readonly PullCtrl pullCtrl;
        private readonly LineRenderer TrayectoriaLinea;
        private Vector3[] segments;

        public TrajectoryCtrl(PullCtrl pullCtrl, LineRenderer trajecLineRen)
        {
            this.pullCtrl = pullCtrl;
            this.TrayectoriaLinea = trajecLineRen;

            segments = new Vector3[pullCtrl.QtyOfsegments];
        }

        private Vector2 segVelocity = Vector2.zero;
        private float shiftX = 0F;
        private const string OFFSET = "_Offset";

       
        public void DisplayTrajectory(float distance)
        {
            TrayectoriaLinea.enabled = true;
            Vector3 diff = pullCtrl.PosPullingStart - pullCtrl.PosDotHelper;

            // primer punto donde se tira el objeto
            Vector3 seg0 = pullCtrl.PosThrowTarget;
            seg0.z = pullCtrl.Puntotiro;
            segments[0] = seg0;
            Vector2 seg0Vec2 = seg0;

            // la velocidad inicial a la que se lanza
            segVelocity = pullCtrl.CalcVelocity(diff, distance);

            for (int i = 1; i < pullCtrl.QtyOfsegments; i++)
            {
                float time2 = i * Time.fixedDeltaTime *2;
                Vector3 iPos = seg0Vec2 + segVelocity * time2 + 0.5f * Physics2D.gravity * (time2 * time2);

                iPos.z = pullCtrl.Puntotiro;
                segments[i] = iPos;
            }

            TrayectoriaLinea.positionCount = pullCtrl.QtyOfsegments;

            
            for (int i = 0; i < pullCtrl.QtyOfsegments; i++)
            {
                TrayectoriaLinea.SetPosition(i, segments[i]);
            }

            shiftX += pullCtrl.StepMatOffset;
            TrayectoriaLinea.material.SetFloat(OFFSET, shiftX);
        }
    }
}