using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float _timer = -1f;
    void Update()
    {
        if (_timer == -1f)
            return;
        _timer -= Time.deltaTime;
        if (_timer <= 0)
            Destroy(gameObject);
    }
    public void Trigger()
    {
        _timer = Time.deltaTime;
    }
}
