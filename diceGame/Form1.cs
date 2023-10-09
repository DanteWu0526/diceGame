using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace diceGame
{
    public partial class Form1 : Form
    {
        enum People { John = 0, Mary, Leo};
        public class playerPeople
        {
            public string Name { get; set; }
            public double PointAvg { get; set; }
        }

        #region 骰子結構
        struct Die
        {
            double[] RaTen;
            bool roll;

            /// <summary>
            /// 骰子骰10次
            /// </summary>
            public void RollTen()
            {
                RaTen = new double[10];
                double[] nums = new double[10];
                Random random = new Random();
                for (int i = 0; i < 10; i++)
                {
                    nums[i] = random.Next(1, 6 + 1);
                    RaTen[i] = nums[i];
                }
                roll = true;
            }
            /// <summary>
            /// 取得骰子骰10次數值
            /// </summary>
            /// <returns>數值</returns>
            public double[] GetRollTenArr()
            {
                ClearArr();
                if (roll == false)
                {
                    RollTen();
                }
                else
                {
                    ClearArr();
                }
                return RaTen.ToArray();
            }

            /// <summary>
            /// 取得10次總和
            /// </summary>
            /// <returns>總和</returns>
            public double GetDieTotal()
            {
                return RaTen.ToArray().Sum();
            }

            /// <summary>
            /// 取得10次平均
            /// </summary>
            /// <returns>平均</returns>
            public double GetDieAvg()
            {
                return RaTen.ToArray().Average();
            }

            public void ClearArr()
            {
                RaTen = new double[10];
                roll = false;
            }
        }
        #endregion

        #region 玩家結構
        struct Player
        {
            Die[] die;

            /// <summary>
            /// 設定玩家人數
            /// </summary>
            /// <param name="player">玩家</param>
            public Player(int player = 1)
            {
                if (player < 1)
                {
                    player = 1;
                }
                die = new Die[player];
            }

            /// <summary>
            /// 新增玩家骰10次
            /// </summary>
            /// <param name="no">玩家編號</param>
            /// <param name="arrRa">10次</param>
            public void AddDieArr(People no,ref double[] arrRa)
            {
                arrRa = die[(int)no].GetRollTenArr();
            }

            /// <summary>
            /// 取得骰子總和
            /// </summary>
            /// <param name="no">玩家</param>
            /// <param name="total">總和</param>
            public void GetDieTotal(People no, ref double total)
            {
                total = die[(int)no].GetDieTotal();
            }

            /// <summary>
            /// 取得玩家骰子平均
            /// </summary>
            /// <param name="no">玩家</param>
            /// <param name="avg">平均</param>
            public void GetDieAvg(People no, ref double avg)
            {
                avg = die[(int)no].GetDieAvg();
            }
        }
        #endregion

        Player players = new Player(3);
        List<playerPeople> playerPeoples = new List<playerPeople>();
        Die die = new Die { };
        string totalStr = "", avgStr = "";
        double[] johnArr = new double[10];
        double[] maryArr = new double[10];
        double[] leoArr = new double[10];
                
        public Form1()
        {
            InitializeComponent();
        }

        #region 各人骰子計算及名次區塊
        /// <summary>
        /// 各人骰子計算
        /// </summary>
        /// <param name="people">各人</param>
        /// <param name="peopleArr">各人陣列</param>
        /// <param name="peopleTota">各人總和</param>
        /// <param name="peopleAvg">各人平均</param>
        private void DiceCalculation(People people, double[] peopleArr , double peopleTota = 0, double peopleAvg = 0)
        {
            string str = people.ToString();
            peopleArr = die.GetRollTenArr();
            players.AddDieArr(people, ref peopleArr);
            peopleTota = die.GetDieTotal();
            players.GetDieTotal(people, ref peopleTota);
            totalStr = String.Format("{0}的骰子總點數為 :{1}", people, peopleTota + "\r\n");
            showTextBox.AppendText(totalStr);
            peopleAvg = die.GetDieAvg();
            players.GetDieAvg(people, ref peopleAvg);
            avgStr = String.Format("{0}的骰子平均數為 : {1}", people,peopleAvg + "\r\n");
            showTextBox.AppendText(avgStr);
            showTextBox.AppendText("----------------------------------------\r\n");

            playerPeoples.Add(new playerPeople() { Name = str, PointAvg = peopleAvg });
        }

        /// <summary>
        /// 名次排序
        /// </summary>
        private void Ranking()
        {
            var sortRanking = playerPeoples.OrderByDescending
                (playerPeoples => playerPeoples.PointAvg).ThenBy( playerPeoples => playerPeoples.Name).ToList();

            string rank, name, avg, sortStr;
            for (int i = 0; i < sortRanking.Count; i++)
            {
                rank = (i + 1).ToString();
                name = sortRanking[i].Name;
                avg = sortRanking[i].PointAvg.ToString();
                sortStr = String.Format("第{0}名: {1}，平均點數為: {2}\r\n", rank, name, avg);
                showTextBox.AppendText(sortStr);
            }
            playerPeoples.Clear();
        }

        private void StartGame()
        {
            DiceCalculation(People.John, johnArr);
            DiceCalculation(People.Mary, maryArr);
            DiceCalculation(People.Leo, leoArr);
            showTextBox.AppendText("--------------------擲骰子結束--------------------\r\n");
            Ranking();
            showTextBox.AppendText("--------------------名次排序完成--------------------\r\n");
        }
        #endregion

        #region 各按鈕區塊
        private void clearButton_Click(object sender, EventArgs e)
        {
            showTextBox.Clear();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartGame();
        }
        #endregion
    }
}
