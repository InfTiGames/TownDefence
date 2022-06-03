using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombsSpell : Spells
{
    [SerializeField] GameObject spawnRangeObj;

    public void SpawnBombs()
    {
        if (_gameManager.isGameActive)
        {            
            StartCoroutine(BombSpawnDelay(5));
        }
        UseSpell(1f);
    }

    IEnumerator BombSpawnDelay(int bombsToSpawn)
    {
        for (int i = 0; i < bombsToSpawn; i++)
        {
            yield return new WaitForSeconds(0.1f);
            GameObject bomb = ObjectPool.SharedInstance.GetPooledBomb();
            if (bomb != null)
            {
                bomb.transform.position = GenerateBombsSpawnPosition();
                bomb.transform.rotation = Quaternion.identity;
                bomb.SetActive(true);
            }
            yield return new WaitForSeconds(0.1f);
            ParticleSystem effect = ObjectPool.SharedInstance.GetPooledBombParticle();
            if (effect != null)
            {
                effect.transform.position = bomb.transform.position;
                effect.transform.rotation = bomb.transform.rotation;
                effect.Play();
            }
        }        
    }

    Vector2 GenerateBombsSpawnPosition()
    {
        float spawnX = Random.Range(-_gameManager.spawnRangeObj.transform.position.x, _gameManager.spawnRangeObj.transform.position.x);
        float spawnY = Random.Range(1, 3);
        Vector2 randomPos = new Vector2(spawnX, spawnY);
        return randomPos;
    }
}