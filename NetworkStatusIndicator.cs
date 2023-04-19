using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace GeirDev
{
    public class NetworkStatusIndicator : MonoBehaviour
    {
        public Image currentStatusIcon;
        
        private Texture2D statusTexture;
        private readonly float defaultSensitivity = 0.8f;

        private void Start()
        {
            SetStatusTextureSensitivity(defaultSensitivity);
            StartCoroutine(CheckNetworkStatus());
        }

        private IEnumerator CheckNetworkStatus()
        {
            while (true)
            {
                using (UnityWebRequest www = UnityWebRequest.Get("http://www.google.com"))
                {
                    float startTime = Time.time;
                    yield return www.SendWebRequest();
                    float ping = Time.time - startTime;

                    float sensitivity = GetNetworkSensitivity(ping);
                    SetStatusTextureSensitivity(sensitivity);

                    yield return new WaitForSeconds(5);
                }
            }
        }

        private float GetNetworkSensitivity(float ping)
        {
            float maxPing = 500;
            float minPing = 10;

            return Mathf.Clamp01(1 - (ping - minPing) / (maxPing - minPing));
        }

        private void SetStatusTextureSensitivity(float sensitivity)
        {
            int width = 40;
            int height = 100;
            statusTexture = new Texture2D(width, height);

            int numOfBars = 4;
            int barWidth = (width - (numOfBars - 1)) / numOfBars;
            int spacing = 1;
            float maxHeightRatio = 0.8f;
            float adjustedSensitivity = Mathf.Max((sensitivity - 0.05f) / (1 - 0.05f), 0);
            Color darkGray = new Color(0.3f, 0.3f, 0.3f);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int barIndex = x / (barWidth + spacing);
                    bool inBarArea = x % (barWidth + spacing) < barWidth;
                    float barHeightRatio = (barIndex + 1) / (float)numOfBars * maxHeightRatio;
                    float barHeight = height * barHeightRatio;

                    if (inBarArea && y < barHeight)
                    {
                        statusTexture.SetPixel(x, y, GetBarColor(barIndex, numOfBars, adjustedSensitivity, darkGray));
                    }
                    else
                    {
                        statusTexture.SetPixel(x, y, Color.clear);
                    }
                }
            }

            statusTexture.Apply();
            currentStatusIcon.sprite = Sprite.Create(statusTexture, new Rect(0, 0, statusTexture.width, statusTexture.height), new Vector2(0.5f, 0.5f));
        }

        private Color GetBarColor(int barIndex, int numOfBars, float adjustedSensitivity, Color darkGray)
        {
            float barPercentage = barIndex / (float)(numOfBars - 1);
            bool showBar = adjustedSensitivity >= barPercentage;
            return showBar ? Color.Lerp(Color.red, Color.green, adjustedSensitivity) : darkGray;
        }
    }
}
