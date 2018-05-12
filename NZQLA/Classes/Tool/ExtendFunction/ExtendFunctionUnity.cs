using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

namespace NZQLA
{
    /// <summary>
    /// Unity的扩展方法
    /// </summary>
    public static class ExtendFunctionUnity
    {

        #region 组件处理



        #region Transform
        /// <summary>重置</summary>
        /// <param name="self"></param>
        public static void Reset(this Transform self)
        {
            self.transform.position = self.transform.localPosition = Vector3.zero;
            self.transform.rotation = self.transform.localRotation = Quaternion.identity;
            self.localScale = Vector3.zero;
        }



        /// <summary>复制Transform</summary>
        /// <param name="self"></param>
        /// <param name="target">目标</param>
        /// <param name="bLocal">拷贝Local？</param>
        public static void Copy(this Transform self, Transform target, bool bLocal = true)
        {
            if (bLocal)
            {
                self.transform.localPosition = target.transform.localPosition;
                self.transform.localRotation = target.transform.localRotation;
            }
            else
            {
                self.transform.position = target.transform.position;
                self.transform.rotation = target.transform.rotation;
            }

            self.localScale = target.transform.localScale;
        }
        #endregion



        #endregion



        #region 组件挂载与获取
        /// <summary>确保自身挂在指定类型的脚本组件(对象必须是激活状态)</summary>
        /// <typeparam name="T">指定脚本组件的类型</typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T EnsureHasComponent<T>(this GameObject obj) where T : Component
        {
            if (obj == null)
                return null;

            if (!obj.activeSelf)
                return null;

            T c = obj.GetComponent<T>();
            if (c == null)
            {
                c = obj.AddComponent<T>();
            }

            return c;
        }

        #endregion


        #region 相对位置
        /// <summary>获取自身的相对位置的世界位置</summary>
        /// <param name="self"></param>
        /// <param name="offset">相对偏移位置</param>
        /// <returns></returns>
        public static Vector3 RelativePos(this Transform self, Vector3 offset)
        {
            return self == null ? Vector3.zero : self.TransformPoint(offset);
        }


        /// <summary>获取相对于自身指定方向指定距离的世界位置</summary>
        /// <param name="self"></param>
        /// <param name="fDis">指定距离</param>
        /// <param name="vDirOffset">指定方向(不需要将其转为LocalSpace 比如正前方可以直接用Vector3.forward)</param>
        /// <returns></returns>
        public static Vector3 RelativePos(this Transform self, float fDis, Vector3 vDirOffset)
        {
            if (self == null)
                return Vector3.zero;

            vDirOffset = vDirOffset.normalized;
            return self.TransformPoint(vDirOffset * fDis);
        }
        #endregion



        #region UI

        /// <summary>设置Text文本内容</summary>
        /// <param name="self"></param>
        /// <param name="strContent">指定文本内容</param>
        public static void SetContent(this Text self, string strContent)
        {
            if (self)
            {
                self.text = strContent;
            }
        }



        #region 位置

        /// <summary>根据父物体的面板大小和自身所处父物体面板区域内的比例处绘制Image</summary>
        /// <param name="selfPanel"></param>
        /// <param name="vScale">自身所处父物体面板区域内的比例</param>
        /// <param name="vStandardPosScale"></param>
        public static void SetPosByScale(this RectTransform selfPanel, Vector2 vScale, Vector2 vStandardPosScale)
        {
            Vector2 vPos;
            vPos.x = (selfPanel.parent as RectTransform).rect.width * (vScale.x - vStandardPosScale.x);

            vPos.y = (selfPanel.parent as RectTransform).rect.height * (vScale.y - vStandardPosScale.y);
            selfPanel.anchoredPosition = vPos;
        }

        #endregion


        #region 尺寸

        /// <summary>设置UI为指定尺寸</summary>
        /// <param name="RTS"></param>
        /// <param name="vSize">指定尺寸</param>
        public static void SetSize(this RectTransform RTS, Vector2 vSize)
        {
            if (RTS)
            {
                RTS.sizeDelta = vSize;
            }
        }



        /// <summary>设置UI为父物体UI区域的中心区域的指定比例大小 </summary>
        /// <param name="RTS"></param>
        /// <param name="vScaleHor">水平比例(比如(0,1)就表示宽度与父物体一样大)</param>
        /// <param name="vScaleVer">垂直比例</param>
        /// <param name="bStayBefore">是否维持原有大小和位置</param>
        public static void SetSizeByScale(this RectTransform RTS, Vector2 vScaleHor, Vector2 vScaleVer, bool bStayBefore = true)
        {
            if (RTS)
            {
                RTS.anchorMin = new Vector2(vScaleHor.x, vScaleVer.x);
                RTS.anchorMax = new Vector2(vScaleHor.y, vScaleVer.y);

                if (bStayBefore)
                {
                    RTS.offsetMin = RTS.offsetMax = Vector2.zero;
                }
            }
        }


        /// <summary>设置UI相对于父级的锚点</summary>
        /// <param name="RTS"></param>
        /// <param name="vAnchorMin">极小锚点(ScaleMinX,ScaleMinY)</param>
        /// <param name="vAnchorMax">极大锚点(ScaleMaxX,ScaleMaxY)</param>
        /// <param name="bStayBefore">是否维持原有大小和位置</param>
        public static void SetSizeByAnchor(this RectTransform RTS, Vector2 vAnchorMin, Vector2 vAnchorMax, bool bStayBefore = true)
        {
            if (RTS)
            {
                RTS.anchorMin = vAnchorMin;
                RTS.anchorMax = vAnchorMax;
                if (bStayBefore)
                {
                    RTS.offsetMin = RTS.offsetMax = Vector2.zero;
                }
            }
        }



        /// <summary>设置UI相对于父级的锚点</summary>
        /// <param name="RTS"></param>
        /// <param name="fStartHor"></param>
        /// <param name="fAreaHor"></param>
        /// <param name="fStartVer"></param>
        /// <param name="fAreaVer"></param>
        ///<param name="bClamp01"></param>
        /// <param name="bStartAsCenter"></param>
        /// <param name="bStayBefore">是否维持原有大小和位置</param>
        public static void SetRectByAnchor(this RectTransform RTS, float fStartHor, float fAreaHor, float fStartVer, float fAreaVer, bool bClamp01 = true, bool bStartAsCenter = false, bool bStayBefore = true)
        {
            if (RTS)
            {
                Vector2 anchorMin, anchorMax;

                fAreaHor = Mathf.Abs(fAreaHor);
                fAreaVer = Mathf.Abs(fAreaVer);

                //计算出极小值
                anchorMin.x = bStartAsCenter ? fStartHor - fAreaHor * 0.5f : fStartHor;
                anchorMin.y = bStartAsCenter ? fStartVer - fAreaVer * 0.5f : fStartVer;
                //计算出极大值
                if (!bClamp01)
                {
                    anchorMax.x = anchorMin.x + fAreaHor;
                    anchorMax.y = anchorMin.y + fAreaVer;
                }
                else
                {
                    anchorMin.x = Mathf.Clamp01(anchorMin.x);
                    anchorMin.y = Mathf.Clamp01(anchorMin.y);

                    anchorMax.x = Mathf.Clamp01(anchorMin.x + fAreaHor);
                    anchorMax.y = Mathf.Clamp01(anchorMin.y + fAreaVer);
                }

                RTS.anchorMin = anchorMin;
                RTS.anchorMax = anchorMax;


                if (bStayBefore)
                {
                    RTS.offsetMin = RTS.offsetMax = Vector2.zero;
                }
            }
        }



        #endregion

        #endregion


        #region Layer层
        /// <summary>判定是否包含指定层</summary>
        /// <param name="self"></param>
        /// <param name="Layer">指定层的Int值</param>
        /// <returns></returns>
        public static bool ContainsLayer(this LayerMask self, int Layer)
        {
            return (1 << Layer & self) != 0;
        }

        #endregion

    }
}
