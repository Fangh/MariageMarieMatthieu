using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
	[Header("Balancing")]
	public float speed = 10f;
	public float jitter = 10f;
	public float rotationSpeed = 10f;
	public int playerId = 0;

	[Header("References")]
	public GameObject FX_Splash;
	public Transform left;
	public Transform right;
	public GameObject light;

	[Header("Audio")]
	public List<AudioClip> SFX_splash;
	public List<AudioClip> SFX_win;

	Rigidbody2D rb;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D>();
		light.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void FixedUpdate()
	{
		if ( MenuManager.Instance.isPaused )
			return;

		bool backward = Input.GetButton( "Backward" + playerId );
		bool leftButton = Input.GetButtonDown( "Left" + playerId );
		bool rightButton = Input.GetButtonDown( "Right" + playerId );
		if (backward)
		{
			if(!GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().Play();
				light.SetActive(true);
			}
		}
		else
		{
			if(GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().Stop();
				light.SetActive(false);
			}
		}

		if ( leftButton )
		{
			if (backward)
			{
				rb.AddForce(transform.right * -speed, ForceMode2D.Impulse);
				rb.AddTorque(-jitter, ForceMode2D.Impulse);
				GameObject.Instantiate(FX_Splash, left.position, left.rotation * Quaternion.Euler( Vector3.right * 180f));
			}
			else
			{
				rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
				rb.AddTorque(jitter, ForceMode2D.Impulse);
				GameObject.Instantiate(FX_Splash, left.position, left.rotation);
			}
			left.GetComponent<AudioSource>().PlayOneShot(SFX_splash[Random.Range(0,SFX_splash.Count)]);

		}
		if ( rightButton )
		{
			if ( backward )
			{
				rb.AddForce(transform.right * -speed, ForceMode2D.Impulse);
				rb.AddTorque(jitter, ForceMode2D.Impulse);				
				GameObject.Instantiate(FX_Splash, right.position, right.rotation * Quaternion.Euler( Vector3.right * 180f));
			}
			else
			{
				rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
				rb.AddTorque(-jitter, ForceMode2D.Impulse);
				GameObject.Instantiate(FX_Splash, right.position, right.rotation);
			}
			right.GetComponent<AudioSource>().PlayOneShot(SFX_splash[Random.Range(0,SFX_splash.Count)]);
		}

		// if( Mathf.Abs( horizontal ) > 0.1f )
		// {
		// 	transform.Rotate( new Vector3(0, 0, rotationSpeed * horizontal) );
		// }

		// if ( stepForward )
		// {
		// 	rigidbody.AddForce( transform.right * speed );
		// }
		// else if ( stepBackward )
		// {
		// 	rigidbody.AddForce( transform.right * -speed );
		// }
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Finish"))
		{
			MenuManager.Instance.Win(name);
		}
	}
}
