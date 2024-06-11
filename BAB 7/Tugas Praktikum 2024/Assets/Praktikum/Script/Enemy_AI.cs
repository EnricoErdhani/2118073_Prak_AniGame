using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy_AI : MonoBehaviour
{
    public float speed; // Kecepatan gerakan musuh
    public float lineOfSite; // Jarak penglihatan musuh
    private Transform player; // Transform dari pemain
    private Vector2 initialPosition; // Posisi awal musuh
    private bool facingRight = false; // Menunjukkan apakah musuh menghadap ke kanan

    // Use this for initialization
    void Start()
    {
        // Mencari pemain berdasarkan tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // Menyimpan posisi awal musuh
        initialPosition = GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        // Menghitung jarak antara musuh dan pemain
        float distanceToPlayer = Vector2.Distance(player.position, transform.position);

        // Jika pemain berada dalam jarak penglihatan musuh
        if (distanceToPlayer < lineOfSite)
        {
            // Musuh bergerak menuju pemain
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
            FacePlayer(); // Memutar musuh untuk menghadap pemain
        }
        else
        {
            // Musuh kembali ke posisi awal
            transform.position = Vector2.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);
            FaceInitialPosition(); // Memutar musuh untuk menghadap posisi awal jika diperlukan
        }
    }

    // Memutar musuh untuk menghadap pemain
    void FacePlayer()
    {
        if (player.position.x > transform.position.x && facingRight)
        {
            Flip();
        }
        else if (player.position.x < transform.position.x && !facingRight)
        {
            Flip();
        }
    }

    // Memutar musuh untuk menghadap posisi awal jika diperlukan
    void FaceInitialPosition()
    {
        if (initialPosition.x < transform.position.x && facingRight)
        {
            Flip();
        }
        else if (initialPosition.x > transform.position.x && !facingRight)
        {
            Flip();
        }
    }

    // Membalik orientasi musuh
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    // Untuk menggambar jarak penglihatan musuh di editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }
}
