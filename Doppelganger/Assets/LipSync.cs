using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LipSync : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] SkinnedMeshRenderer teethMeshRenderer;
    [SerializeField] float randomizeInterval = 2.0f;
    [SerializeField] bool talk;
    [SerializeField] float lipSyncSpeed;
    [SerializeField] bool verbose;

    SkinnedMeshRenderer skinnedMeshRenderer;
    int jawOpenBlendshapeIndex = 0; 
    int smileIndex = 1;
    float currentMaxJawOpenValue;
    float lastRandomizeTime;
    float openAmountMouth;
    float openAmountTeeth;
    

    void Start()
    {
        // Acc�der au Skinned Mesh Renderer
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

        // Assurer que l'AudioSource est assign�
        if (audioSource == null)
        {
            Debug.LogError("AudioSource non assign� !");
        }

        if (teethMeshRenderer == null)
        {
            Debug.LogError("teethMeshRenderer non assign� !");
        }

        talk = false;
    }


    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            talk = !talk;
        }

        if (talk)
        {
            // Randomiser la valeur maximale d'ouverture toutes les `randomizeInterval` secondes
            if (Time.time - lastRandomizeTime >= randomizeInterval)
            {
                RandomizeJawOpenValue();
                lastRandomizeTime = Time.time;
            }

            openAmountMouth = Mathf.PingPong(Time.time * (1f / lipSyncSpeed), currentMaxJawOpenValue);
            openAmountTeeth = openAmountMouth / 2;
           
            if (verbose)
            {
                Debug.Log("amount : " + openAmountMouth);
                Debug.Log("amount Teeth : " + openAmountTeeth);
            }


            skinnedMeshRenderer.SetBlendShapeWeight(jawOpenBlendshapeIndex, openAmountMouth);
            teethMeshRenderer.SetBlendShapeWeight(jawOpenBlendshapeIndex, openAmountTeeth);
        }
        else
        {
            skinnedMeshRenderer.SetBlendShapeWeight(jawOpenBlendshapeIndex, 0);
            teethMeshRenderer.SetBlendShapeWeight(jawOpenBlendshapeIndex, 0);
        }
    }

    private void RandomizeJawOpenValue()
    {
        // D�finir une nouvelle valeur al�atoire entre minJawOpenValue et maxJawOpenValueRange
        currentMaxJawOpenValue = UnityEngine.Random.Range(0.3f, 1.1f);
    }
}

