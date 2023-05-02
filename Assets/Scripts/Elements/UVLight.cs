using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UVLight : MonoBehaviour
{
    public enum LightStatus
    {
        regular,
        focused,
    }
    [SerializeField] private Light2D light2D;

    [SerializeField] private int _focusDamage;
    [SerializeField] private Color _focusColor;
    [SerializeField] private float _focusIntensity;
    [SerializeField] private float _focusAngleReduction;

    private float _baseIntensity;
    private float _baseInnerAngle, _baseOuterAngle;
    private Color _baseColor;
        public int FocusDamage => _focusDamage;

    private void Start()
    {
        _baseInnerAngle = light2D.pointLightInnerAngle;
        _baseOuterAngle = light2D.pointLightOuterAngle;
        _baseColor = light2D.color;
        _baseIntensity = light2D.intensity;
    }

    public void SetStatus(LightStatus status) 
    { 
        _status = status;;
        if(status == LightStatus.focused)
        {
            light2D.color = _focusColor;
            light2D.intensity = _focusIntensity;
            light2D.pointLightInnerAngle = _baseInnerAngle - _focusAngleReduction;
            light2D.pointLightOuterAngle = _baseOuterAngle - _focusAngleReduction;
        }
        else if(status == LightStatus.regular) 
        {
            light2D.color = _baseColor;
            light2D.intensity = _baseIntensity;
            light2D.pointLightInnerAngle = _baseInnerAngle;
            light2D.pointLightOuterAngle = _baseOuterAngle;
        }
    }
    private LightStatus _status;
        public LightStatus Status => _status;
}
