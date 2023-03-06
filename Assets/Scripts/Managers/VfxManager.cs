using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxManager : MonoBehaviour
{
    #region Singleton
    public static VfxManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    public GameObject[] particles;

    public void PlayVFX(VfxType vfxType, Vector3 createPosition)
    {
        GameObject particle = Instantiate(particles[(int)vfxType], createPosition, Quaternion.identity);

        particle.GetComponent<ParticleSystem>().Play();

        Destroy(particle, 2f);
    }
    public enum VfxType
    {
        hit,
        blockFX,
        coin,
        dollarBlast
    }
}
