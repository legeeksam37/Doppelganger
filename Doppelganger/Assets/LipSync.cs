using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LipSync : MonoBehaviour
{
    public AudioSource audioSource;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private SkinnedMeshRenderer teethMeshRenderer;
    private int jawOpenBlendshapeIndex = 0; 
    private int teethOpenBlendshapeIndex = 1;
    public int mutliplicatorAmplitudeMouth;
    public int mutliplicatorAmplitudeTeeth;

#if UNITY_WEBGL
    private float[] audioSpectrum = new float[256];
#else
    private float[] audioSamples = new float[256];
#endif
    void Start()
    {
        // Accéder au Skinned Mesh Renderer
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

        // Assurer que l'AudioSource est assigné
        if (audioSource == null)
        {
            Debug.LogError("AudioSource non assigné !");
        }

        if (teethMeshRenderer == null)
        {
            Debug.LogError("teethMeshRenderer non assigné !");
        }
    }

    void Update()
    {

        /* if (Input.GetKeyDown(KeyCode.T))
         {
             skinnedMeshRenderer.SetBlendShapeWeight(jawOpenBlendshapeIndex,0.2f); // Sourire si le son est fort
         }*/

#if UNITY_WEBGL
        audioSource.GetSpectrumData(audioSpectrum, 0, FFTWindow.Rectangular);

        float averageAmplitude = 0f;
        foreach (float spectrumValue in audioSpectrum)
        {
            averageAmplitude += Mathf.Abs(spectrumValue);
        }
        averageAmplitude /= audioSpectrum.Length;

#else
        audioSource.GetOutputData(audioSamples, 0);

        float averageAmplitude = 0f;
        foreach (float sample in audioSamples)
        {
            averageAmplitude += Mathf.Abs(sample);
        }
        averageAmplitude /= audioSamples.Length;
#endif

        float jawWeight = Mathf.Clamp(averageAmplitude * mutliplicatorAmplitudeMouth, 0, 1);
        skinnedMeshRenderer.SetBlendShapeWeight(jawOpenBlendshapeIndex, jawWeight);

        float jawWeightTeeth = Mathf.Clamp(averageAmplitude * mutliplicatorAmplitudeTeeth, 0, 0.5f);// Ajuste le multiplicateur selon le volume audio
        teethMeshRenderer.SetBlendShapeWeight(jawOpenBlendshapeIndex, jawWeightTeeth);
    }
}

