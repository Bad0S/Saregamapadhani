using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// L'attribute System.Serializable permet de voir ta classe dans l'inspecteur de Unity.
// Sans ça on ne pourrait pas éditer les dialogues dans l'éditeur.
[System.Serializable]
public class DialogueLine
{
    public string actorName;
    [Multiline]
    public string line;
}

public class DialogueComponent : MonoBehaviour
{
    public List<DialogueLine> dialogues = new List<DialogueLine>();

    [Space]
    
    public GameObject dialogueBox;
    [Tooltip("Le temps qui se passe entre l'affichage d'une lettre et l'autre")]
    public float delayBetweenEachLetter = 0.01f;
    [Tooltip("La touche à presser pour passer le dialogue")]
    public KeyCode skipKey = KeyCode.Mouse0;
	private KeyCode skipKey2 = KeyCode.JoystickButton0;

	private bool skipKeyDown2 = false;
    private bool skipKeyDown = false;
    public bool inDialogue = false;
    private float timer = 0f;
    
    // Pour utiliser cette fonction, appelle la depuis un autre script avec un GetComponent<DialogueComponent>().StartDialogue()
    public void StartDialogue()
    {
        // Si le dialogue est en cours, arrête la fonction
        if (inDialogue == true) return;
        // Si on ne trouve pas de référence à l'UI de la boite de dialogue, arrête la fonction
        if (dialogueBox == null)
        {
            Debug.Break();
            Debug.LogError("Tu dois mettre le prefab Canvas dans ta scène et mettre le GameObject DialogueBox dans le champ DialogueBox du component.");

            return;
        }
        if (dialogues == null)
        {
            Debug.Break();
            Debug.LogError("Tu dois ajouter des lignes de dialogue dans la liste de dialogue du component.");

            return;
        }

        // On affiche la boîte de dialogue
        dialogueBox.SetActive(true);

        // On récupère les components de texte de l'UI
        TextMeshProUGUI textBox = GameObject.Find("TextPanelText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI actorName = GameObject.Find("NamePanelLeftText").GetComponent<TextMeshProUGUI>();

        // On lance la fonction principale et on lui envoie tout ce dont elle a besoin
        StartCoroutine(DialogueCoroutine(textBox, actorName, dialogues));
    }

    public void Update()
    {
        // On stocke si le joueur appuie sur le bouton pour passer pour que la coroutine puisse y acceder
        // La coroutine a du mal à récuperer les input car elle n'est pas sur le même thread que l'Update
        skipKeyDown = Input.GetKeyDown(skipKey);
		skipKeyDown2 = Input.GetKeyDown(skipKey2);

    }

    public IEnumerator DialogueCoroutine(TextMeshProUGUI _textBox, TextMeshProUGUI _actorName, List<DialogueLine> _dialogues)
    {
        // On empêche le dialogue de se lancer deux fois
        inDialogue = true;

        // On établit où est-ce qu'on en est
        int currentLine = 0;

        //DialogueLines Loop
        while (true)
        {
            // On met dans l'UI toutes les infos de la ligne actuelle du dialogue
            _actorName.text = _dialogues[currentLine].actorName;
            _textBox.text = _dialogues[currentLine].line;

            // Mais on dit au component de texte de ne pas afficher cellui-ci pour l'instant, pour avoir l'effet machine à écrire
            _textBox.maxVisibleCharacters = 0;

            //Text loop
            while (true)
            {
				if (skipKeyDown|| skipKeyDown2)
                {
                    skipKeyDown = false;
					skipKeyDown2 = false;

                    // Si le joueur passe et que le texte a fini de s'afficher, on sort de la boucle
                    if (_textBox.maxVisibleCharacters >= _textBox.text.Length)
                    {
                        break;
                    }
                    else
                    {
                        // Si le joueur passe et que le texte n'a pas fini de s'afficher, on affiche tout le texte
                        // ça sert si le joueur trouve que le texte ne s'affiche pas assez vite
                        _textBox.maxVisibleCharacters = _textBox.text.Length;
                    }
                }

                // On avance le timer du temps qui s'est passé depuis la dernière fois qu'on a vérifié le temps
                timer += Time.deltaTime;

                // On vérifie que le temps choisit entre l'affichage des lettres est bien correct
                if (timer >= delayBetweenEachLetter)
                {
                    // On augmente le nombre de lettres visibles dans le component de texte de l'UI
                    _textBox.maxVisibleCharacters++;
                    // On redémarre le timer
                    timer = 0f;

                    // Si tu veux mettre du son à chaque lettre affichée, c'est ici qu'il faudrait le mettre
                }

                // On attend la prochaine image avant de recommencer la boucle
                // Si tu remontes, tu peux voir que la seule manière de quitter la boucle est de passer quand tout le texte s'est affiché
                yield return new WaitForEndOfFrame();
            }

            // Si on est arrivé à la fin du dialogue, alors on sort de cette boucle ci
            if (currentLine >= _dialogues.Count - 1) break;

            // Sinon on passe à la ligne suivante et on recommence la boucle
            currentLine++;
        }

        // Si on est sorti de la boucle, c'est qu'on a affiché toutes les lignes, on peut donc désactiver l'UI de la boite de dialogue
        dialogueBox.SetActive(false);

        // On dit qu'on peut relancer le dialogue
        inDialogue = false;


        // On finit la coroutine
        yield break;
    }
}
