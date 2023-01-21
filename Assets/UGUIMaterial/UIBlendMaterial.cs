using UnityEngine;

namespace UGUIMaterial
{
    public class UIBlendMaterial : ImageMaterialHandleBase
    {
        public enum MyCustomBlendMode : byte
        {
            Normal,
            Patern1,
            
        }

        [SerializeField] private MyCustomBlendMode _blendMode = MyCustomBlendMode.Normal;

        private readonly int _SrcFactor = Shader.PropertyToID("_SrcFactor");
        private readonly int _DstFactor = Shader.PropertyToID("_DstFactor");
        private readonly int _BlendOp = Shader.PropertyToID("_DstFactor");
    }
}