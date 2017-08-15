﻿using Merlin.API.Direct;
//using Merlin.API;
using System;
using UnityEngine;

namespace Merlin
{
    public abstract class Profile : MonoBehaviour
    {
        #region Static

        public static TimeSpan UpdateDelay = TimeSpan.FromSeconds(0.1d);

        #endregion Static

        #region Fields

        protected GameManager _client;
        protected ObjectManager _world;
        protected LandscapeManager _landscape;
        protected LocalPlayerCharacterView _localPlayerCharacterView;

        private DateTime _nextUpdate;
        private bool refresh;
        #endregion Fields

        #region Properties and Events

        public abstract string Name { get; }

        #endregion Properties and Events

        #region Methods

        /// <summary>
        /// Called when this instance is enabled.
        /// </summary>
        private void OnEnable()
        {
            _client = GameManager.GetInstance();
            _world = ObjectManager.GetInstance();
            _landscape = _client.GetLandscapeManager();
            _localPlayerCharacterView = _client.GetLocalPlayerCharacterView();
            _nextUpdate = DateTime.Now;
        }

        private void Awake()
        {
        }

        private void Start()
        {
            OnStart();
        }

        private void Stop()
        {
        }

        /// <summary>
        /// Called when this instance is disabled.
        /// </summary>
        private void OnDisable()
        {
            OnStop();

            _client = null;
        }

        /// <summary>
        /// Called when this instance is updated.
        /// </summary>
        private void Update()
        {
            if (_client.GetState() == (ajz)API.GameState.Playing)
            {
                if (refresh)
                {
                    _client = GameManager.GetInstance();
                    _world = ObjectManager.GetInstance();
                    _landscape = _client.GetLandscapeManager();
                    _localPlayerCharacterView = _client.GetLocalPlayerCharacterView();
                    refresh = false;
                }
                if (DateTime.Now < _nextUpdate)
                    return;

                OnUpdate();

                _nextUpdate = DateTime.Now + UpdateDelay;
            }
            else
            {
                refresh = true;
            }
        }

        /// <summary>
        /// Called when the GUI is rendered.
        /// </summary>
        private void OnGUI()
        {
            OnUI();
        }

        /// <summary>
        /// Called when this instance is started.
        /// </summary>
        protected virtual void OnStart()
        {
        }

        /// <summary>
        /// Called when this instance is stopped.
        /// </summary>
        protected virtual void OnStop()
        {
        }

        protected virtual void OnUpdate()
        {
        }

        protected virtual void OnUI()
        {
        }

        #endregion Methods
    }
}