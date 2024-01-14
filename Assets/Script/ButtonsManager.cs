using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour
{
    public Image imageMain;
    public Button fadeButton, flipButton, dropButton, flyButton, browseButton;

    private bool isZoomOut = false, isFading = false, isFlipping = false, isDropping = false,
        isFlying = false, isBrowsing = false;

    private Vector3 originalPosition, storedPosition;

    private void Start()
    {
        fadeButton.onClick.AddListener(Fade);
        flipButton.onClick.AddListener(Flip);
        dropButton.onClick.AddListener(Drop);
        flyButton.onClick.AddListener(Fly);
        browseButton.onClick.AddListener(Browse);

        originalPosition = imageMain.transform.position;
    }

    public void Zoom()
    {
        float zoomVal = 0;
        float targetScale = isZoomOut ? 3.6239f : zoomVal;
        imageMain.transform.DOScale(targetScale, .5f);
        isZoomOut = !isZoomOut;
    }

    public void Fade()
    {
        if (!isFading)
        {
            imageMain.DOFade(0f, 0.5f).OnComplete(() => isFading = true);
        }
        else
        {
            imageMain.DOFade(1f, 0.5f).OnComplete(() => isFading = false);
        }
    }

    public void Flip()
    {
        if (!isFlipping)
        {
            imageMain.transform.DORotate(new Vector3(0f, 90f, 0f), 0.3f);
            imageMain.DOFade(0f, 0.5f).OnComplete(() => isFlipping = true);
        }
        else
        {
            imageMain.DOFade(1f, 0.5f).OnComplete(() => isFlipping = false);
            imageMain.transform.DORotate(Vector3.zero, .3f);
        }

    }

    public void Drop()
    {
        if (!isDropping)
        {
            Vector3 targetPosition = originalPosition - Vector3.down * 30f;
            imageMain.transform.DOScale(0, 0.3f);
            imageMain.transform.DOMove(targetPosition, 0.3f).OnComplete(() => isDropping = true);
            imageMain.DOFade(0f, 0.3f).OnComplete(() => isFading = true);
        }
        else
        {
            imageMain.transform.DOMove(originalPosition, 0.3f).OnComplete(() => isDropping = false);
            imageMain.transform.DOScale(3.6239f, 0.3f);
            imageMain.DOFade(1f, 0.3f).OnComplete(() => isFading = false);
        }
    }

    public void Fly()
    {
        if (!isFlying)
        {
            Vector3 targetPosition = originalPosition;
            targetPosition.x -= 150f;

            imageMain.transform.DOMove(targetPosition, .3f).SetEase(Ease.InSine).OnComplete(() => isFlying = true);
        }
        else
        {
            imageMain.transform.DOMove(originalPosition, .8f).SetEase(Ease.OutBounce).OnComplete(() => isFlying = false);
        }
    }

    public void Browse()
    {
        if (!isBrowsing)
        {
            storedPosition = imageMain.transform.position;
            Vector3 targetPosition = originalPosition - Vector3.right * 20f;
            Vector3 leftTargetPosition = originalPosition - Vector3.left * 18f; 

            imageMain.transform.DOMove(targetPosition, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                imageMain.transform.DOMove(leftTargetPosition, 0.18f).SetEase(Ease.Linear).OnComplete(() =>                
                {
                    imageMain.DOFade(0f, 0.2f).OnComplete(() => isBrowsing = true);
                });
            });
        }
        else
        {
            imageMain.transform.DOMove(storedPosition, 0.3f).SetEase(Ease.InOutQuad);
            imageMain.transform.DOScale(0, 0.01f);
            imageMain.transform.DOScale(3.6239f, 0.3f);
            imageMain.DOFade(1f, 0.3f).OnComplete(() => isBrowsing = false);
        }
    }
}
