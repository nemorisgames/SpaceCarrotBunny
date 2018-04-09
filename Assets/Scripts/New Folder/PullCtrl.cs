using UnityEngine;
using System.Collections;
using System;

namespace CLASE1
{
    public class PullCtrl : MonoBehaviour
    {

        public CambioPadre ElPadre;
        
        public ThrownObject throwObj;
        public Transform dotHelper;
        public Transform pullingStartPoint;
        public LineRenderer TrayectoriaLinea;
        public TrailMaker trail;
        public float VelocidadLanzamiento = 10F;
        public float DistanciaMaxPunto = 1.5F;
        private float CoefPuntoTiro = 1.5F;
        public int LongitudSegmento = 13;
        private float Trayectoriaoff = 0.01F; 
        public float Puntotiro = 0F;

        private EstadoTirar Estadotirar;
        private Camera camMain;
        private Rigidbody2D rgThrowTarget;

        private Vector3 posPullingStart;
        private Vector3 initPos;

        private TrajectoryCtrl trajCtrl;
        

        public Vector3 PosDotHelper { get { return dotHelper.transform.position; } }
        public Vector3 PosThrowTarget { get { return throwObj.transform.position; } }

        public int QtyOfsegments { get { return LongitudSegmento; } }
        public float DotPosZ { get { return Puntotiro; } }  
        public Vector3 PosPullingStart { get { return posPullingStart; } }
        public float StepMatOffset { get { return Trayectoriaoff; } }

        public enum EstadoTirar
        {
            Idle,
            UsuarioTirando,
            ObjVolando
        }

        void Awake()
        {
            trail.emit = false;
            trajCtrl = new TrajectoryCtrl(this, TrayectoriaLinea);
        }

        void Start()
        {
            camMain = Camera.main;
            Estadotirar = EstadoTirar.Idle;
            posPullingStart = pullingStartPoint.position;
            initPos = PosThrowTarget;
        }

        void Update()
        {
            SwitchStates();

            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Restablecer");
                Restart(initPos);
            }
        }

        private void SwitchStates()
        {
            switch (Estadotirar)
            {
                case EstadoTirar.Idle:

                    if (Input.GetMouseButtonDown(0))
                    {
                        //get the point on screen user has tapped

                        Vector3 location= camMain.ScreenToWorldPoint(Input.mousePosition);
                        //if user has tapped onto the ball
                        if (throwObj.Collider == Physics2D.OverlapPoint(location))
                            Estadotirar = EstadoTirar.UsuarioTirando;
                        
                    }
                    break;
                case EstadoTirar. UsuarioTirando:

                    dotHelper.gameObject.SetActive(true);

                    if (Input.GetMouseButton(0))
                    {
                        
                        Vector3 posMouse = camMain.ScreenToWorldPoint(Input.mousePosition);
                        posMouse.z = 0;
                        
                        if (Vector3.Distance(posMouse, posPullingStart) > DistanciaMaxPunto)
                        {
                            Vector3 maxPosition = (posMouse - posPullingStart).normalized * DistanciaMaxPunto + posPullingStart;
                            maxPosition.z = dotHelper.position.z;
                            dotHelper.position = maxPosition;
                        }
                        else
                        {
                            posMouse.z = dotHelper.position.z;
                            dotHelper.position = posMouse;
                        }

                        float distance = Vector3.Distance(posPullingStart, dotHelper.position);
                        trajCtrl.DisplayTrajectory(distance);
                    }
                    else
                    {
                        float distance = Vector3.Distance(posPullingStart, dotHelper.position);
                        TrayectoriaLinea.enabled = false;
                        ThrowObj(distance);
                        ElPadre.Cambio = true;
                    }
                    
                    break;

                default:
                    break;
            }
        }


        
        private void ThrowObj(float distance)
        {
            Debug.Log("ThrowObj");

            Estadotirar = EstadoTirar.Idle;
            Vector3 velocity = posPullingStart - dotHelper.position;
            

            throwObj.ThrowObj(CalcVelocity(velocity, distance));
           

            trail.enabled = true;
            trail.emit = true;
            dotHelper.gameObject.SetActive(false);
        }

        public void Restart(Vector3 posThrownObj)
        {
            trail.emit = false;
            trail.Clear();

            StartCoroutine(ClearTrail());

            TrayectoriaLinea.enabled = false;
            dotHelper.gameObject.SetActive(false);
            Estadotirar = EstadoTirar.Idle;

            throwObj.Reset(posThrownObj);
        }

        private readonly WaitForSeconds wtTimeBeforeClear = new WaitForSeconds(0.3F);
        IEnumerator ClearTrail()
        {
            yield return wtTimeBeforeClear;
            trail.Clear();
            trail.enabled = false;
        }

        Vector3 velocity = Vector3.zero;
        public Vector3 CalcVelocity(Vector3 diff, float distance)
        {
            velocity.x = diff.x * VelocidadLanzamiento * distance * CoefPuntoTiro;
            velocity.y = diff.y * VelocidadLanzamiento * distance * CoefPuntoTiro;

            return velocity;
        }
    }
}
