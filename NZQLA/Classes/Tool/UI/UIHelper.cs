using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


namespace NZQLA.UI
{
    /// <summary></summary>
    public static class UIHelper
    {

        //---------------------------------set-----------------------------

        /// <summary>设置UGUI Text 的文本内容 会预判组件是否为空</summary>
        /// <param name="UIText">UGUI Text</param>
        /// <param name="Content">文本内容</param>
        public static void SetTextContentUGUI(Text UIText, string Content)
        {
            if (UIText != null)
            {
                UIText.text = Content;
            }
        }


        /// <summary>设置UGUI Text 的文本内容 会预判组件是否为空</summary>
        /// <param name="UIText"></param>
        /// <param name="Content"></param>
        public static void SetContent(this Text UIText, string Content)
        {
            if (UIText != null)
            {
                UIText.text = Content;
            }
        }





        #region EventHandler
        /// <summary>注册事件</summary>
        /// <param name="self"></param>
        /// <param name="onClick"></param>
        public static void RigisterOnClick(this Button self, UnityAction onClick)
        {
            if (self == null || onClick == null)
            {
                //>>>>>>>>>>>>>>>先这样<<<<<<<<<<<<<<<<<
                return;
            }

            self.onClick.AddListener(onClick);
        }


        /// <summary>移除事件</summary>
        /// <param name="self"></param>
        /// <param name="onClick"></param>
        public static void RemoveOnClick(this Button self, UnityAction onClick)
        {
            if (self == null || onClick == null)
            {
                //>>>>>>>>>>>>>>>先这样<<<<<<<<<<<<<<<<<
                return;
            }
            self.onClick.RemoveListener(onClick);
        }




        /// <summary>注册事件</summary>
        /// <param name="self"></param>
        /// <param name="onEndEdit"></param>
        public static void RigisterOnEndEdit(this InputField self, UnityAction<string> onEndEdit)
        {
            if (self == null || onEndEdit == null)
            {
                //>>>>>>>>>>>>>>>先这样<<<<<<<<<<<<<<<<<
                return;
            }

            self.onEndEdit.AddListener(onEndEdit);
        }


        /// <summary>移除事件</summary>
        /// <param name="self"></param>
        /// <param name="onEndEdit"></param>
        public static void RemoveOnEndEdit(this InputField self, UnityAction<string> onEndEdit)
        {
            if (self == null || onEndEdit == null)
            {
                //>>>>>>>>>>>>>>>先这样<<<<<<<<<<<<<<<<<
                return;
            }

            self.onEndEdit.RemoveListener(onEndEdit);
        }




        /// <summary>注册事件</summary>
        /// <param name="self"></param>
        /// <param name="onEndEdit"></param>
        public static void RigisterOnValueChanged(this Toggle self, UnityAction<bool> onEndEdit)
        {
            if (self == null || onEndEdit == null)
            {
                //>>>>>>>>>>>>>>>先这样<<<<<<<<<<<<<<<<<
                return;
            }

            self.onValueChanged.AddListener(onEndEdit);
        }


        /// <summary>移除事件</summary>
        /// <param name="self"></param>
        /// <param name="onValueChanged"></param>
        public static void RemoveOnValueChanged(this Toggle self, UnityAction<bool> onValueChanged)
        {
            if (self == null || onValueChanged == null)
            {
                //>>>>>>>>>>>>>>>先这样<<<<<<<<<<<<<<<<<
                return;
            }

            self.onValueChanged.RemoveListener(onValueChanged);
        }




        #endregion









    }
}
