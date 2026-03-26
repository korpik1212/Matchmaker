using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;


public class MatchAnimationSequence : MonoBehaviour
{


    public Image HeartImage, targetImage, candidateImage;

    public void PlayMatchAnimation(CharacterData target, CharacterData candidate)
    {
        StartCoroutine(MatchAnimationLoop(target, candidate));
    }

    public IEnumerator MatchAnimationLoop(CharacterData target, CharacterData candidate)
    {
        targetImage.sprite = target.characterIcon;
        candidateImage.sprite = candidate.characterIcon;

        targetImage.gameObject.SetActive(true);
        candidateImage.gameObject.SetActive(true);
        HeartImage.gameObject.SetActive(true);

        targetImage.transform.localScale = Vector3.one;
        candidateImage.transform.localScale = Vector3.one;
        HeartImage.transform.localScale = Vector3.zero;

        targetImage.transform.localRotation = Quaternion.identity;
        candidateImage.transform.localRotation = Quaternion.identity;
        HeartImage.transform.localRotation = Quaternion.identity;

        Sequence matchSequence = DOTween.Sequence();

        matchSequence.Append(targetImage.transform.DOScale(Vector3.one * 1.5f, 1.5f).SetEase(Ease.OutSine));
        matchSequence.Join(candidateImage.transform.DOScale(Vector3.one * 1.5f, 1.5f).SetEase(Ease.OutSine));
        matchSequence.Join(HeartImage.transform.DOScale(Vector3.one, 1.5f).SetEase(Ease.OutElastic));

        matchSequence.AppendInterval(1f);

        matchSequence.Append(targetImage.transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InBack));
        matchSequence.Join(targetImage.transform.DORotate(new Vector3(0, 0, -360), 1f, RotateMode.FastBeyond360).SetEase(Ease.InBack));

        matchSequence.Join(candidateImage.transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InBack));
        matchSequence.Join(candidateImage.transform.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360).SetEase(Ease.InBack));

        matchSequence.Join(HeartImage.transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InBack));
        matchSequence.Join(HeartImage.transform.DORotate(new Vector3(0, 0, 180), 1f, RotateMode.FastBeyond360).SetEase(Ease.InBack));

        yield return matchSequence.WaitForCompletion();

        targetImage.gameObject.SetActive(false);
        candidateImage.gameObject.SetActive(false);
        HeartImage.gameObject.SetActive(false);
    }



}
