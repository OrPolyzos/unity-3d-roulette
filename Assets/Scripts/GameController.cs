using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    // Roulette ball - GameObject: Sphere
	public GameObject Sphere;
    // Rotating part of Roulette - GameObject: Number_Plates
    public GameObject Roulette;
    // Used just to find the players of the game - GameObject: Players&Jugar
    public GameObject Players;
    // Main Camera
    public GameObject Camera;
    // Message panel on top of Canvas - GameObject: MessagePanel
    public GameObject MessagePanel;
    public GameObject MessagePanelText;

    // Jugar button (used to initiate play mode) - Button: Jugar
	public Button Jugar;
    // Sprite of Jugar button when it is not pressed - Sprite: Jugar_button_2
	public Sprite JugarNormal;
    // Sprite of Jugar button when it is pressed - Sprite: Jugar_LIT_2
	public Sprite JugarLit;

    // Cancel button (used to cancel last bet (on click)/ all bets (on double click) of the active player - Button: Cancel
	public Button Cancel;

    //Array of strings to hold players' names - Strings: "Player 1" - "Player 8"
    public string[] PlayerNames = new string[8];
    // Array of buttons to hold players' buttons - Buttons: Player 1 - Player 8
    public Button[] PlayerButtons = new Button[8];
    // Array of Sprite to hold players' sprites to use when they are not active players: Sprites: p1 - p8
    public Sprite[] PlayerSprites = new Sprite[8];
    // Array of Sprite to hold players' sprites to use when they are active players: Sprites: p1_LIT - p8_LIT
    public Sprite[] PlayerSpritesLit = new Sprite[8];
    // Array to hold players' total credits
    private int[] PlayerCredits = new int[8];
    // Array to hold the bets on each number of each player - 8 players / 13 numbers
    private int[,] PlayerBets = new int[8, 13];
    // Array to hold the total bets of all players on each number - 13 numbers
    private int[] TotalBets = new int[13];
    // Array of chip gameobjects to hold roulete chips - GameObjects: Chip 0 - Chip 1
    public GameObject[] Chips = new GameObject[13];

    // String to hold the Active player - String: "Player 1" - "Player 8" / "Noone"
    public string ActivePlayer;

    // Bool to check if the Jugar button is clicked;
    public bool isJugarClicked = false;

    // Array of Stacks for each player to use for Cancel button functionality, they hold Chip Number & Bet Ammount
    private Stack[] undoBets = new Stack[8];

    // Audioclips
    public AudioClip TickSound;
    public AudioClip ChipSingleSound;

    public bool ThereisWinner = false;
    public bool ShowingAwards = false;
    public bool ExecutingMultibet = false;
    // Use this for initialization
	void Start () {
        // Initializing the undoBets stacks for each player
        for (int i = 0; i < 8; i++)
        {
            undoBets[i] = new Stack();
        }
        
        // Finding the players of the game
        Players = GameObject.Find("Players&Jugar");
        // For each one of the players
        for (int i = 0; i < 8; i++)
        {
            // Setting the player's credits to 0
            PlayerCredits[i] = 0;
            int temp = i + 1;
            // Setting the player's name in the array
            PlayerNames[i] = "Player " +temp+ "";
            // For each one of the available numbers to bet
            for (int j = 0; j < 13; j++)
            {
                // Setting the bets of each player to 0
                PlayerBets[i, j] = 0;
                // Setting total bets of all players for each numbers to 0
                TotalBets[j] = 0;
            }
        }
        // Show the credits of all players on the user
        KeepPlayerCreditsUpdated();
        // Disabling Jugar button
        Jugar.enabled = false;
        // Disabling Cancel button
		Cancel.interactable = false;
    }
	
	// Update is called once per frame
	void Update () {
        //Checking for 1-8 alphanum press
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Onpress adding 10 credits to the correspondent playercredits[i]
            PlayerCredits[0] = PlayerCredits[0] + 10;
            // Show the credits change to the user
            KeepPlayerCreditsUpdated();
            // Change text component to inform the user
            MessagePanel.transform.GetChild(0).GetComponent<Text>().text = "Added 10 credits to Player 1!";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerCredits[1] = PlayerCredits[1] + 10;
            KeepPlayerCreditsUpdated();
            MessagePanel.transform.GetChild(0).GetComponent<Text>().text = "Added 10 credits to Player 2!";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerCredits[2] = PlayerCredits[2] + 10;
            KeepPlayerCreditsUpdated();
            MessagePanel.transform.GetChild(0).GetComponent<Text>().text = "Added 10 credits to Player 3!";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayerCredits[3] = PlayerCredits[3] + 10;
            KeepPlayerCreditsUpdated();
            MessagePanel.transform.GetChild(0).GetComponent<Text>().text = "Added 10 credits to Player 4!";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlayerCredits[4] = PlayerCredits[4] + 10;
            KeepPlayerCreditsUpdated();
            MessagePanel.transform.GetChild(0).GetComponent<Text>().text = "Added 10 credits to Player 5!";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            PlayerCredits[5] = PlayerCredits[5] + 10;
            KeepPlayerCreditsUpdated();
            MessagePanel.transform.GetChild(0).GetComponent<Text>().text = "Added 10 credits to Player 6!";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            PlayerCredits[6] = PlayerCredits[6] + 10;
            KeepPlayerCreditsUpdated();
            MessagePanel.transform.GetChild(0).GetComponent<Text>().text = "Added 10 credits to Player 7!";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            PlayerCredits[7] = PlayerCredits[7] + 10;
            KeepPlayerCreditsUpdated();
            MessagePanel.transform.GetChild(0).GetComponent<Text>().text = "Added 10 credits to Player 8!";
        }
    }

    //Function to user on end of each game so everything is prepared again for new game
    public void GetEverythingReadyToPlay()
    {
        // Call the function to move the camera back at starting position
        Camera.GetComponent<Movement>().MoveCameraUp();

        // Set Sphere to Kinematic so no gravity applies
        Sphere.GetComponent<Rigidbody>().isKinematic = true;
        // Call the function to move the Sphere at starting position
        Sphere.GetComponent<BallMov>().MoveBallUp();
        // Set the needed boolean again to false
        Sphere.GetComponent<BallForce>().Icollided = false;

        // For each chip in Chips[]
        foreach (GameObject chipbutton in Chips)
        {
            // Change the Button components of the 2 children (Chip,Text) of each chip to interactable
            for (int i = 0; i < 2; i++)
            {
                chipbutton.GetComponentsInChildren<Button>()[i].interactable = true;
            }
        }

        // Change the isJugarClicked back to false
        isJugarClicked = false;
        // Change the sprite of Jugar back to normal
        Jugar.image.overrideSprite = JugarNormal;
        // Enable Jugar button
        Jugar.enabled = true;

        // Change active player to none 
        ActivePlayer = String.Empty;

        // Empty the stacks
        //undoBets.Clear();
        for (int i = 0; i < 8; i++)
        {
            undoBets[i].Clear();
        }
        MessagePanelText.GetComponent<Text>().text = "Start playing!";
        // That 's it setting GameState back to "Idle"
        this.GetComponent<State>().GameState = "Idle";
    }

    //Function to start the game when a player clicks Jugar button
	public void startGame()
	{
        // Checking for Gamestate to be "Idle"
        if (GetComponent<State>().GameState == "Idle")
        {
            MessagePanel.GetComponent<PanelMovement>().IWasMovedUp = false;
            // Setting GameState to "GameStarted"
            GetComponent<State>().GameState = "GameStarted";

            for (int i = 0; i<8; i++)
            {
                PlayerButtons[i].image.overrideSprite = PlayerSprites[i];
            }

            // For each Chip in Chips[]
            foreach (GameObject chipbutton in Chips)
            {
                // Change the Button components of the 2 children (Chip,Text) of each chip to non-interactable
                for (int i = 0; i < 2; i++)
                {
                    chipbutton.GetComponentsInChildren<Button>()[i].interactable = false;
                }
            }

            // Change the boolean isJugarClicked to true
            isJugarClicked = true;
            // Change the sprite of Jugar button to JugarLit
            Jugar.image.overrideSprite = JugarLit;
            // Disable the Jugar button
            Jugar.enabled = false;
            
            // Change the Cancel button to non-interactable
            Cancel.interactable = false;

            // Set Sphere to non kinematic so gravity applies
            Sphere.GetComponent<Rigidbody>().isKinematic = false;
            // Call the function to set the starting rotating speed of the roulette
            Roulette.GetComponent<Rotate>().SetSpeedAgain();
            // Start moving the camera down
            Camera.GetComponent<Movement>().MoveCameraDown();

        }
        
    }

    // Function to be called when a player button is clicked and held (gets parameter the clicked object from PlayerClick.cs)
    public void PlayerHoldClick(GameObject ClickedObject)
    {
        // Checking for non-null gameobject
        if (ClickedObject != null)
        {
            // Check the name of the clicked object
            switch (ClickedObject.name)
            {
                // if name == "Player 1"
                case "Player 1":
                    // Change the button spite of first player to the correspondent Lit sprite
                    PlayerButtons[0].image.overrideSprite = PlayerSpritesLit[0];
                    break;
                case "Player 2":
                    PlayerButtons[1].image.overrideSprite = PlayerSpritesLit[1];
                    break;
                case "Player 3":
                    PlayerButtons[2].image.overrideSprite = PlayerSpritesLit[2];
                    break;
                case "Player 4":
                    PlayerButtons[3].image.overrideSprite = PlayerSpritesLit[3];
                    break;
                case "Player 5":
                    PlayerButtons[4].image.overrideSprite = PlayerSpritesLit[4];
                    break;
                case "Player 6":
                    PlayerButtons[5].image.overrideSprite = PlayerSpritesLit[5];
                    break;
                case "Player 7":
                    PlayerButtons[6].image.overrideSprite = PlayerSpritesLit[6];
                    break;
                case "Player 8":
                    PlayerButtons[7].image.overrideSprite = PlayerSpritesLit[7];
                    break;
            }
            // Function to set all player button sprites to the normal player sprite
            ClearRestButtons(ClickedObject.GetComponent<Button>());
            // Set ActivePlayer equal to the clickedobject name
            ActivePlayer = ClickedObject.name;
            // Function to update the specific bets of only the active player
            KeepPlayerBetsUpdated();
            // Show a message to inform who is the current active player
            MessagePanel.transform.GetChild(0).GetComponent<Text>().text = ActivePlayer;
            // Function to fix Jugar / Cancel state
            setJugarState();
        }
    }

    // Function to be called when the user stops holding a player button
    public void PlayerClick()
    {
        // Show all the bets of the all players to the user
        KeepTotalBetsUpdated();
    }

    // Function to show all the players' credits to the user
    public void KeepPlayerCreditsUpdated()
    {
        // For each player
        for (int i = 0; i < 8; i++)
        {
            // Checking if player credits are 0
            if (PlayerCredits[i] == 0)
            {
                // Then change the text to ""
                PlayerButtons[i].transform.GetChild(0).GetComponent<Text>().text = "";
            }
            else
            {
                // else change the text to the player 's credits
                PlayerButtons[i].transform.GetChild(0).GetComponent<Text>().text = PlayerCredits[i].ToString();
            }
        }
        // Fixes enable/disable for Jugar/Cancel button
		setJugarState ();
    }

    // Function to display award information for each player
	public IEnumerator displayAwardInformation()
	{
        // Initializing two color objects for the disable color of button component of each Chip's children (Chip,Text)
        Color DisColor1 = new Color(101, 101, 101, 255);
        Color DisColor2 = new Color(200, 200, 200, 128);
        
        // For each player
        for (int i = 0; i < 8; i++)
        {
            // Setting each player sprite to normal
            PlayerButtons[i].image.overrideSprite = PlayerSprites[i];
        }

        // Wait 2 seconds
        yield return new WaitForSeconds(2);

        //Boolean to check if there is winner
        ThereisWinner = false;
        ShowingAwards = false;

        // Initialize WinningNumber, set it 0 for exceptions
        int WinningNumber = 0;
        // Get the actual winning number
        WinningNumber = int.Parse(Sphere.GetComponent<GetTheNumber>().WinningNumber);

        for (int i = 0; i < 8; i++)
        {
            // For each number
            for (int j = 0; j < 13; j++)
            {
                // Check if they did bet on the number and if the number won
                if (PlayerBets[i,j] > 0 && j == WinningNumber)
                {
                    // Since we are in here, there is winner
                    ThereisWinner = true;
                    // Change the player's sprit to the correspondent Lit sprite
                    PlayerButtons[i].image.overrideSprite = PlayerSpritesLit[i];
                }
            }
        }

        // Change the text color of the winning number chip to white
        Chips[WinningNumber].GetComponentInChildren<Text>().color = Color.white;
        // Enable the outline component for the winning number chip
        Chips[WinningNumber].GetComponentInChildren<Outline>().enabled = true;
        // For the 2 children of the winning number chip (Text,Component)
        for (int i = 0; i < 2; i++)
        {
            // Set their button disabled colors to the correspondent normal colors
            // This way they remain non-interactable, but they stand out
            ColorBlock cb = Chips[WinningNumber].GetComponentsInChildren<Button>()[i].colors;
            if (i == 0)
            {
                DisColor1 = cb.disabledColor;
            }
            else
            {
                DisColor2 = cb.disabledColor;
            }
            cb.disabledColor = cb.normalColor;
            Chips[WinningNumber].GetComponentsInChildren<Button>()[i].colors = cb;
        }
        
        // Set GameState to "AwardInformation"
        this.GetComponent<State>().GameState = "AwardInformation";

        if (ThereisWinner)
        {
            // For each number
            for (int j = 0; j < 13; j++)
            {
                if (TotalBets[j] > 0 && j == WinningNumber)
                {
                    // Since we are in here, we are going to show awards
                    ShowingAwards = true;
                    int Pesos_Amount = TotalBets[j] * 5 * 12;
                    for (int cup = 0; cup <= Pesos_Amount; cup = cup + 5)
                    {
                        int displayamount = cup;
                        this.GetComponent<State>().WinningAmount = displayamount.ToString();
                        yield return new WaitForSeconds(0.05f);

                    }
                    for (int i = 0; i < 8; i++)
                    {
                        if (PlayerBets[i, j] > 0)
                        {
                            // Set the WinningPlayerName equal to the playername
                            this.GetComponent<State>().WinningPlayerName = PlayerNames[i];
                            for (int flashing = 0; flashing <= 4; flashing++)
                            {
                                if (PlayerButtons[i].image.overrideSprite == PlayerSpritesLit[i])
                                {
                                    PlayerButtons[i].image.overrideSprite = PlayerSprites[i];
                                }
                                else
                                {
                                    PlayerButtons[i].image.overrideSprite = PlayerSpritesLit[i];
                                }
                                MessagePanelText.SetActive(!MessagePanelText.activeSelf);
                                yield return new WaitForSeconds(0.25f);
                            }
                            MessagePanelText.SetActive(true);
                            // Change the player's sprit to the correspondent Lit sprite
                            PlayerButtons[i].image.overrideSprite = PlayerSpritesLit[i];
                            for (int k = 0; k < PlayerBets[i, j]; k++)
                            {
                                // Change Text size/color and enabble outline to emphasize the sum of his earnings
                                MessagePanel.GetComponentInChildren<Text>().fontSize = 85;
                                MessagePanel.GetComponentInChildren<Text>().color = Color.white;
                                MessagePanel.GetComponentInChildren<Outline>().enabled = true;
                                // Change the Text size/color and enable Outline to emphasize on the earnings
                                PlayerButtons[i].GetComponentInChildren<Text>().fontSize = 50;
                                PlayerButtons[i].GetComponentInChildren<Text>().color = Color.white;
                                PlayerButtons[i].GetComponentInChildren<Outline>().enabled = true;
                                // Set the WinningAmmount as the iteration number * 5 (Pesos)
                                this.GetComponent<State>().WinningAmount = Pesos_Amount.ToString();
                                for (int Credit_Addition = 0; Credit_Addition < 12; Credit_Addition++)
                                {
                                    Pesos_Amount = Pesos_Amount - 5;
                                    // Set the WinningAmmount as the iteration number * 5 (Pesos)
                                    this.GetComponent<State>().WinningAmount = Pesos_Amount.ToString();
                                    PlayerCredits[i] = PlayerCredits[i] + 1;
                                    // Show the increment to the user
                                    KeepPlayerCreditsUpdated();
                                    // Play a tick sound
                                    this.GetComponent<AudioSource>().clip = TickSound;
                                    this.GetComponent<AudioSource>().Play();
                                    // Wait 0.15 seconds
                                    yield return new WaitForSeconds(0.05f);
                                }
                                // Wait 0.15 seconds (a little more than normal)
                                //yield return new WaitForSeconds(0.15ff);
                            }

                        }
                        // Set the Player sprite to normal sprite again
                        PlayerButtons[i].image.overrideSprite = PlayerSprites[i];
                        // Change the playerbutton Text size/color to default and disable outline component
                        PlayerButtons[i].GetComponentInChildren<Text>().fontSize = 25;
                        PlayerButtons[i].GetComponentInChildren<Text>().color = Color.black;
                        PlayerButtons[i].GetComponentInChildren<Outline>().enabled = false;
                        // Wait 0.25second for the next player
                        yield return new WaitForSeconds(0.75f);
                    }
                }
                // Change MessagePanel Text size/color to default and disable outline component
                MessagePanel.GetComponentInChildren<Text>().fontSize = 75;
                MessagePanel.GetComponentInChildren<Text>().color = Color.black;
                MessagePanel.GetComponentInChildren<Outline>().enabled = false;
            }
        }





        //    // For each player
        //    for (int i = 0; i < 8; i++)
        //    {
        //        // For each number
        //        for (int j = 0; j < 13; j++)
        //        {
        //            // Check if they did bet on the number and if the number won
        //            if (PlayerBets[i,j] > 0 && j == WinningNumber)
        //            {
        //                // Since we are in here, we are going to show awards
        //                ShowingAwards = true;
        //                // Set the WinningPlayerName equal to the playername
        //                this.GetComponent<State>().WinningPlayerName = PlayerNames[i];
        //                // Change the player's sprit to the correspondent Lit sprite
        //                PlayerButtons[i].image.overrideSprite = PlayerSpritesLit[i];
        //                this.GetComponent<State>().WinningAmount = (TotalBets[j] * 5).ToString();
        //                // Loop to calculate his earnings
        //                for (int k = 1; k <= PlayerBets[i, j]; k++)
        //                {
        //                    // Checking if this is the last iteration
        //                    if (k == PlayerBets[i, j])
        //                    {
        //                        // Change Text size/color and enabble outline to emphasize the sum of his earnings
        //                        MessagePanel.GetComponentInChildren<Text>().fontSize = 85;
        //                        MessagePanel.GetComponentInChildren<Text>().color = Color.white;
        //                        MessagePanel.GetComponentInChildren<Outline>().enabled = true;
        //                        // Set the WinningAmmount as the iteration number * 5 (Pesos)
        //                        this.GetComponent<State>().WinningAmount = (k * 5).ToString();
        //                        // Play a tick sound
        //                        this.GetComponent<AudioSource>().clip = TickSound;
        //                        this.GetComponent<AudioSource>().Play();
        //                        // Wait 0.5 seconds (a little more than normal)
        //                        yield return new WaitForSeconds(0.5f);
        //                    }
        //                    //else (on all rest iterations)
        //                    else
        //                    {
        //                        // Keep the Text size/color as default and disable outline component
        //                        MessagePanel.GetComponentInChildren<Text>().fontSize = 75;
        //                        MessagePanel.GetComponentInChildren<Text>().color = Color.black;
        //                        MessagePanel.GetComponentInChildren<Outline>().enabled = false;
        //                        // Set the WinningAmmount as the iteration number * 5 (Pesos)
        //                        this.GetComponent<State>().WinningAmount = (k * 5).ToString();
        //                        // Play a tick sound
        //                        this.GetComponent<AudioSource>().clip = TickSound;
        //                        this.GetComponent<AudioSource>().Play();
        //                        // Wait 0.1 second
        //                        yield return new WaitForSeconds(0.1f);
        //                    }
        //                }
        //                // Going for 2nd time on the same iteration to show the credits earned
        //                for (int k = 1; k <= PlayerBets[i, j]; k++)
        //                {
        //                    // Checking if this is the last iteration
        //                    if (k == PlayerBets[i, j])
        //                    {
        //                        // For loop to show the earned credits 1 by 1 (each credit that was bet on winning number, gives 12 credits as earning)
        //                        for (int CreditAddition = 0; CreditAddition < 12; CreditAddition++)
        //                        {
        //                            // if this is the last iteration
        //                            if (CreditAddition == 11)
        //                            {
        //                                // Change the Text size/color and enable Outline to emphasize on the earnings
        //                                PlayerButtons[i].GetComponentInChildren<Text>().fontSize = 50;
        //                                PlayerButtons[i].GetComponentInChildren<Text>().color = Color.white;
        //                                PlayerButtons[i].GetComponentInChildren<Outline>().enabled = true;
        //                                // Increment the PlayerCredits by 1
        //                                PlayerCredits[i] = PlayerCredits[i] + 1;
        //                                // Play a tick sound
        //                                this.GetComponent<AudioSource>().clip = TickSound;
        //                                this.GetComponent<AudioSource>().Play();
        //                                // Show the increment to the user
        //                                KeepPlayerCreditsUpdated();
        //                                // Wait for 0.5 second
        //                                yield return new WaitForSeconds(0.5f);
        //                            }
        //                            // else for rest iterations
        //                            else
        //                            {
        //                                // Keep the Text size/color as default and disable outline component
        //                                PlayerButtons[i].GetComponentInChildren<Text>().fontSize = 25;
        //                                PlayerButtons[i].GetComponentInChildren<Text>().color = Color.black;
        //                                PlayerButtons[i].GetComponentInChildren<Outline>().enabled = false;
        //                                // Increment the PlayerCredits by 1
        //                                PlayerCredits[i] = PlayerCredits[i] + 1;
        //                                // Play a tick sound
        //                                this.GetComponent<AudioSource>().clip = TickSound;
        //                                this.GetComponent<AudioSource>().Play();
        //                                // Show the increment to the user
        //                                KeepPlayerCreditsUpdated();
        //                                // Wait for 0.05 second
        //                                yield return new WaitForSeconds(0.05f);
        //                            }
        //                        }
        //                    }
        //                    // else for all rest iterations
        //                    else
        //                    {
        //                        // For loop to show the earned credits 1 by 1 (each credit that was bet on winning number, gives 12 credits as earning)
        //                        for (int CreditAddition = 0; CreditAddition < 12; CreditAddition++)
        //                        {
        //                            // Keep the Text size/color as default and disable outline component
        //                            PlayerButtons[i].GetComponentInChildren<Text>().fontSize = 25;
        //                            PlayerButtons[i].GetComponentInChildren<Text>().color = Color.black;
        //                            PlayerButtons[i].GetComponentInChildren<Outline>().enabled = false;
        //                            // Increment PlayerCredits by 1
        //                            PlayerCredits[i] = PlayerCredits[i] + 1;
        //                            // Play a tick sound
        //                            this.GetComponent<AudioSource>().clip = TickSound;
        //                            this.GetComponent<AudioSource>().Play();
        //                            // Show the increment to the user
        //                            KeepPlayerCreditsUpdated();
        //                            // Wait for 0.05 second
        //                            yield return new WaitForSeconds(0.05f);
        //                        }
        //                    }
        //                }
        //                // Change MessagePanel Text size/color to default and disable outline component
        //                MessagePanel.GetComponentInChildren<Text>().fontSize = 75;
        //                MessagePanel.GetComponentInChildren<Text>().color = Color.black;
        //                MessagePanel.GetComponentInChildren<Outline>().enabled = false;
        //                // Change the playerbutton Text size/color to default and disable outline component
        //                PlayerButtons[i].GetComponentInChildren<Text>().fontSize = 25;
        //                PlayerButtons[i].GetComponentInChildren<Text>().color = Color.black;
        //                PlayerButtons[i].GetComponentInChildren<Outline>().enabled = false;
        //            }
        //        }
        //        // Set the Player sprite to normal sprite again
        //        PlayerButtons[i].image.overrideSprite = PlayerSprites[i];
        //        // Wait 0.25second for the next player
        //        yield return new WaitForSeconds(0.25f);
        //    }
        //}

        // If there is no winner
        if (!ThereisWinner)
        {
            // Wait 2 seconds for the messagepanel to inform the user
            yield return new WaitForSeconds(2);
        }
        // For the 2 children of the winning number chip (Text,Component)
        for (int i = 0; i < 2; i++)
        {
            // Set the button component disabledColors to default
            ColorBlock cb = Chips[WinningNumber].GetComponentsInChildren<Button>()[i].colors;
            if (i == 0)
            {
                cb.disabledColor = DisColor1;
            }
            else
            {
                cb.disabledColor = DisColor2;
            }
            Chips[WinningNumber].GetComponentsInChildren<Button>()[i].colors = cb;
        }
        // Set Text color back to default and disable outline
        Chips[WinningNumber].GetComponentInChildren<Text>().color = Color.black;
        Chips[WinningNumber].GetComponentInChildren<Outline>().enabled = false;
        // Empties all the Bets for each player
        EmptyAllPlayerBets();
        // Call this function to get everything ready for another game
        GetEverythingReadyToPlay();
        // Call this function to fix Cancel/Jugar state
        setJugarState();

    }

    // Function to show the specific active player's bets to the user
    public void KeepPlayerBetsUpdated()
    {
        // For each player
        for (int i = 0; i < 8; i++)
        {
            // if the player is the active player
            if (PlayerNames[i] == ActivePlayer)
            {
                // for each number
                for (int j = 0; j < 13; j++)
                {
                    // if the bet is 0 then change the text to ""
                    if (PlayerBets[i,j] == 0)
                    {
                        Chips[j].transform.GetChild(1).GetComponent<Text>().text = "";
                    }
                    // else change the text to the actual player 's bet
                    else
                    {
                        Chips[j].transform.GetChild(1).GetComponent<Text>().text = PlayerBets[i, j].ToString();
                    }
                }
            }
        }
    }

    // Function to empty all bets
	public void EmptyAllPlayerBets(){
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 13; j++) {
				PlayerBets [i, j] = 0;
				TotalBets [j] = 0;
				Chips[j].transform.GetChild(1).GetComponent<Text>().text = "";
			}
		}
	}

    // Function to fix the state of Jugar and Cancel buttons

	public void setJugarState()
    {
        // Boolean to know if a player has made a bet or not, setting it to false
        bool isBetAvailable = false;
        // Boolean to know if cancel should be enabled
        bool isCancelAvailabe = false;
        // For each player
        for (int i = 0; i < 8; i++)
        {
            // For each number
            for (int j = 0; j < 13; j++)
            {
                // Check if there is a bet on a number
                if (PlayerBets[i, j] > 0)
                {
                    // if so set the boolean to true and break
                    isBetAvailable = true;
                    break;
                }
            }
        }
        // For each player
        for (int i = 0; i < 8; i++)
        {
            // Check if the player is the active player
            if (PlayerNames[i] == ActivePlayer)
            {
                // For each number
                for (int j = 0; j < 13; j++)
                {
                    // Check if there is a bet on a number
                    if (PlayerBets[i, j] > 0)
                    {
                        // if so set the boolean to true and break
                        isCancelAvailabe = true;
                        break;
                    }
                }
            }
        }
        // Check if GameState is not "AwardInformation"
        if (GetComponent<State>().GameState != "AwardInformation")
        {
            // Check if there are no bets
            if (!isBetAvailable)
            {
                // Then disable Jugar button
                Jugar.enabled = false;
                Cancel.interactable = false;
            }
            // else if there are bets
            else
            {
                // Then enable Jugar button
                Jugar.enabled = true;

            }
            // If there are not bets
            if (!isCancelAvailabe)
            {
                // Then disable Cancel button
                Cancel.interactable = false;
            }
            // else if there are bets
            else
            {
                // Then enable Cancel button
                Cancel.interactable = true;
            }
        }
	}

    // Function to show total bets of all players to user
    public void KeepTotalBetsUpdated()
    {
        // For each number
        for (int j = 0; j < 13; j++)
        {
            // If the total ammount bet is 0 then change the text to ""
            if (TotalBets[j] == 0)
            {
                Chips[j].transform.GetChild(1).GetComponent<Text>().text = "";
            }
            // else change the text to the actual total bet ammount
            else
            {
                Chips[j].transform.GetChild(1).GetComponent<Text>().text = TotalBets[j].ToString();
            }
        }

    }

    // Function to reset all player button sprites to normal, except for the active player
    public void ClearRestButtons(Button _ActivePlayer)
    {
        // For each plauer
        for (int i=0; i<8; i++)
        {
            // if player is not the active player
            if (PlayerButtons[i] != _ActivePlayer)
            {
                // Change the player's sprite to normal
                PlayerButtons[i].image.overrideSprite = PlayerSprites[i];
            }

        }
    }

    // Function to be called when user double clicks cancel button (cancels all his bets)
    public void CancelDblClick()
    {
        for (int i = 0; i < 8; i++)
        {
            if (PlayerNames[i].Equals(ActivePlayer))
            {
                for (int j = 0; j < 13; j++)
                {
                    if (PlayerBets[i,j] > 0)
                    {
                        PlayerCredits[i] += PlayerBets[i, j];
                        TotalBets[j] -= PlayerBets[i, j];
                        PlayerBets[i, j] = 0;
                    }
                }
                undoBets[i].Clear();
            }
        }
        KeepTotalBetsUpdated();
        KeepPlayerCreditsUpdated();
        setJugarState();
    }



    public void CancelClick()
    {
        for (int i = 0; i < 8; i++)
        {
            if (PlayerNames[i].Equals(ActivePlayer))
            {
                if (undoBets[i].Count == 0)
                {
                    return;
                }
                else
                {
                    string ChipnAmmount = undoBets[i].Pop().ToString();
                    int ChipNumber = Int32.Parse(ChipnAmmount.Split(',')[0]);
                    int BetAmmount = Int32.Parse(ChipnAmmount.Split(',')[1]);
                    PlayerCredits[i] += BetAmmount;
                    PlayerBets[i, ChipNumber] -= BetAmmount;
                    TotalBets[ChipNumber] -= BetAmmount;
                }
            }
        }
        KeepTotalBetsUpdated();
        KeepPlayerCreditsUpdated();
        setJugarState();
    }


    public IEnumerator ChipHoldClick(GameObject ClickedChip)
    {
        if (this.GetComponent<State>().GameState == "Idle")
        {
            ExecutingMultibet = false;
            string ChipnAmmount;
            // If there is an active player
            if (ActivePlayer != String.Empty)
            {
                // For each player
                for (int i = 0; i < 8; i++)
                {
                    // if the player is the active player
                    if (PlayerNames[i] == ActivePlayer)
                    {
                        // For each number
                        for (int j = 0; j < 13; j++)
                        {
                            // Check if the number is equal to the chip number
                            if (ClickedChip.name == "Chip " + j + "")
                            {
                                // Checking for player to has credit and total bet on this number to be less than 40
                                if (PlayerCredits[i] - 5 >= 0 && TotalBets[j] + 5 <= 40)
                                {
                                    ExecutingMultibet = true;
                                    // Get Chip Number and Bet Ammount Info and send it to ChipnAmmount 
                                    ChipnAmmount = j.ToString() + "," + "5";
                                    // push the element to the Player's stack
                                    undoBets[i].Push(ChipnAmmount);
                                    // Decrement the player's credits by 1
                                    PlayerCredits[i] -= 5;
                                    // Increment the player's bets by 1
                                    PlayerBets[i, j] += 5;
                                    // Increment the number's total bets by 1
                                    TotalBets[j] += 5;
                                    // Show the player's credit changes to the user
                                    KeepPlayerCreditsUpdated();
                                    // Show the number's total bet changes to the user
                                    KeepTotalBetsUpdated();
                                    // Play Chip sound
                                    this.GetComponent<AudioSource>().clip = ChipSingleSound;
                                    this.GetComponent<AudioSource>().Play();
                                    yield return new WaitForSeconds(0.15f);
                                }
                                // else show information message
                                else if (PlayerCredits[i] > 0 && TotalBets[j] < 40)
                                {
                                    ExecutingMultibet = true;
                                    // Get Chip Number and Bet Ammount Info and send it to ChipnAmmount 
                                    ChipnAmmount = j.ToString() + "," + "1";
                                    // push the element to the Player's stack
                                    undoBets[i].Push(ChipnAmmount);
                                    // Decrement the player's credits by 1
                                    PlayerCredits[i] -= 1;
                                    // Increment the player's bets by 1
                                    PlayerBets[i, j] += 1;
                                    // Increment the number's total bets by 1
                                    TotalBets[j] += 1;
                                    // Show the player's credit changes to the user
                                    KeepPlayerCreditsUpdated();
                                    // Show the number's total bet changes to the user
                                    KeepTotalBetsUpdated();
                                    // Play Chip sound
                                    this.GetComponent<AudioSource>().clip = ChipSingleSound;
                                    this.GetComponent<AudioSource>().Play();
                                    yield return new WaitForSeconds(0.05f);
                                }
                                else if (PlayerCredits[i] == 0)
                                {
                                    MessagePanel.transform.GetChild(0).GetComponent<Text>().text = "Not enough credits!";
                                }
                                else if (TotalBets[j] == 40)
                                {
                                    MessagePanel.transform.GetChild(0).GetComponent<Text>().text = "Cannot bet to this number!";
                                }
                            }
                        }
                    }
                }
            }
            ExecutingMultibet = false;

        }
    }

    // Function to be called when a player clicks on a chip
    public void ChipClick(GameObject ClickedChip)
    {
        if (this.GetComponent<State>().GameState == "Idle")
        {
            string ChipnAmmount;
            // If there is an active player
            if (ActivePlayer != String.Empty)
            {
                // For each player
                for (int i = 0; i < 8; i++)
                {
                    // if the player is the active player
                    if (PlayerNames[i] == ActivePlayer)
                    {
                        // Get the Clicked chip gameobject
                        //GameObject ClickedChip = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
                        // For each number
                        for (int j = 0; j < 13; j++)
                        {
                            // Check if the number is equal to the chip number
                            if (ClickedChip.name == "Chip " + j + "")
                            {
                                // Checking for player to has credit and total bet on this number to be less than 40
                                if (PlayerCredits[i] > 0 && TotalBets[j] < 40)
                                {
                                    // Get Chip Numbber and Bet Ammount info and send it to ChipnAmmount
                                    ChipnAmmount = j.ToString() + "," + "1";
                                    // push the element to the player's stack
                                    undoBets[i].Push(ChipnAmmount);
                                    // Decrement the player's credits by 1
                                    PlayerCredits[i]--;
                                    // Increment the player's bets by 1
                                    PlayerBets[i, j]++;
                                    // Increment the number's total bets by 1
                                    TotalBets[j]++;
                                    // Show the player's credit changes to the user
                                    KeepPlayerCreditsUpdated();
                                    // Show the number's total bet changes to the user
                                    KeepTotalBetsUpdated();
                                    // Play Chip sound
                                    this.GetComponent<AudioSource>().clip = ChipSingleSound;
                                    this.GetComponent<AudioSource>().Play();
                                }
                                // else show information message
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
}
