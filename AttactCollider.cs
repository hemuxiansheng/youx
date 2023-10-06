using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // Start is called before the first frame update
    public class AttactCollider : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.tag == "Enemy")
            {
                Destroy(collision.gameObject);
            }
        }
    }
        