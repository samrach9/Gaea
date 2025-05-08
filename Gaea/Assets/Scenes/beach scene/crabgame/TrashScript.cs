using UnityEngine;
using UnityEngine.SceneManagement;

public class TrashScript : MonoBehaviour
{
    public trashGenerator trashGenerator;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * trashGenerator.currentSpeed * Time.deltaTime);
    }

    private  void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("nextLine"))
        {
            trashGenerator.GenerateNextTrashGap();
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            Destroy(this.gameObject);
        }
    }
}
