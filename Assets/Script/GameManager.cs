using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region 欄位與屬性
    [Header("道具")]
    public GameObject[] porps;
    [Header("文字介面：道具總數")]
    public Text textCount;
    [Header("文字介面：時間")]
    public Text textTime;
    [Header("文字介面：結束畫面標題")]
    public Text textTitle;
    [Header("結束畫面")]
    public CanvasGroup final;

    /// <summary>
    /// 道具總數
    /// </summary>
    private int countTotal;

    /// <summary>
    /// 取得道具數量
    /// </summary>
    private int countprop;

    /// <summary>
    /// 遊戲時間
    /// </summary>
    private float gameTime = 30.5f;
    #endregion

    #region 方法
    /// <summary>
    /// 生成道具
    /// </summary>
    /// <param name="prop">想要生成的道具</param>
    /// <param name="count">想要生成的數量 + 隨機值 + - 5 </param>
    private int CreateProp(GameObject prop,int count)
    {
        // 取得隨機道具數量 = 指定的數量 + - 5
        int total = count + Random.Range(-5, 5);

        for (int i = 0; i < total; i++)
        {
            // 座標 = (隨機,1.5f,隨機)
            Vector3 pos = new Vector3(Random.Range(-9, 9), 1.5f, Random.Range(-9, 9));
            // 生成(物件,座標,角度)
            Instantiate(prop, pos, Quaternion.identity);
        }

        // 傳回 道具數量
        return total;
    }

    private void CountTime()
    {
        // 遊戲時間 遞減 一禎的時間
        gameTime -= Time.deltaTime;
        // 更新倒數時間介面 ToString("f小數點位數")
        textTime.text = "倒數時間：" + gameTime.ToString("f2");
    }
    #endregion

    #region 事件
    private void Start()
    {
        countTotal = CreateProp(porps[0], 20);      // 道具總數 = 生成道具(道具一號,指定數量)

        textCount.text = "道具數量：0 / " + countTotal;

        CreateProp(porps[1], 10);
    }

    private void Update()
    {
        CountTime();
    }
    #endregion

}
