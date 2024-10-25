using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LipSync : MonoBehaviour
{
    public AudioSource audioSource;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private SkinnedMeshRenderer teethMeshRenderer;
    private int jawOpenBlendshapeIndex = 0; // Indice du blendshape pour la bouche ouverte
    public int mutliplicatorAmplitude;
    private float[] audioSamples = new float[256];

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
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {
            skinnedMeshRenderer.SetBlendShapeWeight(1,0.2f); // Sourire si le son est fort
        }

        // Obtenir les donn�es spectrales de l'audio
        audioSource.GetOutputData(audioSamples, 0);

        // Calculer l'amplitude moyenne
        float averageAmplitude = 0f;
        foreach (float sample in audioSamples)
        {
            averageAmplitude += Mathf.Abs(sample);
        }
        averageAmplitude /= audioSamples.Length;

        // Mapper l'amplitude � un mouvement de la bouche
        float jawWeight = Mathf.Clamp(averageAmplitude * mutliplicatorAmplitude, 0, 1);
        float jawWeightTeeth = Mathf.Clamp(averageAmplitude * 1000, 0, 0.5f);// Ajuste le multiplicateur selon le volume audio
        skinnedMeshRenderer.SetBlendShapeWeight(jawOpenBlendshapeIndex, jawWeight);
        teethMeshRenderer.SetBlendShapeWeight(jawOpenBlendshapeIndex, jawWeightTeeth);
    }
}

