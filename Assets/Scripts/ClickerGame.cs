using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickerGame : MonoBehaviour
{
    public Button clickButton; // Reference to the button
    public Button coinButton; // Reference to the button
    public Button brickButton; // Reference to the button
    public Button marioButton; // Reference to the button
    public Button marioPauseButton; // Reference to the button
    public AudioSource audioSource;
    public AudioClip combo2SFX;
    public AudioClip combo5SFX;
    public AudioClip combo10SFX;
    public AudioClip coinSFX;
    

    
    public TextMeshProUGUI scoreText; // Reference to TextMeshPro for score text
    public TextMeshProUGUI multiplierText; // Reference to TextMeshPro for multiplier text
    public Slider countdownBar; // Reference to the countdown bar (Slider)
    public Image countdownBarFill; // Reference to the slider fill area image

    // Exposed colors for easier editing in the Unity Editor
    public Color startColor = Color.green; // Color at low multiplier
    public Color endColor = Color.red;     // Color at high multiplier

    [SerializeField]private  int score = 0;      // Variable to track the score
    [SerializeField]private int displayedScore = 0; // Displayed score for smooth transition
    private int clickCount = 0; // Variable to track the number of clicks
    private int multiplier = 1; // Score multiplier starts at 1
    private float idleTime = 0f; // Timer to track idle time
    private float maxIdleTime = 2f; // Maximum idle time (initially 2 seconds)

    // For smooth color lerp
    private Color currentColor;

    // Limit the lowest maxIdleTime
    private const float minIdleTime = 0.5f; // Set a minimum idle time limit

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
        // Add a listener to call the OnClick method when the button is pressed
        clickButton.onClick.AddListener(OnClick);

        // Update the score and multiplier text on start
        UpdateScoreText();
        UpdateMultiplierText();

        // Initialize the countdown bar
        countdownBar.maxValue = maxIdleTime;
        countdownBar.value = maxIdleTime;

        // Set initial color (green by default)
        currentColor = startColor;
        UpdateSliderColor();
    }

    void Update()
    {
        // Only update idle timer and countdown bar if the multiplier hasn't reset
        if (multiplier > 1)
        {
            idleTime += Time.deltaTime;
            countdownBar.value = maxIdleTime - idleTime; // Update countdown bar

            // Smoothly lerp the color every frame
            UpdateSliderColor();

            // If idle time exceeds maxIdleTime, reset the multiplier
            if (idleTime >= maxIdleTime)
            {
                ResetMultiplier();
            }
        }

        // Smoothly transition the displayed score to the actual score
        if (displayedScore < score)
        {
            displayedScore += Mathf.CeilToInt((score - displayedScore) * Time.deltaTime * 3f); // Adjust the speed of counting up
            UpdateScoreText();
        }
    }

    // Method called when the button is clicked
    void OnClick()
    {
        clickCount++;              // Increment the number of clicks
        score += multiplier*10;       // Increment the score by the multiplier
        idleTime = 0f; // Reset idle timer when the player clicks
        UpdateIdleTime(); // Update the idle timer according to the multiplier
        countdownBar.value = maxIdleTime; // Reset the countdown bar
        audioSource.PlayOneShot(coinSFX);

        // Check if the number of clicks is divisible by 10
        if (clickCount % 10 == 0)
        {
            multiplier++;         // Increase the multiplier by 1
            UpdateMultiplierText(); // Update the multiplier text
            UpdateIdleTime(); // Adjust the reset timer
        }
    }

    // Method to reset the multiplier to 1
    void ResetMultiplier()
    {
        multiplier = 1;
        UpdateMultiplierText();
        idleTime = 0f; // Reset idle timer
        countdownBar.value = countdownBar.maxValue; // Keep countdown bar full after reset

        // Reset the color to initial state
        currentColor = startColor;
        UpdateSliderColor();

        marioPauseButton.onClick.Invoke();


    }

    // Method to update the score display
    void UpdateScoreText()
    {
        if (score > 0)
        {
        scoreText.text = "Score: " + displayedScore;
        }
    }

    // Method to update the multiplier display
    void UpdateMultiplierText()
    {
        if(multiplier > 1)
        {
            multiplierText.text = "x" + multiplier;
            brickButton.onClick.Invoke();
        }
        if (multiplier > 2 && multiplier < 6)
        {
            marioButton.onClick.Invoke();
            audioSource.PlayOneShot(combo2SFX);
        }
        if (multiplier > 5 && multiplier < 10)
        {
            audioSource.PlayOneShot(combo5SFX);
        }
        if (multiplier > 9)
        {
            audioSource.PlayOneShot(combo10SFX);
        }
        if(multiplier < 2)
        {
            multiplierText.text = "";
        }
        coinButton.onClick.Invoke();
    }

    // Method to adjust the reset timer based on the multiplier
    void UpdateIdleTime()
    {
        // Decrease max idle time gradually, with a minimum limit of minIdleTime
        maxIdleTime = Mathf.Lerp(2f, minIdleTime, (multiplier - 1) / 9f); // Linearly decrease but limit to minIdleTime

        // Reset and update countdown bar
        countdownBar.maxValue = maxIdleTime;
        countdownBar.value = maxIdleTime;
    }

    // Method to update the slider color based on the multiplier using inverse logarithmic scaling with smooth lerp
    void UpdateSliderColor()
    {
        // Calculate the color based on inverse log multiplier scaling (quicker color change at higher multipliers)
        float t = Mathf.Log10(multiplier + 1) / Mathf.Log10(11); // Normalized log scale, making changes faster as multiplier increases
        Color targetColor = Color.Lerp(startColor, endColor, t);

        // Smoothly transition to the target color
        currentColor = Color.Lerp(currentColor, targetColor, Time.deltaTime * 5f); // Adjust smoothness with the multiplier
        countdownBarFill.color = currentColor;
    }

    public void ResetScore()
    {
        score = 0;
        displayedScore = 0;
        scoreText.text = "";
    }
}