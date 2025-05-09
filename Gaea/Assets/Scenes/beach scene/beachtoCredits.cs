using UnityEngine;
using UnityEngine.UI; // For Button
using TMPro; // For TextMeshPro Input Field
using Unity.Services.Authentication;
using Unity.Services.Core;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;

public class beachtoCredits : MonoBehaviour
{
    async public void clickMe(){
        await UnityServices.InitializeAsync();
                if (!AuthenticationService.Instance.IsSignedIn) {
                    await AuthenticationService.Instance.SignInAnonymouslyAsync();
                }

                string playerID = AuthenticationService.Instance.PlayerId;
                
                FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
                Debug.Log($"Player ID: {AuthenticationService.Instance.PlayerId}");

                //DocumentReference docRef = db.Collection("Users").Document(playerID);
                Query query = db.Collection("Users").WhereEqualTo("pID", playerID);
                QuerySnapshot snapshot = await query.GetSnapshotAsync();
                if (snapshot.Count > 0)
                {
                    DocumentSnapshot docRef = snapshot.Documents.First();

                List<object> minisList = docRef.GetValue<List<object>>("minisFRname");
                    if (minisList != null && minisList.Count > 0)
                    {
                        string lastMini = minisList[minisList.Count - 1].ToString();
                        Debug.Log(lastMini);

                        /*if (minisList.Contains("TreeMiniGame") || minisList.Contains("planterminifr") || minisList.Contains("BambooMiniGame") || minisList.Contains("InvasiveSpeciesGame") || minisList.Contains("trashminigame")  )
                        {
                            SceneManager.LoadScene("Backyard");
                        } else if (minisList.Contains("crabminigame") ){
                            SceneManager.LoadScene("Beach");
                        } else {
                            SceneManager.LoadScene("Forest");
                        }*/
                        if (lastMini == "TreeMiniGame" || lastMini == "planterminifr" || lastMini == "BambooMiniGame" || lastMini == "InvasiveSpeciesGame" || lastMini == "trashminigame")
    {
        SceneManager.LoadScene("Backyard");
    }
    else if (lastMini == "crabminigame")
    {
        SceneManager.LoadScene("Beach");
    }
    else
    {
        SceneManager.LoadScene("Forest");
    }
                    }
                //string lastSceneMini = PlayerPrefs.GetString("Mini LastScene", "DefaultSceneName");
                }
                else {
                    SceneManager.LoadScene("map");
                }
    }
}


            
        //