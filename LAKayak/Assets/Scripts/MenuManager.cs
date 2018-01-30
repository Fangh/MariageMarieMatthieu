using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour 
{
	public static MenuManager Instance;
	MenuManager()
	{
		Instance = this;
	}

	[Header("References")]
	public GameObject player1;
	public GameObject player2;
	public GameObject easyBackground;
	public GameObject hardBackground;
	public Button restartButton;
	public Button quitButton;
	public GameObject music;
	public Text startText;
	public Text finishText;
	public Image fanion1;
	public Image fanion2;

	[Header("Audio")]
	public AudioClip SFX_3;
	public AudioClip SFX_2;
	public AudioClip SFX_1;
	public AudioClip SFX_GO;
	public AudioClip SFX_win;

	public bool isPaused = true;

	// Use this for initialization
	void Start () 
	{
		player1.SetActive(false);
		player2.SetActive(false);
		easyBackground.SetActive(true);
		hardBackground.SetActive(true);
		restartButton.gameObject.SetActive(false);
		quitButton.gameObject.SetActive(false);
		music.SetActive( false );
		startText.gameObject.SetActive(false);
		isPaused = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( Input.GetButtonDown("Pause") )
		{
			Pause( !isPaused );
			if (isPaused)
				music.GetComponent<AudioSource>().Pause();
			else
				music.GetComponent<AudioSource>().UnPause();
		}
	}

	public void StartGame(bool easyMode)
	{
		easyBackground.SetActive( easyMode );
		hardBackground.SetActive( !easyMode );
		player1.SetActive( true );
		player2.SetActive( true );
		startText.gameObject.SetActive(true);
		startText.text = "3";
		GetComponent<AudioSource>().PlayOneShot(SFX_3);
		startText.transform.DOScale(2,1f).OnComplete( () => 
		{
			startText.text = "2";
			startText.transform.localScale = Vector3.one;
			GetComponent<AudioSource>().PlayOneShot(SFX_2);
			startText.transform.DOScale(2,1).OnComplete( () => 
			{				
				startText.text = "1";
				startText.transform.localScale = Vector3.one;
				GetComponent<AudioSource>().PlayOneShot(SFX_1);
				startText.transform.DOScale(2,1).OnComplete( () => 
				{
					startText.text = "GO";
					startText.transform.localScale = Vector3.one;
					GetComponent<AudioSource>().PlayOneShot(SFX_GO);
					startText.transform.DOScale(2,0.5f).OnComplete( () => 
					{
						isPaused = false;
						music.SetActive( true );
						startText.gameObject.SetActive(false);
					});
				});
			});
		});
	}

	void Pause(bool toggle)
	{
		EventSystem.current.SetSelectedGameObject(restartButton.gameObject);
		isPaused = toggle;
		restartButton.gameObject.SetActive( toggle );
		quitButton.gameObject.SetActive( toggle );
	}

	public void RestartGame()
	{
		SceneManager.LoadScene(0);
	}

	public void Exit()
	{
		Application.Quit();
	}

	public void Win(string name)
	{
		if (name == "Marie")
			startText.text = name+ " a teminée première !";
		else
			startText.text = name+ " a teminé premier !";
			
		startText.gameObject.SetActive(true);
		startText.transform.localScale = Vector3.one;
		fanion1.rectTransform.DOAnchorPosX(0, 1);
		fanion2.rectTransform.DOAnchorPosX(0, 1);
		isPaused = true;
		GetComponent<AudioSource>().PlayOneShot(SFX_win);
	}
}
