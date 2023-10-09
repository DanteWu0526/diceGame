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

namespace practise02
{
    public partial class Form1 : Form
    {
        enum People { John = 0, Mary, Leo};
        
        struct Die
        {
            double[] RaTen;
            bool roll;
            public int Roll(int roll = 6)
            {
                roll = new Random().Next(1, roll + 1);
                return roll;
            }

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
                //RaTen.ToArray();
                //MessageBox.Show(RaTen.ToArray().Sum().ToString());
            }

            public double[] GetRollTenArr()
            {
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

            public double GetDieTotal()
            {
                return RaTen.ToArray().Sum();
            }

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

        struct Player
        {
            Die[] die;

            public Player(int plater = 1)
            {
                if (plater < 1)
                {
                    plater = 1;
                }
                die = new Die[plater];
            }

            public void AddDieArr(People no,ref double[] arrRa)
            {
                arrRa = die[(int)no].GetRollTenArr();
            }

            public void GetDieTotal(People no, ref double total)
            {
                total = die[(int)no].GetDieTotal();
            }

            public void GetDieAvg(People no, ref double avg)
            {
                avg = die[(int)no].GetDieAvg();
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Player players = new Player(3);
            Die die = new Die { };
            string noStr = "", totalStr = "", avgStr = "";
            double[] arr01 = new double [10];
            double[] arr02 = new double[10];
            double[] arr03 = new double[10];

            double total01 = 0, total02 = 0, total03 = 0;
            double avg01 = 0, avg02 = 0, avg03 = 0;

            arr01 = die.GetRollTenArr();
            players.AddDieArr(People.John,ref arr01);
            total01 = die.GetDieTotal();
            players.GetDieTotal(People.John,ref total01);
            totalStr = String.Format("{0}的骰子總點數為 : {1}", People.John, total01 + "\r\n");
            textBox10.AppendText(totalStr);
            avg01 = die.GetDieAvg();
            players.GetDieAvg(People.John,ref avg01);
            avgStr = String.Format("{0}的骰子平均數為 : {1}", People.John, avg01 + "\r\n");
            textBox10.AppendText(avgStr);
            textBox10.AppendText("----------\r\n");

            arr02 = die.GetRollTenArr();
            players.AddDieArr(People.Mary, ref arr02);
            total02 = die.GetDieTotal();
            players.GetDieTotal(People.Mary, ref total02);
            totalStr = String.Format("{0}的骰子總點數為 : {1}", People.Mary, total02 + "\r\n");
            textBox10.AppendText(totalStr);
            avg02 = die.GetDieAvg();
            players.GetDieAvg(People.Mary, ref avg02);
            avgStr = String.Format("{0}的骰子平均數為 : {1}", People.Mary, avg02 + "\r\n");
            textBox10.AppendText(avgStr);
            textBox10.AppendText("----------\r\n");

            arr03 = die.GetRollTenArr();
            players.AddDieArr(People.Leo, ref arr03);
            total03 = die.GetDieTotal();
            players.GetDieTotal(People.Leo, ref total03);
            totalStr = String.Format("{0}的骰子總點數為 : {1}", People.Leo, total03 + "\r\n");
            textBox10.AppendText(totalStr);
            avg03 = die.GetDieAvg();
            players.GetDieAvg(People.Mary, ref avg03);
            avgStr = String.Format("{0}的骰子平均數為 : {1}", People.Leo, avg03 + "\r\n");
            textBox10.AppendText(avgStr);
            textBox10.AppendText("----------\r\n");

            if (total01 > total02 && total01 > total03)
            {
                textBox10.AppendText(People.John + "第一名 \r\n");
                if (total02 > total03)
                {
                    textBox10.AppendText(People.Mary + "第二名 \r\n");
                    textBox10.AppendText(People.Leo + "第三名 \r\n");
                }
                else if (total02 == total03)
                {
                    textBox10.AppendText(People.Mary + "第二名 \r\n");
                    textBox10.AppendText(People.Leo + "第二名 \r\n");
                }
                else
                {
                    textBox10.AppendText(People.Leo + "第二名 \r\n");
                    textBox10.AppendText(People.Mary + "第三名 \r\n");
                }
            }
            if (total02 > total03 && total02 > total01)
            {
                textBox10.AppendText(People.Mary + "第一名 \r\n");
                if (total03 > total01)
                {
                    textBox10.AppendText(People.Leo + "第二名 \r\n");
                    textBox10.AppendText(People.John + "第三名 \r\n");
                }
                else if (total03 == total01)
                {
                    textBox10.AppendText(People.Leo + "第二名 \r\n");
                    textBox10.AppendText(People.John + "第二名 \r\n");
                }
                else
                {
                    textBox10.AppendText(People.Leo + "第二名 \r\n");
                    textBox10.AppendText(People.John + "第三名 \r\n");
                }
            }
            if(total03 > total02 && total03 > total01)
            {
                textBox10.AppendText(People.Leo + "第一名 \r\n");
                if (total02 > total01)
                {
                    textBox10.AppendText(People.Mary + "第二名 \r\n");
                    textBox10.AppendText(People.John + "第三名 \r\n");
                }
                else if (total02 == total01)
                {
                    textBox10.AppendText(People.Mary + "第二名 \r\n");
                    textBox10.AppendText(People.John + "第二名 \r\n");
                }
                else
                {
                    textBox10.AppendText(People.John + "第二名 \r\n");
                    textBox10.AppendText(People.Mary + "第三名 \r\n");
                }
            }
            if (total01 == total02 && total02 == total03)
            {
                textBox10.AppendText("數值相同在骰一次 \r\n");
            }
        }
    }
}
