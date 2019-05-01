using System.Collections;
using System.Collections.Generic;
using Rewired.Demos;
using UnityEngine;

//REMEMBER TO SET TAG FOR EACH SHAPES COLOUR BEFORE PLAY IF YOU WANT A DIFFERENT COLOUR
//YOU CAN JUST DUPLICATE THE SHAPES IF YOU WANT TO ADD MORE SHAPES IN THE SCENE

    
public class ShapeManager : MonoBehaviour
{
	//Audio Manager
	AudioManager abilityPlayer;

	//Player Actions
	[Header("Player Abilities")]
    [SerializeField] private Vector3 growthSpeed = new Vector3(0.04f, 0.04f, 0.04f);        //How much to increase the shapes size over time
    private Vector3 growthSpeedX;
    private Vector3 growthSpeedY;
    [SerializeField] private float growthLimit = 0f;                                        //This is for how big we can make the shapes per level
    private float pullSpeed = 4f;                                          //How fast the shapes pulls towards the player
    private float floatSpeed = 6f;                                         //How fast the shapes float upwards the player
    private bool stopFloating = false;

    [Header("Shape Management")]
    private Transform[] shapesTransforms;

    //Players
    private GameObject _player1;
    private GameObject _player2;
    private GameObject _player3;
    private GameObject _player4;

    //Shapes
    private GameObject circle;
    private GameObject square;
    private GameObject triangle;

	//SFX Reset bools
	bool growFXPlayed = false;
	bool pullFXPlayed = false;
	bool floatFXPlayed = false;
	bool floatReleaseFXPlayed = false;

	RigidbodyConstraints originalConstraints;

	//death checking bools
	bool player1Dead = false;
	bool player2Dead = false;
	bool player3Dead = false;
	bool player4Dead = false;

	// Start is called before the first frame update
	void Start()
    {
		//Audio Management
		abilityPlayer = FindObjectOfType<AudioManager>();

        //Shape Management
        shapesTransforms = GetShapesList();
        SetShapeColours();

        //Players
        _player1 = GameObject.Find("Player1");
        _player2 = GameObject.Find("Player2");
        _player3 = GameObject.Find("Player3");
        _player4 = GameObject.Find("Player4");

        //Shapes
        circle = Resources.Load("Prefabs/Circle") as GameObject;
        square = Resources.Load("Prefabs/Square") as GameObject;
		triangle = Resources.Load("Prefabs/Triangle") as GameObject;

        //Growth Management
        growthSpeedX = new Vector3 (growthSpeed.x,0f,0f);
        growthSpeedY = new Vector3(0f, growthSpeed.y, 0f);
    }

    void Update()
    {
        stopFloating = true;
		if (_player1.gameObject != null)
		{
			if (_player1.gameObject.GetComponent<GamePlayer>().P1T)
			{
				if (growFXPlayed == false)
				{
					abilityPlayer.Grow.Play();
					growFXPlayed = true;
				}
				Player1Action();
			}
			else
			{
				growFXPlayed = false;
				abilityPlayer.Grow.Stop();
			}
		}
		else if (player1Dead == false)
			{
				Debug.Log("Player 1 has been killed!");
				player1Dead = true;
			}

		if (_player2.gameObject != null)
		{
			if (_player2.gameObject.GetComponent<GamePlayer>().P2T)
			{
			if (pullFXPlayed == false)
			{
				abilityPlayer.Pull.Play();
				pullFXPlayed = true;
			}
			Player2Action();
			}
			else
			{
				pullFXPlayed = false;
				abilityPlayer.Pull.Stop();
			}
		}
		else if(player2Dead == false)
		{
			Debug.Log("Player 2 has been killed!");
			player2Dead = true;
		}

		if (_player3.gameObject != null)
		{
			if (_player3.gameObject.GetComponent<GamePlayer>().P3T)
			{
				Player3Action();
			}
		}
		else if(player3Dead == false){
			Debug.Log("Player 3 has been killed!");
			player3Dead = true;
		}

		if (_player4.gameObject != null)
		{
			if (_player4.gameObject.GetComponent<GamePlayer>().P4T)
			{
			if (floatFXPlayed == false)
				{
					if (floatReleaseFXPlayed == true) {
						abilityPlayer.FloatRelease.Stop();
						floatReleaseFXPlayed = false;
						}
					abilityPlayer.Float_Up.Play();
					floatFXPlayed = true;
				}
				Player4Action();
			}
			else
			{
				floatFXPlayed = false;
				abilityPlayer.Float_Up.Stop();
				//if (floatReleaseFXPlayed == false)
				//{
				//	abilityPlayer.FloatRelease.Play();
				//	floatReleaseFXPlayed = true;
				//}
			}
		}
		else if(player4Dead == false)
		{
			Debug.Log("Player 4 has been killed!");
			player4Dead = true;
		}


		if (stopFloating)
        {
            shapesTransforms = GetShapesList();
            foreach (Transform _ts in shapesTransforms)
            {
                if (_ts.GetComponent<Rigidbody>() != null)
                {
                    if (_ts.GetComponent<Rigidbody>().useGravity == false)
                    {
                        _ts.GetComponent<Rigidbody>().useGravity = true;
						_ts.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
						;
                    }
                }
            }
        }
    }

    #region Player Actions

    /// <summary>
    /// Contains action to be performed when Player1 presses their button - GROWs shapes
    /// </summary>
    public void Player1Action()
    {
        shapesTransforms = GetShapesList();
        
        foreach (Transform _ts in shapesTransforms)
        {
            if (_ts.tag == CheckForColour(_player1))
            {
                if (growthLimit == 0 || _ts.gameObject.GetComponent<ShapeCollisionChecker>().unlimitedGrowth)
                {
                    if (!_ts.gameObject.GetComponent<ShapeCollisionChecker>().squeezeX &&
                        !_ts.gameObject.GetComponent<ShapeCollisionChecker>().squeezeY)
                    {
                        _ts.localScale += growthSpeed + _ts.gameObject.GetComponent<ShapeCollisionChecker>().growthSpeed;
                    }
                    if (_ts.gameObject.GetComponent<ShapeCollisionChecker>().squeezeY &&
                        !_ts.gameObject.GetComponent<ShapeCollisionChecker>().squeezeX)
                    {
                        _ts.localScale += growthSpeedY + _ts.gameObject.GetComponent<ShapeCollisionChecker>().growthSpeed;
                    }
                    if (_ts.gameObject.GetComponent<ShapeCollisionChecker>().squeezeX &&
                        !_ts.gameObject.GetComponent<ShapeCollisionChecker>().squeezeY)
                    {
                        _ts.localScale += growthSpeedX + _ts.gameObject.GetComponent<ShapeCollisionChecker>().growthSpeed;
                    }
                }
                else if (_ts.localScale.x < growthLimit && _ts.localScale.y < growthLimit)
                {
                    if (!_ts.gameObject.GetComponent<ShapeCollisionChecker>().squeezeX &&
                        !_ts.gameObject.GetComponent<ShapeCollisionChecker>().squeezeY)
                    {
                        _ts.localScale += growthSpeed + _ts.gameObject.GetComponent<ShapeCollisionChecker>().growthSpeed;
                    }
                    if (_ts.gameObject.GetComponent<ShapeCollisionChecker>().squeezeY &&
                        !_ts.gameObject.GetComponent<ShapeCollisionChecker>().squeezeX)
                    {
                        _ts.localScale += growthSpeedY + _ts.gameObject.GetComponent<ShapeCollisionChecker>().growthSpeed;
                    }
                    if (_ts.gameObject.GetComponent<ShapeCollisionChecker>().squeezeX &&
                        !_ts.gameObject.GetComponent<ShapeCollisionChecker>().squeezeY)
                    {
                        _ts.localScale += growthSpeedX + _ts.gameObject.GetComponent<ShapeCollisionChecker>().growthSpeed;
                    }
                }
            }
        }
		shapesTransforms = GetShapesList();
	}

    /// <summary>
    /// Contains action to be performed when Player2 presses their button - PULLs shapes towards the player
    /// </summary>
    public void Player2Action()
    {
        shapesTransforms = GetShapesList();
        foreach (Transform _ts in shapesTransforms)
        {
            if (_ts.tag == CheckForColour(_player2))
            {
                _ts.position = Vector3.MoveTowards(_ts.position, _player2.transform.position , pullSpeed*Time.deltaTime);
            }
        }
	}


    /// <summary>
    /// Contains action to be performed when Player3 presses their button - SWITCHes shape with an identically-sized different shape
    /// </summary>
    public void Player3Action()
    {
        foreach (Transform _ts in shapesTransforms)
        {
            if (_ts != null)
            {
                if (_ts.tag == CheckForColour(_player3))
                {
                    string _name = _ts.name;
                    string _tag = _ts.tag;
                    Vector3 _pos = _ts.position;
                    Quaternion _rot = _ts.rotation;
                    Vector3 _sca = _ts.localScale;
                    bool _grow = _ts.GetComponent<ShapeCollisionChecker>().unlimitedGrowth;

                    DestroyImmediate(_ts.gameObject);

                    if (_name.Contains("Circle"))
                    {
                        GameObject _gb;
                        _gb = Instantiate(square, _pos, _rot, transform);
                        _gb.transform.localScale = _sca;
                        _gb.tag = _tag;
                        _gb.GetComponent<Rigidbody>().Sleep();
                        _gb.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        _gb.GetComponent<ShapeCollisionChecker>().unlimitedGrowth = _grow;
                        abilityPlayer.Switch_1.Play();
                    }
                    if (_name.Contains("Square"))
                    {
                        GameObject _gb;
                        _gb = Instantiate(triangle, _pos, _rot, transform);
                        _gb.transform.localScale = _sca;
                        _gb.tag = _tag;
                        _gb.GetComponent<Rigidbody>().Sleep();
                        _gb.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        _gb.GetComponent<ShapeCollisionChecker>().unlimitedGrowth = _grow;
                        abilityPlayer.Switch_2.Play();
					}
                    if (_name.Contains("Triangle"))
                    {
                        GameObject _gb;
                        _gb = Instantiate(circle, _pos, _rot, transform);
                        _gb.transform.localScale = _sca;
                        _gb.tag = _tag;
                        _gb.GetComponent<Rigidbody>().Sleep();
                        _gb.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        _gb.GetComponent<ShapeCollisionChecker>().unlimitedGrowth = _grow;
                        abilityPlayer.Switch_3.Play();
					}
                }
            }
        }
        shapesTransforms = GetShapesList();
        SetShapeColours();
    }

    /// <summary>
    /// Contains action to be performed when Player4 presses their button - FLOATs shapes upwards for as long as the button is held, then returns them slowly to the ground
    /// </summary>
    public void Player4Action()
    {
		shapesTransforms = GetShapesList();
		foreach (Transform _ts in shapesTransforms)
        {
            if (_ts.tag == CheckForColour(_player4))
            {
                Vector3 _vec3 = _ts.position;
                _vec3.y += floatSpeed * Time.deltaTime;
                _ts.GetComponent<Rigidbody>().useGravity = false;
                _ts.GetComponent<Rigidbody>().Sleep();
                originalConstraints = _ts.GetComponent<Rigidbody>().constraints;
				_ts.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
				_ts.position = _vec3;
            }
        }
        stopFloating = false;
    }

    #endregion

    #region Shape Managing

    /// <summary>
    /// Checks which colour will be effected by the player's ability
    /// </summary>
    /// <returns>It returns true if this gameObject has the right colour tag</returns>
    private string CheckForColour(GameObject playGameObject)
    {
        if (playGameObject.gameObject.GetComponent<GamePlayer>().colourRed)
        {
            return "Red";
        }
        if (playGameObject.gameObject.GetComponent<GamePlayer>().colourGreen)
        {
            return "Green";
        }
        if (playGameObject.gameObject.GetComponent<GamePlayer>().colourBlue)
        {
            return "Blue";
        }
        if (playGameObject.gameObject.GetComponent<GamePlayer>().colourYellow)
        {
            return "Yellow";
        }
        return "";
    }

    /// <summary>
    /// Makes a list of all shapes in the Shape Manager
    /// </summary>
    /// <returns>List of GameObject type</returns>
    public Transform[] GetShapesList()
    {
        Transform[] tf = GetComponentsInChildren<Transform>();
        return tf;
    }

    /// <summary>
    /// Changes the shapes material colour
    /// </summary>
    private void SetShapeColours()
    {
        foreach (Transform _tm in shapesTransforms)
        {
            if (_tm.tag == "Green")
            {
                _tm.GetComponent<Renderer>().material = Resources.Load("Materials/ShapeGreen") as Material;
            }
            if (_tm.tag == "Blue")
            {
                _tm.GetComponent<Renderer>().material = Resources.Load("Materials/ShapeBlue") as Material;
            }
            if (_tm.tag == "Red")
            {
                _tm.GetComponent<Renderer>().material = Resources.Load("Materials/ShapeRed") as Material;
            }
            if (_tm.tag == "Yellow")
            {
                _tm.GetComponent<Renderer>().material = Resources.Load("Materials/ShapeYellow") as Material;
            }
        }
    }

    #endregion

    #region IEnumerators

    //IEnumerator SmallPause() {
    //	yield return new WaitForSeconds(2f);
    //}

    #endregion
}