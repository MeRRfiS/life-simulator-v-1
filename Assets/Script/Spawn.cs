using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject creature;

    [SerializeField] private TextMeshProUGUI textMesh;

    private int MAX_X = 33;
    private int MAX_Y = 20;
    private int MIN_X = 13;
    private int MIN_Y = 6;

    private void Start()
    {
        StartCoroutine(Timer());
        MAX_X = 46;
        MAX_Y = 26;
        MIN_X = 1;
        MIN_Y = 1;
        for (int i = 0; i < 50; i++)
        {
            var x = Random.Range(MIN_X, MAX_X);
            var y = Random.Range(MIN_Y, MAX_Y);
            CreatureController.CreateCreature(CreatureType.Green, creature, transform, x, y);
        }
        for (int i = 0; i < 0; i++)
        {
            var x = Random.Range(MIN_X, MAX_X);
            var y = Random.Range(MIN_Y, MAX_Y);
            CreatureController.CreateCreature(CreatureType.Red, creature, transform, x, y);
        }

        StartCoroutine(SpawnEnergy());
        
    }

    private IEnumerator SpawnEnergy()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            var rand = Random.Range(1, 3);
            for (int i = 0; i < rand; i++)
            {
                var x = Random.Range(MIN_X, MAX_X);
                var y = Random.Range(MIN_Y, MAX_Y);
                CreatureController.CreateCreature(CreatureType.Yellow, creature, transform, x, y);
            }
        }
    }

    private IEnumerator StopStartZone()
    {
        yield return new WaitForSeconds(0f);

        MAX_X = 46;
        MAX_Y = 26;
        MIN_X = 1;
        MIN_Y = 1;
    }

    private IEnumerator Timer()
    {
        var sec = 0;
        while (true)
        {
            textMesh.text = sec.ToString();
            yield return new WaitForSeconds(1f);
            sec++;
        }
    }
}
