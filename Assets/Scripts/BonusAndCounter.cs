using System.Collections;
using UnityEngine.UI; 
using UnityEngine;

public class BonusAndCounter : MonoBehaviour
{  
    public Text text;
    public GameObject[] bonuses;
    [Range(0,20)]
    public float delayBetweenBonuses;
    public int left,top,right,bottom;

    private int enemyCount;
    private bool canSpawn;

    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        text.text = "Enemies left : "+ enemyCount;

        SpawnBonus();
    }

    private void SpawnBonus()
    {
        if(canSpawn)
        {
            int random = Random.Range(0, bonuses.Length);
            int rdmX = Random.Range(left, right);
            int rdmY = Random.Range(bottom, top);

            Instantiate(bonuses[random], new Vector3(rdmX, rdmY, 0), Quaternion.identity);
            StartCoroutine(SpawnBonusCo());
        }
    }

    IEnumerator SpawnBonusCo()
    {
        canSpawn = false;
        yield return new WaitForSeconds(delayBetweenBonuses);
        canSpawn = true;
    }

    
}
