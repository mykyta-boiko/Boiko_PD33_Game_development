﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCancel : MonoBehaviour
{
        [SerializeField] private GameObject _currentMenu;
        [SerializeField] private Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(OnButtonClickHandler);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
                OnButtonClickHandler();
        }

        private void OnButtonClickHandler()
        {
            _currentMenu.SetActive(false);
        }
}
