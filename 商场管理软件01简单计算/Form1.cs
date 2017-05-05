using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 商场管理软件01简单计算
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private double total = 0.0d;

        //如果商场做活动促销，需要修改代码，重新部署发布带代码，麻烦！
        /*
        private void button1_Click(object sender, EventArgs e)
        {
            double totalPrice = Convert.ToDouble(textBox1.Text)*Convert.ToDouble(textBox2.Text);
            total += totalPrice;
            textBox3.Text = "单价：" + textBox1.Text + "数量：" + textBox2.Text + "\r\n" +
                            "合计：" + totalPrice.ToString(CultureInfo.InvariantCulture);
            label4.Text = total.ToString(CultureInfo.InvariantCulture);
        }
        */


        private void Form1_Load(object sender, EventArgs e)
        {
           
            comboBox1.Items.AddRange(new object[]{ "正常收费", "打八折", "满300返100"});
            comboBox1.SelectedIndex = 0; 
            
        }

        //比刚才的灵活，但是重复的代码，太多。考虑重构代码。

        /*
        private void button1_Click(object sender, EventArgs e)
        {

            double totalPrice = 0d;
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    totalPrice = Convert.ToDouble(textBox1.Text) * Convert.ToDouble(textBox2.Text);
                    break;
                case 1:
                    totalPrice = Convert.ToDouble(textBox1.Text) * Convert.ToDouble(textBox2.Text)*0.8;
                    break;
                case 2:
                    totalPrice = Convert.ToDouble(textBox1.Text) * Convert.ToDouble(textBox2.Text)*0.7;
                    break;
                case 3:
                    totalPrice = Convert.ToDouble(textBox1.Text) * Convert.ToDouble(textBox2.Text)*0.5;
                    break;

            }


            total += totalPrice;
            lstbox.Items.Add( "单价：" + textBox1.Text + "数量：" + textBox2.Text  +
                            "合计：" + totalPrice.ToString(CultureInfo.InvariantCulture));
            label4.Text = total.ToString(CultureInfo.InvariantCulture);
        }*/

        private void button2_Click(object sender, EventArgs e)
        {
            total = 0d;
            textBox1.Text = "0.00";
            textBox2.Text = "1";
            comboBox1.Items.Clear();
            label4.Text = "0.00";
        }

        #region 使用简单工厂方式
        private void button1_Click(object sender, EventArgs e)
        {
            CashSuper csSuper = CashFactory.CreateCashAccept(comboBox1.SelectedItem.ToString());
            double totalPrice = 0d;
            totalPrice = csSuper.AcceptCash(Convert.ToDouble(textBox1.Text)) * Convert.ToDouble(textBox2.Text);
            total += totalPrice;
            lstbox.Items.Add("单价：" + textBox1.Text + "￥   数量：" + textBox2.Text +"  "+ comboBox1.SelectedItem+
                            "合计：" + totalPrice.ToString(CultureInfo.InvariantCulture) +"￥");
            label4.Text = total.ToString(CultureInfo.InvariantCulture);
        }
        #endregion
    }


    //现金收费抽象类
    abstract class CashSuper
    {
        public abstract double AcceptCash(double money);//收取原价，返回当前打折后的价格
    }

    //正常收费子类
    class CashNormal : CashSuper
    {
        public override double AcceptCash(double money)
        {
            return money;
        }
    }

    //打折收费子类(重构了打折)
    class CashRebate : CashSuper
    {
        //声明一个打折率
        private double _moneyRebate;

        public CashRebate(string rate)
        {
            _moneyRebate = double.Parse(rate);
        }

        public override double AcceptCash(double money)
        {
            return money * _moneyRebate;
        }
    }

    //返利收费子类(重构了返利)
    class CashReturn : CashSuper
    {
        private readonly double _moneyCondition;
        private readonly double _moneyReturn;

        /// <summary>
        ///  返利收费，初始化时候
        /// </summary>
        /// <param name="moneyCondition">返利条件</param>
        /// <param name="moneyReturn">返利值</param>
        public CashReturn(string moneyCondition, string moneyReturn)
        {
            _moneyCondition = double.Parse(moneyCondition);
            _moneyReturn = double.Parse(moneyReturn);
        }

        public override double AcceptCash(double money)
        {
            double result = money;
            if (money >= _moneyCondition)
                result = money - Math.Floor(money / _moneyCondition) * _moneyReturn;
            return result;
        }
    }

    /// <summary>
    ///现金收费工厂
    /// </summary>
    class CashFactory
    {
        public static CashSuper CreateCashAccept(string type)
        {
            CashSuper cs = null;
            switch (type)
            {
                case "正常收费":
                    cs = new CashNormal();
                    break;
                case "满300返100":
                    cs = new CashReturn("300", "100");
                    break;
                case "打八折":
                    cs = new CashRebate("0.8");
                    break;
            }
            return cs;
        }
    }


}
