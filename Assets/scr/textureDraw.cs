using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class textureDraw : MonoBehaviour
{
    public Texture2D texture2D;
    public Material material;
    public int width;
    public int height;
    public FilterMode filterMode;
    public TextureWrapMode wrapMode;
    public Sprite spr;
    public int sizePoint;
    public Color color;

    public Vector2 pointTouch;
    public List<Vector2> positions = new List<Vector2>();

    

    private void Update()
    {

        #region создание текстуры

        if (width != Screen.width)
            width = Screen.width;
        if (height != Screen.height)
            height = Screen.height;

        if (texture2D == null)
            texture2D = new Texture2D(width, height);

        if (texture2D.width != width || texture2D.height != height)
        {
            texture2D.Resize(width, height);
        }

        texture2D.filterMode = filterMode;
        texture2D.wrapMode = wrapMode;

        material.mainTexture = texture2D;

        texture2D.Apply();

        if (spr == null)
        {
            spr = Sprite.Create(texture2D, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
            GetComponent<Image>().sprite = spr;
        }

        #endregion

        if (Input.GetMouseButton(0))
            Draw();
        if (Input.GetKeyDown(KeyCode.T))
            DrawAutomat();
    }

    public async void DrawAutomat()
    {
        /*var data = new coordinatDraw();

        await Task.Run(() =>
        {
            foreach (var coordinat in data.coordinats)
            {
                Debug.Log(coordinat);
                DrawCircle((int) coordinat.x, (int) coordinat.y);
                texture2D.Apply();
                Task.Delay(1);
            }
        });*/
        StartCoroutine(drawAnim());
    }

    private IEnumerator drawAnim()
    {
        var data = new coordinatDraw();
        foreach (var coordinat in data.coordinats)
        {
            Debug.Log(coordinat);
            DrawCircle((int) coordinat.x, (int) coordinat.y);
            texture2D.Apply();
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Draw()
    {
        pointTouch = Input.mousePosition;
        positions.Add(new Vector2((int) pointTouch.x, (int) pointTouch.y));

        DrawCircle((int) pointTouch.x, (int) pointTouch.y);
        texture2D.Apply();
    }

    private void DrawBox(int rayX, int rayY)
    {
        for (int x = 0; x < sizePoint; x++)
        {
            for (int y = 0; y < sizePoint; y++)
            {
                texture2D.SetPixel(rayX + x - sizePoint / 2, rayY + y - sizePoint / 2,
                    Color.black);
            }
        }
    }

    public void SendData()
    {
        foreach (var pos in positions)
        {
            Debug.Log($"X:{pos.x}    Y:{pos.y}");
        }
    }

    public void ClearCanvas()
    {
        #region создание текстуры

        if (width != Screen.width)
            width = Screen.width;
        if (height != Screen.height)
            height = Screen.height;

        texture2D = new Texture2D(width, height);

        texture2D.filterMode = filterMode;
        texture2D.wrapMode = wrapMode;

        material.mainTexture = texture2D;

        texture2D.Apply();

        if (spr == null)
        {
            spr = Sprite.Create(texture2D, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
            GetComponent<Image>().sprite = spr;
        }

        #endregion

        #region создание текстуры

        if (width != Screen.width)
            width = Screen.width;
        if (height != Screen.height)
            height = Screen.height;

        texture2D = new Texture2D(width, height);

        texture2D.filterMode = filterMode;
        texture2D.wrapMode = wrapMode;

        material.mainTexture = texture2D;

        texture2D.Apply();

        spr = Sprite.Create(texture2D, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
        GetComponent<Image>().sprite = spr;


        #endregion
        
        positions.Clear();
    }

    private void DrawCircle(int rayX, int rayY)
    {
        for (int x = 0; x < sizePoint; x++)
        {
            for (int y = 0; y < sizePoint; y++)
            {
                float x2 = Mathf.Pow(x - sizePoint / 2, 2);
                float y2 = Mathf.Pow(y - sizePoint / 2, 2);
                float r2 = Mathf.Pow(sizePoint / 2-0.5f, 2);

                if (x2 + y2 < r2)
                {
                    int pixelX = rayX + x - sizePoint / 2;
                    int pixelY = rayY + y - sizePoint / 2;
                    Color oldColor = texture2D.GetPixel(pixelX, pixelY);
                    Color resultColor = Color.Lerp(oldColor, color, color.a);
                    texture2D.SetPixel(pixelX, pixelY, resultColor);
                }
            }
        }
    }
}
