// Copyright (C) 2015-2019 ricimi - All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement.
// A Copy of the Asset Store EULA is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Ricimi
{
    // This class manages the level scene of the demo. It handles the left and right
    // selection buttons that are used to navigate across the available levels and their
    // associated animations.
    public class LevelScene : MonoBehaviour
    {
        public GameObject prevLevelButton;
        public GameObject nextLevelButton;

        public GameObject levelGroup;
        public GameObject levelNew;
        public GameObject levelThreeStars;
        public GameObject levelLocked;
        public GameObject page;

        private readonly int totalLevels = 20;
        private readonly float pageOffsetX = 2358;

        public Text levelText;

        private const int numLevelIndexes = 3;

        private int currentLevelIndex = 0;

        private Animator animator;

        private readonly List<Vector3> prefabLocations;
     
        private void Awake()
        {
            animator = levelGroup.GetComponent<Animator>();

            CreateLevelsOnScreen();
        }

        public void CreateLevelsOnScreen()
        {
            Transform pageTransform = Instantiate(page, levelGroup.transform.position, Quaternion.identity, levelGroup.transform).transform;

            //Need to get how many levels the player has completed...
            //number of level for text

            int levelsComplete = LevelManager.GetCompletedLevels();

            for (int i = 0; i < totalLevels; i++)
            {
                int index = i % 10;

                // If i is not 0, but is divisible by 10 we need to create a new page of levels
                if (i != 0 && i % 10 == 0)
                {
                    pageTransform = Instantiate(page, levelGroup.transform.position, Quaternion.identity, levelGroup.transform).transform;
                    pageTransform.localPosition = new Vector3(pageTransform.localPosition.x + pageOffsetX, pageTransform.localPosition.y, pageTransform.localPosition.z);
                }

                if (i < levelsComplete)
                {
                    // If level is completed, need to create a level select with the right amount of stars...
                    Instantiate(levelThreeStars, pageTransform.GetChild(index).position, Quaternion.identity, pageTransform.GetChild(index));
                }

                else if (levelsComplete != totalLevels && i == levelsComplete)
                {
                    Instantiate(levelNew, pageTransform.GetChild(index).position, Quaternion.identity, pageTransform.GetChild(index));
                }

                else
                {
                    Instantiate(levelLocked, pageTransform.GetChild(index).position, Quaternion.identity, pageTransform.GetChild(index));
                }
            }
        }

        public void ShowPreviousLevels()
        {
            --currentLevelIndex;
            if (currentLevelIndex < 0)
                currentLevelIndex = 0;

            SetLevelText(currentLevelIndex + 1);
            switch (currentLevelIndex)
            {
                case 0:
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Animation4"))
                        animator.Play("Animation4");
                    DisablePrevLevelButton();
                    break;

                case 1:
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Animation3"))
                        animator.Play("Animation3");
                    EnablePrevLevelButton();
                    EnableNextLevelButton();
                    break;

                default:
                    break;
            }
        }

        public void ShowNextLevels()
        {
            ++currentLevelIndex;
            if (currentLevelIndex == numLevelIndexes)
                currentLevelIndex = numLevelIndexes - 1;

            SetLevelText(currentLevelIndex + 1);
            switch (currentLevelIndex)
            {
                case 1:
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Animation1"))
                        animator.Play("Animation1");
                    EnablePrevLevelButton();
                    EnableNextLevelButton();
                    break;

                case 2:
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Animation2"))
                        animator.Play("Animation2");
                    DisableNextLevelButton();
                    break;

                default:
                    break;
            }
        }

        public void EnablePrevLevelButton()
        {
            var image = prevLevelButton.GetComponentsInChildren<Image>()[1];
            var newColor = image.color;
            newColor.a = 1.0f;
            image.color = newColor;

            var shadow = prevLevelButton.GetComponentsInChildren<Image>()[0];
            var newShadowColor = shadow.color;
            newShadowColor.a = 1.0f;
            shadow.color = newShadowColor;

            prevLevelButton.GetComponent<AnimatedButton>().interactable = true;
        }

        public void DisablePrevLevelButton()
        {
            var image = prevLevelButton.GetComponentsInChildren<Image>()[1];
            var newColor = image.color;
            newColor.a = 40 / 255.0f;
            image.color = newColor;

            var shadow = prevLevelButton.GetComponentsInChildren<Image>()[0];
            var newShadowColor = shadow.color;
            newShadowColor.a = 0.0f;
            shadow.color = newShadowColor;

            prevLevelButton.GetComponent<AnimatedButton>().interactable = false;
        }

        public void EnableNextLevelButton()
        {
            var image = nextLevelButton.GetComponentsInChildren<Image>()[1];
            var newColor = image.color;
            newColor.a = 1.0f;
            image.color = newColor;

            var shadow = nextLevelButton.GetComponentsInChildren<Image>()[0];
            var newShadowColor = shadow.color;
            newShadowColor.a = 1.0f;
            shadow.color = newShadowColor;

            nextLevelButton.GetComponent<AnimatedButton>().interactable = true;
        }

        public void DisableNextLevelButton()
        {
            var image = nextLevelButton.GetComponentsInChildren<Image>()[1];
            var newColor = image.color;
            newColor.a = 40 / 255.0f;
            image.color = newColor;

            var shadow = nextLevelButton.GetComponentsInChildren<Image>()[0];
            var newShadowColor = shadow.color;
            newShadowColor.a = 0.0f;
            shadow.color = newShadowColor;

            nextLevelButton.GetComponent<AnimatedButton>().interactable = false;
        }

        private void SetLevelText(int level)
        {
            levelText.text = level.ToString();
        }
    }
}
