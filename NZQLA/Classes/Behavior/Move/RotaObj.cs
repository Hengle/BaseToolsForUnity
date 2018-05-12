//**********************************
//AuthorName: NZQLA
//创建时间: #CreateTime#
//备注：修改注释
//**********************************
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NZQLA
{
    public class RotaObj : MonoBehaviour
    {
        [SerializeField]
        private RotaSettingData data;

        void Update()
        {
            Rota();
        }


        Vector3 GetDir()
        {
            switch (data.dir)
            {
                case Dir.Up:
                    return data.bLocal ? Vector3.up : transform.up;
                case Dir.Forward:
                    return data.bLocal ? Vector3.forward : transform.forward;
                case Dir.Right:
                    return data.bLocal ? Vector3.right : transform.right;
                default:
                    break;
            }
            return Vector3.up;
        }


        void Rota()
        {
            transform.Rotate(GetDir(), data.fSpeed*(data.bInverse?-1:1));
        }
    }

    public enum Dir
    {
        Up,
        Forward,
        Right,
    }

    [Serializable]
    public class RotaSettingData
    {
        public Dir dir = Dir.Forward;
        public float fSpeed = 10;
        public bool bLocal = true;
        public bool bInverse = false;

        public RotaSettingData() { }

        public RotaSettingData(float fSpeed = 1, Dir dir = Dir.Forward,bool bLocal= true, bool bInverse = false) {
            this.fSpeed = fSpeed;
            this.dir = dir;
            this.bLocal = bLocal;
            this.bInverse = bInverse;
        }

    }
}
