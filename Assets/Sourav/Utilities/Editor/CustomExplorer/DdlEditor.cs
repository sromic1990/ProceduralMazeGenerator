using Sourav.Utilities.Scripts.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Sourav.Utilities.EditorScripts
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoBehaviour), editorForChildClasses: true, isFallback = false)]
    public class DdlEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            DontDestroyOnLoadAttribute ddlAttribute = (DontDestroyOnLoadAttribute)Attribute.GetCustomAttribute(target.GetType(), typeof(DontDestroyOnLoadAttribute));
            if (ddlAttribute != null)
            {
                CheckAndAttachDontDestroyOnLoad();
            }
        }
        private void CheckAndAttachDontDestroyOnLoad()
        {
            GameObject gObj = ((MonoBehaviour)target).gameObject;
            if (gObj == null)
            {
                Debug.Log("null");
            }
            else
            {
                if (gObj.GetComponent<DontDestroyOnLoad>() == null)
                {
                    gObj.AddComponent<DontDestroyOnLoad>();
                }
            }
        }
    }
}
    
