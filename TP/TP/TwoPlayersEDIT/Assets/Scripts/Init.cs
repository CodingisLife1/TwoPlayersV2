using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




public class Init : MonoBehaviour
{
	bool canAd;
	[SerializeField] private string platform;
	public int gameNumber;
    //[Header("Purchased")]


    //[Header("Rewarded")]
    //string rewardTag;

    [Header("Localization")]
    public string language;

    [SerializeField] private TextMeshProUGUI pingTitle;
    [SerializeField] private TextMeshProUGUI tankTitle;
    [SerializeField] private TextMeshProUGUI horseRacingTitle;
    [SerializeField] private TextMeshProUGUI tictactoeTitle;
    [SerializeField] private TextMeshProUGUI shooterTitle;
    [SerializeField] private TextMeshProUGUI fishingTitle;
    //[SerializeField] private TextMeshProUGUI racingTitle;
    [SerializeField] private TextMeshProUGUI rpsTitle;
    //[SerializeField] private TextMeshProUGUI airHockeyTitle;
    [SerializeField] private TextMeshProUGUI paintingTitle;

    [SerializeField] private TextMeshProUGUI pingTitle2;
    [SerializeField] private TextMeshProUGUI tankTitle2;
    [SerializeField] private TextMeshProUGUI horseRacingTitle2;
    [SerializeField] private TextMeshProUGUI tictactoeTitle2;
    [SerializeField] private TextMeshProUGUI shooterTitle2;
    [SerializeField] private TextMeshProUGUI fishingTitle2;
    //[SerializeField] private TextMeshProUGUI racingTitle2;
    [SerializeField] private TextMeshProUGUI rpsTitle2;
    //[SerializeField] private TextMeshProUGUI airHockeyTitle2;
    [SerializeField] private TextMeshProUGUI paintingTitle2;

    [SerializeField] private TextMeshProUGUI pingDescription;
    [SerializeField] private TextMeshProUGUI tankDescription;
    [SerializeField] private TextMeshProUGUI horseRacingDescription;
    [SerializeField] private TextMeshProUGUI tictactoeDescription;
    [SerializeField] private TextMeshProUGUI shooterDescription;
    [SerializeField] private TextMeshProUGUI fishingDescription;
    //[SerializeField] private TextMeshProUGUI racingDescription;
    [SerializeField] private TextMeshProUGUI rpsDescription;
    //[SerializeField] private TextMeshProUGUI airHockeyDescription;
    [SerializeField] private TextMeshProUGUI paintingDescription;

    [SerializeField] private TextMeshProUGUI gameTitle;
    [SerializeField] private TextMeshProUGUI tournamentText;
    [SerializeField] private TextMeshProUGUI tournamentText2;

    [SerializeField] private TextMeshProUGUI[] backText;

    [SerializeField] private TextMeshProUGUI otherGamesText;

    [SerializeField] private TextMeshProUGUI[] redWinText;
    [SerializeField] private TextMeshProUGUI[] blueWinText;

    [SerializeField] private TextMeshProUGUI redWinTournamentText;
    [SerializeField] private TextMeshProUGUI blueWinTournamentText;

    [SerializeField] private TextMeshProUGUI turnText;

    public TextMeshProUGUI redTutuor;
    public TextMeshProUGUI blueTutor;
    [SerializeField] private TextMeshProUGUI drawText;
    //[Header("Save")]
    //public PlayerData _saveData;
    //bool wasLoad;

    [DllImport("__Internal")]
    private static extern void RateGame();

    [DllImport("__Internal")]
    private static extern string GetDomain();
    string developerName = "GeeKid%20-%20школа%20программирования";

    [DllImport("__Internal")]
    private static extern void SaveExtern(string date);

    [DllImport("__Internal")]
    private static extern void LoadExtern();

    [DllImport("__Internal")]
    private static extern string GetLang();

    [DllImport("__Internal")]
    private static extern void AdInterstitial();

    [DllImport("__Internal")]
    private static extern void AdReward();

    [DllImport("__Internal")]
    private static extern void SetToLeaderboard(int value, string leaderboardName);


    public bool mobile;
    private bool adOpen;

    [DllImport("__Internal")]
    private static extern void BuyItem(string idOrTag);

    [DllImport("__Internal")]
    private static extern void CheckBuyItem(string idOrTag);

    //МЕТОДЫ ВК//
    [DllImport("__Internal")]
    public static extern void VK_Star();

    [DllImport("__Internal")]
    public static extern void VK_Share();

    [DllImport("__Internal")]
    public static extern void VK_Invite();

    [DllImport("__Internal")]
    public static extern void VK_ToGroup();

    [DllImport("__Internal")]
    public static extern void VK_Banner();

    [DllImport("__Internal")]
    public static extern void VK_AdInterCheck();

    [DllImport("__Internal")]
    public static extern void VK_AdRewardCheck();

    [DllImport("__Internal")]
    public static extern void VK_Interstitial();

    [DllImport("__Internal")]
    public static extern void VK_Rewarded();

    [DllImport("__Internal")]
    public static extern void VK_OpenLeaderboard(int value);

    [DllImport("__Internal")]
    public static extern void VK_Load();

    [DllImport("__Internal")]
    public static extern void VK_Save(string saveData);
    //МЕТОДЫ ВК//

    //string purchasedTag;

    public void ItIsMobile()
    {
        mobile = true;
    }

    private void Start()
    {
		if (platform == "editor")
		{
			Debug.Log($"<color=yellow>?MOBILE? YANDEX</color>");
	        language = "ru";
	        Localization();
	        ShowInterstitialAd();
		}
		if (platform == "yandex")
		{
          language = GetLang();
          Localization();
          ShowInterstitialAd();
        }
        if (platform == "vk")
		{
			canAd = true;
			language = "ru";
          	Localization();
          	StartCoroutine(BannerVK());
            StartCoroutine(InterLoad());
		}
    }

    IEnumerator AD()
    {
    	yield return new WaitForSeconds(62);
    	canAd = true;
    }
    IEnumerator InterLoad()
    {
    	while (true)
    	{	
    		yield return new WaitForSeconds(15);
	        VK_AdInterCheck();
    	}
    }


    IEnumerator BannerVK()
    {
    	yield return new WaitForSeconds(5);
    	VK_Banner();
    }

    //РЕКЛАМА//
    public void ShowInterstitialAd()
    {
		if (platform == "editor")
		{
          Debug.Log($"<color=yellow>INTERSTITIAL SHOW YANDEX</color>");
		}
		if (platform == "yandex")
		{
          AdInterstitial();
         }
         if (platform == "vk")
         {
         	if (canAd)
         	{
         		VK_Interstitial();
         		canAd = false;
         		StartCoroutine(AD());
         	}
         }
    }
    //РЕКЛАМА//


    //ПАУЗА И ПРОДОЛЖЕНИЕ//
    public void StopMusAndGame()
    {
        adOpen = true;
        AudioListener.volume = 0;
        AudioListener.pause = true;
        Time.timeScale = 0;
    }

    public void ResumeMusAndGame()
    {
        adOpen = false;
        AudioListener.volume = 1;
        AudioListener.pause = false;
        Time.timeScale = 1;
    }
    //ПАУЗА И ПРОДОЛЖЕНИЕ//



    //ЛОКАЛИЗАЦИЯ//
    public void Localization()
    {
    	if (language == "ru")
    	{
    		drawText.text = "Ничья";
    		turnText.text = "Твой ход";
    		pingTitle.text = "Пинг-понг";
		    tankTitle.text = "Танки";
		    horseRacingTitle.text = "Скачки";
		    tictactoeTitle.text = "Крестики-нолики";
		    shooterTitle.text = "Вестерн";
		    fishingTitle.text = "Рыбалка";
		    //racingTitle.text = "Гонки";
		    rpsTitle.text = "Камень, ножницы, бумага";
		    //airHockeyTitle.text = "Аэрохоккей";
		    paintingTitle.text = "Рисование";

		    pingTitle2.text = "Пинг-понг";
		    tankTitle2.text = "Танки";
		    horseRacingTitle2.text = "Скачки";
		    tictactoeTitle2.text = "Крестики-нолики";
		    shooterTitle2.text = "Вестерн";
		    fishingTitle2.text = "Рыбалка";
		    //racingTitle2.text = "Гонки";
		    rpsTitle2.text = "Камень, ножницы, бумага";
		    //airHockeyTitle2.text = "Аэрохоккей";
		    paintingTitle2.text = "Рисование";

		    gameTitle.text = "Игры На Двоих: Дуэль";
		    tournamentText.text = "Турнир";
		    tournamentText2.text = "Турнир";

		    if (platform == "yandex" || platform == "editor")
		    	otherGamesText.text = "Другие игры";
		    if (platform == "vk")
		    	otherGamesText.text = "Вступить в группу";

		    for (int i = 0; i < backText.Length; i++)
		    {
		    	backText[i].text = "В меню";
		    }

		    if (!mobile)
		    {
		    	pingDescription.text = "Жмите Z и M, чтобы поднимать ракетки и отбивать ими мяч. Забейте больше очков, чем ваш соперник!";
			    tankDescription.text = "Жмите Z и M, чтобы стрелять и двигаться. Стреляйте по своему сопернику!";
			    horseRacingDescription.text = "Как можно больше жмите Z и M, чтобы добежать до финиша быстрее соперника!";
			    tictactoeDescription.text = "Кликайте туда, где хотите поставить свою фигуру. Соберите 3 в ряд и обыграйте соперника в стандартную партию в крестики-нолики!";
			    shooterDescription.text = "Жмите Z и M, чтобы стрелять. Попадайте по предметам, набирайте очки и опережайте соперника!";
			    fishingDescription.text = "Жмите Z и M, чтобы опустить удочку. Выигрывает тот, кто быстрее наберет кучу рыбы!";
			    //racingDescription.text = "С помощью WASD и стрелочек успейте сделать 7 кругов быстрее своего соперника!";
			    rpsDescription.text = "Выбирайте камень, ножницы или бумагу, затем пусть это сделает ваш соперник. Победите его в этом рандоме!";
			    //airHockeyDescription.text = "С помощью WASD и стрелочек управляйте ракетками и забивайте шайбу в ворота соперника!";
			    paintingDescription.text = "С помощью WASD и стрелочек закрасьте большую часть участка, чем ваш соперник за 30 секунд!";
		    }
		    else
		    {
		    	pingDescription.text = "Жмите по своей половине экрана, чтобы поднимать ракетки и отбивать ими мяч. Забейте больше очков, чем ваш соперник!";
			    tankDescription.text = "Жмите по своей половине экрана, чтобы стрелять и двигаться. Стреляйте по своему сопернику!";
			    horseRacingDescription.text = "Как можно больше жмите по своей половине экрана, чтобы добежать до финиша быстрее соперника!";
			    tictactoeDescription.text = "Кликайте туда, где хотите поставить свою фигуру. Соберите 3 в ряд и обыграйте соперника в стандартную партию в крестики-нолики!";
			    shooterDescription.text = "Жмите по своей половине экрана, чтобы стрелять. Попадайте по предметам, набирайте очки и опережайте соперника!";
			    fishingDescription.text = "Жмите по своей половине экрана, чтобы опустить удочку. Выигрывает тот, кто быстрее наберет кучу рыбы!";
			    //racingDescription.text = "С помощью джойстика успейте сделать 7 кругов быстрее своего соперника!";
			    rpsDescription.text = "Выбирайте камень, ножницы или бумагу, затем пусть это сделает ваш соперник. Победите его в этом рандоме!";
			    //airHockeyDescription.text = "С помощью джойстика управляйте ракетками и забивайте шайбу в ворота соперника!";
			    paintingDescription.text = "С помощью джойстика закрасьте большую часть участка, чем ваш соперник за 30 секунд!";
		    }

		    redWinTournamentText.text = "Красный победил турнир";
		    blueWinTournamentText.text = "Синий победил турнир";

		    for (int i = 0; i < redWinText.Length; i++)
		    {
		    	redWinText[i].text = "Красный победил";
		    }
		    for (int i = 0; i < blueWinText.Length; i++)
		    {
		    	blueWinText[i].text = "Синий победил";
		    }

		    redTutuor.text = "Z / WASD / Джойстик";
		    blueTutor.text = "M / Стрелки / Джойстик";
    	}
    	else if (language == "en")
    	{
    		drawText.text = "Draw";
    		turnText.text = "Your turn";
    		pingTitle.text = "Ping Pong";
			tankTitle.text = "Tanks";
			horseRacingTitle.text = "Racing";
			tictactoeTitle.text = "Tic Tac Toe";
			shooterTitle.text = "Western";
			fishingTitle.text = "Fishing";
			//racingTitle.text = "Racing";
			rpsTitle.text = "Rock, paper, scissors";
			//airHockeyTitle.text = "Air Hockey";
			paintingTitle.text = "Painting";

			pingTitle2.text = "Ping Pong";
			tankTitle2.text = "Tanks";
			horseRacingTitle2.text = "Racing";
			tictactoeTitle2.text = "Tic Tac Toe";
			shooterTitle2.text = "Western";
			fishingTitle2.text = "Fishing";
			//racingTitle2.text = "Racing";
			rpsTitle2.text = "Rock, paper, scissors";
			//airHockeyTitle2.text = "Air Hockey";
			paintingTitle2.text = "Painting";

			gameTitle.text = "2 Player Games: Duel";
			tournamentText.text = "Tournament";
			tournamentText2.text = "Tournament";

			otherGamesText.text = "Other games";

			for (int i = 0; i < backText.Length; i++)
			{
				backText[i].text = "In menu";
			}

			if (!mobile)
			{
				pingDescription.text = "Press Z and M to raise your paddles and hit the ball. Score more points than your opponent!";
				tankDescription.text = "Press Z and M to shoot and move. Shoot your opponent!";
				horseRacingDescription.text = "Press Z and M as much as possible to get to the finish line faster than your opponent!";
				tictactoeDescription.text = "Click where you want to place your piece. Match 3 and beat your opponent in a standard game of tic-tac-toe!";
				shooterDescription.text = "Press Z and M to shoot. Hit objects, score and get ahead of your opponent!";
				fishingDescription.text = "Press Z and M to lower your fishing rod. Whoever gets the most fish wins!";
				//racingDescription.text = "Use WASD and the arrows to get 7 laps faster than your opponent!";
				rpsDescription.text = "Choose rock, paper or scissors, then let your opponent do it. Defeat him in this random!";
				//airHockeyDescription.text = "Use WASD and arrows to control your paddles and shoot the puck into your opponent's goal!";
				paintingDescription.text = "Use WASD and arrows to paint more of the area than your opponent in 30 seconds!";
			}
			else
			{
				pingDescription.text = "Tap your half of the screen to raise your paddles and hit the ball. Score more points than your opponent!";
				tankDescription.text = "Tap your half of the screen to shoot and move. Shoot your opponent!";
				horseRacingDescription.text = "Tap your half of the screen as much as possible to get to the finish line faster than your opponent!";
				tictactoeDescription.text = "Click where you want to place your piece. Match 3 and beat your opponent in a standard game of tic-tac-toe!";
				shooterDescription.text = "Tap on your half of the screen to shoot. Hit objects, score and get ahead of your opponent!";
				fishingDescription.text = "Tap your half of the screen to lower your fishing rod. Whoever gets the most fish wins!";
				//racingDescription.text = "Use the joystick to make 7 laps faster than your opponent!";
				rpsDescription.text = "Choose rock, paper or scissors, then let your opponent do it. Defeat him in this random!";
				//airHockeyDescription.text = "Use the joystick to control the paddles and shoot the puck into your opponent's goal!";
				paintingDescription.text = "Use the joystick to paint more of the area than your opponent in 30 seconds!";
			}

			redWinTournamentText.text = "RED WIN TOURNAMENT";
		    blueWinTournamentText.text = "BLUE WIN TOURNAMENT";

		    for (int i = 0; i < redWinText.Length; i++)
		    {
		    	redWinText[i].text = "RED WIN";
		    }
		    for (int i = 0; i < blueWinText.Length; i++)
		    {
		    	blueWinText[i].text = "BLUE WIN";
		    }

		    redTutuor.text = "Z / WASD / Joystick";
		    blueTutor.text = "M / Arrows / Joystick";
    	}
    	else if (language == "tr")
    	{
    		drawText.text = "Örneğin";
    		turnText.text = "Teşekkürler";
    		pingTitle.text = "Ping Pong";
			tankTitle.text = "Tanklar";
			horseRacingTitle.text = "Yarış";
			tictactoeTitle.text = "Tic Tac Toe";
			shooterTitle.text = "Batı";
			fishingTitle.text = "Balık Tutma";
			//racingTitle.text = "Yarış";
			rpsTitle.text = "Taş, kağıt, makas";
			//airHockeyTitle.text = "Hava Hokeyi";
			paintingTitle.text = "Resim";

			pingTitle2.text = "Ping Pong";
			tankTitle2.text = "Tanklar";
			horseRacingTitle2.text = "Yarış";
			tictactoeTitle2.text = "Tic Tac Toe";
			shooterTitle2.text = "Batı";
			fishingTitle2.text = "Balık Tutma";
			//racingTitle2.text = "Yarış";
			rpsTitle2.text = "Taş, kağıt, makas";
			//airHockeyTitle2.text = "Hava Hokeyi";
			paintingTitle2.text = "Resim";

			gameTitle.text = "2 Kişilik Oyunlar: Düello";
			tournamentText.text = "Turnuva";
			tournamentText2.text = "Turnuva";

			for (int i = 0; i < backText.Length; i++)
			{
				backText[i].text = "Menüde";
			}

			otherGamesText.text = "Oyunu Oynat";

			if (!mobile)
			{
				pingDescription.text = "Z ve M tuşlarına basarak küreklerinizi kaldırın ve topa vurun. Rakibinizden daha fazla puan alın!";
				tankDescription.text = "Ateş etmek ve hareket etmek için Z ve M'ye basın. Rakibinizi vurun!";
				horseRacingDescription.text = "Bitiş çizgisine rakibinizden daha hızlı ulaşmak için mümkün olduğunca Z ve M tuşlarına basın!";
				tictactoeDescription.text = "Taşınızı yerleştirmek istediğiniz yere tıklayın. Standart bir tic-tac-toe oyununda 3'ünü eşleştirin ve rakibinizi yenin!";
				shooterDescription.text = "Ateş etmek için Z ve M tuşlarına basın. Nesneleri vurun, sayı yapın ve rakibinizin önüne geçin!";
				fishingDescription.text = "Oltanızı indirmek için Z ve M tuşlarına basın. En çok balığa sahip olan kazanır!";
				//racingDescription.text = "Rakibinizden 7 tur daha hızlı geçmek için WASD'yi ve okları kullanın!";
				rpsDescription.text = "Taş, kağıt veya makas seçin, sonra bırakın rakibiniz yapsın. Onu bu rastgele oyunda yenin!";
				//airHockeyDescription.text = "Küreklerinizi kontrol etmek için WASD'yi ve okları kullanın ve diski rakibinizin kalesine atın!";
				paintingDescription.text = "30 saniye içinde rakibinizden daha fazla alanı boyamak için WASD'yi ve okları kullanın!";
			}
			else
			{
				pingDescription.text = "Küreklerinizi kaldırmak ve topa vurmak için ekranın kendi yarısına dokunun. Rakibinizden daha fazla puan alın!";
				tankDescription.text = "Ateş etmek ve hareket etmek için ekranın yarısına dokunun. Rakibinizi vurun!";
				horseRacingDescription.text = "Bitiş çizgisine rakibinizden daha hızlı ulaşmak için ekranın kendi yarısına mümkün olduğunca çok dokunun!";
				tictactoeDescription.text = "Taşınızı yerleştirmek istediğiniz yere tıklayın. Standart bir tic-tac-toe oyununda 3'ünü eşleştirin ve rakibinizi yenin!";
				shooterDescription.text = "Ateş etmek için ekranın yarısına dokunun. Nesneleri vurun, sayı yapın ve rakibinizin önüne geçin!";
				fishingDescription.text = "Oltanızı indirmek için ekranın yarısına dokunun. En çok balığa sahip olan kazanır!";
				//racingDescription.text = "Rakibinizden 7 tur daha hızlı geçmek için joystick'i kullanın!";
				rpsDescription.text = "Taş, kağıt veya makas seçin, sonra bırakın rakibiniz yapsın. Onu bu rastgele oyunda yenin!";
				//airHockeyDescription.text = "Kürekleri kontrol etmek için joystick'i kullanın ve diski rakibinizin kalesine atın!";
				paintingDescription.text = "30 saniye içinde rakibinizden daha fazla alanı boyamak için joystick'i kullanın!";
			}

			redWinTournamentText.text = "KIRMIZI KAZANÇ TURNUVASI";
		    blueWinTournamentText.text = "MAVİ KAZANÇ TURNUVASI";

		    for (int i = 0; i < redWinText.Length; i++)
		    {
		    	redWinText[i].text = "KIRMIZI KAZANÇ";
		    }
		    for (int i = 0; i < blueWinText.Length; i++)
		    {
		    	blueWinText[i].text = "MAVİ KAZANÇ";
		    }

		    redTutuor.text = "Z / WASD / Oyun Çubuğu";
		    blueTutor.text = "M / Oklar / Kumanda Çubuğu";
    	}
    }
    //ЛОКАЛИЗАЦИЯ//



    //КНОПКА ДРУГИЕ ИГРЫ//
    public void OpenOtherGames()
    {
		if (platform == "editor")
		{
          Debug.Log($"<color=yellow>OPEN OTHER GAMES YANDEX</color>");
		}
		if (platform == "yandex")
		{
          var domain = GetDomain();
          Application.OpenURL($"https://yandex.{domain}/games/developer?name=" + developerName);
		}
		if (platform == "vk")
		{
			VK_ToGroup();
		}
    }
    //КНОПКА ДРУГИЕ ИГРЫ//



    //ОТЗЫВЫ//
    public void RateGameFunc()
    {
		if (platform == "editor")
		{
          Debug.Log($"<color=yellow>REWIEV GAME YANDEX</color>");
		}
		if (platform == "yandex")
		{
          RateGame();
        }
    }
    //ОТЗЫВЫ//


    //ЗВУК И ПАУЗА ПРИ СВОРАЧИВАНИИ
    void OnApplicationFocus(bool hasFocus)
    {
        Silence(!hasFocus);
    }

    void OnApplicationPause(bool isPaused)
    {
        Silence(isPaused);
    }

    private void Silence(bool silence)
    {
        AudioListener.volume = silence ? 0 : 1;
        Time.timeScale = silence ? 0 : 1;

        if (adOpen)
        {
            Time.timeScale = 0;
            AudioListener.volume = 0;
        }
    }
    //ЗВУК И ПАУЗА ПРИ СВОРАЧИВАНИИ


    public void ShareVK()
    {
    	VK_Share();
    }

    public void StarGame()
    {
    	VK_Star();
    }
}