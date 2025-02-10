using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAttackGround : MonoBehaviour
{

    [SerializeField] private float firstColliderDelaySpawn;
    [SerializeField] private float secondColliderDelaySpawn;
    [SerializeField] private float firstColliderDelayDeSpawn;
    [SerializeField] private float secondColliderDelayDeSpawn;
    [SerializeField] private GameObject Collider1;
    [SerializeField] private GameObject Collider2;
    private float counter = 0;

    private void Update()
    {
        if (counter > firstColliderDelaySpawn && counter < firstColliderDelayDeSpawn)
        {
            Collider1.SetActive(true);
        }
        if (counter > secondColliderDelaySpawn && counter < secondColliderDelayDeSpawn)
        {
            Collider2.SetActive(true);
        }

        if (counter > firstColliderDelayDeSpawn)
        {
            Collider1.SetActive(false);
        }
        if (counter > secondColliderDelayDeSpawn)
        {
            Collider2.SetActive(false);
        }

        if (counter > 5)
        {
            Destroy(this.gameObject);
        }

        counter += Time.deltaTime;
    }
}
