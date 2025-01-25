using Unity.Cinemachine;
using UnityEngine;

[ExecuteInEditMode]
public class CameraShake : CinemachineExtension
{
    [SerializeField] private float speed;
    [SerializeField] private float intensity;

    private float timer;

    private void DecreaseShakeTimer()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            intensity = 0;
        }
    }

    public void Shake(float _duration, float _intensity)
    {
        intensity = _intensity;

        timer = _duration;
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        DecreaseShakeTimer();

        Vector3 pos = state.RawPosition;

        Vector3 shake = new Vector3((Mathf.PerlinNoise(Time.time * speed, 0.5f) + 0.0352f) * 2 - 1, (Mathf.PerlinNoise(0.5f, Time.time * speed) + 0.0345f) * 2 - 1, 0f) * intensity;

        state.RawPosition = pos + shake;
    }
}
