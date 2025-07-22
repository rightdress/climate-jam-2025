using System.Collections.Generic;
using UnityEngine;

public class T3A_WebsiteProgressTracker : MonoBehaviour
{
    [SerializeField] private GameObject NextLevelButton;

    private Dictionary<int, GameObject> websiteNumToStar = new Dictionary<int, GameObject>();
    private Dictionary<int, bool> isWebsiteVisited = new Dictionary<int, bool>();

    [SerializeField] private GameObject _star1;
    [SerializeField] private GameObject _star2;
    [SerializeField] private GameObject _star3;
    [SerializeField] private GameObject _star4;
    [SerializeField] private GameObject _star5;

    private void Start()
    {
        websiteNumToStar.Add(1, _star1);
        websiteNumToStar.Add(2, _star2);
        websiteNumToStar.Add(3, _star3);
        websiteNumToStar.Add(4, _star4);
        websiteNumToStar.Add(5, _star5);

        isWebsiteVisited.Add(1, false);
        isWebsiteVisited.Add(2, false);
        isWebsiteVisited.Add(3, false);
        isWebsiteVisited.Add(4, false);
        isWebsiteVisited.Add(5, false);
    }

    public void WebsiteVisited(int num)
    {
        websiteNumToStar[num].SetActive(true);

        AreAllWebsitesVisited();
    }

    private bool AreAllWebsitesVisited()
    {
        int numVisited = 0;

        foreach (var True in isWebsiteVisited)
        {
            numVisited++;
        }

        if (numVisited == 5)
        {
            NextLevelButton.SetActive(true);
            return true;
        }

        return false;
    }
}
