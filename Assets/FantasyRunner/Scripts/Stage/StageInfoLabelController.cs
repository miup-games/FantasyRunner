using UnityEngine;
using System.Collections;
using DG.Tweening;

public class StageInfoLabelController : MonoBehaviour
{
    [SerializeField] private TextMesh _text;
    [SerializeField] private Color _winColor;
    [SerializeField] private Color _loseColor;
    [SerializeField] private Color _infoColor;
    [SerializeField] private float animationDuration;
    [SerializeField] private float stayDuration;
    [SerializeField] private float hideY;
    [SerializeField] private float showY;

    private float _localScaleY;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public IEnumerator ShowWinText()
    {
        this.gameObject.SetActive(true);
        yield return StartCoroutine(ShowText(this._winColor, "Win"));
    }

    public IEnumerator ShowLoseText()
    {
        this.gameObject.SetActive(true);
        yield return StartCoroutine(ShowText(this._loseColor, "Lose"));
    }

    public IEnumerator ShowWaveText()
    {
        this.gameObject.SetActive(true);
        yield return StartCoroutine(ShowText(this._infoColor, "Next Wave", this.stayDuration));
    }

    public IEnumerator ShowStartText()
    {
        this.gameObject.SetActive(true);
        yield return StartCoroutine(ShowText(this._infoColor, "GO!", this.stayDuration));
    }

    public IEnumerator ShowLastWaveText()
    {
        this.gameObject.SetActive(true);
        yield return StartCoroutine(ShowText(this._infoColor, "Last Enemy", this.stayDuration));
    }

    public IEnumerator ShowText(Color color, string text, float duration = -1f)
    {
        this._text.color = color;
        this._text.text = text;

        yield return this.transform.DOLocalMoveY(showY, animationDuration).SetEase(Ease.OutBounce).WaitForCompletion();

        if (duration != -1f)
        {
            yield return new WaitForSeconds(duration);

            yield return this.transform.DOLocalMoveY(hideY, animationDuration).SetEase(Ease.InBack).WaitForCompletion();

            this.gameObject.SetActive(false);
        }
    }
}
