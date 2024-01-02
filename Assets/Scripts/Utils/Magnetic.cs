using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    public float dist = .2f;

    public float CoinSpeed = 3f;

    private void Update()
    {
        if(Vector3.Distance(transform.position,Player.instance.transform.position) > dist)
        {
            CoinSpeed++;
            transform.position = Vector3.MoveTowards(transform.position, Player.instance.transform.position, Time.deltaTime * CoinSpeed);
        }
    }
}
