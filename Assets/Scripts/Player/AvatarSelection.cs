using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSelection : MonoBehaviour
{
    
    [SerializeField] AudioSource _audio;
    [SerializeField] AudioClip _SelectionClip;
    [SerializeField] AudioClip _avatarSelectionClip;
    //private bool KnightB = true;
    [SerializeField] GameObject spotA,spotB,spotC,spotD,spotE;
    [SerializeField] GameObject knightA,knightB,knightC,knightD,knightE;

    private Animator animKnightA,animKnightB,animKnightC,animKnightD,animKnightE;
    private void Start() {
        
        animKnightA = knightA.GetComponent<Animator>();
        animKnightB = knightB.GetComponent<Animator>();
        animKnightC = knightC.GetComponent<Animator>();
        animKnightD = knightD.GetComponent<Animator>();
        animKnightE = knightE.GetComponent<Animator>();
        PlayerManager.Instance.SetAvatar("KnightB");
        animKnightB.SetBool("selected",true);
    }

    public void SelectedToGO(){
        GameManager.Instance.LoadScene("Variante 1",true);
    }
    public void AddScreen(){
        GameManager.Instance.LoadSceneAdition("SampleScene");
    }
    public void ChangeSelect(string selectAvatar){
        switch (selectAvatar){
            case "KnightA":
                spotA.SetActive(true);
                spotB.SetActive(false);
                spotC.SetActive(false);
                spotD.SetActive(false);
                spotE.SetActive(false);
                PlayerManager.Instance.SetAvatar(selectAvatar);
                animKnightA.SetBool("selected",true);
                animKnightB.SetBool("selected",false);
                animKnightC.SetBool("selected",false);
                animKnightD.SetBool("selected",false);
                animKnightE.SetBool("selected",false);
                _audio.PlayOneShot(_avatarSelectionClip);
                

            break;
            case "KnightB":
                spotA.SetActive(false);
                spotB.SetActive(true);
                spotC.SetActive(false);
                spotD.SetActive(false);
                spotE.SetActive(false);
                PlayerManager.Instance.SetAvatar(selectAvatar);
                animKnightA.SetBool("selected",false);
                animKnightB.SetBool("selected",true);
                animKnightC.SetBool("selected",false);
                animKnightD.SetBool("selected",false);
                animKnightE.SetBool("selected",false);
                _audio.PlayOneShot(_avatarSelectionClip);
            break;
            case "KnightC":
                spotA.SetActive(false);
                spotB.SetActive(false);
                spotC.SetActive(true);
                spotD.SetActive(false);
                spotE.SetActive(false);
                PlayerManager.Instance.SetAvatar(selectAvatar);
                animKnightA.SetBool("selected",false);
                animKnightB.SetBool("selected",false);
                animKnightC.SetBool("selected",true);
                animKnightD.SetBool("selected",false);
                animKnightE.SetBool("selected",false);
                _audio.PlayOneShot(_avatarSelectionClip);
            break;
            case "KnightD":
                spotA.SetActive(false);
                spotB.SetActive(false);
                spotC.SetActive(false);
                spotD.SetActive(true);
                spotE.SetActive(false);
                PlayerManager.Instance.SetAvatar(selectAvatar);
                animKnightA.SetBool("selected",false);
                animKnightB.SetBool("selected",false);
                animKnightC.SetBool("selected",false);
                animKnightD.SetBool("selected",true);
                animKnightE.SetBool("selected",false);
                _audio.PlayOneShot(_avatarSelectionClip);
            break;
            case "KnightE":
                spotA.SetActive(false);
                spotB.SetActive(false);
                spotC.SetActive(false);
                spotD.SetActive(false);
                spotE.SetActive(true);
                PlayerManager.Instance.SetAvatar(selectAvatar);
                animKnightA.SetBool("selected",false);
                animKnightB.SetBool("selected",false);
                animKnightC.SetBool("selected",false);
                animKnightD.SetBool("selected",false);
                animKnightE.SetBool("selected",true); 
                _audio.PlayOneShot(_avatarSelectionClip);

            break;

        }
    }
    public void PlaySound(){
        _audio.PlayOneShot(_SelectionClip);
    }
}
