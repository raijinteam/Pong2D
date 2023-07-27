using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class FortuneWheelSpinUI : MonoBehaviour
{

    [Header("Require Components")]
    public bool isLastSpin;
    [SerializeField] private RectTransform wheelTransform;
    [SerializeField] private Button btn_Spin;
    [SerializeField] private Transform pivotPoint;
    [SerializeField] private Image[] all_RewardsIcons;
    [SerializeField] private int[] all_RewardAmount;
    [SerializeField] private TextMeshProUGUI[] all_RewardText;



    [Header("Properites")]
    [SerializeField] private float stopPoint;
    [SerializeField] private bool isSpinning;
    [SerializeField] private int[] all_SegmentProbalities;
    [SerializeField] private int segments = 8;
    [SerializeField] private float spinDuration = 5f;

    float totalRotation;
    float angle;
    private float landedSegment;

    private void OnEnable()
    {
        for(int i = 0; i < segments; i++)
        {
            all_RewardText[i].text = "x"+ all_RewardAmount[i].ToString();
        }
    }

    private IEnumerator SpinWheelCO()
    {

        // Start Rotation
        wheelTransform.DORotate(new Vector3(0f, 0f, 720 * 1), 1, RotateMode.FastBeyond360).SetEase(Ease.InSine);
        yield return new WaitForSeconds(1);

        // Continue Rotation
        wheelTransform.DORotate(new Vector3(0f, 0f, 720 * spinDuration), spinDuration, RotateMode.FastBeyond360).SetEase(Ease.Linear);
        yield return new WaitForSeconds(spinDuration - 2);

        // End Rotation);
        angle = (stopPoint / 8f) * 360f;
        float addedAngle = angle + 25f;
        totalRotation = 360f * 5 / 2f + (addedAngle);
        wheelTransform.DORotate(new Vector3(0f, 0f, totalRotation), 3, RotateMode.FastBeyond360).SetEase(Ease.OutQuart);


        isLastSpin = true;
        yield return new WaitForSeconds(3f);
        wheelTransform.DORotate(new Vector3(0f, 0f, totalRotation - 25f), 1f).SetEase(Ease.OutSine);

        yield return new WaitForSeconds(1f);

        float currentRotation = pivotPoint.eulerAngles.z;
        float segmentSize = 360f / segments;
        landedSegment = Mathf.RoundToInt(currentRotation / segmentSize);

        Debug.Log(" after Landed on segment: " + landedSegment);
        all_RewardsIcons[(int)landedSegment].transform.DOScale(1.2f, 0.5f).SetEase(Ease.InOutBounce).SetLoops(5).OnComplete(SetRewardScreen);


        
        isLastSpin = false;
    }

    private void SetRewardScreen()
    {
        this.gameObject.SetActive(false);
        UIManager.instance.ui_RewardSummary.SetRewardSummaryData(all_RewardsIcons[(int)landedSegment].sprite, all_RewardText[(int)landedSegment].text);
        UIManager.instance.ui_RewardSummary.gameObject.SetActive(true);
        all_RewardsIcons[(int)landedSegment].transform.DOScale(1f, 0.5f);
        isSpinning = false;
    }


    public void OnClick_SpinWheel()
    {
        //SpinWheel();
        StartCoroutine(SpinWheelCO());
    }
}
