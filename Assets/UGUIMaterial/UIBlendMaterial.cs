using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace UGUIMaterial
{
    public class UIBlendMaterial : ImageMaterialHandleBase
    {
        public enum CustomBlendMode : byte
        {
            Normal = 0,
            LinearDodge,
            Darken,
            Multiply,
            Subtract, // 減算
            Lighten,
            BlendAdd, // 加算
        }

        [SerializeField] private CustomBlendMode _blendMode = CustomBlendMode.Normal;

        private readonly int _SrcFactor = Shader.PropertyToID("_SrcFactor");
        private readonly int _DstFactor = Shader.PropertyToID("_DstFactor");
        private readonly int _BlendOp = Shader.PropertyToID("_BlendOp");

        protected override void UpdateMaterial(Material baseMaterial)
        {
            if (material == null)
            {
                // material = new Material(Shader.Find("AI/UI/Blend"));
                material = new Material(Shader.Find("AI/UI/Blend"));
                material.enabledKeywords = new LocalKeyword[] { };
            }
            
            // 一旦キーワードをすべてオフに
            material.enabledKeywords = new LocalKeyword[] { };

            switch (_blendMode)
            {
                case CustomBlendMode.Normal: // 通常 (プリマルチプライドの透明)
                    material.SetInt(_SrcFactor, (int)BlendMode.One);
                    material.SetInt(_DstFactor, (int)BlendMode.OneMinusSrcAlpha);
                    material.SetInt(_BlendOp, (int)BlendOp.Add);
                    break;

                case CustomBlendMode.LinearDodge: // 覆い焼き(リニア) - 加算
                    material.SetInt(_SrcFactor, (int)BlendMode.SrcAlpha);
                    material.SetInt(_DstFactor, (int)BlendMode.One);
                    material.SetInt(_BlendOp, (int)BlendOp.Add);
                    break;

                case CustomBlendMode.Multiply: // 乗算
                    material.SetInt(_SrcFactor, (int)BlendMode.Zero);
                    material.SetInt(_DstFactor, (int)BlendMode.SrcColor);
                    material.SetInt(_BlendOp, (int)BlendOp.Add);
                    break;

                case CustomBlendMode.Darken: // 比較 (暗) 
                    material.SetInt(_SrcFactor, (int)BlendMode.One);
                    material.SetInt(_DstFactor, (int)BlendMode.One);
                    material.SetInt(_BlendOp, (int)BlendOp.Min);
                    break;

                case CustomBlendMode.Subtract: // 減算
                    material.SetInt(_SrcFactor, (int)BlendMode.One);
                    material.SetInt(_DstFactor, (int)BlendMode.One);
                    material.SetInt(_BlendOp, (int)BlendOp.ReverseSubtract);
                    break;

                case CustomBlendMode.Lighten: // 比較 (明)
                    material.SetInt(_SrcFactor, (int)BlendMode.One);
                    material.SetInt(_DstFactor, (int)BlendMode.One);
                    material.SetInt(_BlendOp, (int)BlendOp.Max);
                    break;
                case CustomBlendMode.BlendAdd: // 加算
                    material.SetInt(_SrcFactor, (int)BlendMode.One);
                    material.SetInt(_DstFactor, (int)BlendMode.OneMinusSrcAlpha);
                    material.SetInt(_BlendOp, (int)BlendOp.Add);
                    break;
            }

            material.EnableKeyword(_blendMode.ToString());
        }
    }
}