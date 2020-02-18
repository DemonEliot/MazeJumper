using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{

    // Particle 
    private SkinnedMeshRenderer[] listOfMeshRender;
    private ParticleSystem playerParticle;
    private ParticleSystem.EmissionModule playerEmmissionModule;
    private bool isIntangible = false;

    // Start is called before the first frame update
    void Start()
    {
        listOfMeshRender = GetComponentsInChildren<SkinnedMeshRenderer>();
        playerParticle = GetComponentInChildren<ParticleSystem>();
        playerEmmissionModule = playerParticle.emission;
    }

    public void ClearParticle()
    {
        playerParticle.Clear();
    }

    public void EnableRender(bool fleshState)
    {
        foreach (SkinnedMeshRenderer render in listOfMeshRender)
        {
            render.enabled = fleshState;
        }
        playerEmmissionModule.enabled = !fleshState;
        isIntangible = !fleshState;
    }

    public bool GetIsIntangible()
    {
        return isIntangible;
    }
}
