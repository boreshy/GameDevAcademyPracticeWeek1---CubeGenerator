using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RecoloringBehavior : MonoBehaviour
{
    [SerializeField] private float _recoloringDuration = 2f;
    [SerializeField] private float _colorChangeDelay = 1f;

    private Color _startColor;
    private Color _nextColor;
    private Renderer _renderer;

    private float _recoloringTime;
    private float _colorChangeTime;

    void Awake()
    {
        _renderer = GetComponent<Renderer>();
        GenerateNextColor();
        _startColor = _renderer.material.color;
    }

    private void Update()
    {
        _recoloringTime += Time.deltaTime;

        if (_recoloringTime >= _recoloringDuration)
        {
            if (Time.time - _colorChangeTime < _colorChangeDelay)
                return;

            _recoloringTime = 0f;
            GenerateNextColor();
            _colorChangeTime = Time.time;
        }

        var progress = _recoloringTime / _recoloringDuration;
        var currentColor = Color.Lerp(_startColor, _nextColor, progress);
        _renderer.material.color = currentColor;
    }

    private void GenerateNextColor()
    {
        _nextColor = Random.ColorHSV(0f, 1f, 0.8f, 1f, 1f, 1f);
    }
}