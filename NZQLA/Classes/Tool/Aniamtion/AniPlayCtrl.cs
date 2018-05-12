using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace NZQLA
{
    /// <summary>
    /// 用于控制跨Animation的动画播放
    /// </summary>
    public class AniPlayCtrl : MonoBehaviour
    {
        /// <summary>实例</summary>
        public static AniPlayCtrl Ins;

        /// <summary>Animation组件</summary>
        [Header("指定Animation组件")]
        public Animation[] arrAni;


        //用于存放所有的动画片段与Animation的映射关系
        private Dictionary<string, Animation> dicAni = new Dictionary<string, Animation>();


#if UNITY_EDITOR//测试
        /// <summary>制定本需要播放的动画剪辑名称</summary>
        [Header("制定本需要播放的动画剪辑名称")]
        public string strAniNamePlay;

        /// <summary>逆向播放动画</summary>
        [Header("逆向播放动画")]
        public bool bPlayInverse;
#endif

        #region 外调

        /// <summary>
        /// 外调：播放指定的动画
        /// </summary>
        /// <param name="strAniClipName">指定想要播放的动画</param>
        /// <param name="_PlayInverse">是否是逆向播放</param>
        /// <param name="bAllowBusy">动画播放：繁忙中 是否允许强制播放</param>
        public void ActionPlayAni(string strAniClipName, bool _PlayInverse = false, bool bAllowBusy = false)
        {
            //处理繁忙
            if (!bAllowBusy)
            {
                if (isBusy())
                {
                    Debug.Log("<color=#ff0000ff>动画播放：繁忙中</color>");
                    return;
                }
            }

            if (dicAni.ContainsKey(strAniClipName))
            {
                //处理逆向播放周边
                dicAni[strAniClipName][strAniClipName].speed = _PlayInverse ? -1 : 1;
                if (_PlayInverse)
                    dicAni[strAniClipName][strAniClipName].time = dicAni[strAniClipName][strAniClipName].length;

                //播放指定动画剪辑
                dicAni[strAniClipName].Play(strAniClipName, PlayMode.StopAll);
            }
        }

        #endregion


        #region Unity 

        void Awake()
        {
            #region 初始化单例
            if (Ins == null)
            {
                Ins = this;

                //遍历Aniamtion数组并获取其内部的AnimationClip后录入词典
                GetAllClipsNameFromAnimation();
            }
            else if (Ins != this)
            {
                Destroy(gameObject);
            }

            #endregion
        }

        #endregion


        #region 测试

        [ContextMenu("测试播放指定的动画剪辑")]
        void TestPlayAni()
        {
            ActionPlayAni(strAniNamePlay, bPlayInverse);
        }

        #endregion



        #region 内部方法
        string[] GetClipsNameFromAnimation(Animation ani)
        {
            if (ani == null || ani.GetClipCount() == 0)
                return null;

            string[] arrNames = new string[ani.GetClipCount()];
            int i = 0;
            foreach (AnimationState item in ani)
            {
                arrNames[i++] = item.name;
            }

            return arrNames;
        }


        //[ContextMenu("遍历Aniamtion数组并获取其内部的AnimationClip后录入词典")]
        void GetAllClipsNameFromAnimation()
        {
            if (arrAni == null || arrAni.Length == 0)
                return;

            for (int i = 0; i < arrAni.Length; i++)
            {
                string[] arrNames = GetClipsNameFromAnimation(arrAni[i]);
                for (int j = 0; j < arrNames.Length; j++)
                {
                    if (dicAni.ContainsKey(arrNames[j]))
                        continue;

                    dicAni.Add(arrNames[j], arrAni[i]);
                }
            }

        }

        //判定是否处于繁忙状态 （有一个或以上的Animation处于播放状态）
        bool isBusy()
        {
            for (int i = 0; i < arrAni.Length; i++)
            {
                if (arrAni[i].isPlaying)
                    return true;
            }
            return false;
        }

        #endregion


    }
}
