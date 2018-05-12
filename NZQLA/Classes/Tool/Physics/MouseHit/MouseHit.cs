using System;
using UnityEngine;
using System.Collections;

namespace NZQLA
{
    /// <summary>
    /// 用于检测鼠标点击3D场景的位置
    /// </summary>
    public class MouseHit : MonoSingtonUnAuto<MouseHit>
    {
        /// <summary></summary>
        [SerializeField]
        private Camera RayCamera;

        /// <summary></summary>
        [SerializeField]
        private float fDisRayMax = 100;

        //-1           Eveything 
        //0             Nothing 
        //1<<0      Default
        //1<<1      TransparentFX
        //1<<2      Ignore
        //1<<5      UI
        /// <summary></summary>
        [SerializeField]
        private LayerMask RayCastLayer = 1;

        /// <summary></summary>
        [SerializeField]
        private string strAxisMouse = "Fire2";

        /// <summary></summary>
        public event Action OnMousePressHandler;

        /// <summary></summary>
        public event Action<RaycastHit> OnMouseHitHandler;

        /// <summary></summary>
        public event Action<Vector3> OnMouseHitPosHandler;


        /// <summary>配置参数</summary>
        /// <param name="cam"></param>
        /// <param name=""></param>
        /// <param name="RayCastLayer"></param>
        /// <param name="fDisRayMax"></param>
        /// <param name="strAxisMouse"></param>
        public void Setting(Camera cam, LayerMask RayCastLayer, float fDisRayMax = 100, string strAxisMouse = "Fire2")
        {
            this.RayCamera = cam;
            this.RayCastLayer = RayCastLayer;
            this.fDisRayMax = fDisRayMax;
            this.strAxisMouse = strAxisMouse;
        }



        /// <summary></summary>
        public override void Awake()
        {
            if (RayCamera == null)
            {
                RayCamera = Camera.main;
            }

            OnMousePressHandler += OnMousePress;

        }

        Ray rayMouse;
        //监听鼠标移动点击//并计算射线
        void ListenMousePress()
        {
            if (Input.GetButtonDown(strAxisMouse))
            {
                OnMousePress();
            }
        }

        private void OnMousePress()
        {
            rayMouse = RayCamera.ScreenPointToRay(Input.mousePosition);
            GetMouseHitPos();
        }



        RaycastHit hitMouse;
        void GetMouseHitPos()
        {
            if (Physics.Raycast(rayMouse, out hitMouse, fDisRayMax, RayCastLayer))
            {
                if (OnMouseHitHandler != null)
                {
                    OnMouseHitHandler(hitMouse);
                }

                if (OnMouseHitPosHandler != null)
                {
                    OnMouseHitPosHandler(hitMouse.point);
                }
            }
        }



        // Update is called once per frame
        void Update()
        {
            ListenMousePress();
        }
    }
}
