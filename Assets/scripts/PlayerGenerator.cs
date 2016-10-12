using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerGenerator : MonoBehaviour {

    [SerializeField] List<PlayerDataAsset> m_poolData;
    [SerializeField] float m_deltaGen = 2.0f;
    [SerializeField] GameObject m_uiPlayerPrefab;
    [SerializeField] int m_max = 4;

    List<PlayerDataAsset> m_activeDeck;

    float m_time;

    public bool m_launched = false;

	// Use this for initialization
	void Start () {
        StartGeneration();
	}
	
	// Update is called once per frame
	void Update () {
        m_time += Time.deltaTime;
        if( m_time > m_deltaGen)
        {
            m_time = 0.0f;
            SpawnUIPlayer();
        }
	}

    public void StartGeneration()
    {
        m_time = 0;
        m_launched = true;
    }

    void SpawnUIPlayer()
    {
        var r = Random.Range(0, m_poolData.Count);
        //Set data
        var draggable = GetUIObject();
        if (draggable == null)
            return;
        draggable.Fill(Instantiate(m_poolData[r]));
    }

    UIDraggablePlayer GetUIObject()
    {
        for(int i=0; i < transform.childCount; i++)
        {
            var dragg = transform.GetChild(i).GetComponent<UIDraggablePlayer>();
            if (dragg.IsEmpty)
                return dragg;
        }
        return null;
    }
}
