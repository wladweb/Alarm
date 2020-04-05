using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AlarmLight : MonoBehaviour
{
    [SerializeField] private float _lightIntensity = 20;
    [SerializeField] private Light _redLight;
    [SerializeField] private Light _blueLight;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        LightOff();
    }

    public void AlarmOn()
    {
        _animator.SetTrigger("On");
        LightOn();
    }

    public void AlarmOff()
    {
        _animator.SetTrigger("Off");
        LightOff();
    }

    private void LightOn()
    {
        _redLight.intensity = _lightIntensity;
        _blueLight.intensity = _lightIntensity;
    }

    private void LightOff()
    {
        _redLight.intensity = 0;
        _blueLight.intensity = 0;
    }
}
