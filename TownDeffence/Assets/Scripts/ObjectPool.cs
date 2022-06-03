using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{    
    public static ObjectPool SharedInstance;

    #region Bullets
    public List<GameObject> pooledObjects;    
    public GameObject objectToPool;
    public int amountObjectsToPool;

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountObjectsToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
    #endregion

    #region Bombs 
    public List<GameObject> pooledBombs;
    public GameObject bombToPool;
    public int amountBombsToPool;

    public GameObject GetPooledBomb()
    {
        for (int i = 0; i < amountBombsToPool; i++)
        {
            if (!pooledBombs[i].activeInHierarchy)
            {
                return pooledBombs[i];
            }
        }
        return null;
    }
    #endregion

    #region HitFX Particles
    public List<ParticleSystem> pooledParticles;
    public ParticleSystem particleToPool;
    public int amountParticlesToPool;

    public ParticleSystem GetPooledParticle()
    {
        for (int i = 0; i < amountParticlesToPool; i++)
        {
            if (!pooledParticles[i].isPlaying)
            {
                return pooledParticles[i];
            }
        }
        return null;
    }
    #endregion

    #region Bomb Explosion Particles
    public List<ParticleSystem> pooledBombParticles;
    public ParticleSystem bombParticleToPool;
    public int amountBombParticlesToPool;

    public ParticleSystem GetPooledBombParticle()
    {
        for (int i = 0; i < amountBombParticlesToPool; i++)
        {
            if (!pooledBombParticles[i].isPlaying)
            {
                return pooledBombParticles[i];
            }
        }
        return null;
    }
    #endregion

    void Awake()
    {
        SharedInstance = this;
    }    

    void Start()
    {        

        //Bullets
        pooledObjects = new List<GameObject>();        
        GameObject tmp;        
        for (int i = 0; i < amountObjectsToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }

        //Bombs
        pooledBombs = new List<GameObject>();
        GameObject bmb;
        for (int i = 0; i < amountBombsToPool; i++)
        {
            bmb = Instantiate(bombToPool);
            bmb.SetActive(false);
            pooledBombs.Add(bmb);
        }

        //HitFX
        pooledParticles = new List<ParticleSystem>();
        ParticleSystem prt;
        for (int i = 0; i < amountParticlesToPool; i++)
        {
            prt = Instantiate(particleToPool);
            prt.Stop();
            pooledParticles.Add(prt);
        }

        //Bomb ExplosionFX
        pooledBombParticles = new List<ParticleSystem>();
        ParticleSystem bmbprt;
        for (int i = 0; i < amountBombParticlesToPool; i++)
        {
            bmbprt = Instantiate(bombParticleToPool);
            bmbprt.Stop();
            pooledBombParticles.Add(bmbprt);
        }
    }
}