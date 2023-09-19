using Assets.WasapiAudio.Scripts.Unity;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "SpectrumTransformerScale", menuName = "SpectrumTransformer/Scaler", order = 0)]
    public class SpectrumTransformerScale : SpectrumTransformer
    {
        [SerializeField] private float scale = 1f;
        
        protected override void PerformTransform(float[] input, ref float[] output)
        {
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = input[i] * scale;
            }
        }
    }
}