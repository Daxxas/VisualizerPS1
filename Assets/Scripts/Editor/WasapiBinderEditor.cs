using UnityEditor;
using UnityEngine;

namespace DefaultNamespace.Editor
{
    [CustomEditor(typeof(WasapiBinder))]
    public class WasapiBinderEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            var wasapiBinder = (WasapiBinder) target;
            LevelMeterDrawer.DrawMeter(wasapiBinder);
        }
    }
}