using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject grass;
    [SerializeField] GameObject road;
    [SerializeField] int extent = 5;
    [SerializeField] int frontDistance = 10;
    //seberapa jauh player bisa ke belakang
    [SerializeField] int minZpos = -5;
    [SerializeField] int maxSomeTerrainRepeat = 3;

    Dictionary<int, TerrainBlock> map = new Dictionary<int, TerrainBlock>(50);

    private void Start() {
        //instantiate tanah di belakang
        for (int z = minZpos; z <= 0; z++)
        {
            CreateTerrain(grass,z);
        }
        
        //instantiate depan
        for (int z = 1; z < frontDistance; z++)
        {   
            
            var prefab = GetNextRandomTerrainPrefab(z);

            //instantiate block
            CreateTerrain(prefab,z);
        }

        player.Setup(minZpos, extent);
    }

     //method instantiate block
    private void CreateTerrain(GameObject prefab, int zPos){
                   
            var go = Instantiate(prefab, new Vector3(0, 0, zPos), Quaternion.identity);
            var tb = go.GetComponent<TerrainBlock>();   
            tb.Build(extent);
            
            map.Add(zPos, tb);
    }

    //method untuk mengatur agar tidak lebih dari n
    private GameObject GetNextRandomTerrainPrefab(int nextPos){
        bool isUniform = true;
        //posisi 1 ke belakang
        var tbRef = map[nextPos - 1];
        for (int distance = 2; distance <= maxSomeTerrainRepeat; distance++)
        {
            if (map[nextPos - distance].GetType() != tbRef.GetType())
            {
                isUniform = false;
                break;
            }
        }

        if(isUniform){
            if(tbRef is Grass)
                return road;
            else
                return grass;
        }

        //penentuan terrain block dengan probabilitas 50%
        return Random.value > 0.5f ? road : grass;
    }
}
