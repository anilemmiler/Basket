using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class Skor : MonoBehaviour
{
    public int skor = 0;
    public TextMeshPro scoreText;
    public TextMeshProUGUI scoreUI;
    public int timer = 60;
    public int hedefSayi = 10;
    public TextMeshProUGUI timerText, winFailText;
    public GameObject endPanel;
    public Transform pota;

    // Yeni eklenen değişken
    private bool ikinciSkorSonrasi = false;

    void Start()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        if (timer > 0)
        {
            timer--;
            StartCoroutine(Timer());
            timerText.text = timer.ToString();
        }
        else if (timer == 0)
        {
            // Skoru kontrol et ve oyunu bitir
            if (skor >= hedefSayi)
            {
                // Kazandın
                winFailText.text = "Kazandin!!";
                winFailText.color = Color.green;
            }
            else if (skor < hedefSayi)
            {
                // Kaybettin
                winFailText.text = "Kaybettin!!";
                winFailText.color = Color.red;
            }

            yield return new WaitForSeconds(2);
            endPanel.SetActive(true);

            if (skor == 2)
            {
                // Skor 2 olduğunda bir sonraki sahneye geç
                SceneManager.LoadScene("Level02");
            }
            else
            {
                // Skor 2 değilse, oyunu sıfırla
                SceneManager.LoadScene(0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Basketball")
        {
            if (other.GetComponent<BasketCheck>().alt == true &&
                other.GetComponent<BasketCheck>().ust == true)
            {
                skor++;
                scoreText.text = skor.ToString();
                scoreUI.text = "Amazing!!";
                scoreText.color = Color.green;
                Invoke(nameof(BakctoBlack), 1);
                pota.position = new Vector3(Random.Range(-6.0f, -4.0f), 0, Random.Range(-8.0f, -7.0f));

                // Skor 2 olduğunda bir sonraki sahneye geç
                if (skor == 2)
                {
                    SceneManager.LoadScene("Level02");
                }
            }
        }
    }

    void BakctoBlack()
    {
        scoreText.color = Color.black;
        scoreUI.text = "";
    }
}
