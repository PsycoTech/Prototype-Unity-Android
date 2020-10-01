using UnityEngine;
public class testLabel : MonoBehaviour
{
    void Start()
    {
        feedback_label.Instance.RegisterMessage(transform, gameObject.name);
    }
}
