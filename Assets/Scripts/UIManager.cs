using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum ScoreDirections
{
    Right,
    Left
}

public class ScoreManager
{
    public int pontos;
    public int maxPontos;
    public List<GameObject> contadores;

    public ScoreManager(int pts, List<GameObject> conts)
    {
        pontos = 0;
        maxPontos = pts;
        contadores = conts;
    }

    public GameObject marcarPonto()
    {
        // deve atualziar o placar e retornar o objeto correspondente
        if(++pontos > maxPontos){
            return null;
        }

        return contadores[pontos - 1];
    }
}

public class UIManager : MonoBehaviour
{
    [Header("Sprite Reference")]
    public Sprite sprVazio;
    public Sprite sprCheio;
    public int spriteSize;


    [Header("Position Settings")]
    public int margin;

    private int canvasX;
    private int canvasY;

    private int totalPontos;
    private Dictionary<Equipes, ScoreManager> scores;

    // listeners
    void Awake() { quadraManager.onPonto += onPontoFunction; }
    void Destroy() { quadraManager.onPonto -= onPontoFunction; }

    // Start is called before the first frame update
    void Start()
    {
        // salva o tamanho do canvas
        RectTransform canvasTransf = gameObject.GetComponent<RectTransform>();
        canvasX = (int) canvasTransf.rect.width;
        canvasY = (int) canvasTransf.rect.height;

        // totalPontos = quadraManager.instance.pontuacaoMax;
        totalPontos = quadraManager.pontuacaoMax;

        // calcula onde objetos devem ser colocados, e dps os cria
        scores = new Dictionary<Equipes, ScoreManager>();
        Vector2 begin;
        int halfSprSize = spriteSize / 2;

        // begin eh a posicao do primeiro contador, scoredirection eh a direcao dos contadores seguintes
        begin =  new Vector2(
            ((canvasX / 2) - (margin + halfSprSize)) * -1, 
            (canvasY / 2) - (margin + halfSprSize)
        );
        scores.Add(Equipes.A, new ScoreManager(totalPontos, createNewScoreCounters(begin, ScoreDirections.Right)));

        begin =  new Vector2(
            ((canvasX / 2) - (margin + halfSprSize)), 
            (canvasY / 2) - (margin + halfSprSize)
        );
        scores.Add(Equipes.B, new ScoreManager(totalPontos, createNewScoreCounters(begin, ScoreDirections.Left)));

    }

    // CRIMINOSO isso aqui ta
    private List<GameObject> createNewScoreCounters(Vector2 beginPos, ScoreDirections dir)
    {
        List<GameObject> contadores = new List<GameObject>();

        for(int i = 0; i < totalPontos; i++){
            // Cria o objeto, poe o componente Image e define o sprite certo
            // Hmmm talbez eu pudesse ter feito isso com prefabs mas ok acontece agora ja foi
            GameObject newCounter = new GameObject();
            Image imgComp = newCounter.AddComponent<Image>();
            imgComp.sprite = sprVazio;

            // coloca o objeto no canvas
            RectTransform rectTransfComp = newCounter.GetComponent<RectTransform>();
            rectTransfComp.SetParent(gameObject.transform);

            rectTransfComp.sizeDelta = new Vector2(spriteSize, spriteSize);

            // posiciona o objeto
            Vector2 displace;
            switch(dir){
            case ScoreDirections.Right:
                displace = new Vector2(margin + spriteSize, 0);
                break;

            case ScoreDirections.Left:
                displace = new Vector2((margin + spriteSize) * -1, 0);
                break;

            default:
                displace = Vector2.zero;
                break;
            }

            rectTransfComp.anchoredPosition = beginPos + (i * displace);

            // ativa
            newCounter.SetActive(true);
            
            contadores.Add(newCounter);
        }

        return contadores;
    }

    public void onPontoFunction(Equipes team)
    {
        GameObject contador = scores[team].marcarPonto();

        if(contador != null){
            contador.GetComponent<Image>().sprite = sprCheio;
        }
    }
}
