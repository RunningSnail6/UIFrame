﻿using System;
using UnityEngine;

namespace UIFrameWork
{
    public abstract class Singleton<T> where T:class,new()
    {
        protected static T _Instance = null;

        public static T Instance
        {
            get
            {
                if(null == _Instance)
                {
                    _Instance = new T();
                }
                return _Instance;
            }
        }
        protected Singleton()
        {
            if (null != _Instance)
                throw new SingletonException("This" + (typeof(T)).ToString() + "Singleton Instance is not null !!!");
            Init();
        }

        public virtual void Init()
        {

        }
    }
}
