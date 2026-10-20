using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private float deactivateTime;

    public void Activate(Vector3 position, float size, float lifetime)
    {
        transform.position = position;
        transform.localScale = Vector3.one * size;
        deactivateTime = Time.time + lifetime;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Time.time >= deactivateTime)
            Deactivate();
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
