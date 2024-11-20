using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject optionsMenu;
        [SerializeField] private GameObject creditsMenu;

        void Start()
        {
            ShowMainMenu();
        }

        public void ShowMainMenu()
        {
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
            creditsMenu.SetActive(false);
        }

        public void ShowOptionsMenu()
        {
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
            creditsMenu.SetActive(false);
        }

        public void ShowCredits()
        {
            mainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            creditsMenu.SetActive(true);
        }

        public void StartGame()
        {
            SceneManager.LoadScene("Selecction"); // Cargar la escena llamada "Selection"
        }

        public void VolverAlMenu()
        {
            ShowMainMenu();
        }
    }
}