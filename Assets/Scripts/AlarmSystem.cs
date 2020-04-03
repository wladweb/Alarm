using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private Doors _doors;
    
    private Animator _doorsAnimator;
    private Animator _animator;
    private Sensor _innerSensor;
    private Sensor _outerSensor;
    private bool _isSystemAlarmed;
    private AlarmLight[] _alarmLights;
    private AudioSource _alarmSound;

    private void Awake()
    {
        _innerSensor = transform.Find("InnerSensor").GetComponent<Sensor>();
        _outerSensor = transform.Find("OuterSensor").GetComponent<Sensor>();
        _doorsAnimator = _doors.GetComponent<Animator>();
        _animator = GetComponent<Animator>();
        _alarmLights = FindObjectsOfType<AlarmLight>();
        _alarmSound = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _innerSensor.SensorTriggered += InnerSensorHandler;
        _outerSensor.SensorTriggered += OuterSensorHandler;
    }

    private void InnerSensorHandler()
    {
        if (!_isSystemAlarmed)
        {
            SystemAlarm();
        }
    }

    private void OuterSensorHandler()
    {
        if (!_isSystemAlarmed)
        {
            _doorsAnimator.SetTrigger("Open");
        }
        else
        {
            _doorsAnimator.SetTrigger("Close");
            SystemDisalarm();
        }
    }

    private void OnDisable()
    {
        _innerSensor.SensorTriggered -= InnerSensorHandler;
        _outerSensor.SensorTriggered -= OuterSensorHandler;
    }

    private void SystemAlarm()
    {
        _isSystemAlarmed = true;

        foreach (AlarmLight light in _alarmLights)
        {
            light.AlarmOn();
        }

        PlayAlarmSound();
    }

    private void SystemDisalarm()
    {
        _isSystemAlarmed = false;

        foreach (AlarmLight light in _alarmLights)
        {
            light.AlarmOff();
        }

        StopAlarmSound();  
    }

    private void PlayAlarmSound()
    {
        _alarmSound.Play();
        _animator.SetTrigger("On");
    }

    private void StopAlarmSound()
    {
        _alarmSound.Stop();
        _animator.SetTrigger("Off");
    }
}
