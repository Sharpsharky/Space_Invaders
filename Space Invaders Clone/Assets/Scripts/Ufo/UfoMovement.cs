using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1;

    private int directionSign;

    public void SetDirection(int sign)
    {
        directionSign = sign;
    }

    private void Update()
    {
        transform.Translate(transform.right * directionSign * speed * Time.deltaTime);
    }

}
