using UnityEngine;

public class IdleAnimation : MonoBehaviour
{
    [SerializeField]
    private float Tick = 0.7f;

    [SerializeField]
    private float MinScale = 0.95f;

    [SerializeField]
    private float MaxScale = 1f;

    private float _timer = 0f;
    private int _scaleFactor = 1;

    private void Update()
    {
        _timer += Time.deltaTime * _scaleFactor;

        if (_timer >= Tick)
        {
            _timer = Tick;
            _scaleFactor = -1;
        }
        else if (_timer <= 0)
        {
            _timer = 0;
            _scaleFactor = 1;
        }

        transform.localScale = new Vector3(1f * (MinScale + ((MaxScale - MinScale) * (_timer / Tick))), 1f, 1f);
    }
}
