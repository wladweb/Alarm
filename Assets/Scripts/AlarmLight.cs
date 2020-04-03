using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AlarmLight : MonoBehaviour
{
    [SerializeField] private float _lightIntensity = 20;

    private Animator _animator;
    private Light _redLight;
    private Light _blueLight;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _redLight = transform.Find("Red").GetComponent<Light>();
        _blueLight = transform.Find("Blue").GetComponent<Light>();

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
