using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneSequence : MonoBehaviour
{
    [System.Serializable]
    public class CutsceneSection
    {
        public string sectionName;
        public CanvasGroup[] frames;
    }

    [Header("Cutscene Sections")]
    [SerializeField] private CutsceneSection[] sections;

    [Header("Frame Animation")]
    [SerializeField] private float firstSectionDelay = 0.3f;
    [SerializeField] private float frameFadeDuration = 0.35f;
    [SerializeField] private float delayBetweenFrames = 0.65f;

    [SerializeField, Range(0.8f, 1f)]
    private float frameStartingScale = 0.94f;

    [Header("Continue Button")]
    [SerializeField] private Button continueButton;
    [SerializeField] private CanvasGroup continueButtonGroup;
    [SerializeField] private float buttonDelay = 0.4f;
    [SerializeField] private float buttonAnimationDuration = 0.35f;

    [Header("Section Transition")]
    [SerializeField] private float blackScreenDuration = 0.6f;

    [Header("Next Scene")]
    [SerializeField] private string nextSceneName = "BramTest";

    private int currentSectionIndex;
    private bool canContinue;

    private RectTransform continueButtonRect;
    private Vector3 continueButtonFinalScale;

    private void Awake()
    {
        PrepareAllFrames();
        PrepareContinueButton();
    }

    private IEnumerator Start()
    {
        if (sections == null || sections.Length == 0)
        {
            Debug.LogWarning("No cutscene sections were assigned.");
            yield break;
        }

        yield return new WaitForSecondsRealtime(firstSectionDelay);

        yield return PlaySection(0);
    }

    private void PrepareAllFrames()
    {
        if (sections == null)
            return;

        foreach (CutsceneSection section in sections)
        {
            if (section == null || section.frames == null)
                continue;

            foreach (CanvasGroup frame in section.frames)
            {
                if (frame == null)
                    continue;

                frame.alpha = 0f;
                frame.interactable = false;
                frame.blocksRaycasts = false;
                frame.gameObject.SetActive(false);
            }
        }
    }

    private void PrepareContinueButton()
    {
        if (continueButton == null)
        {
            Debug.LogWarning("No Continue Button was assigned.");
            return;
        }

        if (continueButtonGroup == null)
        {
            continueButtonGroup =
                continueButton.GetComponent<CanvasGroup>();
        }

        if (continueButtonGroup == null)
        {
            continueButtonGroup =
                continueButton.gameObject.AddComponent<CanvasGroup>();
        }

        continueButtonRect =
            continueButton.GetComponent<RectTransform>();

        continueButtonFinalScale =
            continueButtonRect.localScale;

        continueButtonGroup.alpha = 0f;
        continueButtonGroup.interactable = false;
        continueButtonGroup.blocksRaycasts = false;

        continueButton.interactable = false;
        continueButton.gameObject.SetActive(false);

        continueButton.onClick.AddListener(OnContinueClicked);
    }

    private IEnumerator PlaySection(int sectionIndex)
    {
        if (sectionIndex < 0 || sectionIndex >= sections.Length)
            yield break;

        currentSectionIndex = sectionIndex;

        CutsceneSection section = sections[sectionIndex];

        if (section.frames == null || section.frames.Length == 0)
        {
            Debug.LogWarning(
                $"Section {sectionIndex + 1} has no frames."
            );

            yield break;
        }

        for (int i = 0; i < section.frames.Length; i++)
        {
            CanvasGroup frame = section.frames[i];

            if (frame == null)
                continue;

            yield return FadeInFrame(frame);

            if (i < section.frames.Length - 1)
            {
                yield return new WaitForSecondsRealtime(
                    delayBetweenFrames
                );
            }
        }

        yield return new WaitForSecondsRealtime(buttonDelay);

        yield return ShowContinueButton();

        canContinue = true;
    }

    private IEnumerator FadeInFrame(CanvasGroup frame)
    {
        frame.gameObject.SetActive(true);

        RectTransform frameRect =
            frame.GetComponent<RectTransform>();

        Vector3 finalScale = frameRect.localScale;
        Vector3 smallScale =
            finalScale * frameStartingScale;

        frame.alpha = 0f;
        frameRect.localScale = smallScale;

        float elapsedTime = 0f;

        while (elapsedTime < frameFadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;

            float progress = Mathf.Clamp01(
                elapsedTime / frameFadeDuration
            );

            float smoothProgress = Mathf.SmoothStep(
                0f,
                1f,
                progress
            );

            frame.alpha = smoothProgress;

            frameRect.localScale = Vector3.Lerp(
                smallScale,
                finalScale,
                smoothProgress
            );

            yield return null;
        }

        frame.alpha = 1f;
        frameRect.localScale = finalScale;
    }

    private IEnumerator ShowContinueButton()
    {
        if (continueButton == null)
            yield break;

        continueButton.gameObject.SetActive(true);
        continueButton.transform.SetAsLastSibling();

        continueButtonGroup.alpha = 0f;
        continueButtonGroup.interactable = false;
        continueButtonGroup.blocksRaycasts = false;

        continueButton.interactable = false;

        Vector3 smallScale =
            continueButtonFinalScale * 0.8f;

        continueButtonRect.localScale = smallScale;

        float elapsedTime = 0f;

        while (elapsedTime < buttonAnimationDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;

            float progress = Mathf.Clamp01(
                elapsedTime / buttonAnimationDuration
            );

            float smoothProgress = Mathf.SmoothStep(
                0f,
                1f,
                progress
            );

            continueButtonGroup.alpha = smoothProgress;

            float popScale;

            if (progress < 0.75f)
            {
                float growProgress = progress / 0.75f;

                popScale = Mathf.Lerp(
                    0.8f,
                    1.08f,
                    Mathf.SmoothStep(0f, 1f, growProgress)
                );
            }
            else
            {
                float settleProgress =
                    (progress - 0.75f) / 0.25f;

                popScale = Mathf.Lerp(
                    1.08f,
                    1f,
                    Mathf.SmoothStep(0f, 1f, settleProgress)
                );
            }

            continueButtonRect.localScale =
                continueButtonFinalScale * popScale;

            yield return null;
        }

        continueButtonGroup.alpha = 1f;
        continueButtonGroup.interactable = true;
        continueButtonGroup.blocksRaycasts = true;

        continueButton.interactable = true;

        continueButtonRect.localScale =
            continueButtonFinalScale;
    }

    private void OnContinueClicked()
    {
        if (!canContinue)
            return;

        canContinue = false;

        StartCoroutine(ContinueToNextSection());
    }

    private IEnumerator ContinueToNextSection()
    {
        HideContinueButton();
        HideCurrentSection();

        // The main BlackBackground is now visible.
        yield return new WaitForSecondsRealtime(
            blackScreenDuration
        );

        int nextSectionIndex =
            currentSectionIndex + 1;

        if (nextSectionIndex < sections.Length)
        {
            yield return PlaySection(nextSectionIndex);
        }
        else
        {
            LoadNextScene();
        }
    }

    private void HideCurrentSection()
    {
        if (currentSectionIndex < 0 ||
            currentSectionIndex >= sections.Length)
        {
            return;
        }

        CanvasGroup[] currentFrames =
            sections[currentSectionIndex].frames;

        if (currentFrames == null)
            return;

        foreach (CanvasGroup frame in currentFrames)
        {
            if (frame == null)
                continue;

            frame.alpha = 0f;
            frame.gameObject.SetActive(false);
        }
    }

    private void HideContinueButton()
    {
        if (continueButton == null)
            return;

        continueButton.interactable = false;

        continueButtonGroup.alpha = 0f;
        continueButtonGroup.interactable = false;
        continueButtonGroup.blocksRaycasts = false;

        continueButton.gameObject.SetActive(false);
    }

    private void LoadNextScene()
    {
        if (string.IsNullOrWhiteSpace(nextSceneName))
        {
            Debug.LogWarning(
                "No next scene name was entered."
            );

            return;
        }

        SceneManager.LoadScene(nextSceneName);
    }

    private void OnDestroy()
    {
        if (continueButton != null)
        {
            continueButton.onClick.RemoveListener(
                OnContinueClicked
            );
        }
    }
}
