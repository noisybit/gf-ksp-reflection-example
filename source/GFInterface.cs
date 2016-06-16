using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace GameframerReflection
{
    public class GFInterface : MonoBehaviour
    {
        AssemblyLoader.LoadedAssembly GF;
        Type PublicInterfaceType;
        MethodInfo registerEventMethod;
        MethodInfo deregisterEventMethod;
        MethodInfo captureEventMethod;
        object PublicInterfaceInstance;
        string lastStatus;

        public void Update()
        {
            if (GF == null)
            {
                GF = AssemblyLoader.loadedAssemblies.SingleOrDefault(a => a.dllName == "Gameframer");
                PublicInterfaceType = GF.assembly.GetTypes().FirstOrDefault(t => t.Name == "GFPublicInterface");
                registerEventMethod = PublicInterfaceType.GetMethod("RegisterEvent");
                deregisterEventMethod = PublicInterfaceType.GetMethod("DeregisterEvent");
                captureEventMethod = PublicInterfaceType.GetMethod("CaptureEvent");
                PublicInterfaceInstance = MonoBehaviour.FindObjectsOfType(PublicInterfaceType).FirstOrDefault();
            }
        }

        public string GetLastStatus()
        {
            return lastStatus;
        }
         
        public void RegisterEvent(string id, string eventName)
        {
            if (PublicInterfaceInstance != null)
            {
                object[] parametersArray = new object[] { id, eventName };
                lastStatus = DateTime.Now.ToFileTimeUtc() + " : " + (bool)registerEventMethod.Invoke(PublicInterfaceInstance, parametersArray);
            }
            else
            {
                UnityEngine.Debug.Log("RegisterEvent: PublicInterfaceInstance is null :(");
            }
        }
        public void DeregisterEvent(string id, string eventName)
        {
            if (PublicInterfaceInstance != null)
            {
                object[] parametersArray = new object[] { id, eventName };
                lastStatus = DateTime.Now.ToFileTimeUtc() + " : " + (bool)deregisterEventMethod.Invoke(PublicInterfaceInstance, parametersArray);
            }
            else
            {
                UnityEngine.Debug.Log("DeregisterEvent: PublicInterfaceInstance is null :(");
            }
        }

        public void CaptureEvent(string id, string name, string description)
        {
            if (PublicInterfaceInstance != null)
            {
                object[] parametersArray = new object[] { id, name, description };
                lastStatus = DateTime.Now.ToFileTimeUtc() + " : " + (bool)captureEventMethod.Invoke(PublicInterfaceInstance, parametersArray);
            }
            else
            {
                UnityEngine.Debug.Log("CaptureEvent: PublicInterfaceInstance is null :(");
            }
        }
    }
}
