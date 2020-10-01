using UnityEngine;
public class feedback_action : MonoBehaviour
{
    // public static feedback_action Instance;
    // void Awake()
    // {
    //     if (Instance == null)
    //         Instance = this;
    //     else
    //         Destroy(gameObject);
    // }
    public void Disable()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
        transform.SetParent(null);
    }
    public void Initialize(Transform target, float radius, Color color)
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
        transform.localScale = Vector2.one * radius * 2f;
        GetComponent<SpriteRenderer>().color = color;
        transform.SetParent(target);
        transform.localPosition = Vector3.zero;
    }
}
