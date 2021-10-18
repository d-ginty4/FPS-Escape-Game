using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    static Transform player;
    NavMeshAgent navMesh;
    float playerDistance;
    public int enemyHealth = 100;
    public static int enemyDamage = 20;
    bool shot = false;
    bool alive = true;
    Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().GetComponent<Transform>();
        navMesh = GetComponent<NavMeshAgent>();
        healthBar = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
        EnemyDeath();
        ControlHealthBar();
    }

    private void EnemyDeath()
    {
        if(enemyHealth <= 0)
        {
            print("Enemy dead");
        }
    }

    private void ChasePlayer()
    {
        if (alive)
        {
            playerDistance = Vector3.Distance(transform.position, player.position);
            if (playerDistance <= 10f || shot == true)
            {
                GetComponent<Animator>().SetBool("Chase", true);
                navMesh.SetDestination(player.position);
                if (playerDistance <= navMesh.stoppingDistance)
                {
                    GetComponent<Animator>().SetBool("Attack", true);
                    transform.LookAt(player);
                }
                else
                {
                    GetComponent<Animator>().SetBool("Attack", false);
                }
            }
            else
            {
                GetComponent<Animator>().SetBool("Chase", false);
            }
        }
    }

    // called from enemy attach animation 
    private void DamagePlayer()
    {
        Player.playerHealth -= enemyDamage;
    }

    public void TakeDamage(int damage)
    {
        if (alive)
        {
            shot = true;
            enemyHealth -= damage;
            if (enemyHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        if (!alive) return;
        GetComponent<Animator>().SetTrigger("Die");
        alive = false;
    }

    public void ControlHealthBar()
    {
        healthBar.value = enemyHealth;
    }
}
