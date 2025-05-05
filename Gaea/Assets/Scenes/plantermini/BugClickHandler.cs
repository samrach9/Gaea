using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public class BugClickHandler : MonoBehaviour
{
    private BugManager manager;
    private bool       isBad;
    private GameObject self;

    public void Init(BugManager mgr, bool bad, GameObject me)
    {
        manager = mgr;
        isBad    = bad;
        self     = me;
    }

    void OnMouseDown()
    {
        manager.BugClicked(self, isBad);
    }
}

/*using UnityEngine;

public class BugClickHandler : MonoBehaviour
{
    private bugmanager manager;
    private bool isBadBug;

    public void Init(bugmanager manager, bool isBad)
    {
        this.manager = manager;
        this.isBadBug = isBad;
    }

    void OnMouseDown()
    {
        manager.BugClicked(isBadBug);
    }
}
*/