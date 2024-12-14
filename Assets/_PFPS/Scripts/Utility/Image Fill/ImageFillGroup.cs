using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlexusTechdev.Utility
{
    public class ImageFillGroup : MonoBehaviour
    {
        public float m_value;
        public ImageFillController[] controllers = new ImageFillController[5];


        public void Initialize(float value)
        {
            m_value = value;
            float currentValue = value;
            //Debug.Log($"call initialize {value}");
            ResetData();
            //initialize positive
            if (value >= 0)
            {
                for (int i = 0; i < controllers.Length; i++)
                {
                    if (currentValue >= 1)
                    {
                        controllers[i].Initialize(1);
                        currentValue -= 1;
                    }
                    else
                    {
                        controllers[i].Initialize(currentValue);
                        currentValue = 0;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < controllers.Length; i++)
                {
                    if (currentValue <= -1)
                    {
                        controllers[i].Initialize(-1);
                        currentValue += 1;
                    }
                    else
                    {
                        controllers[i].Initialize(currentValue);
                        currentValue = 0;
                        break;
                    }
                }
            }


        }

        public void ResetData()
        {
            for (int i = 0; i < controllers.Length; i++)
            {
                controllers[i].Reset();
            }
        }

    }
}