/* 
 * Full Name        : Abdulhakeem Idris
 * StudentID        : 101278361
 * Date Modified    : December 17, 2021
 * File             : FloatingPlatformController.cs
 * Description      : This script controls the floating Platform
 * Revision History : Version01
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatformController : MonoBehaviour
{
    PlayerBehaviour player;
    float platformTimer = 0f;
    public float ScaleSpeed = 1f;
    public float ActivateTimer = 2f;
    bool shrinkActivate = false;

    Vector3 floatEffect;
    Transform floatStart;

    Vector3 OriginalScale;
    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();
        OriginalScale = transform.localScale;
        floatEffect = new Vector3(0, 3f, 0);
        floatStart = transform;
    }

    void Update()
    {
        //FloatingMovement();

        ScalePlatform();
    }

    void ScalePlatform()
    {
        if(shrinkActivate == true)
        {
            Vector3 newScale = transform.localScale;
            newScale.x -= Time.deltaTime * ScaleSpeed;
            if (transform.localScale.x > 0f)
            {
                player.sounds[(int)ImpulseSounds.FlOATINGPLATFORM].Play();
                transform.localScale = newScale;
            }
        }
        else if (shrinkActivate == false)
        {
            Vector3 newScale = transform.localScale;
            newScale.x += Time.deltaTime * ScaleSpeed;
            if (transform.localScale.x < 1f)
            {
                transform.localScale = newScale;
            }
        }
    }

    private void FloatingMovement()
    {
        //platformTimer += Time.deltaTime;
        //var distanceX = (floatEffect.x > 0) ? 
        //    floatStart.position.x + Mathf.PingPong(platformTimer, floatEffect.x) : floatStart.position.x;
        //var distanceY = (floatEffect.y > 0) ? 
        //    floatStart.position.y + Mathf.PingPong(platformTimer, floatEffect.y) : floatStart.position.y;
        //transform.position = new Vector3(distanceX, distanceY, 0.0f);

        Debug.Log(Mathf.PingPong(0, 5f));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SetActivate(true));
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SetActivate(false));
            player.sounds[(int)ImpulseSounds.GEM].Stop();
        }
    }

    IEnumerator SetActivate(bool value)
    {
        yield return new WaitForSeconds(ActivateTimer);
        Debug.Log("Change");
        shrinkActivate = value;
    }

    public void Reset()
    {
        transform.localScale = OriginalScale;
        shrinkActivate = false;
    }
}
