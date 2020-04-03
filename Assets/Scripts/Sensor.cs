using UnityEngine;
using UnityEngine.Events;

public class Sensor : MonoBehaviour
{
    [SerializeField] private UnityEvent _sensorTriggered;

    public event UnityAction SensorTriggered
    {
        add => _sensorTriggered.AddListener(value);
        remove => _sensorTriggered.RemoveListener(value);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Robber>(out Robber robber))
        {
            _sensorTriggered.Invoke();
        }
    }
}
