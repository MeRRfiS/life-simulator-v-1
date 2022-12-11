using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
    private CreatureType _creatureType;

    public CreatureType CreatureType
    {
        get { return _creatureType; }
        set { _creatureType = value; }
    }

    [SerializeField] private List<SidesController> sides;
    [SerializeField] private GameObject creaturePrefab;
    public List<Sprite> sprites;

    private bool stand = false;

    public static void CreateCreature(CreatureType creatureType, GameObject obj, Transform parent, int x, int y)
    {
        var xPosition = x <= 23 ? -38 * (23 - x) : 38 * (x - 23);
        var yPosition = y <= 13 ? -38 * (13 - y) : 38 * (y - 13);
        var position = new Vector3(xPosition, yPosition, -1);
        GameObject creature = Instantiate(obj, parent);
        creature.transform.localPosition = position;
        
        creature.GetComponent<CreatureController>().CreatureType = creatureType;
        creature.GetComponent<SpriteRenderer>().sprite = obj.GetComponent<CreatureController>().sprites[(int)creatureType];

        switch (creatureType)
        {
            case CreatureType.Green:
                creature.GetComponent<CreatureController>().StartGreenWork();
                break;
            case CreatureType.Red:
                break;
            case CreatureType.Yellow:
                creature.GetComponent<CreatureController>().StartYellowWork();
                break;
            case CreatureType.Dark_Green:
                break;
            case CreatureType.Dark_Red:
                break;
        }
    }

    public void StartGreenWork()
    {
        StartCoroutine(GreenHandler());
        StartCoroutine(GreenDie());
    }

    public void StartYellowWork()
    {
        StartCoroutine(YellowDie());
    }

    private void FixedUpdate()
    {
        var hits = Physics2D.CircleCastAll(transform.position, 0.1f, transform.position, 0);
        if (hits.Length == 2 && !stand)
        {
            var x = Random.Range(0, 47);
            var y = Random.Range(0, 27);
            var xPosition = x <= 23 ? -38 * (23 - x) : 38 * (x - 23);
            var yPosition = y <= 13 ? -38 * (13 - y) : 38 * (y - 13);
            var position = new Vector3(xPosition, yPosition, -1);
            transform.localPosition = position;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(transform.position + transform.position * 0f, transform.position);
        Gizmos.DrawWireSphere(transform.position + transform.position * 0f, 0.1f);
    }

    private IEnumerator GreenHandler()
    {
        while (_creatureType != CreatureType.Dark_Green)
        {
            yield return new WaitForSeconds(1f);
            stand = true;
            for (int i = 0; i < 4; i++)
            {
                var side = transform.GetChild(i).GetComponent<SidesController>().hit;
                if (side.collider != null)
                {
                    if (side.collider.name.Contains("Water"))
                    {
                        continue;
                    }
                    if (side.collider.GetComponent<CreatureController>().CreatureType == CreatureType.Yellow)
                    {
                        var x = 23 + (int)(side.collider.transform.localPosition.x / 38);
                        var y = 13 + (int)(side.collider.transform.localPosition.y / 38);
                        CreateCreature(CreatureType.Green, creaturePrefab, transform.parent, x, y);
                        Destroy(side.collider.gameObject);
                    }
                }
            }
        }

        StartCoroutine(DarkGreenRemove());
    }

    private IEnumerator GreenDie()
    {
        yield return new WaitForSeconds(120f);

        _creatureType = CreatureType.Dark_Green;
        GetComponent<SpriteRenderer>().sprite = sprites[(int)_creatureType];
    }

    private IEnumerator YellowDie()
    {
        yield return new WaitForSeconds(1f);
        stand = true;
        yield return new WaitForSeconds(29f);

        Destroy(gameObject);
    }

    private IEnumerator DarkGreenRemove()
    {
        yield return new WaitForSeconds(60f);

        Destroy(gameObject);
    }
}
