using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour {
    private float normalTimeScale = 1f;
    
    private void Start() {
        ResetTimeScale();
    }
    
    public void ResetTimeScale() {
        Time.timeScale = normalTimeScale;
    }
    
    public void SetTimeScale(float timeScale) {
        Time.timeScale = timeScale;
    }
    
    public void SetTimeScaleForDuration(float timeScale, float duration) {
        StartCoroutine(HandleTimeChangeForDuration(timeScale, duration));
    }
    
    public void SlowDownTimeTwiceForDuration(float duration) {
        SetTimeScaleForDuration(0.5f, duration);
    }
    
    public void SpeedUpTimeTwiceForDuration(float duration) {
        SetTimeScaleForDuration(2f, duration);
    }
    
    private IEnumerator HandleTimeChangeForDuration(float newTimeScale, float duration) {
        SetTimeScale(newTimeScale);
        yield return new WaitForSecondsRealtime(duration);
        ResetTimeScale();
    }
    
}