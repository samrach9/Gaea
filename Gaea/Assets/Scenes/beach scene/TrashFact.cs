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
}
