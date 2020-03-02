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
        private RectTransform levelGroupRectTransform;

        private readonly int totalLevels = 20;
        private readonly float pageOffsetX = 2358;
        private readonly float levelPivotOffsetX = 23.58f;

        public Text levelText;

        private const int numLevelIndexes = 2;

        private int currentLevelIndex = 1;

        private Animator animator;

        private readonly List<Vector3> prefabLocations;
     
        private void Awake()
        {
            animator = levelGroup.GetComponent<Animator>();
            levelGroupRectTransform = levelGroup.GetComponent<RectTransform>();

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

                // If level is completed, need to create a level select with the right amount of stars...
                if (i < levelsComplete)
                {
                    GameObject level = Instantiate(levelThreeStars, pageTransform.GetChild(index).position, Quaternion.identity, pageTransform.GetChild(index));
                    level.GetComponentInChildren<Text>().text = (i+1).ToString();
                }

                // If level after levels complete is available, needs to be the 'new level'
                else if (levelsComplete != totalLevels && i == levelsComplete)
                {
                    GameObject level = Instantiate(levelNew, pageTransform.GetChild(index).position, Quaternion.identity, pageTransform.GetChild(index));
                    level.GetComponentInChildren<Text>().text = (i+1).ToString();
                }

                // Every other level should be a locked level
                else
                {
                    Instantiate(levelLocked, pageTransform.GetChild(index).position, Quaternion.identity, pageTransform.GetChild(index));
                }
            }
        }

        public void ShowPreviousLevels()
        {
            --currentLevelIndex;
            if (currentLevelIndex < 1)
                currentLevelIndex = 1;

            SetLevelText(currentLevelIndex);
            switch (currentLevelIndex)
            {
                // If we've reached the first page... Disable previous button
                case 1:
                    //if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Animation4"))
                    //animator.Play("Animation4");
                    levelGroupRectTransform.pivot -= new Vector2(levelPivotOffsetX, 0);
                    DisablePrevLevelButton();
                    EnableNextLevelButton();
                    break;
                
                // Every other page should enable both buttons
                default:
                    //if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Animation3"))
                    //animator.Play("Animation3");
                    levelGroupRectTransform.pivot -= new Vector2(levelPivotOffsetX, 0);
                    EnablePrevLevelButton();
                    EnableNextLevelButton();
                    break;
            }
        }

        public void ShowNextLevels()
        {
            ++currentLevelIndex;
            if (currentLevelIndex == numLevelIndexes+1)
                currentLevelIndex = numLevelIndexes - 1;

            SetLevelText(currentLevelIndex);

            switch (currentLevelIndex)
            {
                // If we've reached the end... Disable next button
                case numLevelIndexes:
                    //if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Animation2"))
                    //animator.Play("Animation2");

                    levelGroupRectTransform.pivot += new Vector2(levelPivotOffsetX, 0); 
                    EnablePrevLevelButton();
                    DisableNextLevelButton();
                    break;

                // Otherwise, enable both buttons
                default:
                    //if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Animation1"))
                        //animator.Play("Animation1");

                    levelGroupRectTransform.pivot += new Vector2(levelPivotOffsetX, 0);
                    EnablePrevLevelButton();
                    EnableNextLevelButton();
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
