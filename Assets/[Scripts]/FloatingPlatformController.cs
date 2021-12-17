using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatformController : MonoBehaviour
{
    public PlayerBehaviour player;
    public float ScaleReductionSpeed = 1f;

    bool shrinkActivate = false;

    Vector3 OriginalScale;
    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();
        OriginalScale = transform.localScale;
    }

    void Update()
    {
        Vector3 newScale = transform.localScale;
        newScale.x -= Time.deltaTime * ScaleReductionSpeed;
        if(shrinkActivate == true && transform.localScale.x > 0f)
        {
            transform.localScale = newScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            shrinkActivate = true;
        }
    }

    public void Reset()
    {
        transform.localScale = OriginalScale;
        shrinkActivate = false;
    }
}
