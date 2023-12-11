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

public class UIGameBehaviour : UIGenericBehaviour
{    
    [Header("Canvas Reference")]
    public GameObject canvasRef;

    [Header("Sprite Reference")]
    public Sprite sprVazio;
    public Sprite sprCheio;
    public int spriteSize;

    [Header("Position Settings")]
    public float margin;

    private int maxPontos;
    private Dictionary<Equipes, ScoreManager> scores;


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        maxPontos = quadraManager.pontuacaoMax;

        loadContadores();
    }

    public void loadContadores()
    {
        // parametros
        float canvasX, canvasY;
        Vector2 begin;

        // canvas size
        RectTransform canvasTransf = canvasRef.GetComponent<RectTransform>();
        canvasX = canvasTransf.rect.width;
        canvasY = canvasTransf.rect.height;

        // inicializa os managers
        scores = new Dictionary<Equipes, ScoreManager>();


        // calcula onde objetos devem ser colocados
        // begin eh a posicao do primeiro contador, scoredirection eh a direcao dos contadores seguintes
        begin =  new Vector2(
            ((canvasX / 2) - (margin + (spriteSize / 2))) * -1, 
            (canvasY / 2) - (margin + (spriteSize / 2))
        );
        scores.Add(Equipes.A, new ScoreManager(maxPontos, createNewContadores(begin, ScoreDirections.Right)));

        // valores de begin diferentes para cada um dos conjuntos de contadores
        begin =  new Vector2(
            ((canvasX / 2) - (margin + (spriteSize / 2))), 
            (canvasY / 2) - (margin + (spriteSize / 2))
        );
        scores.Add(Equipes.B, new ScoreManager(maxPontos, createNewContadores(begin, ScoreDirections.Left)));
    }

    // CRIMINOSO isso aqui ta
    private List<GameObject> createNewContadores(Vector2 beginPos, ScoreDirections dir)
    {
        Vector2 displace;
        List<GameObject> contadores = new List<GameObject>();

        for(int i = 0; i < maxPontos; i++){
            // Hmmm talbez eu pudesse ter feito isso com prefabs mas ok acontece agora ja foi
            // Cria o objeto
            GameObject newContador = new GameObject();
            Image imgComp = newContador.AddComponent<Image>();
            RectTransform rectTransfComp = newContador.GetComponent<RectTransform>();

            // configura a imagem
            imgComp.sprite = sprVazio;
            rectTransfComp.sizeDelta = new Vector2(spriteSize, spriteSize);

            // coloca o objeto no canvas
            rectTransfComp.SetParent(gameObject.transform);

            // posiciona o objeto (de acordo com onde ele deve ser colocado)
            switch(dir)
            {
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
            newContador.SetActive(true);            
            contadores.Add(newContador);
        }

        return contadores;
    }

    public void marcarPonto(Equipes team)
    {
        GameObject contador = scores[team].marcarPonto();

        if(contador != null){
            contador.GetComponent<Image>().sprite = sprCheio;
        }
    }
}
