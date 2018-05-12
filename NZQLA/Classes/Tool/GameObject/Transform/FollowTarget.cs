using UnityEngine;
using System.Collections;

namespace NZQLA
{
    /// <summary>
    /// 跟随目标
    /// </summary>
    public class FollowTarget : MonoBehaviour
    {
        /// <summary>跟随目标</summary>
        public Transform target;

        /// <summary>与目标的相对偏移距离</summary>
        public Vector3 vOffsetPos = new Vector3(0, 0, 1);

        #region ExternalCall
        /// <summary>开始追随指定目标</summary>
        /// <param name="target">指定目标</param>
        /// <param name="vOffsetPos">与目标的相对位置</param>
        public void ActionFollowTarget(Transform target, Vector3 vOffsetPos)
        {
            this.target = target;
            this.vOffsetPos = vOffsetPos;
            enabled = true;
            //脱离父物体
            if (transform.parent != null)
            {
                transform.SetParent(null, true);
            }
        }

        /// <summary>暂停跟随</summary>
        public void PauseFollow()
        {
            enabled = false;
        }

        /// <summary>继续追随</summary>
        public void ContinueFollow()
        {
            enabled = true;
        }


        /// <summary>终止追随</summary>
        public void StopFollow()
        {
            target = null;
            enabled = false;
        }


        #endregion




        #region Unity
        void OnEnable()
        {
            if (target == null)
            {
                enabled = false;
            }
        }


        void Update()
        {
            Follow();
        }
        #endregion




        #region Core
        void Follow()
        {
            if (target == null)
            {
                enabled = false;
                return;
            }
            transform.position = target.RelativePos(vOffsetPos);
            //将自身的Forward与目标的Forward保持一致
            transform.rotation = Quaternion.LookRotation(target.forward);

        }

        #endregion


    }
}
