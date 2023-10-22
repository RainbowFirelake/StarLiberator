using Sirenix.OdinInspector;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GPUInstancingInitializator : SerializedMonoBehaviour
{
    [Button("Set GPU Instancing Enablers on child mesh renderers", ButtonSizes.Large)]
    public void SetGPUInstancingEnablers()
    {
        SetGPUInstancingEnablerOnChilds(gameObject);
    }

    [Button("Remove GPU Instancing Enablers on all childs", ButtonSizes.Large)]
    public void RemoveGPUInstancingEnablers()
    {
        var childs = gameObject.GetComponentsInChildren<Transform>();

        foreach (var child in childs)
        {
            if (child.TryGetComponent<GPUInstancingEnabler>(out var instancing))
            {
                DestroyImmediate(instancing);
            }
        }
    }

    private void SetGPUInstancingEnablerOnChilds(GameObject root)
    {
        var childs = root.GetComponentsInChildren<MeshRenderer>();

        foreach (var child in childs)
        {
            if (child.TryGetComponent<GPUInstancingEnabler>(out var enabler))
                continue;

            child.gameObject.AddComponent<GPUInstancingEnabler>();
        }
    }
}