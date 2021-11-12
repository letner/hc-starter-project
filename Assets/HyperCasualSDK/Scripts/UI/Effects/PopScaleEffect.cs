using UnityEngine;

public class PopScaleEffect : MonoBehaviour
{
    public Vector2 scaleRange;
    public float effectDuration;

    private RectTransform _rectTransform;
    private bool _isPlaying;
    private float _startTime;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (_isPlaying) {
            var time = Time.time - _startTime;
            var scale = (Mathf.Sin(time * Mathf.PI / effectDuration) + scaleRange.x) * (scaleRange.y - scaleRange.x) + scaleRange.x;
            if (time > effectDuration) {
                scale = scaleRange.x;
            }
            _rectTransform.localScale = new Vector3(scale, scale, scale);
        }
    }

    public void Play()
    {
        _isPlaying = true;
        _startTime = Time.time;
    }
}
