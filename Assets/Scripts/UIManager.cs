using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum ScoreDirections
{
    Rigth,
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
        if(++pontos >= maxPontos){
            return null;
        }

        return contadores[pontos];
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
    private ScoreManager scoreA, scoreB;
    
    // Start is called before the first frame update
    void Start()
    {
        // salva o tamanho do canvas
        RectTransform canvasTransf = gameObject.GetComponent<RectTransform>();
        canvasX = (int) canvasTransf.rect.width;
        canvasY = (int) canvasTransf.rect.height;

        // totalPontos = quadraManager.instance.pontuacaoMax;
        totalPontos = 3;

        // calcula onde objetos devem ser colocados, e dps os cria
        int halfSprSize = spriteSize / 2;

        // begin eh a posicao do primeiro contador, scoredirection eh a direcao dos contadores seguintes
        Vector2 beginA =  new Vector2(((canvasX / 2) - (margin + halfSprSize)) * -1, (canvasY / 2) - (margin + halfSprSize));
        scoreA = new ScoreManager(totalPontos, createNewScoreCounters(beginA, ScoreDirections.Rigth));

        Vector2 beginB =  new Vector2(((canvasX / 2) - (margin + halfSprSize)), (canvasY / 2) - (margin + halfSprSize));
        scoreB = new ScoreManager(totalPontos, createNewScoreCounters(beginB, ScoreDirections.Left));

    }

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
            case ScoreDirections.Rigth:
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

    public void ScorePonto(Equipes team)
    {
        GameObject contador;

        switch(team){
        case Equipes.A:
            contador = scoreA.marcarPonto();
            break;

        case Equipes.B:
            contador = scoreB.marcarPonto();
            break;

        default:
            return;
        }

        if(contador != null){
            contador.GetComponent<Image>().sprite = sprCheio;
        }
    }
}
