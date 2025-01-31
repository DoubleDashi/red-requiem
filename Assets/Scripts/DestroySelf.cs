using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 0;
    }

    public void EndSelf()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }
}
