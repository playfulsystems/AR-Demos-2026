using UnityEngine;

public class RemoveOnTriggerWithTag : MonoBehaviour
{
    public string strTag;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == strTag)
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
