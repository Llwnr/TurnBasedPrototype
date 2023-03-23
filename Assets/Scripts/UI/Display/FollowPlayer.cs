using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]private Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(transform.parent.transform.parent.transform.position + offset);
    }
}
