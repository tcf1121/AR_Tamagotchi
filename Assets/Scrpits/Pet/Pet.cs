using UnityEngine;
using UnityEngine.Events;

public class Pet : MonoBehaviour
{
    [SerializeField] private int _phase;
    [SerializeField] private GameObject NextPhase;
    public Sprite image;
    public int Stress;  // 스트레스 100이 지속될 경우 아픔
    public int Satiety; // 포만감   0이 지속될 경우 스트레스 증가
    public int SickDay; // 아픈 날이 5일 이상 지속되면 죽음
    public bool IsSick; // 아픈지 아닌지 확인
    public int Exp;     // 경험치 100이 될 경우 최종 단계가 아니면 다음 단계로 진화
    public UnityAction IsDie;
    public UnityAction IsEvolution;

    private void Awake() => Init();

    private void Init()
    {
        if (PlayerPrefs.HasKey("Stress"))
        {
            Stress = PlayerPrefs.GetInt("Stress");
            Satiety = PlayerPrefs.GetInt("Satiety");
            SickDay = PlayerPrefs.GetInt("Satiety");
            Exp = PlayerPrefs.GetInt("Exp");
            IsSick = PlayerPrefs.GetInt("IsSick") == 1 ? true : false;
            int lastTime = (int)GameManager.LastTime / 600;
            int lastDay = (int)GameManager.LastTime / 86400;
            for (int i = 0; i < lastTime; i++)
                PetUpdate();
            for (int i = 0; i < lastDay; i++)
                SickUpdate();
        }
        else
        {
            ResetStress();
            ResetSickDay();
            Satiety = 50;
            Exp = 0;
            IsSick = false;
            IsDie += Die;
            IsEvolution += Evolution;
        }

        InvokeRepeating("PetUpdate", 600f, 600f);
        InvokeRepeating("SickUpdate", 86400f, 86400f);
    }


    private void PetUpdate()
    {
        Satiety -= 2;
        if (Satiety < 0) Satiety = 0;
        if (Satiety == 0) GetStress(5);
    }

    private void SickUpdate()
    {
        if (Stress == 100) IsSick = true;
        else IsSick = false;

        if (IsSick) SickDay++;
        else ResetSickDay();

        if (SickDay >= 5) IsDie?.Invoke();
    }

    public void GetStress(int stress)
    {
        Stress += stress;
        if (Stress < 0) Stress = 0;
        else if (Stress > 100) Stress = 100;
    }

    public void ResetStress()
    {
        Stress = 0;
    }

    public void ResetSickDay()
    {
        SickDay = 0;
    }

    public void Feed()
    {
        Satiety += 20;
        if (Satiety > 100) Satiety = 100;
        GetExp();
    }

    public void PlayingWithToys()
    {
        Satiety -= 5;
        if (Satiety < 0) Satiety = 0;
        GetStress(-10);
        GetExp();
    }

    public void GetExp()
    {
        Exp++;
        if (Exp > 100) IsEvolution?.Invoke();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void Evolution()
    {
        if (_phase == 3) return;
        GameObject Pet = Instantiate(NextPhase);
        Pet.name = NextPhase.name;
        Destroy(gameObject);
        GameManager.bag.AddPet(Pet);
        GameManager.bag.RemovePet(gameObject);
    }

    void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveQuitTime();
        }
    }

    void OnApplicationQuit()
    {
        SaveQuitTime();
    }

    private void SaveQuitTime()
    {
        PlayerPrefs.SetString("PetStatus", $"{gameObject.name}");

        PlayerPrefs.SetInt("Stress", Stress);
        PlayerPrefs.SetInt("Satiety", Satiety);
        PlayerPrefs.SetInt("Satiety", SickDay);
        PlayerPrefs.SetInt("Exp", Exp);
        PlayerPrefs.SetInt("IsSick", IsSick ? 1 : 0);
        PlayerPrefs.Save();
    }
}
