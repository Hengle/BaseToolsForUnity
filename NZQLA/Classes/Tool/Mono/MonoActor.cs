using UnityEngine;
using System.Collections;
using NZQLA;
using System;

namespace NZQLA.Mono
{
    /// <summary></summary>
    public delegate void MonoUpdaterEvent();

    /// <summary></summary>
    public class MonoActor : MonoSingtonAuto<MonoActor>
    {

        //===========================================================

        private event MonoUpdaterEvent UpdateHandler;
        private event MonoUpdaterEvent FixedUpdateHandler;
        private event MonoUpdaterEvent LateUpdateHandler;


        /// <summary></summary>
        public void AddUpdateListener(MonoUpdaterEvent listener)
        {
            UpdateHandler += listener;
        }

        /// <summary></summary>
        public void RemoveUpdateListener(MonoUpdaterEvent listener)
        {
            UpdateHandler -= listener;
        }

        /// <summary></summary>
        public void AddLateUpdateListener(MonoUpdaterEvent listener)
        {
            LateUpdateHandler += listener;
        }

        /// <summary></summary>
        public void RemoveLateUpdateListener(MonoUpdaterEvent listener)
        {
            LateUpdateHandler -= listener;
        }


        /// <summary></summary>
        public void AddFixedUpdateListener(MonoUpdaterEvent listener)
        {
            FixedUpdateHandler += listener;
        }

        /// <summary></summary>
        public void RemoveFixedUpdateListener(MonoUpdaterEvent listener)
        {
            FixedUpdateHandler -= listener;
        }



        void Update()
        {
            if (UpdateHandler != null)
            {
                try
                {
                    UpdateHandler();
                }
                catch (Exception e)
                {
                    Log.LogAtUnityEditorError(string.Format("{0}(Update):{1}", typeof(MonoActor).Name, e.Message));
                }
            }
        }

        void FixedUpdate()
        {
            if (FixedUpdateHandler != null)
            {
                try
                {
                    FixedUpdateHandler();
                }
                catch (Exception e)
                {
                    Log.LogAtUnityEditorError(string.Format("{0}(FixedUpdate):{1}", typeof(MonoActor).Name, e.Message));
                }
            }
        }

        void LateUpdate()
        {
            if (LateUpdateHandler != null)
            {
                try
                {
                    LateUpdateHandler();
                }
                catch (Exception e)
                {
                    Log.LogAtUnityEditorError(string.Format("{0}(LateUpdate):{1}", typeof(MonoActor).Name, e.Message));
                }
            }
        }

        //===========================================================

        /// <summary></summary>
        public void StartMonoCoroutine(IEnumerator routine)
        {
            MonoBehaviour mono = GetIns();
            mono.StartCoroutine(routine);
        }

    }



}
