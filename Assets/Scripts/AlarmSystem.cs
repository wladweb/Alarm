using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private Doors _doors;
    
    private Animator _doorsAnimator;
    private Sensor _innerSensor;
    private Sensor _outerSensor;
    private bool _isSystemAlarmed;
    private AlarmLight[] _alarmLights;

    private void Awake()
    {
        _innerSensor = transform.Find("InnerSensor").GetComponent<Sensor>();
        _outerSensor = transform.Find("OuterSensor").GetComponent<Sensor>();
        _doorsAnimator = _doors.GetComponent<Animator>();
        _alarmLights = FindObjectsOfType<AlarmLight>();
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
    }

    private void SystemDisalarm()
    {
        _isSystemAlarmed = false;

        foreach (AlarmLight light in _alarmLights)
        {
            light.AlarmOff();
        }
    }
}
