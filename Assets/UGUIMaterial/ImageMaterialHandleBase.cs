using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UGUIMaterial
{
    [ExecuteAlways]
    [RequireComponent(typeof(Graphics))]
    public class ImageMaterialHandleBase : MonoBehaviour, IMaterialModifier
    {
        [NonSerialized] private Graphic _targetGraphic;
        protected Material material;

        public Graphic TargetGraphic 
        {
            get
            {
                if (_targetGraphic == null)
                {
                    _targetGraphic = GetComponent<Graphic>();
                }
                return _targetGraphic;
            }
        }

        public Material GetModifiedMaterial(Material baseMaterial)
        {
            if (!isActiveAndEnabled || _targetGraphic == null)
            {
                return baseMaterial;
            }
            
            BlendOp
            
            UpdateMaterial(baseMaterial);
            return material;
        }

        private void OnDidApplyAnimationProperties()
        {
            if (!isActiveAndEnabled || _targetGraphic == null)
            {
                return;
            }

            _targetGraphic.SetMaterialDirty();
        }
        
        protected virtual void UpdateMaterial(Material baseMaterial)
        {}

        protected void OnEnable()
        {
            if (TargetGraphic == null)
            {
                return;
            }
            
            _targetGraphic.SetMaterialDirty();
        }

        protected void OnDisable()
        {
            if (material != null)
            {
                DestroyMaterial();
            }

            if (TargetGraphic != null)
            {
                _targetGraphic.SetMaterialDirty();
            }
        }

        public void DestroyMaterial()
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying == false)
            {
                DestroyImmediate(material);
                material = null;
                return;
            }
#endif
            Destroy(material);
            material = null;
        }

#if UNITY_EDITOR
        protected void OnValidate()
        {
            if (!isActiveAndEnabled || TargetGraphic == null)
            {
                return;
            }
            TargetGraphic.SetMaterialDirty();
        }
#endif
    }
}