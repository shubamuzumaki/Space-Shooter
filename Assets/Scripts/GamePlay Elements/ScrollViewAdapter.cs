using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class ScrollViewAdapter : MonoBehaviour
{
    private int count = 50;
    [SerializeField] private RectTransform playerScorePrefab;
    [SerializeField] private RectTransform scrollViewList;

    void Start()
    {
        StartCoroutine(FetchFromServer(res => OnReceiveItemModel(res)));
    }

    private void OnReceiveItemModel(ItemModel[] models)
    {
        foreach(var model in models)
        {
            var instance = Instantiate(playerScorePrefab);
        
            var rankText = instance.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            var tittleText = instance.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

            var scoreText = instance.gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            rankText.text = model.rank.ToString();
            tittleText.text = model.tittle.ToString();
            scoreText.text = model.score.ToString();

            instance.gameObject.transform.SetParent(scrollViewList,false);
        }
    }

    IEnumerator FetchFromServer(Action<ItemModel[]> onDone)
    {
        yield return new WaitForSeconds(0.1f);

        var results = new ItemModel[count];
        for(int i=0; i<count; ++i)
        {
            results[i] = new ItemModel.Builder()
                .Rank(i+1)
                .Tittle("Shubam")
                .Score(UnityEngine.Random.Range(0, 99999))
                .Build();
        }

        onDone(results);
    }

}


class ItemModel
{
    public int rank { get; private set; }
    public string tittle { get; private set; }
    public int score { get; private set; }

    private ItemModel()
    {

    }

    //[Note to Future Self] There is no need for bulder Pattern here just implementing
    // to learn it remove if neccessary.)
    public class Builder
    {
        ItemModel instance;
        public Builder()
        {
            instance = new ItemModel();
        }

        public Builder Rank(int rank)
        {
            instance.rank = rank;
            return this;
        }

        public Builder Tittle(string tittle)
        {
            instance.tittle = tittle;
            return this;
        }

        public Builder Score(int score)
        {
            instance.score = score;
            return this;
        }

        public ItemModel Build()
        {
            return instance;
        }
    }
}
