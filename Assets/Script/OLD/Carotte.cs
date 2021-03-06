﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carotte : MonoBehaviour
{

    private GameObject Joueur;
    bool Run = false;
    public float speed = 5f;
    public GameObject Bouf;
    private float DistanceExpl = 2f;
    private Vector3 RunTo;
    private bool HasExploded = false;
    private Animator animator;
    private Vector3 prevPos;
    private Vector3 nowPos;
    // Start is called before the first frame update
    void Start()
    {
        Joueur = GameObject.FindGameObjectsWithTag("Player")[0];
        InvokeRepeating("WatchUntilExplosion", 1f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!HasExploded)
        {
            //transform.LookAt(Joueur.transform);
            if (Run)
            {
                transform.position = Vector3.MoveTowards(transform.position, RunTo, speed * Time.deltaTime);
            }
            if (Vector3.Distance(Joueur.transform.position, transform.position) < DistanceExpl) Explosion();
        }

        nowPos = transform.position;
        float TempHor = prevPos.x - nowPos.x;
        float TempVer = prevPos.y - nowPos.y;
        
        if (((TempHor > 0 && TempHor < 0.1f) || (TempHor < 0 && TempHor > -0.1f)) && TempVer > 0)
        {
            animator.SetBool("isFace", true);
            animator.SetBool("isBack", false);
        }

        if (((TempHor > 0 && TempHor < 0.1f) || (TempHor < 0 && TempHor > -0.1f)) && TempVer < 0)
        {
            animator.SetBool("isFace", false);
            animator.SetBool("isBack", true);
        }


        prevPos = nowPos;
    }

    void WatchUntilExplosion()
    {
        Run = true;
        RunTo = Joueur.transform.position;
    }

    private void Explosion()
    {
        for (int i = 0; i < Random.Range(2,5); i++)
        {
            GameObject TempBouf = GameObject.Instantiate(Bouf, transform.position, transform.rotation);
            TempBouf.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5f, 5f), (Random.Range(-5f, 5f))),ForceMode2D.Impulse);    
        }
        HasExploded = true;
        Destroy(this.gameObject);
    }
}
