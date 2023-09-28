using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
   public LevelDataBase levelDB;
   public Text nameText;
   public SpriteRenderer artworkSprite;

   private int selectedOption = 0;

   private void Start()
   {
      UpdateLevel(selectedOption);
   }

   public void NextOption()
   {
      selectedOption++;
      if (selectedOption >= levelDB.levelCount)
      {
         selectedOption = 0;
      }
      
      UpdateLevel(selectedOption);
   }
   
   public void BackOption()
   {
      selectedOption--;
      if (selectedOption < 0)
      {
         selectedOption = levelDB.levelCount -1;
      }
      
      UpdateLevel(selectedOption);
   }

   private void UpdateLevel(int selectedOption)
   {
      Level level = levelDB.GetLevel(selectedOption);
      artworkSprite.sprite = level.levelSprite;
      nameText.text = level.levelName;
   }

   public void SelectedLevel()
   {
      if (selectedOption == 0)
      {
         SceneManager.LoadScene("punjabi");
      }
      else if (selectedOption == 1)
      {
         Debug.Log("hindi");
      }
      else if (selectedOption == 2)
      {
         Debug.Log("english");
      }
   }
}
