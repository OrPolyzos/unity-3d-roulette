using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class GameController : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{

	public GameObject Sphere;
    public GameObject Roulette;
    public GameObject Camera;
    public GameObject MessagePanel;
    public GameObject Players;

	public Button Jugar;
	public Sprite JugarNormal;
	public Sprite JugarLit;
	public Button Cancel;

    public Button[] PlayerButtons = new Button[8];
    public GameObject[] Chips = new GameObject[13];
    public Sprite[] PlayerSprites = new Sprite[8];
    public Sprite[] PlayerSpritesLit = new Sprite[8];

    private int[] PlayerCredits = new int[8];
    private int[] PlayerOriginalCredits = new int[8];
    private int[,] PlayerBets = new int[8, 13];
    private int[] TotalBets = new int[13];
    public string[] PlayerNames = new string[8];

	public bool isJugarClicked = false;
    public bool playerButtonClicked = false;
    public string ActivePlayer;

	private Stack undoBets,tmpBets;
	
    // Use this for initialization
	void Start () {
		undoBets = new Stack();
        Players = GameObject.Find("Players&Jugar");

        for (int i = 0; i < 8; i++)
        {
            PlayerCredits[i] = 0;
            int temp = i + 1;
            PlayerNames[i] = "Player " +temp+ "";
            for (int j = 0; j < 13; j++)
            {
                PlayerBets[i, j] = 0;
                TotalBets[j] = 0;
            }
        }
        KeepPlayerCreditsUpdated();

		Jugar.enabled = false;
		Cancel.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerCredits[0] = PlayerCredits[0] + 10;
            PlayerOriginalCredits[0] = PlayerCredits[0];
            KeepPlayerCreditsUpdated();
            MessagePanel.transform.GetChild(0).GetComponent<Text>().text = "Added 10 credits to Player 1!";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerCredits[1] = PlayerCredits[1] + 10;
            PlayerOriginalCredits[1] = PlayerCredits[1];
            KeepPlayerCreditsUpdated();
            MessagePanel.transform.GetChild(0).GetComponent<Text>().text = "Added 10 credits to Player 2!";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerCredits[2] = PlayerCredits[2] + 10;
            PlayerOriginalCredits[2] = PlayerCredits[2];
            KeepPlayerCreditsUpdated();
            MessagePanel.transform.GetChild(0).GetComponent<Text>().text = "Added 10 credits to Player 3!";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayerCredits[3] = PlayerCredits[3] + 10;
            PlayerOriginalCredits[3] = PlayerCredits[3];
            KeepPlayerCreditsUpdated();
            MessagePanel.transform.GetChild(0).GetComponent<Text>().text = "Added 10 credits to Player 4!";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlayerCredits[4] = PlayerCredits[4] + 10;
            PlayerOriginalCredits[4] = PlayerCredits[4];
            KeepPlayerCreditsUpdated();
            MessagePanel.transform.GetChild(0).GetComponent<Text>().text = "Added 10 credits to Player 5!";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            PlayerCredits[5] = PlayerCredits[5] + 10;
            PlayerOriginalCredits[5] = PlayerCredits[5];
            KeepPlayerCreditsUpdated();
            MessagePanel.transform.GetChild(0).GetComponent<Text>().text = "Added 10 credits to Player 6!";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            PlayerCredits[6] = PlayerCredits[6] + 10;
            PlayerOriginalCredits[6] = PlayerCredits[6];
            KeepPlayerCreditsUpdated();
            MessagePanel.transform.GetChild(0).GetComponent<Text>().text = "Added 10 credits to Player 7!";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            PlayerCredits[7] = PlayerCredits[7] + 10;
            PlayerOriginalCredits[7] = PlayerCredits[7];
            KeepPlayerCreditsUpdated();
            MessagePanel.transform.GetChild(0).GetComponent<Text>().text = "Added 10 credits to Player 8!";
        }
        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                switch (EventSystem.current.currentSelectedGameObject.name)
                {
                    case "Player 1":
                    case "Player 2":
                    case "Player 3":
                    case "Player 4":
                    case "Player 5":
                    case "Player 6":
                    case "Player 7":
                    case "Player 8":
                        KeepPlayerBetsUpdated();
                        break;
                }
            }
        }
        else
        {
            KeepTotalBetsUpdated();
        }

    }

	public void startGame()
	{
        GetComponent<State>().GameState = "GameStarted";
		if (isJugarClicked) {
			Jugar.image.overrideSprite = JugarNormal;
		} else {
			isJugarClicked = true;
			Jugar.image.overrideSprite = JugarLit;
			Sphere.GetComponent<Rigidbody> ().isKinematic = false;
            Roulette.GetComponent<Rotate>().enabled = true;
            Camera.GetComponent<Movement>().enabled = true;
			Cancel.enabled = false;
		}
	}

    public void PlayerClick()
    {
        switch (EventSystem.current.currentSelectedGameObject.name)
        {
            case "Player 1":
                PlayerButtons[0].image.overrideSprite = PlayerSpritesLit[0];
                ClearRestButtons(EventSystem.current.currentSelectedGameObject.GetComponent<Button>());
                break;
            case "Player 2":
                PlayerButtons[1].image.overrideSprite = PlayerSpritesLit[1];
                ClearRestButtons(EventSystem.current.currentSelectedGameObject.GetComponent<Button>());
                KeepPlayerBetsUpdated();
                break;
            case "Player 3":
                PlayerButtons[2].image.overrideSprite = PlayerSpritesLit[2];
                ClearRestButtons(EventSystem.current.currentSelectedGameObject.GetComponent<Button>());
                break;
            case "Player 4":
                PlayerButtons[3].image.overrideSprite = PlayerSpritesLit[3];
                ClearRestButtons(EventSystem.current.currentSelectedGameObject.GetComponent<Button>());
                break;
            case "Player 5":
                PlayerButtons[4].image.overrideSprite = PlayerSpritesLit[4];
                ClearRestButtons(EventSystem.current.currentSelectedGameObject.GetComponent<Button>());
                break;
            case "Player 6":
                PlayerButtons[5].image.overrideSprite = PlayerSpritesLit[5];
                ClearRestButtons(EventSystem.current.currentSelectedGameObject.GetComponent<Button>());
                break;
            case "Player 7":
                PlayerButtons[6].image.overrideSprite = PlayerSpritesLit[6];
                ClearRestButtons(EventSystem.current.currentSelectedGameObject.GetComponent<Button>());
                break;
            case "Player 8":
                PlayerButtons[7].image.overrideSprite = PlayerSpritesLit[7];
                ClearRestButtons(EventSystem.current.currentSelectedGameObject.GetComponent<Button>());
                break;
        }
        ActivePlayer = EventSystem.current.currentSelectedGameObject.name;
        MessagePanel.transform.GetChild(0).GetComponent<Text>().text = ActivePlayer;
        KeepPlayerCreditsUpdated();
       //KeepPlayerBetsUpdated();
    }

    public void KeepPlayerCreditsUpdated()
    {
        for (int i = 0; i < 8; i++)
        {
            if (PlayerCredits[i] == 0)
            {
                PlayerButtons[i].transform.GetChild(0).GetComponent<Text>().text = "";
            }
            else
            {
                PlayerButtons[i].transform.GetChild(0).GetComponent<Text>().text = PlayerCredits[i].ToString();
            }
        }
		setJugarState ();
    }

//	IEnumerator delayforMessage(){
//		yield return new WaitForSeconds (5);
//	}

	public IEnumerator displayAwardInformation()
	{
		int WinningNumber = 0;
		if (Sphere.GetComponent<GetTheNumber> ().WinningNumber.Contains ("Sensor N0")) {
			WinningNumber = 0;
		} else {
			 WinningNumber = int.Parse (Sphere.GetComponent<GetTheNumber> ().WinningNumber);
		}
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 13; j++) {
				if (PlayerBets [i, j] > 0 && j==WinningNumber) {
					yield return new WaitForSeconds (2);
					Debug.Log ("Winning Player is" + PlayerNames[i] + "and Number of Bets" + PlayerBets [i, j]);
					this.GetComponent<State>().GameState = "AwardInformation";
					this.GetComponent<State> ().WinningPlayerName = PlayerNames [i];
					for (int k = 1; k <= PlayerBets [i, j]; k++) {
						this.GetComponent<State> ().WinningAmount = (k * 5).ToString();
                        PlayerCredits[i] = PlayerCredits[i] + 12;
                        KeepPlayerCreditsUpdated();
                        yield return new WaitForSeconds (0.5f);
					}

				}
			}
		}
		EmptyAllPlayerBets ();
		setJugarState ();
	}

    public void KeepPlayerBetsUpdated()
    {
        for (int i = 0; i < 8; i++)
        {
            if (PlayerNames[i] == ActivePlayer)
            {
                for (int j = 0; j < 13; j++)
                {
                    if (PlayerBets[i,j] == 0)
                    {
                        Chips[j].transform.GetChild(1).GetComponent<Text>().text = "";
                    }
                    else
                    {
                        Chips[j].transform.GetChild(1).GetComponent<Text>().text = PlayerBets[i, j].ToString();
                    }
                }
            }
        }
    }

	public void EmptyAllPlayerBets(){
		Debug.Log ("Clear Bets");
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 13; j++) {
				PlayerBets [i, j] = 0;
				TotalBets [j] = 0;
				Chips[j].transform.GetChild(1).GetComponent<Text>().text = "";
			}
		}
	}

	public void setJugarState(){
		bool isBetAvailable = false;
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 13; j++) {
				if (PlayerBets [i, j] > 0) {
					isBetAvailable = true;
					break;
				}
			}
		}
		if (!isBetAvailable) {
			Jugar.enabled = false;
			Cancel.enabled = false;
		} else {
			Jugar.enabled = true;
			Cancel.enabled = true;
		}
	}
    public void KeepTotalBetsUpdated()
    {
        for (int j = 0; j < 13; j++)
        {
            if (TotalBets[j] == 0)
            {
                Chips[j].transform.GetChild(1).GetComponent<Text>().text = "";
            }
            else
            {
                Chips[j].transform.GetChild(1).GetComponent<Text>().text = TotalBets[j].ToString();
            }
        }

    }
    public void ClearRestButtons(Button _ActivePlayer)
    {
        for (int i=0; i<8; i++)
        {
            if (PlayerButtons[i] != _ActivePlayer)
            {
                PlayerButtons[i].image.overrideSprite = PlayerSprites[i];
            }

        }
    }

	public void CancelDblClick()
	{
		Debug.Log("inside Cancel Double Click for " + ActivePlayer );
		tmpBets = new Stack ();
		while (undoBets.Count != 0) {
			string ChipnPlayerinfo = undoBets.Pop().ToString();
			int playernum = int.Parse(ChipnPlayerinfo.Split(',')[0]);
			int betnum = int.Parse(ChipnPlayerinfo.Split(',')[1]);
			Debug.Log ("player num: " + playernum + "  Bet number: " + betnum + " Player Name " + PlayerNames [playernum] );
			if (PlayerNames [playernum] == ActivePlayer) {
				PlayerCredits [playernum] += 1;
				TotalBets [betnum] -= 1;
			} else {
				tmpBets.Push (ChipnPlayerinfo);
			}
		}
		if ( tmpBets.Count > 0 )
			undoBets = tmpBets;
//		for (int i = 0; i < 8; i++)
//		{
//			//int diff = PlayerOriginalCredits[i] - PlayerCredits[i];
//				PlayerCredits[i] = PlayerOriginalCredits[i];
//				for (int j = 0; j < 13; j++)
//				{
//					int diff = PlayerBets[i, j];
//					PlayerBets[i, j] = 0;
//					if (TotalBets[j] != 0)
//					{
//						TotalBets[j] = TotalBets[j] - diff;
//					}
//
//				}
//		}
		KeepTotalBetsUpdated();
		KeepPlayerCreditsUpdated();
		setJugarState ();
	}


    public void CancelClick()
    {
		if (undoBets.Count == 0) {
			return;
		}

//        for (int i = 0; i < 8; i++)
//        {
//            if (PlayerNames[i] == ActivePlayer)
//            {
//                //int diff = PlayerOriginalCredits[i] - PlayerCredits[i];
//                PlayerCredits[i] = PlayerOriginalCredits[i];
//                for (int j = 0; j < 13; j++)
//                {
//                    int diff = PlayerBets[i, j];
//                    PlayerBets[i, j] = 0;
//                    if (TotalBets[j] != 0)
//                    {
//                        TotalBets[j] = TotalBets[j] - diff;
//                    }
//
//                }
//            }
//        }
		tmpBets = new Stack();
		while (undoBets.Count > 0) {
			string ChipnPlayerinfo = undoBets.Pop().ToString();
			int playernum = int.Parse(ChipnPlayerinfo.Split(',')[0]);
			int betnum = int.Parse(ChipnPlayerinfo.Split(',')[1]);
			Debug.Log ("player num: " + playernum + "  Bet number: " + betnum);
			if (PlayerNames [playernum] == ActivePlayer) {
				PlayerCredits [playernum] += 1;
				TotalBets [betnum] -= 1;
				break;
			} else {
				tmpBets.Push (ChipnPlayerinfo);
			}
		}
		while (tmpBets.Count > 0) {
			undoBets.Push (tmpBets.Pop());
		}
        KeepTotalBetsUpdated();
        KeepPlayerCreditsUpdated();
		setJugarState ();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("here");
        Debug.Log(EventSystem.current.currentSelectedGameObject.name);

        if (EventSystem.current.currentSelectedGameObject.name == "Cancel")
        {
            Debug.Log("first block");
            int tap = eventData.clickCount;
            if (tap == 2)
            {
                Debug.Log("2 tap block");
            }
        }
    }

    public void ChipClick()
    {
		string ChipnPlayerInfo;
        if (ActivePlayer != String.Empty)
        {
            for (int i = 0; i < 8; i++)
            {
                if (PlayerNames[i] == ActivePlayer)
                {
                    GameObject ClickedChip = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
                    for (int j = 0; j < 13; j++)
                    {
                        if (ClickedChip.name == "Chip " + j + "")
                        {
                            if (PlayerCredits[i] > 0 && TotalBets[j] < 40)
                            {
								ChipnPlayerInfo = i.ToString () + "," + j.ToString ();
								undoBets.Push (ChipnPlayerInfo);
                                PlayerCredits[i]--;
                                PlayerBets[i, j]++;
                                TotalBets[j]++;
                                KeepPlayerCreditsUpdated();
                                KeepTotalBetsUpdated();
                                //KeepPlayerBetsUpdated();
                            }
                            else
                            {
                                MessagePanel.transform.GetChild(0).GetComponent<Text>().text = "Not enough credits!";
                            }
                        }
                    }
                }
            }
        }
    }
}
