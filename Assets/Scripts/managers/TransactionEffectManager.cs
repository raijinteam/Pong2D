using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransactionEffectManager : MonoBehaviour
{
    public static TransactionEffectManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private Camera camera;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int coinsParticalCount;
    //[SerializeField] private ParticleSystem pf_CoinPS;
    public GameObject pf_CoinPS;
    [SerializeField] private RectTransform rt_CoinSpawnParent;
    [SerializeField] private Transform spawnParent;


    private int coinAmount;


    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) { }
            //SpawnCoinsParticalEffect(0);
    }




    public void SpawnCoinsParticalEffect(int particalCount)
    {
        /*ParticleSystem.Burst buastCount= pf_CoinPS.emission.GetBurst(0);
        buastCount.count = particalCount;

        ParticleSystem.MainModule mainModule = pf_CoinPS.main;
        mainModule.gravityModifier = 40;

        pf_CoinPS.emission.SetBurst( 0, buastCount);

       

        Debug.Log("Is playing : " + pf_CoinPS.isPlaying);
        coinAmount = particalCount;*/
        Debug.Log("Play");

       // pf_CoinPS.GetComponent<ParticleSystem>().Play();
        Debug.Log("Is playing : " + pf_CoinPS.GetComponent<ParticleSystem>().isPlaying);
       // StartCoroutine(MoveTorwadsParent());
    }

   // private IEnumerator MoveTorwadsParent()
  //  {
        //yield return new WaitForSeconds(0.3f);
/*
        Debug.Log("Is playing : " + pf_CoinPS.isPlaying);
        ParticleSystem.Particle[] particals = new ParticleSystem.Particle[coinAmount];

        ParticleSystem.MainModule mainModule = pf_CoinPS.main;
        mainModule.gravityModifier = 0;


        Vector3 positin = rt_CoinSpawnParent.position;
        Debug.Log("Rect Transform Position : " + positin);


        Debug.Log("ps is playing : " + pf_CoinPS.isPlaying);


        pf_CoinPS.GetParticles(particals);

        while (pf_CoinPS.isPlaying)
        {
            Debug.Log("in while");
            for (int i = 0; i < particals.Length; i++)
            {
                pf_CoinPS.GetParticles(particals);
              //  particals[i].position = Vector2.MoveTowards(particals[i].position, positin, moveSpeed * Time.deltaTime);
                pf_CoinPS.SetParticles(particals);
            }
            yield return null;
        }

        pf_CoinPS.SetParticles(particals);*/
    //}
}
