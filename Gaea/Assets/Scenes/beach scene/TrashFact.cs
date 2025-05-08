using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrashFact : MonoBehaviour
{
    public void ToTrashFact()
    {
        SceneManager.LoadScene("TrashInfo");
    }

    public void ToTrashFact2()
    {
        SceneManager.LoadScene("TrashInfo2");
    }

    public void ToTrashFact3()
    {
        SceneManager.LoadScene("TrashInfo3");
    }
}
