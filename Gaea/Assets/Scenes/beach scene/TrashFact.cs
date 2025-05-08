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

    public void ToCrabGame()
    {
        Debug.Log("scrotum");
        SceneManager.LoadScene("crabminigame");

    }

    public void ToBeach()
    {
        Debug.Log("iwas slcicked");
        SceneManager.LoadScene("Beach");
    }

    public void ToIntroCrabGame()
    {
        Debug.Log("scrotum");
        SceneManager.LoadScene("introcrabgame");

    }
}
